using AngleSharp.Text;
using Ganss.XSS;
using NETCore.Encrypt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Common.ExMethods
{
    public static class StringEx
    {
        public static string AesEncrypt(this string text, string Key)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException("Key Cannot be null.");

            string Encrypt = EncryptProvider.AESEncrypt(text, Key);

            return Encrypt;
        }

        public static string AesDecrypt(this string text, string Key)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException("Key Cannot be null.");

            string Decrypt = EncryptProvider.AESDecrypt(text, Key);

            return Decrypt;
        }

        public static Guid ToGuid(this string text)
        {
            return Guid.Parse(text);
        }

        public static string ToMD5(this string text)
        {
            string Md5Hash = EncryptProvider.Md5(text);
            return Md5Hash;
        }

        public static string ReplaceRegex(this string text, string Pattern, string NewText)
        {
            string ReplaceTxt = Regex.Replace(text, Pattern, NewText, RegexOptions.Multiline | RegexOptions.Singleline);
            return ReplaceTxt;
        }

        public static bool IsMatch(this string text, string Pattern, RegexOptions regexOptions = default)
        {
            return Regex.IsMatch(text, Pattern, regexOptions);
        }

        public static bool CheckCharsForUrlName(this string text)
        {
            //return Regex.IsMatch(text, @"^[A-Za-z0-9\-]*$");
            return true;
        }

        public static bool CheckCharsForProductTitle(this string text)
        {
            return Regex.IsMatch(text, @"^[a-zا-یA-Z،,آ0-9\-\.\)\(\+\s_ءئأإؤيةًٌٍَُِّۀ»«]*$");
        }

        public static string GetSanitizeHtml(this string html)
        {
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Add("style");
            sanitizer.AllowedAttributes.Add("class");
            var strSanitizeHtml = sanitizer.Sanitize(html);

            return strSanitizeHtml;
        }

        public static string RemoveAllHtmlTags(this string str)
        {
            string _Text = str.ReplaceRegex("<.*?>", "");
            return _Text;
        }

        public static string ToLowerCaseForUrl(this string UrlName)
        {
            string NewStr = "";

            foreach (var item in UrlName)
            {
                if (Regex.IsMatch(item.ToString(), "^[A-Z]*$"))
                    NewStr += "-" + item.ToString().ToLower();

                NewStr += item.ToString();
            }

            return NewStr.Trim('-').Replace("--", "-");
        }

        public static string ToNormalizedUrl(this string Str)
        {
            foreach (var item in Str)
            {
                if (Regex.IsMatch(item.ToString(), @"^[A-Zا-یa-zئءأإؤيةَُِّۀًٌٍلآ\-\.]*$") == false)
                    Str = Str.Replace(item, '-');
            }

            return Str.Trim('-').Replace("--", "-");
        }

        public static string ToEnglishNumber(this string Str)
        {
            return Str.Replace("۰", "0")
                      .Replace("۱", "1")
                      .Replace("۲", "2")
                      .Replace("۳", "3")
                      .Replace("۴", "4")
                      .Replace("۵", "5")
                      .Replace("۶", "6")
                      .Replace("۷", "7")
                      .Replace("۸", "8")
                      .Replace("۹", "9")
                      //iphone numeric
                      .Replace("٠", "0")
                      .Replace("١", "1")
                      .Replace("٢", "2")
                      .Replace("٣", "3")
                      .Replace("٤", "4")
                      .Replace("٥", "5")
                      .Replace("٦", "6")
                      .Replace("٧", "7")
                      .Replace("٨", "8")
                      .Replace("٩", "9");
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }

        public static double GetValidPrice(this string Str)
        {
            Str = Str.ToEnglishNumber();

            string ValidPrice = "";

            foreach (var item in Str.ToCharArray())
            {
                if (item.ToString().IsMatch(@"^[0-9\.\/]$"))
                    ValidPrice += item;
            }

            return double.Parse(ValidPrice);
        }
    }
}
