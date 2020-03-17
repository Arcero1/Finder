using System;

namespace NCommon
{
    public partial class Position
    {
        private bool    _inDocument = true;
        public bool     InDocument  { get { return _inDocument && Line >= 0 && Column >= 0; } set { _inDocument = value; } }

        public Column   Line        { get; set; }
        public Column   Column      { get; set; }

        public Position(int line = 0, int column = 0)
        {
            this.Line = line;
            this.Column = column;
        }

        public override string ToString() => "\tL:" + Line.ToString() + ",\tC:" + Column.ToString();
    };

    public partial class Position : IComparable
    {
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if ((Position)obj == null) throw new ArgumentException("Object is not Position type");

            int total = this.Line.CompareTo(((Position)obj).Line);
            return total == 0 ? this.Column.CompareTo(((Position)obj).Column) : total;
        }

        public override bool Equals(object obj) => !((obj == null) || !this.GetType().Equals(obj.GetType())) && this == (Position)obj;

        public override int GetHashCode() => !InDocument ? -1 : (Line << 12) ^ Column;

        public static bool operator ==(Position p1, Position p2)
        {
            if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
            {
                return (ReferenceEquals(p1, null) && ReferenceEquals(p2, null));
            }

            if (p1.InDocument == p2.InDocument)
            {
                if (!p1.InDocument) return true;

                return p1.Line == p2.Line && p1.Column == p2.Column;
            }

            return false;
        }
        public static bool operator !=(Position p1, Position p2) => !(p1 == p2);

        public static bool operator <(Position p1, Position p2)
        {
            if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
            {
                return ReferenceEquals(p1, null);
            }

            return p1.Line == p2.Line ? p1.Column < p2.Column : p1.Line < p2.Line;
        }
        public static bool operator <=(Position p1, Position p2)    => p1 == p2 || p1 < p2;

        public static bool operator >(Position p1, Position p2)     => !(p1 <= p2);
        public static bool operator >=(Position p1, Position p2)    => !(p1 < p2);
    }
}