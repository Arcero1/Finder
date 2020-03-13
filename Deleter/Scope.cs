using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.IO;

namespace Deleter
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
        : base(message)
    {
        }

        public NotFoundException(string message, Exception inner)
        : base(message, inner)
    {
        }
    }

    public class ScopeBlock
    {
        public enum Scope
        {
            Global,
            Class,
            Other
        }

        public override bool Equals(object obj)
        {
            var toCompareWith = obj as ScopeBlock;
            if (toCompareWith == null)
            {
                return false;
            }

            return this.scope == toCompareWith.scope
                && this.start == toCompareWith.start
                && this.end == toCompareWith.end;
        }

        public Position start = new Position();
        public Position end = new Position();
        public Scope scope;

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (scope)
            {
                case (Scope.Global):
                    stringBuilder.Append("Global");
                    break;
                case (Scope.Class):
                    stringBuilder.Append("Class");
                    break;
                default:
                    stringBuilder.Append("Undetermined (usually local)");
                    break;
            }
            stringBuilder.AppendLine(" Scope");
            if(scope != Scope.Global)
            {
                stringBuilder.Append("[start] ").AppendLine(start.ToString());
                stringBuilder.Append("[end] ").AppendLine(end.ToString());
            }

            return stringBuilder.ToString();
        }
    }

    public class ScopeFinder : Finder
    {
        private readonly List<FinderOutputElement> bracePositions;

        // takes a file, builds a dictionary of all braces contained in the file
        public ScopeFinder(string fileName) : base(fileName)
        {
            bracePositions = FindAllIgnoringComments(FindableStrings.Get(Findable.Brace_Open));
            bracePositions.AddRange(FindAllIgnoringComments(FindableStrings.Get(Findable.Brace_Close)));
            bracePositions.Sort((el1, el2) => 
            {
                return el1.Position.CompareTo(el2.Position);
            });
        }

        public ScopeBlock FindScope(Position toScope)
        {
            ScopeBlock scope = new ScopeBlock();
            scope.start = FindStartOfScope(toScope);
            if(!scope.start.inDocument)
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
            FinderOutputElement startOfScope;
            List<FinderOutputElement> braces = bracePositions.TakeWhile(item =>
            {
                return item.Position < toScope;
            }).ToList();

            if (braces.Count == 0)
            {
                Position p = new Position();
                p.inDocument = false;

                return p;
            }

            startOfScope = braces.Last(item => item.Item == FindableStrings.Get(Findable.Brace_Open));

            while (startOfScope.Position != braces.Last().Position)
            {
                FinderOutputElement c = braces.First(item =>
                {
                    return item.Position > startOfScope.Position;
                });

                braces.Remove(startOfScope);
                braces.Remove(c);

                if (braces.Count == 0)
                {
                    startOfScope.Position.inDocument = false;
                    break;
                }
                startOfScope = braces.Last(item => item.Item == FindableStrings.Get(Findable.Brace_Open));
            }

            return startOfScope.Position;
        }


        private Position FindEndOfScope(Position toScope)
        {
            List<FinderOutputElement> braces = bracePositions.SkipWhile(item =>
            {
                return item.Position < toScope;
            }).ToList();

            FinderOutputElement endOfScope = braces.First(item => item.Item == FindableStrings.Get(Findable.Brace_Close));

            while (endOfScope.Position != braces.First().Position)
            {
                FinderOutputElement c = braces.Last(item =>
                {
                    return item.Position < endOfScope.Position;
                });

                braces.Remove(endOfScope);
                braces.Remove(c);
                
                endOfScope = braces.First(item => item.Item == FindableStrings.Get(Findable.Brace_Close));
            }

            return endOfScope.Position;
        }
    }
}
