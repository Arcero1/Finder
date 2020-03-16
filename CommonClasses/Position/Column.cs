namespace NCommon
{
    public class Column
    {
        private int _column;
        private bool _isValid;

        public Column(int column = 0, bool isValid = true)
        {
            this._column = column;
            this._isValid = isValid;
        }

        public int Get()
        {
            return _column;
        }

        public void Set(int column)
        {
            this._column = column;
        }

        public bool IsValid()
        {
            return _isValid ? _column >= 0 : _isValid;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Column c = (Column)obj;
                return c == this;
            }
        }

        public static bool operator ==(Column c1, Column c2)
        {
            if (c1._isValid == c2._isValid)
            {
                if (!c1._isValid) return true;

                return c1._column == c2._column;
            }

            return false;
        }

        public static bool operator !=(Column c1, Column c2) => !(c1 == c2);

        public Column Next()
        {
            return this + 1;
        }

        public static Column operator ++(Column c)
        {
            c.Set(c.Get() + 1);
            return c;
        }

        public static Column operator +(Column c1, Column c2)
        {
            Column c = new Column();
            c.Set(c1.Get() + c2.Get());
            c._isValid = c1._isValid && c2._isValid;
            return c;
        }

        public static Column operator +(Column c1, int i1)
        {
            Column c = new Column();
            c.Set(c1.Get() + i1);
            c._isValid = c1._isValid;
            return c;
        }

        public static Column operator -(Column c1, Column c2)
        {
            Column c = new Column();
            c.Set(c1.Get() - c2.Get());
            c._isValid = c1._isValid && c2._isValid;
            return c;
        }

        public static Column operator -(Column c1, int i1)
        {
            Column c = new Column();
            c.Set(c1.Get() - i1);
            c._isValid = c1._isValid;
            return c;
        }

        public static implicit operator Column(int column) => new Column(column);
        public static implicit operator int(Column column) => column._column;
    }
}
