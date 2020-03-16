using System.Collections.Generic;
using NFinder.Internal;

namespace NFinder
{
    public enum Findable
    {
        Brace_Open,
        Brace_Close,

        LineComment,
        
        MultiLineComment_Open,
        MultiLineComment_Close,

        Char,
        String,
        LineBreak,

        Unmatched
    }

    static class FindableMethods
    {
        private static readonly Dictionary<Findable, string> strings = new Dictionary<Findable, string>()
        {
            { Findable.Brace_Open,              "{" },
            { Findable.Brace_Close,             "}" },
            
            { Findable.LineComment,             "//" },

            { Findable.MultiLineComment_Open,   "/*" },
            { Findable.MultiLineComment_Close,  "*/"},

            { Findable.Char,                    "\'" },
            { Findable.String,                  "\"" },
            { Findable.LineBreak,               "\\" },
        };

        private static readonly Dictionary<Findable, Findable> matches = new Dictionary<Findable, Findable>()
        {
            { Findable.Brace_Open,              Findable.Brace_Close },
            { Findable.Brace_Close,             Findable.Brace_Open },

            { Findable.LineComment,             Findable.Unmatched },
            
            { Findable.MultiLineComment_Open,   Findable.MultiLineComment_Close },
            { Findable.MultiLineComment_Close,  Findable.MultiLineComment_Open},

            { Findable.Char,                    Findable.Char },
            { Findable.String,                  Findable.String },
            { Findable.LineBreak,               Findable.Unmatched }
        };

        public static Findable GetMatch(this Findable findable)
        {
            try
            {
                return matches[findable];
            }
            catch (System.Exception)
            {
                throw new NotFindableException();
            }
        }

        public static string ToString(this Findable findable)
        {
            try
            {
                return strings[findable];
            } catch (System.Exception)
            {
                throw new NotFindableException();
            }
        }
    }
}
