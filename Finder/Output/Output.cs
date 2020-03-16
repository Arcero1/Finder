using NCommon;
using System;

namespace NFinder
{
    public class FinderOutput : IComparable
    {
        public Position Position { get; set; }
        public string Item { get; private set; }

        protected FinderOutput(Position position = null) { }

        public FinderOutput(string item, Position position = null)
        {
            this.Item = item;
            this.Position = position;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            FinderOutput output = obj as FinderOutput;
            if (output != null)
            {
                return this.Position.CompareTo(output.Position);
            }
            else
            {
                throw new ArgumentException("Object is not a Temperature");
            }
        }
    }
}
