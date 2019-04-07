using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Wackypedia.Models;

namespace Wackypedia.Controllers
{
	public class ArticlesController : Controller
	{
        // article home page
        [HttpGet("/articles")] 
		public ActionResult Index()
		{
			List<Article> allArticles = Article.GetAll();
			return View("Article-Index", allArticles);
		}

        // article home page
        [HttpGet("/articles/{id}")]
        public ActionResult ShowArticle(int id)
        {
            Article article = Article.Find(id);

            //string content = 
            //    "==Section 1==\r\n" +
            //    "This is the content of section 1\r\n\r\n" +
            //     "==Section 2==\r\n" +
            //     "This is the content of section 2\r\n\r\n" +
            //     "== Section 3 ==\r\n" +
            //     "This is the content of section 3\r\n\r\n" +
            //     "==      Section 4            ==\r\n" +
            //     "This is the content of section 4\r\n\r\n" +
            //     "== Section 5  == This is the content of section 5";

            //Article article = new Article(
            //    "This is an article title",
            //    content,
            //    id: 1);

            return View("Article-Show", article);
        }

        // article home page
        [HttpPost("/articles/{id}")]
        public ActionResult UpdateArticle(int id, string editedContent)
        {
            // Save Article, read new article and show
            Article article = Article.Find(id);
            article.SetContent(editedContent);
            article.Save();
            article.AddSearchStrings();

            return RedirectToAction("ShowArticle");
        }

        [HttpGet("/articles/new")]
		public ActionResult New()
		{
			return View("Article-New");
		}

		[HttpPost("/articles")]
		public ActionResult Create(string articleTitle, string articleAuthor, string articleContent)
		{
			Article newArticle = new Article(articleTitle, articleContent);
			newArticle.Save();
            newArticle.AddSearchStrings();

            Author author = new Author(articleAuthor);
            author.Save();

            newArticle.AddAuthor(author);
			List<Article> allArticles = Article.GetAll();
            return RedirectToAction("Index");
		}


		[HttpPost("/articles/search")]
		public ActionResult Search(string searchString)
		{
            List<Article> matchingArticles = Article.SearchForArticles(searchString);
			return View("Article-Index", matchingArticles);
		}

        //[HttpGet("/articles/{id}")]
        //public ActionResult Show(int id)
        //{
        //    Dictionary<string, object> model = new Dictionary<string, object>();
        //    Article selectedArticle = Article.Find(id);
        //    List<Author> articleAuthors = selectedArticle.GetAuthors();
        //    model.Add("article", selectedArticle);
        //    model.Add("authors", articleAuthors);
        //    return View("Article-Show", model);
        //}
    }
}