using System.Collections.Generic;
using System.Linq;
using Wackypedia.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Wackypedia.Tests
{
    [TestClass]
    public class ArticleTests
    {
        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=wackypedia_test;";
        }

        [TestMethod]
        public void GetTitle_ReturnsTheExpectedArticleTitle()
        {
            //Arrange
            Article article = new Article("Article1", "AnyContent");

            //Act
            string newTitle = article.GetTitle();

            //Assert
            Assert.IsInstanceOfType(newTitle, typeof(string));
        }

        [TestMethod]
        public void GetId_ReturnsTheExpectedArticleId()
        {
            //Arrange
            Article article = new Article("Article2", "AnyContent", 1);

            //Act
            int articleId = article.GetId();

            //Assert
            Assert.AreEqual(1, articleId);
        }
        
        [TestMethod]
        public void ClearAll_RemovesAllArticlesFromTheDatabase()
        {
            //Act
            Article.ClearAll();
            List<Article> allArticles = Article.GetAll();

            //Assert
            Assert.IsTrue(allArticles.Count == 0);
        }

        [TestMethod]
        public void GetAll_ReturnsAllArticlesInTheDatabase()
        {
            //Arrange
            Article anyArticle = new Article("Article3", "AnyContent");

            //Act
            anyArticle.Save();

            //Act
            List<Article> allArticles = Article.GetAll();

            //Assert
            Assert.IsTrue(allArticles.Count >= 1);
        }

        [TestMethod]
        public void Save_CreatesANewArticleInTheDatabase()
        {
            //Arrange
            string articleTitle = "Article4";
            Article article = new Article(articleTitle, "AnyContent");

            //Act
            article.Save();
            List<Article> result = Article.GetAll();

            //Assert
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Any(r => r.GetTitle() == articleTitle));
        }

        [TestMethod]
        public void FindById_ReturnsTheExpectedArticle()
        {
            //Arrange
            string articleTitle = "Article5";
            Article expectedArticle = new Article(articleTitle, "AnyContent");

            //Act
            expectedArticle.Save();
            Article actualArticle = Article.Find(expectedArticle.GetId());

            // Assert
            Assert.IsNotNull(actualArticle);
        }

        [TestMethod]
        public void Delete_PermanentlyRemovesTheArticleFromTheDatabase()
        {
            //Arrange
            string articleTitle = "Article6";
            Article expectedArticle = new Article(articleTitle, "AnyContent");

            //Act
            expectedArticle.Save();
            Article actualArticle = Article.Find(expectedArticle.GetId());

            actualArticle.Delete();

            // Assert
            Article deletedArticle = Article.Find(expectedArticle.GetId());
            Assert.IsNull(deletedArticle);
        }

        [TestMethod]
        public void GetSections_ParsesExpectedSectionsFromASetOfText()
        {
            string exampleContent =
                @"==Section 1==
                  This is the content of section 1
                   
                  ==Section 2==
                  This is the content of section 2

                  == Section 3 ==
                  This is the content of section 3

                  ==      Section 4            ==
                  This is the content of section 4

                  == Section 5  == This is the content of section 5";

            Article article = new Article("AnyTitle", exampleContent);
            List<Section> articleSections = article.GetSections();

            Assert.IsTrue(articleSections.Count == 5);
            CollectionAssert.AreEqual(
                new List<string>
                {
                    "Section 1",
                    "Section 2",
                    "Section 3",
                    "Section 4",
                    "Section 5"
                },
                articleSections.Select(section => section.GetTitle()).ToArray());

            CollectionAssert.AreEqual(
                new List<string>
                {
                    "This is the content of section 1",
                    "This is the content of section 2",
                    "This is the content of section 3",
                    "This is the content of section 4",
                    "This is the content of section 5"
                },
                articleSections.Select(section => section.GetContent()).ToArray());
        }
    }
}
