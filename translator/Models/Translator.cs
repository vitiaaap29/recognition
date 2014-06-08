using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace translator.Models
{
    public class Translator
    {
        public String MangleWord { get; private set; }
        public Dictionary<String, float> PercentTable { get; private set; }
        private List<Entities.Language> langs;

        public Translator(String word): this()
        {
            MangleWord = word;
        }

        public Translator()
        {
            PercentTable = new Dictionary<string, float>();
            langs = new List<Entities.Language>();
            langs.Add( new Entities.Language
                {
                    LanguageId = 0,
                    Name = "Russian",
                    Words = null
                });
            using (var context = new TranslatorContext())
            {
                context.Langs.Add(langs[0]);
            }
        }

        public void CalculatePercentReliability()
        {
            if (MangleWord != null)
            {
                //here was request to BD

                int percent = 0;
                using (var context = new TranslatorContext())
                {
                    context.Words.Add(new Entities.Word
                    {
                        WordId = 0,
                        Text = MangleWord,
                        LanguageId = 0,
                        Language = (from l in context.Langs orderby l.LanguageId select l).FirstOrDefault(a => a.LanguageId == 0)
                    });

                    percent = context.Words.Count();
                }


                PercentTable.Add("English_" + MangleWord, 100 - percent);
                PercentTable.Add("Russian_" + MangleWord, percent);
            }
        }
    }
}