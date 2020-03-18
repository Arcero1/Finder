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

        public Findable GetMatch()
        {
            Findable match = new Findable();

            switch (this.Item)
            {
                case (Findable.MultiLineComment_Open):
                    match = Findable.MultiLineComment_Close;
                    break;
                case (Findable.MultiLineComment_Close):
                    match = Findable.MultiLineComment_Open;
                    break;
                case (Findable.Brace_Open):
                    match = Findable.Brace_Close;
                    break;
                case (Findable.Brace_Close):
                    match = Findable.Brace_Open;
                    break;
                case (Findable.String):
                case (Findable.Char):
                    match = this.Item;
                    break;
                default:
                    throw new System.Exception();
            }
            return match;
        }
    }
}
