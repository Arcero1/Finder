using System;

namespace NCommon
{
    public partial class Column
    {
        private int _column;

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid ? _column >= 0 : _isValid;
            set => this._isValid = value;
        }

        public Column(int column = 0, bool isValid = true)
        {
            this._column = column;
            this._isValid = isValid;
        }

        public Column Next => this + 1;

        public static int ToVisible(int i) => i + 1;
        public static int ToCalculable(int i) => i - 1;

        public override string ToString() => this._isValid ? ToVisible(this).ToString() : "INVALID";

        public static implicit operator Column(int column) => new Column(column);
        public static implicit operator int(Column column) => column._column;
    }

    public partial class Column : IComparable
    {
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (!this.GetType().Equals(obj.GetType())) throw new ArgumentException("Object is not Column type");

            return this._column.CompareTo(((Column)obj)._column);
        }

        public override bool Equals(object obj) =>
                !((obj == null) || !this.GetType().Equals(obj.GetType())) && this == (Column)obj;

        public override int GetHashCode() => this._isValid ? _column : -1;

        public static bool operator ==(Column c1, Column c2) =>
            c1._isValid == c2._isValid && (!c1._isValid || c1._column == c2._column);
        public static bool operator !=(Column c1, Column c2) => !(c1 == c2);

        public static Column operator ++(Column c) => c._column++;
        public static Column operator --(Column c) => c._column--;

        public static Column operator +(Column c1, Column c2) => new Column(c1._column + c2._column, c1._isValid && c2._isValid);
        public static Column operator -(Column c1, Column c2) => new Column(c1._column + c2._column, c1._isValid && c2._isValid);
    }
}
