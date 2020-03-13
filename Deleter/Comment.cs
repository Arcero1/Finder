using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deleter
{

    public class FinderOutputElement
    {
        public Position Position { get; set; }
        public string Item { get; set; }

        public FinderOutputElement(string item)
        {
            this.Item = item;
        }

        public FinderOutputElement(string item, Position position)
        {
            this.Item = item;
            this.Position = position;
        }
    }

    //public class CommentFinder : Finder
    //{
    //    private readonly List<Position> stringLiteralPositions;
    //    private readonly List<Position> commentPositions;
    //    private readonly List<Position> multiLineCommentPositions;

    //    CommentFinder(string fileName) : base(fileName)
    //    {
    //        var p1 = FindAll('');
    //        p1.AddRange(FindAll("//"));
    //        p1.AddRange(FindAll(""))
    //        multiLineCommentPositions = FindAll("/*").Concat(FindAll("*/")).ToList();
    //    }

    //    public

    //    public ScopeBlock FindScope(Position toScope)
    //    {
    //        ScopeBlock scope = new ScopeBlock();
    //        scope.start = FindStartOfScope(toScope);
    //        if (!scope.start.inDocument)
    //        {
    //            scope.scope = ScopeBlock.Scope.Global;
    //        }
    //        else
    //        {
    //            scope.end = FindEndOfScope(toScope);
    //            scope.scope = ScopeBlock.Scope.Other;
    //        }

    //        return scope;
    //    }


    //    private Position FindEndOfScope(Position toScope)
    //    {
    //        SortedDictionary<Position, Brace> braces = new SortedDictionary<Position, Brace>(stringLiteralPositions.SkipWhile(item =>
    //        {
    //            return item.Key < toScope;
    //        }).ToDictionary(item => item.Key, item => item.Value));
    //        Position o = braces.First(item => item.Value == Brace.Closing).Key;

    //        while (o != braces.First().Key)
    //        {
    //            Position c = braces.Last(item =>
    //            {
    //                return item.Key < o;
    //            }).Key;

    //            braces.Remove(o);
    //            braces.Remove(c);

    //            o = braces.First(item => item.Value == Brace.Closing).Key;
    //        }

    //        return o;
    //    }

    //    private Position FindStartOfScope(Position toScope)
    //    {
    //        Position startOfScope;
    //        SortedDictionary<Position, Brace> braces = new SortedDictionary<Position, Brace>(stringLiteralPositions.TakeWhile(item =>
    //        {
    //            return item.Key < toScope;
    //        }).ToDictionary(item => item.Key, item => item.Value));

    //        if (braces.Count == 0)
    //        {
    //            Position p = new Position();
    //            p.inDocument = false;

    //            return p;
    //        }

    //        startOfScope = braces.Last(item => item.Value == Brace.Opening).Key;

    //        while (startOfScope != braces.Last().Key)
    //        {
    //            Position c = braces.First(item =>
    //            {
    //                return item.Key > startOfScope;
    //            }).Key;

    //            braces.Remove(startOfScope);
    //            braces.Remove(c);

    //            if (braces.Count == 0)
    //            {
    //                startOfScope.inDocument = false;
    //                return startOfScope;
    //            }
    //            startOfScope = braces.Last(item => item.Value == Brace.Opening).Key;
    //        }

    //        return startOfScope;
    //    }

    //}


    //public enum Comment
    //{
    //    None,
    //    SingleLine,
    //    MultiLineStart,
    //    MultiLineEnd
    //}

    //class Line
    //{
    //    private string line;
    //    public bool HasComment()
    //    {
    //        return !line.Contains("//");
    //    }

    //    string Get()
    //    {
    //        return this.line;
    //    }

    //    void Set(string line)
    //    {
    //        this.line = line;
    //    }
    //}
}
