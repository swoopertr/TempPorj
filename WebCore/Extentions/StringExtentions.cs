using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCore.Extentions
{
    public static class StringExtentions
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly string urlPattern = "[^a-zA-Z0-9-.]";
        public static string F(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        public static string ToTitleCase(this string mText)
        {
            if (mText == null) return mText;
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(mText.ToLower());
        }

        public static string GenerateRandom(int length = 15)
        {
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(Chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        public static string ToJsonString<T>(this T obj)
        {
            using (var stream = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static T FromJsonString<T>(this string obj)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(obj)))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                var ret = (T)ser.ReadObject(stream);
                return ret;
            }
        }

        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }

        public static string ToUrlSlug(this string text)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(
                        text.Trim().ToLower()
                            .Replace("ö", "o")
                            .Replace("ç", "c")
                            .Replace("ş", "s")
                            .Replace("ı", "i")
                            .Replace("ğ", "g")
                            .Replace("ü", "u"),
                        @"\s+", " "), // multiple spaces to one space
                    @"\s", "-"), // spaces to hypens
                @"[^a-z0-9\s-]", ""); // removing invalid chars
        }

        public static Stream ToStream(this string text)
        {
            var byteArr = Encoding.ASCII.GetBytes(text);
            var stream = new MemoryStream(byteArr);
            return stream;

        }
    }
}
