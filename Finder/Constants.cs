using System.Collections.Generic;

namespace NFinder
{
    public enum Findable
    {
        Brace_Open,
        Brace_Close,
        String,
        Char,
        LineComment,
        MultiLineComment_Open,
        MultiLineComment_Close,
        EscapeCharacter,
        NonFindable
    }

    static class FindableStrings
    {
        private static readonly Dictionary<Findable, string> strings = new Dictionary<Findable, string>()
        {
            { Findable.Brace_Open,              "{" },
            { Findable.Brace_Close,             "}" },
            { Findable.String,                  "\"" },
            { Findable.Char,                    "'"},
            { Findable.LineComment,             "//" },
            { Findable.MultiLineComment_Open,   "/*" },
            { Findable.MultiLineComment_Close,  "*/"},
            { Findable.EscapeCharacter,         "\\" }
        };

        public static string Get(Findable findable)
        {
            return strings[findable];
        }
    }
}
