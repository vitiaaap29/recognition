using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace translator.Models.Entities
{
    public class Word
    {
        public int WordId { get; set; }
        public string Text { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}