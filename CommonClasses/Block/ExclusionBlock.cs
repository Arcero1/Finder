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
    }
}
