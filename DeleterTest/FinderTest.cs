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
        public void TestFindsAtStartOfScope()
        {
            Position position = new Position();
            position.Line   = Column.ToCalculable(1);
            position.Column = Column.ToCalculable(1);

            Assert.AreEqual(position, finder.Find("A"));
        }

        [TestMethod]
        public void TestFindsAnywhere()
        {
            Position position = new Position();
            position.Line = Column.ToCalculable(3);
            position.Column = Column.ToCalculable(5);

            Assert.AreEqual(position, finder.Find("B"));
        }

        [TestMethod]
        public void TestFindsInLineWithOtherText()
        {
            Position position = new Position();
            position.Line = Column.ToCalculable(5);
            position.Column = Column.ToCalculable(8);

            Assert.AreEqual(position, finder.Find("C"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestThrowsExceptionWhereNothingFound()
        {
            finder.Find("E");
        }

        [TestMethod]
        public void TestFindsDistinct()
        {
            Position position = new Position();
            position.Line = Column.ToCalculable(7);
            position.Column = Column.ToCalculable(9);

            Assert.AreEqual(position, finder.FindDistinct("D"));
        }

        [TestMethod]
        public void TestFindsMultiple()
        {
        }
    }
}
