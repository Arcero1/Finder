using NCommon;

namespace NFinder
{
    public class FinderOutputFindable
    {
        public Position Position { get; set; }
        public Findable Item { get; set; }

        public FinderOutputFindable(Findable item)
        {
            this.Item = item;
        }

        public FinderOutputFindable(Findable item, Position position)
        {
            this.Item = item;
            this.Position = position;
        }
    }
}
