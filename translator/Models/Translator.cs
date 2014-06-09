using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace translator.Models
{
    public class Translator
    {
        private List<Entities.Language> langs = null;
        private Dictionary<String, float> percentTable;

        public Dictionary<String, float> PercentTable 
        { 
            get
            {
                return percentTable;
            }
            private set
            {
                if (percentTable.Count >= langs.Count)
                {
                    percentTable.Clear();
                }
                percentTable = value;
            }
        }

        public Translator()
        {
            if (langs == null)
            {
                percentTable = new Dictionary<string, float>();
                langs = new List<Entities.Language>();
                langs.Add( new Entities.Language
                    {
                        LanguageId = 0,
                        Name = "Russian",
                        Words = null
                    });
                langs.Add(new Entities.Language
                {
                    LanguageId = 1,
                    Name = "English",
                    Words = null
                });

                //using (var context = new TranslatorContext())
                //{
                //    foreach (Entities.Language l in langs)
                //    {
                //        context.Langs.Add(l);
                //    }
                //    context.SaveChanges();
                //}
            }
        }

        public void CalculatePercentReliability(String word)
        {
            int percent = 0;
            using (var context = new TranslatorContext())
            {
                context.Words.Add(new Entities.Word
                {
                    //WordId = Entities.Word.NextId,
                    Text = word,
                    LanguageId = 1,
                    //Language = (from l in context.Langs orderby l.LanguageId select l).FirstOrDefault(a => a.LanguageId == 0)
                });

                context.SaveChanges();

                percent = context.Words.Count();
            }

            PercentTable.Add("English_" + word, 100 - percent);
            PercentTable.Add("Russian_" + word, percent);
        }

    }
}