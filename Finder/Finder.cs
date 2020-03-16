using System.Collections.Generic;
using System.Linq;
using System.IO;

using NCommon;

namespace NFinder
{
    public class Term
    {
        private readonly string _term;
        public Column Start { get; set; }
        public Column End { get; private set; }
        public int Size
        {
            get { return _term.Length; }
        }

        public Term(string term, Column column = default(Column))
        {
            _term = term;
            Start = column;
            End = column + Size;
        }

        public Column Previous
        {
            get { return Start - 1; }
        }

        public Column Next
        {
            get { return End + 1; }
        }

        public static implicit operator string(Term term) => term._term;
    }

    public class Line
    {
        private readonly string _line;

        public Line(string line)
        {
            _line = line;
        }

        public Column Find(string term)
        {
            if (!_line.Contains(term)) { return null; }

            return _line.IndexOf(term);
        }

        public Column FindFrom(string term, int from)
        {
            if (!_line.Substring(from).Contains(term)) { return null; }

            return _line.IndexOf(term, from);
        }

        public Column FindTo(string term, int to)
        {
            if (!_line.Substring(0, to).Contains(term)) { return null; }

            return _line.IndexOf(term, 0, to);
        }

        public Column FindBetween(string term, int from, int to)
        {
            if (!_line.Substring(from, to).Contains(term)) { return null; }

            return _line.IndexOf(term, from, to);
        }

        public Column FindDistinct(string term)
        {
            Column c = null;

            while (_line.Contains(term))
            {
                Term t = new Term(term, _line.IndexOf(term));

                if (t.Previous == 0 || _line[t.Previous] == ' '
                    && t.Next > _line.Length || _line[t.Next] == ' ')
                {
                    c = _line.IndexOf(term);
                    break;
                }
            }


            return c;
        }

        public static implicit operator string(Line line) => line._line;
        public static implicit operator Line(string line) => new Line(line);
    }

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

            position.Line = lineNumber;
            position.Column = lines.IndexOf(term);

            return position;
        }

        public List<FinderOutput> FindAll(char term)
        {
            return FindAll(term.ToString());
        }

        public List<FindableFO> FindAll(Findable findable)
        {
            List<FindableFO> results = new List<FindableFO>();

            int lineIndex = 0;
            File.ReadLines(fileName).ToList().ForEach(line =>
            {
                int cursor = 0;
                string l = line;
                while (l.Contains(FindableMethods.ToString(findable)))
                {
                    cursor += l.IndexOf(FindableMethods.ToString(findable));
                    results.Add(new FindableFO(findable, new Position(lineIndex, cursor)));
                    cursor++;
                    l = line.Substring(cursor);
                }

                lineIndex++;
            });

            return results;
        }

        public List<FinderOutput> FindAll(string term)
        {
            List<FinderOutput> results = new List<FinderOutput>();

            int lineIndex = 0;
            File.ReadLines(fileName).ToList().ForEach(line =>
            {

                int cursor = 0;
                string l = line;
                while (l.Contains(term))
                {
                    cursor += l.IndexOf(term);
                    results.Add(new FinderOutput(term, new Position(lineIndex, cursor)));
                    cursor++;
                    l = line.Substring(cursor);
                }

                lineIndex++;
            });

            ignoreComments = false;
            return results;
        }

        public List<FinderOutput> FindAllDistinct(string term)
        {
            List<FinderOutput> results = new List<FinderOutput>();

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
                    results.Add(new FinderOutput(term, new Position(lineIndex, cursor)));
                    cursor++;
                    l = line.Substring(cursor);
                }

                lineIndex++;
            });

            ignoreComments = false;
            return results;
        }

        public List<FindableFO> FindAllIgnoringComments(Findable term)
        {
            ignoreComments = true;
            return FindAll(term);
        }
    }
}
