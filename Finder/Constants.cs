using System.Collections.Generic;

namespace NFinder
{
    public enum Findable
    {
        Brace_Open,
        Brace_Close,
        String,
        LineComment,
        MultiLineComment_Open,
        MultiLineComment_Close,
        LineBreak
    }

    static class FindableStrings
    {
        private static readonly Dictionary<Findable, string> strings = new Dictionary<Findable, string>()
        {
            { Findable.Brace_Open,              "{" },
            { Findable.Brace_Close,             "}" },
            { Findable.String,                  "\"" },
            { Findable.LineComment,             "//" },
            { Findable.MultiLineComment_Open,   "/*" },
            { Findable.MultiLineComment_Close,  "*/"},
            { Findable.LineBreak,               "\\" }
        };

        public static string Get(Findable findable)
        {
            return strings[findable];
        }
    }
}
