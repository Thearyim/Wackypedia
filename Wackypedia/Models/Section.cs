using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Wackypedia.Models
{
    public class Section
    {
        private string _title;
        private string _content;

        public Section(string title, string content)
        {
            _title = title;
            _content = content;
        }

        public string GetTitle() { return _title; }
        public string GetContent() { return _content; }
        public void SetTitle(string title) { _title = title; }
        public void SetContent(string body) { _content = body; }

        public override bool Equals(System.Object otherSection)
        {
            if (!(otherSection is Section))
            {
                return false;
            }
            else
            {
                Section newSection = (Section)otherSection;
                bool sectionEquality = (this.GetTitle() == newSection.GetTitle() && this.GetContent() == newSection.GetContent());
                return (sectionEquality);
            }
        }

        //public static List<Section> GetAll()
        //{
        //    List<Section> allSections = new List<Section>();
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();
        //    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"SELECT * FROM sections;";
        //    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        //    while (rdr.Read())
        //    {
        //        int sectionID = rdr.GetInt32(0);
        //        string title = rdr.GetString(1);
        //        string imageLink = rdr.GetString(2);
        //        string body = rdr.GetString(3); //This should be a TEXT field (MEDIUMTEXT/LONGTEXT)
        //        int articleID = rdr.GetInt32(4);
        //        Section newSection = new Section(title, imageLink, body, articleID, sectionID);
        //        allSections.Add(newSection);
        //    }

        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }

        //    return allSections;
        //}

        // public void SetImageLink(string imageLink) { _imageLink = imageLink; }
        //public void SetArticleID(int articleID) { MyArticleID = articleID; }
        //public static void ClearAll()
        //{
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();
        //    var cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"DELETE FROM sections;";
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }
        //}

        //public void Save()
        //{
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();
        //    var cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"INSERT INTO sections (title, imageLink, body, articleID) VALUES (@title, @imageLink, @body, @articleID);";
        //    MySqlParameter title = new MySqlParameter();
        //    title.ParameterName = "@title";
        //    title.Value = this._title;
        //    cmd.Parameters.Add(title);
        //    MySqlParameter imageLink = new MySqlParameter();
        //    imageLink.ParameterName = "@imageLink";
        //    imageLink.Value = this._imageLink;
        //    cmd.Parameters.Add(imageLink);
        //    MySqlParameter body = new MySqlParameter();
        //    body.ParameterName = "@body";
        //    body.Value = this._content;
        //    cmd.Parameters.Add(body);
        //    MySqlParameter articleID = new MySqlParameter();
        //    articleID.ParameterName = "@articleID";
        //    articleID.Value = this.MyArticleID;
        //    cmd.Parameters.Add(articleID);
        //    cmd.ExecuteNonQuery();
        //    MyID = (int)cmd.LastInsertedId;

        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }
        //}

        //public void Edit(string newTitle, string newImageLink, string newBody)
        //{
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();
        //    var cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"UPDATE sections SET title = @title, imageLink = @imageLink, body = @body WHERE ID = @searchID;";
        //    MySqlParameter searchID = new MySqlParameter();
        //    searchID.ParameterName = "@searchID";
        //    searchID.Value = MyID;
        //    cmd.Parameters.Add(searchID);
        //    MySqlParameter title = new MySqlParameter();
        //    title.ParameterName = "@title";
        //    title.Value = newTitle;
        //    cmd.Parameters.Add(title);
        //    MySqlParameter imageLink = new MySqlParameter();
        //    imageLink.ParameterName = "@imageLink";
        //    imageLink.Value = newImageLink;
        //    cmd.Parameters.Add(imageLink);
        //    MySqlParameter body = new MySqlParameter();
        //    body.ParameterName = "@body";
        //    body.Value = newBody;
        //    cmd.Parameters.Add(body);
        //    cmd.ExecuteNonQuery();
        //    _title = newTitle;
        //    _imageLink = newImageLink;
        //    _content = newBody;
        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }
        //}

        //public static Section Find(int ID)
        //{
        //    MySqlConnection conn = DB.Connection();
        //    conn.Open();
        //    var cmd = conn.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"SELECT * FROM sections WHERE ID = @thisID;";
        //    MySqlParameter thisID = new MySqlParameter();
        //    thisID.ParameterName = "@thisID";
        //    thisID.Value = ID;
        //    cmd.Parameters.Add(thisID);
        //    var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //    int sectionID = 0;
        //    string title = "";
        //    string imageLink = "";
        //    string body = "";
        //    int articleID = 0;
        //    while (rdr.Read())
        //    {
        //        sectionID = rdr.GetInt32(0);
        //        title = rdr.GetString(1);
        //        imageLink = rdr.GetString(2);
        //        body = rdr.GetString(3); //This should be a TEXT field (MEDIUMTEXT/LONGTEXT)
        //        articleID = rdr.GetInt32(4);
        //    }
        //    Section foundSection = new Section(title, imageLink, body, articleID, sectionID);

        //    conn.Close();
        //    if (conn != null)
        //    {
        //        conn.Dispose();
        //    }
        //    return foundSection;
        //}

    }
}
