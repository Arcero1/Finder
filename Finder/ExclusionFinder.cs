using System.Collections.Generic;
using System.Linq;

using NCommon;

namespace NFinder
{
    public class ExclusionFinder : Finder
    {
        private List<FindableFO> finderOutput;
        private List<ExclusionBlock> excludedBlocks;

        public ExclusionFinder(string fileName) : base(fileName)
        {
            finderOutput = FindAll(Findable.LineBreak);
            finderOutput.AddRange(FindAll(Findable.LineComment));
            finderOutput.AddRange(FindAll(Findable.MultiLineComment_Close));
            finderOutput.AddRange(FindAll(Findable.MultiLineComment_Open));
            finderOutput.AddRange(FindAll(Findable.String));

            finderOutput.Sort((el1, el2) =>
            {
                return el1.Position.CompareTo(el2.Position);
            });
        }

        public List<ExclusionBlock> FindExcludedCode()
        {
            excludedBlocks = new List<ExclusionBlock>();
            while (finderOutput.Count > 0)
            {
                FindableFO element = finderOutput.First();
                finderOutput.Remove(element);

                switch (element.Item)
                {
                    case (Findable.LineComment):
                        ExclusionBlock exclusionScope = new ExclusionBlock();
                        break;
                }
            }

            return excludedBlocks;
        }
    }
}
