using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.constraints;
using Ocl20.parser.cst.context;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestIfExp : TestPropertyCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testIfOK() {
            List<object> constraints = doTestContextOK("context Film inv: if 20 > 10 then self.name = self.name else self.name.concat(\"Alex\") = self.name endif",     
                              	                  getCurrentMethodName());
	
            foreach (CSTInvariantCS constraint in constraints) {
                CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
                OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
                Assert.IsTrue(oclExpression is IfExp);
                IfExp exp = (IfExp) oclExpression;
			
                Assert.AreEqual("Boolean", exp.getType().getName());
			
                Assert.IsTrue(((OperationCallExp) exp.getThenExpression()).getSource() is AttributeCallExp);
                Assert.IsTrue(((OperationCallExp) exp.getElseExpression()).getSource() is OperationCallExp);
            }
        }

        [TestMethod]
        public void testIfOK_02() {
            List<object> constraints	= doTestContextOK("context Film inv: let a : OclAny = if 20 > 10 then 40 else true endif in a = 60.0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testIfError_01() {
            doTestContextNotOK("context Film inv: if 20 > 10 then 10 else self.name.concat(\"Alex\") endif",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIfError_02() {
            doTestContextNotOK("context Film inv: if  10 then 10 else self.name.concat(\"Alex\") endif",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIfError_03() {
            doTestContextNotOK("context Film inv: if 20 > 10 then 10 endif",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIfError_04() {
            doTestContextNotOK("context Film inv: if 20 > 10 then 10 else 20",     
                               getCurrentMethodName());
        }
    }
}
