using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.utils;

namespace Ocl20Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            string leo = "leo antunes";

            string m1 = leo.Substring(1, leo.Length - 1);
            string m2 = leo.MySubString(1, leo.Length);

            var numbers = new[] { 1, 2, 3, 4 };
            var words = new[] { "one", "two", "three" };

            var numbersAndWords = numbers.Zip(words, (n, w) => new { Number = n, Word = w });
            foreach (var nw in numbersAndWords)
            {
                Console.WriteLine(nw.Number + nw.Word);
            }
        }
    }

    static class Extensions
    {
        public static string MySubString(this string s, int start, int end)
        {
            return s.Substring(start, end - start);
        }
    }
}
