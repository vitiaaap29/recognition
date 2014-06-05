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

        public Translator(String word): this()
        {
            MangleWord = word;
        }

        public Translator()
        {
            PercentTable = new Dictionary<string, float>();
        }

        public void CalculatePercentReliability()
        {
            if (MangleWord != null)
            {
                //here was request to BD
                PercentTable.Add("English_" + MangleWord, 50);
                PercentTable.Add("Russian_" + MangleWord, 50);
            }
        }
    }
}