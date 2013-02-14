using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.parser.cst;
using Ocl20.parser.cst.literalExp;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestPrimitiveLiteralExp : TestLiteralExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testBooleanExpOK_01() {
            doTestBoolean(true);
        }

        [TestMethod]
        public void testBooleanExpOK_02() {
            doTestBoolean(false);
        }

        [TestMethod]
        public void testIntegerExpOK_01() {
            doTestInteger(3000);
        }

        [TestMethod]
        public void testIntegerExpOK_02() {
            doTestInteger(0);
        }

        [TestMethod]
        public void testRealExpOK_01() {
            doTestReal("100.0", 100.0);
        }

        [TestMethod]
        public void testRealExpOK_02() {
            doTestReal("100.0E2", 10000.0);
        }

        [TestMethod]
        public void testRealExpOK_03() {
            doTestReal("100.0e2", 10000.0);
        }

        [TestMethod]
        public void testRealExpOK_04() {
            doTestReal("100.0e+2", 10000.0);
        }

        [TestMethod]
        public void testRealExpOK_05() {
            doTestReal("100.0e-2", 1.0);
        }

        [TestMethod]
        public void testRealExpOK_06() {
            doTestReal("100e2", 10000.0);
        }

        [TestMethod]
        public void testStringExpOK_01() {
            doTestString("\"alex\"", "alex");
        }

        [TestMethod]
        public void testStringExpOK_02() {
            doTestString("\"\"", "");
        }

        private void doTestBoolean(bool expectedValue) {
            CSTNode node = parseOK (expectedValue.ToString().ToLower(), this.getCurrentMethodName());
            Assert.IsTrue(node is CSTBooleanLiteralExpCS);
            CSTBooleanLiteralExpCS literalExp = (CSTBooleanLiteralExpCS) node;
		
            Assert.IsNotNull(literalExp.getAst());
            Assert.IsTrue(literalExp.getAst() is BooleanLiteralExp);
            BooleanLiteralExp ast = (BooleanLiteralExp) literalExp.getAst();
            Assert.AreEqual(expectedValue, ast.isBooleanSymbol());
            CoreClassifier type = ast.getType();
            Assert.IsNotNull(type);
            Assert.AreEqual("Boolean", type.getName()); 
        }

        private void doTestInteger(int expectedValue) {
            CSTNode node = parseOK (expectedValue.ToString(CultureInfo.InvariantCulture), this.getCurrentMethodName());
            Assert.IsTrue(node is CSTIntegerLiteralExpCS);
            CSTIntegerLiteralExpCS literalExp = (CSTIntegerLiteralExpCS) node;
            Assert.IsNotNull(literalExp.getAst());
            Assert.IsTrue(literalExp.getAst() is IntegerLiteralExp);
            IntegerLiteralExp ast = (IntegerLiteralExp) literalExp.getAst();
            Assert.AreEqual(expectedValue, ast.getIntegerSymbol());
            CoreClassifier type = ast.getType();
            Assert.IsNotNull(type);
            Assert.AreEqual("Integer", type.getName()); 
        }

        private void doTestReal(String expectedValueStr, double expectedValue) {
            CSTNode node = parseOK (expectedValueStr, this.getCurrentMethodName());
            Assert.IsTrue(node is CSTRealLiteralExpCS);
            CSTRealLiteralExpCS literalExp = (CSTRealLiteralExpCS) node;
            Assert.IsNotNull(literalExp.getAst());
            Assert.IsTrue(literalExp.getAst() is RealLiteralExp);
            RealLiteralExp ast = (RealLiteralExp) literalExp.getAst();
            Assert.AreEqual(expectedValue, Double.Parse(ast.getRealSymbol(), CultureInfo.InvariantCulture));
            CoreClassifier type = ast.getType();
            Assert.IsNotNull(type);
            Assert.AreEqual("Real", type.getName()); 
        }

        private void doTestString(String input, String expectedValue) {
            CSTNode node = parseOK (input, this.getCurrentMethodName());
            Assert.IsTrue(node is CSTStringLiteralExpCS);
            CSTStringLiteralExpCS literalExp = (CSTStringLiteralExpCS) node;
		
            Assert.IsNotNull(literalExp.getAst());
            Assert.IsTrue(literalExp.getAst() is StringLiteralExp);
            StringLiteralExp ast = (StringLiteralExp) literalExp.getAst();
            Assert.AreEqual(expectedValue, ast.getStringSymbol());
            CoreClassifier type = ast.getType();
            Assert.IsNotNull(type);
            Assert.AreEqual("String", type.getName()); 
        }
    }
}
