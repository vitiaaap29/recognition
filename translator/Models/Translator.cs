using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using translator.Models.Entities;
using translator.Models.PercentSimilarity;

namespace translator.Models
{
    public class Translator
    {
        //private List<Entities.Language> langs = null;

        public static int MinLengthWord = 4;
        public bool IsWordTooSmall { get; private set; }

        public Dictionary<String, float> PercentTable { get; private set; }

        public Translator()
        {
            PercentTable = new Dictionary<string, float>();
            //if (langs == null)
            //{
            //    PercentTable = new Dictionary<string, float>();
            //    langs = new List<Language>();
            //    langs.Add( new Language
            //        {
            //            LanguageId = 0,
            //            Name = "Russian",
            //            Words = null
            //        });
            //    langs.Add(new Language
            //    {
            //        LanguageId = 1,
            //        Name = "English",
            //        Words = null
            //    });

            //    //using (var context = new TranslatorContext())
            //    //{
            //    //    foreach (Entities.Language l in langs)
            //    //    {
            //    //        context.Langs.Add(l);
            //    //    }
            //    //    context.SaveChanges();
            //    //}
            //}
        }

        public void CalculatePercentReliability(String word)
        {
            if (word.Length >= MinLengthWord)
            {
                IsWordTooSmall = false;

                using (var context = new TranslatorContext())
                {
                    var words = from w in context.Words orderby w.WordId select w;

                    Dictionary<float, String> bigThanZeroPercents = new Dictionary<float, String>();
                    foreach (var w in words)
                    {
                        float percent = word.PercentOfSimilarity(w.Text);
                        if (percent > 0)
                        {
                            var langName = 
                                (from l in context.Langs where l.LanguageId == w.LanguageId 
                                 select l.Name);
                            bigThanZeroPercents.Add(percent, (String)langName.First());
                        }
                    }

                    var namesLangs = (from l in context.Langs orderby l.LanguageId 
                         select l.Name).Take(context.Langs.Count());

                    foreach (String nameLang in namesLangs)
                    {
                        var listPercentCurrentLang = 
                            (from percent in bigThanZeroPercents  where percent.Value == nameLang 
                             orderby percent.Key descending select percent);
                        if (listPercentCurrentLang.Count() > 0)
                        {
                            PercentTable.Add(nameLang, listPercentCurrentLang.First().Key);
                        }
                    }
                    
                }


            }
            else
            {
                IsWordTooSmall = true;
            }
        }

    }
}