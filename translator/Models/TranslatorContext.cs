using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using translator.Models.Entities;
using System.Data.SQLite;
using System.IO;

namespace translator.Models
{
    public class TranslatorContext : DbContext 
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Language> Langs { get; set; }
        private readonly String dbFilePath = @"./Model/translator.sqlite";

        public TranslatorContext()
        {
            //if (!File.Exists(dbFilePath))
            //{
            //    SQLiteConnection.CreateFile(dbFilePath);
            //    var m_dbConnection = new SQLiteConnection("Data Source=" + dbFilePath +";Version=3;");
            //    m_dbConnection.Open();
            //    string sql = "create table words (name varchar(20), score int)";
            //}
            Database.CreateIfNotExists();
        }
    }
    //translator.Models.TranslatorContext
}