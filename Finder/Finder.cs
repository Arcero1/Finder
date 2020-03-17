using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

using NCommon;

namespace NFinder
{
    public class Finder
    {
        protected readonly string fileName;

        private Finder() { }
        public Finder(string fileName) { this.fileName = fileName; }

        public Position Find(string term)
        {
            int lineNumber = -1;
            string line = File.ReadLines(fileName).Where(l => 
            {
                lineNumber++;
                return l.Contains(term); 
            }).Take(1).First();

            return new Position(lineNumber, line.IndexOf(term));
        }
        public Position FindDistinct(string term)
        {
            int lineNumber = -1;
            string line = File.ReadLines(fileName).Where(l =>
            {
                lineNumber++;
                return l.ContainsDistinct(term);
            }).Take(1).First();

            return new Position(lineNumber, line.IndexOfDistinct(term));
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
                    l = line.Substring(++cursor);
                }

                lineIndex++;
            });

            return results;
        }
        public List<FindableFO> FindAll(Findable findable) => FindAll(FindableMethods.ToString(findable)).Transform();

        public List<FinderOutput> FindAllDistinct(string term)
        {
            List<FinderOutput> results = new List<FinderOutput>();

            int lineIndex = 0;
            File.ReadLines(fileName).ToList().ForEach(line =>
            {
                int cursor = 0;
                string l = line;
                while (l.ContainsDistinct(term))
                {
                    cursor += l.IndexOfDistinct(term);
                    results.Add(new FinderOutput(term, new Position(lineIndex, cursor)));
                    l = line.Substring(++cursor);
                }

                lineIndex++;
            });

            return results;
        }
    }
}
