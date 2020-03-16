using System.Collections.Generic;
using System.Linq;

using NCommon;

namespace NFinder
{
    public class ScopeFinder : Finder
    {
        private readonly List<FindableFO> bracePositions;

        // takes a file, builds a dictionary of all braces contained in the file
        public ScopeFinder(string fileName) : base(fileName)
        {
            bracePositions = FindAllIgnoringComments(Findable.Brace_Open);
            bracePositions.AddRange(FindAllIgnoringComments(Findable.Brace_Close));
            bracePositions.Sort();
        }

        public ScopeBlock FindScope(Position toScope)
        {
            ScopeBlock scope = new ScopeBlock();
            scope.start = FindStartOfScope(toScope);
            if (!scope.start.InDocument)
            {
                scope.scope = ScopeBlock.Scope.Global;
            }
            else
            {
                scope.end = FindEndOfScope(toScope);
                scope.scope = ScopeBlock.Scope.Other;
            }

            return scope;
        }

        private Position FindStartOfScope(Position toScope)
        {
            FindableFO startOfScope;
            List<FindableFO> braces = bracePositions.TakeWhile(item =>
            {
                return item.Position < toScope;
            }).ToList();

            if (braces.Count == 0)
            {
                Position p = new Position();
                p.InDocument = false;

                return p;
            }

            startOfScope = braces.Last(item => item.Item == Findable.Brace_Open);

            while (startOfScope.Position != braces.Last().Position)
            {
                FindableFO c = braces.First(item =>
                {
                    return item.Position > startOfScope.Position;
                });

                braces.Remove(startOfScope);
                braces.Remove(c);

                if (braces.Count == 0)
                {
                    startOfScope.Position.InDocument = false;
                    break;
                }
                startOfScope = braces.Last(item => item.Item == Findable.Brace_Open);
            }

            return startOfScope.Position;
        }


        private Position FindEndOfScope(Position toScope)
        {
            List<FindableFO> braces = bracePositions.SkipWhile(item =>
            {
                return item.Position < toScope;
            }).ToList();

            FindableFO endOfScope = braces.First(item => item.Item == Findable.Brace_Close);

            while (endOfScope.Position != braces.First().Position)
            {
                FindableFO c = braces.Last(item =>
                {
                    return item.Position < endOfScope.Position;
                });

                braces.Remove(endOfScope);
                braces.Remove(c);

                endOfScope = braces.First(item => item.Item == Findable.Brace_Close);
            }

            return endOfScope.Position;
        }
    }
}
