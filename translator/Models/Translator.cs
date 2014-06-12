using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using translator.Models.Entities;
using translator.Models.PercentSimilarity;
using translator.Models.DbOperationException;

namespace translator.Models
{
    public class Translator
    {
        public static int MinLengthWord = 4;
        public bool IsWordTooSmall { get; private set; }
        public String CurrentWord { get; private set; }

        public Dictionary<String, float> PercentTable { get; private set; }

        public Translator()
        {
            PercentTable = new Dictionary<string, float>();
        }

        /// <summary>
        /// Считаем словарь: язык => процент
        /// по вем языкам
        /// </summary>
        /// <param name="word"></param>
        public void CalculatePercentReliability(String word)
        {
            CurrentWord = word;
            if (word.Length >= MinLengthWord)
            {
                IsWordTooSmall = false;

                using (var context = new TranslatorContext())
                {
                    var words = from w in context.Words orderby w.WordId select w;
                    
                    //get langId => percent
                    List<KeyValuePair<int, float>> langIdAndPercent = new List<KeyValuePair<int, float>>(words.Count());
                    foreach (var w in words)
                    {
                        float percent = word.PercentOfSimilarity(w.Text);
                        if (percent > 0)
                        {
                            langIdAndPercent.Add(new KeyValuePair<int, float>(w.LanguageId, percent));
                        }
                    }

                    //get langName => max percent
                    var langsIds = from l in context.Langs orderby l.LanguageId select l.LanguageId;

                    foreach (int langId in langsIds)
                    {
                        var listPercentCurrentLangId = 
                            from idPercent in langIdAndPercent where idPercent.Key == langId 
                            orderby idPercent.Value descending select idPercent.Value;

                        if (listPercentCurrentLangId.Count() > 0)
                        {
                            var nameLang = 
                                (from l in context.Langs where l.LanguageId == langId select l.Name).First();
                            PercentTable.Add(nameLang, listPercentCurrentLangId.First());
                        }
                    }

                    //if all by 0 percent
                    if (PercentTable.Count == 0)
                    {
                        var langsNames = from l in context.Langs orderby l.LanguageId select l.Name;
                        foreach (String name in langsNames)
                        {
                            PercentTable.Add(name, 0);
                        }
                    } 
                }
            }
            else
            {
                IsWordTooSmall = true;
            }
        }
       
        /// <summary>
        /// Add new word to database.
        /// </summary>
        /// <param name="word">Word, which added to the base</param>
        /// <param name="languageName">Name language, if this name does not exist, than throw 
        /// UndefineLangException</param>
        public void AddWord(String word, String languageName)
        {
            using (var context = new TranslatorContext())
            {
                var languageIdsAccordingName = 
                    from l in context.Langs where l.Name == languageName select l.LanguageId;
                if (languageIdsAccordingName.Count() > 0)
                {
                    int langId = languageIdsAccordingName.First();
                    context.Words.Add(new Word
                    {
                        LanguageId = langId,
                        Text = word
                    });
                    context.SaveChanges();
                }
                else
                {
                    throw new UndefineLangException();
                }
            }
        }

        //public void AddLang(String lang, String word)
        //{
        //    using (var context = new TranslatorContext())
        //    {
        //        Language addedLang = new Language { Name = lang };
        //        context.Langs.Add(addedLang);

        //        int addedLangId = (from l in context.Langs where l.Name == lang select l.LanguageId).First();

        //        context.Words.Add(new Word
        //        {
        //            LanguageId = addedLangId,
        //            Text = word
        //        });
        //        context.SaveChanges();
        //    }
        //}
    }
}