namespace NCommon
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

        public static Column operator +(Column c1, Column c2)
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
}
