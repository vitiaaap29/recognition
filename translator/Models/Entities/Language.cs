using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace translator.Models.Entities
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual List<Word> Words { get; set; }
    }
}