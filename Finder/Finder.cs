using System.Collections.Generic;
using System.Linq;
using System.IO;

using NCommon;

namespace NFinder
{
    public class Finder
    {
        protected readonly string fileName;
        private bool ignoreComments = false;

        private Finder() { }
        public Finder(string fileName) { this.fileName = fileName; }

        public Position Find(string term)
        {
            Position position = new Position();
            int lineNumber = 0;
            var lines = File.ReadLines(fileName).Where(line =>
            {
                if (line.Contains(term))
                {
                    return true;
                }

                lineNumber++;
                return false;
            }).Take(1).First();

            position.line = lineNumber;
            position.column = lines.IndexOf(term);

            return position;
        }

        public List<FinderOutputElement> FindAll(char term)
        {
            return FindAll(term.ToString());
        }

        public List<FinderOutputFindable> FindAll(Findable findable)
        {
            List<FinderOutputFindable> results = new List<FinderOutputFindable>();

            int lineIndex = 0;
            File.ReadLines(fileName).ToList().ForEach(line =>
            {
                int lineCommentIndex = ignoreComments && line.Contains("//") ? line.IndexOf("//") : -1;

                int cursor = 0;
                string l = line;
                while (l.Contains(FindableStrings.Get(findable)))
                {
                    cursor += l.IndexOf(FindableStrings.Get(findable));
                    if (lineCommentIndex != -1 && lineCommentIndex < cursor) break;
                    results.Add(new FinderOutputFindable(findable, new Position(lineIndex, cursor)));
                    cursor++;
                    l = line.Substring(cursor);
                }

                lineIndex++;
            });

            return results;
        }

        public List<FinderOutputElement> FindAll(string term)
        {
            List<FinderOutputElement> results = new List<FinderOutputElement>();

            int lineIndex = 0;
            File.ReadLines(fileName).ToList().ForEach(line =>
            {
                int lineCommentIndex = ignoreComments && line.Contains("//") ? line.IndexOf("//") : -1;

                int cursor = 0;
                string l = line;
                while (l.Contains(term))
                {
                    cursor += l.IndexOf(term);
                    if (lineCommentIndex != -1 && lineCommentIndex < cursor) break;
                    results.Add(new FinderOutputElement(term, new Position(lineIndex, cursor)));
                    cursor++;
                    l = line.Substring(cursor);
                }

                lineIndex++;
            });

            ignoreComments = false;
            return results;
        }

        public List<FinderOutputElement> FindAllIgnoringComments(string term)
        {
            ignoreComments = true;
            return FindAll(term);
        }

        public List<FinderOutputElement> FindAllIgnoringComments(char term)
        {
            ignoreComments = true;
            return FindAll(term);
        }
    }
}
