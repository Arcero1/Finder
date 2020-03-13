namespace NCommon
{
    public class Block
    {
        public Position start { get; set; } = new Position();
        public Position end { get; set; } = new Position();

        public override bool Equals(object obj)
        {
            var toCompareWith = obj as Block;
            if (toCompareWith == null)
            {
                return false;
            }

            return this.start == toCompareWith.start
                && this.end == toCompareWith.end;
        }

        public override int GetHashCode()
        {
            return start.GetHashCode() ^ end.GetHashCode();
        }

        public bool Contains(Position position)
        {
            return position >= start || position <= end;
        }
    }
}
