﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deleter
{
    public class Column
    {
        private int column;
        private bool isValid;

        public Column()
        {
            this.column = 0;
            this.isValid = true;
        }

        public Column(int column)
        {
            this.column = column;
            this.isValid = true;
        }

        public Column(bool isValid)
        {
            this.column = 0;
            this.isValid = isValid;
        }

        public int Get()
        {
            return column;
        }

        public void Set(int column)
        {
            this.column = column;
        }

        public bool IsValid()
        {
            return isValid ? column >= 0 : isValid;
        }

        public Column Next()
        {
            return this + 1;
        }

        public static Column operator ++(Column c)
        {
            c.Set(c.Get() + 1);
            return c;
        }

        public static Column operator+(Column c1, Column c2)
        {
            Column c = new Column();
            c.Set(c1.Get() + c2.Get());
            c.isValid = c1.isValid && c2.isValid;
            return c;
        }

        public static Column operator +(Column c1, int i1)
        {
            Column c = new Column();
            c.Set(c1.Get() + i1);
            c.isValid = c1.isValid;
            return c;
        }

        public static Column operator -(Column c1, Column c2)
        {
            Column c = new Column();
            c.Set(c1.Get() - c2.Get());
            c.isValid = c1.isValid && c2.isValid;
            return c;
        }

        public static Column operator -(Column c1, int i1)
        {
            Column c = new Column();
            c.Set(c1.Get() - i1);
            c.isValid = c1.isValid;
            return c;
        }
    }

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

        public Position NextColumn()
        {
            Position p = this;
            p.column++;
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
            if(p1.line == p2.line)
            {
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
    };
}