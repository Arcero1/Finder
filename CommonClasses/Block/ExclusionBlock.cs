namespace NCommon
{
    public class ExclusionBlock : Block
    {
        private Type _type;
        public void SetAsLineType(Position startPosition)
        {
            start = startPosition;
            _type = Type.EntireLine;
            end = null;
        }
        public enum Type
        {
            PartLine,
            EntireLine,
            MultiLine
        }

        public override bool Equals(object obj)
        {
            var toCompareWith = obj as ExclusionBlock;
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
    }
}
