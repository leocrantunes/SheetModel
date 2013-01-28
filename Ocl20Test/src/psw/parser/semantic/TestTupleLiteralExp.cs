using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.parser.cst;
using Ocl20.parser.cst.literalExp;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestTupleLiteralExp : TestLiteralExp  {

        [TestMethod]
        public void testTupleLiteralWithDuplicateNames_03() {
            parseWithError("Tuple{a:Integer = 10, b : Boolean = true, a: Integer = 9}", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralOK_01() {
            doTestOK("Tuple{a:Integer = 10, b:Boolean = true, c: Real = 9.5}", "Tuple(a : Integer, b : Boolean, c : Real)", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralOK_03() {
            doTestOK("Tuple{a:Integer = 10, s:Set(Integer) = Set {1, 5}}", "Tuple(a : Integer, s : Set(Integer))", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralOK_02() {
            doTestOK("Tuple{a:Integer = 10}", "Tuple(a : Integer)", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithoutType() {
            doTestOK("Tuple{a:Integer = 10, b = true, c: Real = 9.5}", "Tuple(a : Integer, b : Boolean, c : Real)", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testTupleLiteralWithoutType_02() {
            doTestOK("Tuple{a = 10, s = Set {1, 5}}", "Tuple(a : Integer, s : Set(Integer))", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithoutType_03() {
            doTestOK("Tuple{a = 10, s = Set {Set{1, 5}, Bag{4.9, 5.9}}}", "Tuple(a : Integer, s : Set(Collection(Real)))", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithTypeConformanceError() {
            parseWithError("Tuple{a:Integer = 10, b:Boolean = 10, c: Real = 9.5}", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithTypeConformanceError_02() {
            parseWithError("Tuple{a = 10, s : Set(Set(Real)) = Set {Set{1, 5}, Bag{4.9, 5.9}}}", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithoutInitialization() {
            parseWithError("Tuple{a:Integer = 10, b:Boolean, c: Real = 9.5}", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithDuplicateNames() {
            parseWithError("Tuple{a:Integer = 10, a : Boolean = true, c: Real = 9.5}", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithDuplicateNames_02() {
            parseWithError("Tuple{a:Integer = 10, b : Boolean = true, a: Real = 9.5}", this.getCurrentMethodName());	
        }

        [TestMethod]
        public void testTupleLiteralWithoutTypeAndInitialization() {
            parseWithError("Tuple{a:Integer = 10, d, c: Real = 9.5}", this.getCurrentMethodName());	
        }

        public TupleLiteralExp doTestOK(String literal, String typeName, String callerMethodName) {
            CSTNode node = parseOK(literal, callerMethodName);
            Assert.IsTrue(node is CSTTupleLiteralExpCS);
            CSTTupleLiteralExpCS literalExp = (CSTTupleLiteralExpCS) node;
		
            Assert.IsNotNull(literalExp.getAst());
            Assert.IsTrue(literalExp.getAst() is TupleLiteralExp);
            Assert.AreEqual(typeName, literalExp.getAst().getType().getName());
            return (TupleLiteralExp) literalExp.getAst();
        }
    }
}
