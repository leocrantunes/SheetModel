using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.parser.cst;
using Ocl20.parser.cst.expression;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestEnumLiteralExp : TestLiteralExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testEnumOK_01() {
            doTestEnumOK("Situation", "Situation", "married", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testEnumOK_02() {
            doTestEnumOK("Situation", "Situation", "single", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testEnumOK_03() {
            doTestEnumOK("MyExample::Situation", "Situation", "married", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testEnumError_01() {
            parseWithError("Situation::other", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testEnumError_02() {
            parseWithError("Foo::married", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testEnumError_03() {
            parseWithError("married", this.getCurrentMethodName());
        }

        [TestMethod]
        public void testEnumError_04() {
            parseWithError("Situation::married@pre", this.getCurrentMethodName());
        }

        public void doTestEnumOK(String enumFullName, String enumName, String enumLiteral, String source) {
            CSTNode node = parseOK(enumFullName + "::" +  enumLiteral, source);
            Assert.IsTrue(node is CSTClassifierAttributeCallExpCS);
            CSTClassifierAttributeCallExpCS literalExp = (CSTClassifierAttributeCallExpCS) node;
		
            Assert.IsNotNull(literalExp.getAst());
            Assert.IsTrue(literalExp.getAst() is EnumLiteralExp);
            EnumLiteralExp ast = (EnumLiteralExp) literalExp.getAst();
		
            Assert.AreEqual(enumLiteral, ast.getReferredEnumLiteral().getName());
		
            Assert.IsNotNull(ast.getType());
            Assert.AreEqual(enumName, ast.getType().getName()); 
        }
	
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestLiteralExp#getSpecificNodeClass()
	 */

        protected override Type getSpecificNodeClass()
        {
            return typeof (CSTClassifierAttributeCallExpCS);
        }
    }

}
