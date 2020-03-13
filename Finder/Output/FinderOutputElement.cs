using NCommon;

namespace NFinder
{
    public class FinderOutputElement
    {
        public Position Position { get; set; }
        public string Item { get; set; }

        public FinderOutputElement(string item)
        {
            this.Item = item;
        }

        public FinderOutputElement(string item, Position position)
        {
            this.Item = item;
            this.Position = position;
        }
    }
}
