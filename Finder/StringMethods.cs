using System;
using System.Linq;

namespace NFinder
{
    public static class StringMethods
    {
        public static int IndexOfDistinct(this string line, string term)
        {
            int substringIndex = 0;
            while (!line.Substring(substringIndex).NextIsDistinct(term))
            {
                substringIndex = line.IndexOf(term, line.IndexOf(term) + 1);
            }

            return substringIndex;
        }

        public static bool NextIsDistinct(this string line, string term)
        {
            if (!line.Contains(term)) return false;

            int startIndex = line.IndexOf(term) - 1;
            int endIndex = line.IndexOf(term) + term.Length + 1;
            return (startIndex < 0 || IsDistinctingChar(line[startIndex]))
                && (endIndex > line.Length || IsDistinctingChar(line[endIndex]));
        }

        public static bool ContainsDistinct(this string line, string term)
        {
            int substringIndex = 0;
            while (line.Substring(substringIndex).Contains(term))
            {
                if (line.Substring(substringIndex).NextIsDistinct(term)) return true;
                substringIndex = line.IndexOf(term, line.IndexOf(term) + 1);
            }

            return false;
        }

        private static bool IsDistinctingChar(char c)
        {
            if (Char.IsLetterOrDigit(c)) return false;
            if (Char.IsWhiteSpace(c)) return true;
            if (Char.IsPunctuation(c)) return !".".Contains(c);
            if (Char.IsSymbol(c)) return !"_".Contains(c);

            throw new Exception();
        }
    }
}
