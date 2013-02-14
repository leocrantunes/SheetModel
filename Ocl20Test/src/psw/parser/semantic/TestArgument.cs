using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.parser.cst;
using Ocl20.parser.cst.expression;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestArgument : TestNodeCS {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testArgument_01() {
            doTestOK("100", "Integer", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testArgument_02() {
            doTestOK("Set{1, 3, 5}", "Set(Integer)", this.getCurrentMethodName());
        }
	
        [TestMethod]
        public void testArgument_03() {
            CoreClassifier c = doTestOK("Film", "OclModelElementType", this.getCurrentMethodName());
            checkReferredOclType(c, "Film");
        }

        [TestMethod]
        public void testArgument_04() {
            CoreClassifier c = doTestOK("MyExample::Film", "OclModelElementType", this.getCurrentMethodName());
            checkReferredOclType(c, "Film");
        }

        [TestMethod]
        public void testArgument_05() {
            doTestOK("Situation::married", "Situation", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testArgument_06() {
            doTestOK("Rental::maxDaysToReturn", "Integer", this.getCurrentMethodName());
        }
    
        protected CoreClassifier doTestOK(String expression, String expectedTypeName, String testName)  {
            CSTNode rootNode = parseOK(expression, testName);
            return checkResult(rootNode, expectedTypeName);
        }

        protected override Type getSpecificNodeClass() {
            return typeof(CSTArgumentCS);
        }
    
        private	CoreClassifier checkResult(CSTNode rootNode, String expectedTypeName) {
            CSTArgumentCS argument = (CSTArgumentCS) rootNode;
            OclExpression ast = argument.getAst();
            Assert.AreEqual(expectedTypeName, ast.getType().getName());
            return	ast.getType();
        }

        private	void checkReferredOclType(Object c, String expectedClassifierName) {
            Assert.IsTrue(c is OclModelElementType);
            OclModelElementType ast = (OclModelElementType) c;
            Assert.AreEqual(expectedClassifierName, ast.getReferredModelElement().getName());
        }
    }
}
