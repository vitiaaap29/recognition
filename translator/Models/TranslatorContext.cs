using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using translator.Models.Entities;

namespace translator.Models
{
    public class TranslatorContext : DbContext 
    {
        public DbSet<Word> Blogs { get; set; }
        public DbSet<Language> Posts { get; set; }
    }
    //translator.Models.TranslatorContext
}