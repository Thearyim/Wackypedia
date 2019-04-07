using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Wackypedia.Models
{
    public class Article
    {
        private string _title;
        private string _content;
        private int _id;
        private DateTime _createdDate;
        private DateTime _lastEditedDate;


        public Article(string title, string content, int id = 0 )
        {
            _title = title;
            _content = content;
            _id = id;
            _createdDate = DateTime.Now;
            _lastEditedDate = DateTime.Now;
        }

        public string GetTitle() { return _title; }
        public string GetContent() { return _content;  }
        public int GetId() { return _id; }

        public void SetContent(string content)
        {
            _content = content;
        }

        public void SetCreatedDate(DateTime date)
        {
            _createdDate = date;
        }

        public void SetLastEditedDate(DateTime date)
        {
            _lastEditedDate = date;
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        //public List<Section> GetSections()
        //{
        //    List<Section> allArticleSections = new List<Section>();
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();
        //    var cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"SELECT * FROM sections WHERE articleID = @articleID;";
        //    MySqlParameter articleID = new MySqlParameter();
        //    articleID.ParameterName = "@articleID";
        //    articleID.Value = this._id;
        //    cmd.Parameters.Add(articleID);
        //    var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //    while (rdr.Read())
        //    {
        //        int sectionID = rdr.GetInt32(0);
        //        string title = rdr.GetString(1);
        //        string imageLink = rdr.GetString(2);
        //        string body = rdr.GetString(3); //This should be a TEXT field (MEDIUMTEXT/LONGTEXT)
        //        Section newSection = new Section(title, imageLink, body, this._id, sectionID);
        //        allArticleSections.Add(newSection);
        //    }
        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }
        //    return allArticleSections;
        //}

        public static List<Article> GetAll()
        {
            List<Article> allArticles = new List<Article>();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT id, title, content, createdDate, lastEditedDate FROM article;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int articleId = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                string content = rdr.GetString(2);
                DateTime createdDate = rdr.GetDateTime(3);
                DateTime lastEditedDate = rdr.GetDateTime(4);
                Article newArticle = new Article(title, content, articleId);
                newArticle.SetCreatedDate(createdDate);
                newArticle.SetLastEditedDate(lastEditedDate);
                allArticles.Add(newArticle);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allArticles;
        }

        public string GetPartialContent()
        {
            List<Section> sections = this.GetSections();
            return sections.First().GetContent() + "...";
        }

        public HashSet<string> GetSearchStrings()
        {
            HashSet<string> searchStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<Section> sections = this.GetSections();

            // Add search strings from the article title
            foreach (string word in this._title.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (word.Length >= 5)
                {
                    searchStrings.Add(word.Trim().TrimEnd('.'));
                }
            }

            foreach (Section section in sections)
            {
                // Add search strings from the article section title
                foreach (string word in section.GetTitle().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (word.Length >= 5)
                    {
                        searchStrings.Add(word.Trim().TrimEnd('.'));
                    }
                }

                // Add search strings from the article section content
                foreach (string word in section.GetContent().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (word.Length >= 5)
                    {
                        searchStrings.Add(word.Trim().TrimEnd('.'));
                    }
                }
            }

            return searchStrings;
        }

        public List<Section> GetSections()
        {
            // In this logic, sections are divided using a
            // '==' character similar to the way wikis are.
            // We can divide the content into 'sections' by 
            // parsing on the word that begin and end with the '=='
            // section markup.
            // 
            // Example
            // ==Section 1==
            // Section 1 text here
            // 
            // ==Section 2==
            // Section 2 text here
            //
            // becomes
            // <h3>Section 1</h3>
            // <textarea>This is the content of section 1</textarea>
            //
            // <h3>Section 2</h3>
            // <textarea>This is the content of section 2</textarea>
            //
            // <h3>Section 3</h3>
            // <textarea>This is the content of section 3</textarea


            // [\x20-\x7e] means all printable ASCII characters
            List<Section> sections = new List<Section>();
            Regex sectionHeaderExpression = new Regex(@"==([\x20-\x3c\x3e-\x7e]+)==", RegexOptions.IgnoreCase);
            Regex sectionContentExpression = new Regex(@"==[\x20-\x3c\x3e-\x7e]+==", RegexOptions.IgnoreCase);
            MatchCollection sectionMatches = sectionHeaderExpression.Matches(this._content);

            if (sectionMatches.Count > 0)
            {
                string[] sectionContentValues = sectionContentExpression.Split(this._content)
                    .Where(content => !string.IsNullOrEmpty(content))
                    .ToArray();

                if (sectionContentValues?.Any() == true)
                {
                    for (int matchIndex = 0; matchIndex < sectionMatches.Count; matchIndex++)
                    {
                        string sectionTitle = sectionMatches[matchIndex].Groups[1].Value;
                        string sectionContent = sectionContentValues[matchIndex];
                        sections.Add(new Section(sectionTitle.Trim(), sectionContent.Trim()));
                    }
                }
            }

            return sections;
        }

        public void AddAuthor(Author newAuthor)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO articles_authors (article_id, author_id) VALUES (@articleId, @authorId);";
            MySqlParameter articleID = new MySqlParameter();
            articleID.ParameterName = "@articleId";
            articleID.Value = _id;
            cmd.Parameters.Add(articleID);
            MySqlParameter authorID = new MySqlParameter();
            authorID.ParameterName = "@authorId";
            authorID.Value = newAuthor.GetID();
            cmd.Parameters.Add(authorID);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddSearchStrings()
        {
            HashSet<string> searchStrings = this.GetSearchStrings();
            if (searchStrings.Count > 0)
            {
                string sql = @"INSERT INTO search (article_id, searchString) VALUES ";
                foreach (string searchString in searchStrings)
                {
                    sql += $"({this._id}, '{searchString}'),";
                }

                MySqlConnection conn = DB.Connection();
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = sql.TrimEnd(',');
                cmd.ExecuteNonQuery();

                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

        //This method will DELETE EVERYTHING. ALL models.
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM articles_authors;DELETE FROM article";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            // Section.ClearAll();
        }

        public static List<Article> SearchForArticles(string searchText)
        {
            List<Article> articles = new List<Article>();

            string[] searchWords = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (searchWords.Length > 0)
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();
                
                string sql =
                    $@"SELECT article.*
                  FROM search
                  JOIN article ON (article.id = search.article_id)
                  WHERE searchString IN ({string.Join(",", searchWords.Select(s => $"'{s}'"))})";

                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

                while (rdr.Read())
                {
                    int articleId = rdr.GetInt32(0);
                    string title = rdr.GetString(1);
                    string content = rdr.GetString(2);
                    DateTime createdDate = rdr.GetDateTime(3);
                    DateTime lastEditedDate = rdr.GetDateTime(4);
                    Article newArticle = new Article(title, content, articleId);
                    newArticle.SetCreatedDate(createdDate);
                    newArticle.SetLastEditedDate(lastEditedDate);
                    articles.Add(newArticle);
                }

                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            return articles;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText =
                @"DELETE FROM article WHERE id = @thisID;
                  DELETE from articles_authors WHERE article_id = @thisID;";

            MySqlParameter thisID = new MySqlParameter();
            thisID.ParameterName = "@thisID";
            thisID.Value = this._id;
            cmd.Parameters.Add(thisID);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            if (this._id > 0)
            {
                cmd.CommandText =
                    @"UPDATE article
                      SET title = @title, 
                          content = @content,
                          lastEditedDate = @currentDate
                      WHERE id = @id";

                MySqlParameter id = new MySqlParameter();
                id.ParameterName = "@id";
                id.Value = this._id;
                cmd.Parameters.Add(id);

                MySqlParameter lastEditedDate = new MySqlParameter();
                lastEditedDate.ParameterName = "@currentDate";
                lastEditedDate.Value = DateTime.Now;
                cmd.Parameters.Add(lastEditedDate);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO article (title, content) VALUES (@title, @content);";
            }

            MySqlParameter title = new MySqlParameter();
            title.ParameterName = "@title";
            title.Value = this._title;
            cmd.Parameters.Add(title);

            MySqlParameter content = new MySqlParameter();
            content.ParameterName = "@content";
            content.Value = this._content;
            cmd.Parameters.Add(content);

            cmd.ExecuteNonQuery();

            if (this._id <= 0)
            {
                this._id = (int)cmd.LastInsertedId;
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Author> GetAuthors()
        {
            List<Author> allAuthors = new List<Author> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = 
                @"SELECT author.*
                  FROM article 
                  JOIN articles_authors ON (article.id = articles_authors.article_id)
                  JOIN author ON (articles_authors.author_id = author.id)
                  WHERE article.id = (@articleId);";

            MySqlParameter articleID = new MySqlParameter();
            articleID.ParameterName = "@articleId";
            articleID.Value = _id;
            cmd.Parameters.Add(articleID);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int authorID = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Author newAuthor = new Author(name, authorID);
                allAuthors.Add(newAuthor);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAuthors;
        }

        public string GetAuthorNames()
        {
            // Format:
            // author1, author2, author3
            string authorNames = string.Empty;
            foreach (Author author in this.GetAuthors())
            {
                if (!string.IsNullOrEmpty(authorNames))
                {
                    authorNames += ", ";
                }

                authorNames += author.GetName();
            }

            return authorNames;
        }

        public override bool Equals(System.Object otherArticle)
        {
            if (!(otherArticle is Article))
            {
                return false;
            }
            else
            {
                Article newArticle = (Article)otherArticle;
                bool articleEquality = (this.GetTitle() == newArticle.GetTitle() && this.GetId() == newArticle.GetId());
                return (articleEquality);
            }
        }

        public static Article Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM article WHERE ID = @thisID;";
            MySqlParameter thisID = new MySqlParameter();
            thisID.ParameterName = "@thisID";
            thisID.Value = id;
            cmd.Parameters.Add(thisID);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int articleID = 0;
            string title = "";
            string content = "";
            while (rdr.Read())
            {
                articleID = rdr.GetInt32(0);
                title = rdr.GetString(1);
                content = rdr.GetString(2);
            }
            Article foundArticle = new Article(title, content, articleID);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundArticle;
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }


    }
}
