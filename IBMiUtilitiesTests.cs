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
    public class IBMiUtilitiesTests
    {
        [TestMethod()]
        public void isValidQSYSObjectNameTest()
        {
            string testInput = null; // null
            Assert.IsFalse(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = ""; // empty
            Assert.IsFalse(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = "1asd"; // begins with #
            Assert.IsFalse(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = "abcdefghjklmno"; // too long
            Assert.IsFalse(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = "a"; // Valid
            Assert.IsTrue(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = "abc123"; // Valid
            Assert.IsTrue(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = "abc_123"; // Valid
            Assert.IsTrue(IBMiUtilities.isValidQSYSObjectName(testInput));

            testInput = "abc_123_zz"; // Valid
            Assert.IsTrue(IBMiUtilities.isValidQSYSObjectName(testInput));
        }


        [TestMethod()]
        public void extractStringTest()
        {
            string testResult = null;
            string testInput = null; // Null
            testResult = IBMiUtilities.extractString(testInput, null, "^");
            Assert.IsTrue(testResult == "");

            testInput = ""; // Empty
            testResult = IBMiUtilities.extractString(testInput, "123", "^");
            Assert.IsTrue(testResult == "");

            testInput = "dcl-ds "; // search arg too big
            testResult = IBMiUtilities.extractString(testInput, "too long search", " like");
            Assert.IsTrue(testResult == "");

            testInput = "dcl-ds myDS likeDS(T_TABLE);"; // Extract part blanks
            testResult = IBMiUtilities.extractString(testInput, "dcl-ds ", " like");
            Assert.IsTrue(testResult == "myDS");

            testInput = "dcl-ds myDS EXTNAME('MYTABLE') qualified alias"; // Extract part
            testResult = IBMiUtilities.extractString(testInput, "EXTNAME('", "')");
            Assert.IsTrue(testResult == "MYTABLE");

            testInput = "dcl-ds myDS EXTNAME('MYTABLE') qualified alias"; // Extract no limit
            testResult = IBMiUtilities.extractString(testInput, "EXTNAME('");
            Assert.IsTrue(testResult == "MYTABLE') qualified alias");
        }
    }
}