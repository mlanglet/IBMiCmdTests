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
    public class RPGAutoCompleterTests
    {
        [TestMethod()]
        public void MatchLineTest()
        {
            List<DataStructure> testData = new List<DataStructure>();

            List<string> MyList = new List<string>();
            List<string> MyList1 = new List<string>();
            List<string> MyList2 = new List<string>();
            List<string> MyList3 = new List<string>();
            List<string> MyList4 = new List<string>();
            List<string> MyList5 = new List<string>();

            MyList.Add("abc"); MyList.Add("cde"); MyList.Add("efg");
            testData.Add(new DataStructure()
            {
                name = "match1",
                fields = MyList,
                dataStructures = new List<DataStructure>()
            });

            MyList1.Add("abc"); MyList.Add("cde"); MyList.Add("efg");
            testData.Add(new DataStructure()
            {
                name = "nomatch",
                fields = MyList1,
                dataStructures = new List<DataStructure>()
            });

            MyList2.Add("abc"); MyList.Add("cde"); MyList.Add("efg");
            DataStructure nested = new DataStructure()
            {
                name = "nested",
                fields = MyList2,
                dataStructures = new List<DataStructure>()
            };

            MyList3.Add("matchingthings"); MyList.Add("is"); MyList.Add("easy");
            nested.dataStructures.Add(new DataStructure()
            {
                name = "matchinner",
                fields = MyList3,
                dataStructures = new List<DataStructure>()
            });

            MyList5.Add("abc"); MyList.Add("matchInnerInner"); MyList.Add("efg");
            DataStructure nested2 = new DataStructure()
            {
                name = "matchnested",
                fields = MyList5,
                dataStructures = new List<DataStructure>()
            };

            MyList4.Add("mat"); MyList.Add("cde"); MyList.Add("efg");


            DataStructure nested1 = new DataStructure()
            {
                name = "nomatchinner",
                fields = MyList4,
                dataStructures = new List<DataStructure>()
            };

            nested1.dataStructures.Add(nested2);
            nested.dataStructures.Add(nested1);

            testData.Add(nested);

            RPGParser.dataStructures = testData;

            List<string> result = RPGAutoCompleter.MatchLine("match");
            Assert.IsTrue(result.Count == 5);
            Assert.IsTrue(result[0] == "match1");
            Assert.IsTrue(result[1] == "matchInnerInner");
            Assert.IsTrue(result[2] == "matchinner");
            Assert.IsTrue(result[3] == "matchingthings");
            Assert.IsTrue(result[4] == "matchnested");
            
        }
    }
}