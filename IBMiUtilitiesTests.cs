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
            Assert.IsFalse(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = ""; // empty
            Assert.IsFalse(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = "1asd"; // begins with #
            Assert.IsFalse(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = "abcdefghjklmno"; // too long
            Assert.IsFalse(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = "a"; // Valid
            Assert.IsTrue(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = "abc123"; // Valid
            Assert.IsTrue(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = "abc_123"; // Valid
            Assert.IsTrue(IBMiUtilities.IsValidQSYSObjectName(testInput));

            testInput = "abc_123_zz"; // Valid
            Assert.IsTrue(IBMiUtilities.IsValidQSYSObjectName(testInput));
        }


        [TestMethod()]
        public void extractStringTest()
        {
            string testResult = null;
            string testInput = null; // Null
            testResult = IBMiUtilities.ExtractString(testInput, null, "^");
            Assert.IsTrue(testResult == "");

            testInput = ""; // Empty
            testResult = IBMiUtilities.ExtractString(testInput, "123", "^");
            Assert.IsTrue(testResult == "");

            testInput = "dcl-ds "; // search arg too big
            testResult = IBMiUtilities.ExtractString(testInput, "too long search", " like");
            Assert.IsTrue(testResult == "");

            testInput = "dcl-ds myDS likeDS(T_TABLE);"; // Extract part blanks
            testResult = IBMiUtilities.ExtractString(testInput, "dcl-ds ", " like");
            Assert.IsTrue(testResult == "myDS");

            testInput = "dcl-ds myDS EXTNAME('MYTABLE') qualified alias"; // Extract part
            testResult = IBMiUtilities.ExtractString(testInput, "EXTNAME('", "')");
            Assert.IsTrue(testResult == "MYTABLE");

            testInput = "dcl-ds myDS EXTNAME('MYTABLE') qualified alias"; // Extract no limit
            testResult = IBMiUtilities.ExtractString(testInput, "EXTNAME('");
            Assert.IsTrue(testResult == "MYTABLE') qualified alias");

            testInput = "		<column type=\"int(10)\" length=\"4\">AMEOFLONGFIELDTWO</column>"; // Extract no limit
            testResult = IBMiUtilities.ExtractString(testInput, "type=\"", "\"");
            Assert.IsTrue(testResult == "int(10)");
        }
    }
}