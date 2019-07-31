using System.Text.RegularExpressions;

namespace Youtube.NET
{
    public static class StringUpdateMethods
    {
        public static string Append(this string original, string content) => original + content;
        public static string Prepend(this string original, string content) => content + original;

        public static (int, string) Remove(this string original, string content, bool ignoreCase = false)
        {
            content = Regex.Escape(content);
            return Replace(original, content, "", ignoreCase);
        }

        public static (int, string) Replace(this string original, string content, string newValue, bool ignoreCase = false)
        {
            int matchCount = 0;
            content = content.Replace("?", "\\?");

            if (ignoreCase)
            {
                original = Regex.Replace(original, content,
                    (match) =>
                    {
                        matchCount++;
                        return match.Result(newValue);
                    }, RegexOptions.IgnoreCase | RegexOptions.Multiline );
            }
            else
            {
                original = Regex.Replace(original, content,
                    (match) =>
                    {
                        matchCount++;
                        return match.Result(newValue);
                    }, RegexOptions.Multiline);
            }


            return (matchCount, original);
        }
    }
}
