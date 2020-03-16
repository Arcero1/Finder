using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NFinder;
using NCommon;

namespace DeleterTest
{
    [TestClass]
    public class FinderTest
    {
        private readonly Finder finder = new Finder("../../FinderTestFile.txt");

        [TestMethod]
        public void TestFindsSingle()
        {
            Position position = new Position();
            position.Line = Position.ToCalculable(22);
            position.Column = Position.ToCalculable(18);

            Assert.AreEqual(position, finder.Find("A"));
        }

        [TestMethod]
        public void TestFindsMultiple()
        {
        }
    }
}
