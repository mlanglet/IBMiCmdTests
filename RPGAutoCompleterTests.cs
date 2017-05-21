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

            List<DataColumn> MyList  = new List<DataColumn>();
            List<DataColumn> MyList1 = new List<DataColumn>();
            List<DataColumn> MyList2 = new List<DataColumn>();
            List<DataColumn> MyList3 = new List<DataColumn>();
            List<DataColumn> MyList4 = new List<DataColumn>();
            List<DataColumn> MyList5 = new List<DataColumn>();

            MyList.Add(new DataColumn() {
                name = "abc"
            });
            MyList.Add(new DataColumn()
            {
                name = "cde"
            });
            MyList.Add(new DataColumn()
            {
                name = "efg"
            }); 
            testData.Add(new DataStructure()
            {
                name = "match1",
                fields = MyList,
                dataStructures = new List<DataStructure>()
            });

            MyList1.Add(new DataColumn()
            {
                name = "abc"
            });
            MyList1.Add(new DataColumn()
            {
                name = "cde"
            });
            MyList1.Add(new DataColumn()
            {
                name = "efg"
            });
            testData.Add(new DataStructure()
            {
                name = "nomatch",
                fields = MyList1,
                dataStructures = new List<DataStructure>()
            });

            MyList2.Add(new DataColumn()
            {
                name = "abc"
            });
            MyList2.Add(new DataColumn()
            {
                name = "cde"
            });
            MyList2.Add(new DataColumn()
            {
                name = "efg"
            });
            DataStructure nested = new DataStructure()
            {
                name = "nested",
                fields = MyList2,
                dataStructures = new List<DataStructure>()
            };

            MyList3.Add(new DataColumn()
            {
                name = "matchingthings"
            });
            MyList3.Add(new DataColumn()
            {
                name = "cde"
            });
            MyList3.Add(new DataColumn()
            {
                name = "efg"
            });
            nested.dataStructures.Add(new DataStructure()
            {
                name = "matchinner",
                fields = MyList3,
                dataStructures = new List<DataStructure>()
            });

            MyList5.Add(new DataColumn()
            {
                name = "abc"
            });
            MyList5.Add(new DataColumn()
            {
                name = "matchInnerInner"
            });
            MyList5.Add(new DataColumn()
            {
                name = "efg"
            });
            DataStructure nested2 = new DataStructure()
            {
                name = "matchnested",
                fields = MyList5,
                dataStructures = new List<DataStructure>()
            };

            MyList4.Add(new DataColumn()
            {
                name = "mat"
            });
            MyList4.Add(new DataColumn()
            {
                name = "cde"
            });
            MyList4.Add(new DataColumn()
            {
                name = "efg"
            });

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
            Assert.IsTrue(result.Contains("match1"));
            Assert.IsTrue(result.Contains("matchInnerInner"));
            Assert.IsTrue(result.Contains("matchinner"));
            Assert.IsTrue(result.Contains("matchingthings"));
            Assert.IsTrue(result.Contains("matchnested"));
            
        }
    }
}