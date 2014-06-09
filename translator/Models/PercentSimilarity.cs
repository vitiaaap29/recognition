using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace translator.Models.PercentSimilarity
{
    public static class PercentSimilarity
    {
        public static float PercentOfSimilarity(this String obj, string str)
        {
            float result = 0;
            if (obj[0] == str[0])
            {
                float weightLetter = 100 / obj.Length;
                int minLength = Math.Min(obj.Length, str.Length);

                int countEqualsChars = 1;
                for (int i = 1; i < minLength && obj[i] == str[i]; i++)
                {
                    countEqualsChars++;
                }

                result = weightLetter * countEqualsChars;
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}