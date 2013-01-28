using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestIterateExp : TestPropertyCallExp  {

        protected IterateExp checkIterateExp(OclExpression oclExpression, String typeName, String resultName, String iteratorName, String iteratorType, String bodyType, int iteratorsQty) {
            Assert.IsTrue(oclExpression is IterateExp);
            IterateExp exp = (IterateExp) oclExpression;
            Assert.AreEqual(typeName, exp.getType().getName());
            Assert.AreEqual("iterate", exp.getName());
            Assert.AreEqual(iteratorsQty, exp.getIterators().Count);
            if (iteratorName == null)
                iteratorName = "iterator";
            VariableDeclaration varDecl = (VariableDeclaration) exp.getIterators()[0];
            Assert.AreEqual(iteratorName, varDecl.getName());
            Assert.AreEqual(iteratorType, varDecl.getType().getName());
            Assert.AreEqual(resultName, ((VariableDeclaration) exp.getResult()).getName());
            Assert.AreEqual(typeName, ((VariableDeclaration) exp.getResult()).getType().getName());

            Assert.AreEqual(bodyType, exp.getBody().getType().getName());
		
            return	exp;
        }

        [TestMethod]
        public void testIterate_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->iterate(total : Integer = 0 | total + number) > 5",     
                              	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            Assert.IsTrue(oclExpression is OperationCallExp);
		
            OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
            checkIterateExp(opCallExp.getSource(), "Integer",  "total", null, "Tape", "Integer", 1);
        }

        [TestMethod]
        public void testIterate_02() {
            List<object> constraints = doTestContextOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer = 0 | total + x.number) > 5",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            Assert.IsTrue(oclExpression is OperationCallExp);
		
            OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
            checkIterateExp(opCallExp.getSource(), "Integer",  "total", "x", "Tape", "Integer", 1);
        }

        [TestMethod]
        public void testIterate_03() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->iterate(x : Tape; result : Set(Tape) = Set{} | if x.number > 5 then result->including(x) else result endif)->size() > 5",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            Assert.IsTrue(oclExpression is OperationCallExp);
		
            OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
            checkIterateExp( ((OperationCallExp)(opCallExp.getSource())).getSource(), "Set(Tape)",  "result", "x", "Tape", "Set(Tape)", 1);
        }

        [TestMethod]
        public void testIterateWithError_03() {
            List<object> constraints = doTestContextOK("context Film inv: self.tapes->iterate(x : Tape, y : Tape; total : Integer = 0 | total + x.number) > 5",     
                                               getCurrentMethodName());
				
            OclExpression oclExpression = getConstraintExpression(constraints);
            Assert.IsTrue(oclExpression is OperationCallExp);
		
            OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
            checkIterateExp(opCallExp.getSource(), "Integer",  "total", "x", "Tape", "Integer", 2);

        }

        [TestMethod]
        public void testIterateWithError_04() {
            doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer | total + x.number) > 5",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIterateWithError_05() {
            doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total = 10 | total + x.number) > 5",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIterateWithError_06() {
            doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer = 10 | total + x.theFilm.name) > 5",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIterateWithError_07() {
            doTestContextNotOK("context Film inv: self.tapes->iterate(x : Integer = 5; total : Integer = 10 | total + x.theFilm.name) > 5",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIterateWithError_08() {
            doTestContextNotOK("context Film inv: self.tapes->iterate(x : Integer; total : Integer = 10 | total + x.theFilm.name) > 5",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testIterateWithError_09() {
            doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer = 10 | x.theFilm) > 5",     
                               getCurrentMethodName());
        }
    }
}
