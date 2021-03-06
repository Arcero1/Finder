﻿using System;
using System.Collections.Generic;

using NFinder;
using NCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeleterTest
{
    static class TestFile
    {
        public static readonly string scopeFinder = "../../ScopeFinderTestFile.txt";
        public static readonly string exclusionFinder = "../../ExclusionFinderTestFile.txt";
    }

    [TestClass]
    public class ScopeFinderTest
    {
        Finder finder = new Finder(TestFile.scopeFinder);
        ScopeFinder scopeFinder = new ScopeFinder(TestFile.scopeFinder);

        [TestMethod]
        public void TestFindsGlobalScope()
        {
            ScopeBlock scope = scopeFinder.FindScope(finder.Find("A"));
            Console.WriteLine(scope);
            Assert.AreEqual(ScopeBlock.Scope.Global, scope.scope, "Scope should be GLOBAL");
        }

        [TestMethod]
        public void TestFindsSingleLineScope()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.end.Line      = ToCalculable(3);
            expectedScope.start.Line    = ToCalculable(3);
            expectedScope.start.Column  = ToCalculable(1);
            expectedScope.end.Column    = ToCalculable(3);

            expectedScope.scope = ScopeBlock.Scope.Other;

            ScopeBlock scope = scopeFinder.FindScope(finder.Find("B"));
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestFindsMultiLineScope()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.Line = ToCalculable(5);
            expectedScope.start.Column = ToCalculable(1);

            expectedScope.end.Line = ToCalculable(7);
            expectedScope.end.Column = ToCalculable(1);

            expectedScope.scope = ScopeBlock.Scope.Other;

            ScopeBlock scope = scopeFinder.FindScope(finder.Find("C"));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestIgnoresTrailingLowerScope()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.Line = ToCalculable(9);
            expectedScope.start.Column = ToCalculable(1);

            expectedScope.end.Line = ToCalculable(9);
            expectedScope.end.Column = ToCalculable(5);

            expectedScope.scope = ScopeBlock.Scope.Other;

            ScopeBlock scope = scopeFinder.FindScope(finder.Find("D"));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestIgnoresMultipleTrailingLowerScope()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.Line = ToCalculable(13);
            expectedScope.start.Column = ToCalculable(1);

            expectedScope.end.Line = ToCalculable(13);
            expectedScope.end.Column = ToCalculable(9);

            expectedScope.scope = ScopeBlock.Scope.Other;

            ScopeBlock scope = scopeFinder.FindScope(finder.Find("F"));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestIgnoresLeadingLowerScope()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.Line = ToCalculable(11);
            expectedScope.start.Column = ToCalculable(1);

            expectedScope.end.Line = ToCalculable(11);
            expectedScope.end.Column = ToCalculable(5);

            expectedScope.scope = ScopeBlock.Scope.Other;
            

            ScopeBlock scope = scopeFinder.FindScope(finder.Find("E"));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestFindMidFileGlobalScope()
        {
            ScopeBlock scope = scopeFinder.FindScope(finder.Find("G"));
            Console.WriteLine(scope);
            Assert.AreEqual(ScopeBlock.Scope.Global, scope.scope, "Scope should be GLOBAL");
        }

        [TestMethod]
        public void TestFreeform1()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.Line = ToCalculable(22);
            expectedScope.start.Column = ToCalculable(18);

            expectedScope.end.Line = ToCalculable(22);
            expectedScope.end.Column = ToCalculable(31);

            expectedScope.scope = ScopeBlock.Scope.Other;


            ScopeBlock scope = scopeFinder.FindScope(finder.Find("\"Exemplar\""));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestFreeform2()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.Line = ToCalculable(21);
            expectedScope.start.Column = ToCalculable(1);

            expectedScope.end.Line = ToCalculable(29);
            expectedScope.end.Column = ToCalculable(1);

            expectedScope.scope = ScopeBlock.Scope.Other;


            ScopeBlock scope = scopeFinder.FindScope(finder.Find("s.clear();"));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }

        [TestMethod]
        public void TestFreeform3()
        {
            ScopeBlock scope = scopeFinder.FindScope(finder.Find("#include <string>"));
            Console.WriteLine(scope);
            Assert.AreEqual(ScopeBlock.Scope.Global, scope.scope, "Scope should be GLOBAL");
        }
        int ToVisible(int i)
        {
            return i + 1;
        }

        int ToCalculable(int i)
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.line = ToCalculable(31);
            expectedScope.start.column = ToCalculable(1);

            expectedScope.end.line = ToCalculable(32);
            expectedScope.end.column = ToCalculable(1);

        [TestMethod]
        public void TestIgnoresLineComments()
        {
            ScopeBlock expectedScope = new ScopeBlock();
            expectedScope.start.line = ToCalculable(31);
            expectedScope.start.column = ToCalculable(1);

            expectedScope.end.line = ToCalculable(32);
            expectedScope.end.column = ToCalculable(1);

            expectedScope.scope = ScopeBlock.Scope.Other;


            ScopeBlock scope = scopeFinder.FindScope(finder.Find("H"));
            Console.WriteLine(scope);
            Assert.AreEqual(expectedScope, scope);
        }
        int ToVisible(int i)
        {
            return i + 1;
        }

        int ToCalculable(int i)
        {
            return i - 1;
        }
    }

    [TestClass]
    public class FinderTest
    {
        Finder scopeFinder = new Finder("../../ScopeFinderTestFile.txt");
        enum Brace
        {
            Opening = '{',
            Closing = '}'
        }

        [TestMethod]
        public void TestFinds()
        {
        Console.WriteLine((char)Brace.Opening);
        }

        [TestMethod]
        public void TestFindsX()
        {
            Position p;
            p = scopeFinder.Find("A");
            Console.WriteLine(p);
        }
    }
}
