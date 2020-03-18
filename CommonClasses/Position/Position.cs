using System;

namespace NCommon
{
    public class Position : IComparable
    {
        public bool inDocument { get; set; } = true;
        public int line { get; set; } = 0;
        public int column { get; set; } = 0;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Position position = obj as Position;
            if (position != null)
            {
                int total = this.line - position.line;
                return total == 0 ? this.column - position.column : total;
            }
            else
            {
                throw new ArgumentException("Object is not a Temperature");
            }
        }

        public Position(int line, int column)
        {
            this.line = line;
            this.column = column;
        }



        public Position()
        {
            this.line = 0;
            this.column = 0;
            this.inDocument = true;
        }

        public Position Next()
        {
            Position p = this;
            p.column++;
            return p;
        }

        public Position Previous()
        {
            Position p = this;
            p.column--;
            return p;
        }

        public bool IsValid()
        {
            return line < 0 || column < 0;
        }

        public override string ToString()
        {
            return "L:" + (line + 1) + ", C:" + (column + 1);
        }

        public static bool operator ==(Position p1, Position p2)
        {
            if (ReferenceEquals(p1, null))
            {
                return false;
            }
            if (ReferenceEquals(p2, null))
            {
                return false;
            }

            return p1.line == p2.line && p1.column == p2.column;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            if (ReferenceEquals(p1, null))
            {
                return true;
            }
            if (ReferenceEquals(p2, null))
            {
                return true;
            }
            return p1.line != p2.line || p1.column != p2.column;
        }

        public static bool operator <(Position p1, Position p2)
        {
            if (p1.line == p2.line)
            {
                return p1.column < p2.column;
            }

            return p1.line < p2.line;
        }

        public static bool operator <=(Position p1, Position p2)
        {
            if (p1.line == p2.line)
            {
                if (p1.column == p2.column) return true;
                return p1.column < p2.column;
            }

            return p1.line < p2.line;
        }

        public static bool operator >(Position p1, Position p2)
        {
            if (p1.line == p2.line)
            {
                return p1.column > p2.column;
            }

            return p1.line > p2.line;
        }

        public static bool operator >=(Position p1, Position p2)
        {
            if (p1.line == p2.line)
            {
                if (p1.column == p2.column) return true;
                return p1.column > p2.column;
            }

            return p1.line > p2.line;
        }
    };
}