using System.Collections.Generic;
using NFinder.Internal;

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

    static class FindableMethods
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

        public static string ToString(Findable findable)
        {
            try
            {
                return strings[findable];
            } catch (System.Exception)
            {
                throw new NotFindableException();
            }
        }

        private static Findable ToFindable(this string findableString)
        {
            if (strings.ContainsValue(findableString))
            {
                foreach (var keyValuePair in strings)
                {
                    if (keyValuePair.Value == findableString) return keyValuePair.Key;
                }
            }

            throw new System.Exception();
        }

        public static List<FindableFO> Transform(this List<FinderOutput> finderOutput)
        {
            List<FindableFO> findableFOs = new List<FindableFO>();
            finderOutput.ForEach(el => findableFOs.Add(new FindableFO(el.Item.ToFindable(), el.Position)));
            return findableFOs;
        }
    }
}
