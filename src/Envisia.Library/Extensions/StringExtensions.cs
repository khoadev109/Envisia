using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace Envisia.Library.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNewLines(this string input)
        {
            if (input == null) return null;

            return Regex.Replace(input, @"\r\n?|\n", "");
        }

        public static string ToSentenceCase(this string input)
        {
            var lower = input.ToLower();            
            var regex = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);            
            var result = regex.Replace(lower, match => match.Value.ToUpper());

            return result;
        }

        public static string ToNegativeNumber(this string input)
        {
            var negative = "-" + input;

            return negative;
        }

        public static bool IsUppercase(this string s)
        {
            return !s.Any(c => char.IsLower(c));
        }

        public static byte[] ToByteArray(this string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }

        public static string[] Split(this string s, string seperator)
        {
            if (s == null) return new string[] { };

            return s.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);            
        }

        public static string ToTitleCase(this string s)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            var result = textInfo.ToTitleCase(s.ToLower());

            return result;
        }        

        public static string ReplaceAll(this string s, string pattern, string replacement)
        {
            return Regex.Replace(s, pattern, replacement);
        }

        public static string NumbersOnly(this string s)
        {
            return string.Join("", s.ToCharArray().Where(x => char.IsNumber(x)));
        }

        public static int GetNumbers(this string s)
        {
            return int.Parse(s.NumbersOnly());
        }

        public static bool ContainsAny(this string s, IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                if (s.Contains(item)) return true;
            }

            return false;
        }

        public static bool StartsWith(this string s, IEnumerable<string> targets)
        {
            foreach (var target in targets)
            {
                if (s.StartsWith(target))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetTextFromQueryString(this string queryString)
        {
            var text = queryString.Replace('-', ' ');

            var capitalizedFirstLetter = char.ToUpper(text[0]) + text[1..];

            return capitalizedFirstLetter;
        }
    }
}
