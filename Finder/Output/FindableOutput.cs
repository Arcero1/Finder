using NCommon;
using System.Collections.Generic;
using System;

namespace NFinder
{
    public class FindableFO : FinderOutput
    {
        public new Findable Item { get; private set; }

        public FindableFO(Findable item, Position position = null)
        {
            this.Item = item;
            this.Position = position;
        }
        
        public static implicit operator Findable(FindableFO f) => f.Item;
    }
}
