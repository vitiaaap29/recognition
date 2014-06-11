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

        public TranslatorContext():
            base("TranslatorConnection")
        {
        }
    }
}