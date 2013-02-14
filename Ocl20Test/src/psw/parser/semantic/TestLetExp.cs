using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.constraints;
using Ocl20.parser.cst.context;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestLetExp : TestPropertyCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testLetOK() {
            List<object> constraints = doTestContextOK("context Film inv: let x : Integer = 20 in x > 10 endif",     
                              	                  getCurrentMethodName());
	
            foreach (CSTInvariantCS constraint in constraints) {
                CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
                OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
                Assert.IsTrue(oclExpression is LetExp);
                LetExp exp = (LetExp) oclExpression;
			
                Assert.AreEqual("Boolean", exp.getType().getName());
            }
        }

        [TestMethod]
        public void testLetOK_02() {
            List<object> constraints	= doTestContextOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in x > 10 and y < 30.2 endif",     
                            	                  getCurrentMethodName());
	
            foreach (CSTInvariantCS constraint in constraints) {
                CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
                OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
                Assert.IsTrue(oclExpression is LetExp);
                LetExp exp = (LetExp) oclExpression;
			
                Assert.AreEqual("Boolean", exp.getType().getName());
            }
        }

        [TestMethod]
        public void testLetOK_03() {
            List<object> constraints	= doTestContextOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in x > 0 ",     
                            	                  getCurrentMethodName());
	
            foreach (CSTInvariantCS constraint in constraints) {
                CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
                OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
                Assert.IsTrue(oclExpression is LetExp);
                LetExp exp = (LetExp) oclExpression;
			
                Assert.AreEqual("Boolean", exp.getType().getName());
            }
        }

        [TestMethod]
        public void testLetOK_04() {
            List<object> constraints	= doTestContextOK("context Film inv: if let x : Integer = 20 in x > 0 then let a : Integer = 10 in a + 20 < 4 else let b : Integer = 20 in b * 10 > 5 endif",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testLetOK_05() {
            doTestContextOK("context Film inv: if let x : Integer = 20 in x > 0 then let x : Integer = 10 in x + 20 < 4 else let b : Integer = 20 in b * 10 > 5 endif",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testLetOK_06() {
            doTestContextOK("context SpecialFilm def: xpto : Integer = self.doSomething(let a : Integer = 10 in a + 10, let b : Integer = 20 in b * 20, 40.42)",  
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testLetOK_07() {
            doTestContextOK("context SpecialFilm inv:  let a : Integer = 10 in self.tapes->forAll(t | let b : Integer = 30 in t.number > (let c : Integer = 40 in a + b + c))",  
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testLetOK_08() {
            doTestContextOK("context SpecialFilm inv:  let a : Integer = 10 in let b : Integer = 20 in a + b > 20",  
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testLetOK_09() {
            doTestManyContextNotOK("context SpecialFilm inv:  let a : Integer = 10, b : Integer = a + 10, c : Integer = b + a in a + b > 20   inv : let x : Integer = a in x + 10 > 20",  
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testLetNotOK_01() {
            doTestContextNotOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in z > 10 and y < 30.2 endif",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testLetNotOK_02() {
            doTestContextNotOK("context Film inv: let x = 20, y : Real = 40.10 in x > 10 and y < 30.2 endif",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testLetNotOK_03() {
            doTestContextNotOK("context Film inv: let x : Integer = 20, y : Real = 40.10, x : Boolean = true in z > 10 and y < 30.2 endif",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testLetNotOK_04() {
            doTestContextNotOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in let x : Boolean = true in z > 10 and y < 30.2 endif",     
                               getCurrentMethodName());
        }
    }
}
