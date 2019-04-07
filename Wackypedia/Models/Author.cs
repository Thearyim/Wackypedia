using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Wackypedia.Models
{
    public class Author
    {

        private string MyName;
        private int MyID;

        public Author(string name, int id = 0)
        {
            MyName = name;
            MyID = id;
        }

        public string GetName() { return MyName; }
        public int GetID() { return MyID; }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM authors; DELETE FROM articles_authors;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void SetName(string name) { MyName = name; }

        public void AddArticle(Article newArticle)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO articles_authors (article_ID, author_ID) VALUES (@articleID, @authorID);";
            MySqlParameter article_ID = new MySqlParameter();
            article_ID.ParameterName = "@articleID";
            article_ID.Value = newArticle.GetId();
            cmd.Parameters.Add(article_ID);
            MySqlParameter author_ID = new MySqlParameter();
            author_ID.ParameterName = "@authorID";
            author_ID.Value = MyID;
            cmd.Parameters.Add(author_ID);
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
            cmd.CommandText = @"INSERT INTO author (name) VALUES (@name);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.MyName;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            MyID = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Article> GetArticles()
        {
            List<Article> allArticles = new List<Article> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT articles.* FROM articles JOIN articles_authors ON (articles.ID = articles_authors.article_ID) JOIN authors ON (articles_authors.author_ID = authors.ID) WHERE authors.ID = (@authorID);";
            MySqlParameter authorID = new MySqlParameter();
            authorID.ParameterName = "@authorID";
            authorID.Value = MyID;
            cmd.Parameters.Add(authorID);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int articleID = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                string description = rdr.GetString(2);
                Article newArticle = new Article(title, description, articleID);
                allArticles.Add(newArticle);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allArticles;
        }

        public static List<Author> GetAll()
        {
            List<Author> allAuthors = new List<Author>();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int ID = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Author newAuthor = new Author(name, ID);
                allAuthors.Add(newAuthor);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allAuthors;
        }

        public override bool Equals(System.Object otherAuthor)
        {
            if (!(otherAuthor is Author))
            {
                return false;
            }
            else
            {
                Author newAuthor = (Author)otherAuthor;
                bool authorEquality = (this.GetName() == newAuthor.GetName() && this.GetID() == newAuthor.GetID());
                return (authorEquality);
            }
        }
    }
}
