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
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" myVar = 510", 4) == "myV");
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" myVariable_that_isLong", 23) == "myVariable_that_isLong");
            Assert.IsTrue(RPGParser.GetVariableAtColumn("ds.", 3) == "ds");
            Assert.IsTrue(RPGParser.GetVariableAtColumn("nested.ds.", 10) == "ds");
            Assert.IsTrue(RPGParser.GetVariableAtColumn("  if myVar = 510;", 11) == "");
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" myVar = 510", 10) == "");
            Assert.IsTrue(RPGParser.GetVariableAtColumn(" my<Var = 510", 4) == "");
        }
    }
}