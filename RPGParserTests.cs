using Microsoft.VisualStudio.TestTools.UnitTesting;
using IBMiCmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMiCmd.Tests
{
    [TestClass()]
    public class RPGParserTests
    {
        [TestMethod()]
        public void parseCurrentFileForExtNameTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GetVariableAtColumnTest()
        {
            MatchType mt = 0;
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" myVar = 510", 4, out mt) == "myV");
            Assert.IsTrue(mt == MatchType.VARIABLE);
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" myVariable_that_isLong", 23, out mt) == "myVariable_that_isLong");
            Assert.IsTrue(mt == MatchType.VARIABLE);
            Assert.IsTrue(RPGParser.GetVariableAtColumn("ds.", 3, out mt) == "ds");
            Assert.IsTrue(mt == MatchType.STRUCT);
            Assert.IsTrue(RPGParser.GetVariableAtColumn("nested.ds.", 10, out mt) == "ds");
            Assert.IsTrue(mt == MatchType.STRUCT);
            Assert.IsTrue(RPGParser.GetVariableAtColumn("  if myVar = 510;", 11, out mt) == "");
            Assert.IsTrue(mt == MatchType.NONE);
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" myVar = 510", 10, out mt) == "");
            Assert.IsTrue(mt == MatchType.NONE);
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" my<Var = 510", 4 , out mt) == "");
            Assert.IsTrue(mt == MatchType.NONE);
        }
    }
}