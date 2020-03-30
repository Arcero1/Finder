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
            _finderOutput = FindAll(Findable.EscapeCharacter);
            _finderOutput.AddRange(FindAll(Findable.LineComment));
            _finderOutput.AddRange(FindAll(Findable.MultiLineComment_Close));
            _finderOutput.AddRange(FindAll(Findable.MultiLineComment_Open));
            _finderOutput.AddRange(FindAll(Findable.String));
            _finderOutput.AddRange(FindAll(Findable.Char));

            _finderOutput.Sort((el1, el2) =>
            {
                return el1.Position.CompareTo(el2.Position);
            });
        }

        public List<ExclusionBlock> FindExcludedCode()
        {
            _excludedBlocks = new List<ExclusionBlock>();
            while (_finderOutput.Count > 0)
            {
                FindableFO element = finderOutput.First();
                finderOutput.Remove(element);

                FinderOutputFindable endElement;
                ExclusionBlock exclusionScope = new ExclusionBlock();
                switch (element.Item)
                {
                    case (Findable.LineComment):
                        exclusionScope.SetAsLineType(element.Position);
                        _finderOutput.RemoveAll(el => 
                        {
                            return el.Position.line == element.Position.line;
                        });
                        break;
                    case (Findable.MultiLineComment_Open):
                    case (Findable.String):
                    case (Findable.Char):
                        endElement = _finderOutput.First(item =>
                        {
                            return item.Item == element.GetMatch();
                        });
                        exclusionScope.start = element.Position;
                        exclusionScope.end = endElement.Position;

                        _finderOutput.RemoveAll(el =>
                        {
                            return el.Position <= endElement.Position;
                        });
                        break;
                }
                _excludedBlocks.Add(exclusionScope);
            }
            return _excludedBlocks;
        }
    }
}
