using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.parser.cst.context;
using Ocl20.parser.typeChecker;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestCollectionOperationsExp : TestPropertyCallExp  {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testSize_01() {
            List<object> constraints	= doTestContextOK("context Tape inv: self.theFilm->size() > 10",     
                              	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkOperationCallExp(exp.getSource(), "asSet", "Set(Film)");
        }

        [TestMethod]
        public void testSize_02() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->size() > 10",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkAssociationEndCallExp(exp.getSource(), "tapes", "Set(Tape)");
        }

        [TestMethod]
        public void testSize_03() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getTapes()->size() > 10",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkOperationCallExp(exp.getSource(), "getTapes", "Set(Tape)");
        }

        [TestMethod]
        public void testSize_04() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm->size() > 10",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
        }

        [TestMethod]
        public void testSize_04A() {
            List<object> constraints	= doTestContextOK("context Film inv: (self.getTapes().theFilm)->size() > 10",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
        }

        [TestMethod]
        public void testSize_04B() {
            List<object> constraints	= doTestContextOK("context Film inv: (self.getTapes()).theFilm->size() > 10",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
        }

        [TestMethod]
        public void testSize_05() {
            List<object> constraints	= doTestContextOK("context Film inv: Set{1, 2}->size() > 10",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
            checkCollectionLiteralExp(exp.getSource(), "Set(Integer)");
        }

        [TestMethod]
        public void testSize_06() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes[1]->isEmpty()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
            checkOperationCallExp(exp.getSource(), "asSet", "Set(Tape)");
        }

        [TestMethod]
        public void testSize_07() {
            List<object> constraints	= doTestContextOK("context Film inv: self.name->isEmpty()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
            checkOperationCallExp(exp.getSource(), "asSet", "Set(String)");
        }

        [TestMethod]
        public void testSize_08() {
            List<object> constraints	= doTestContextOK("context Film inv: self.name->asOrderedSet()->isEmpty()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
            checkOperationCallExp(exp.getSource(), "asOrderedSet", "OrderedSet(String)");
        }

        [TestMethod]
        public void testProduct_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->product(self.Reservation)->product(self.tapes)->isEmpty()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
            Console.WriteLine("type = " + exp.getSource().getType().getName());
            checkOperationCallExp(exp.getSource(), "product", "Set(Tuple(first : Tuple(first : Tape, second : Reservation), second : Tape))");
        }

        [TestMethod]
        public void testProduct_02() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->product(self.Reservation)->isEmpty()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
            Console.WriteLine("type = " + exp.getSource().getType().getName());
            checkOperationCallExp(exp.getSource(), "product", "Set(Tuple(first : Tape, second : Reservation))");
        }

        [TestMethod]
        public void testProduct_03() {
            List<object> constraints	= doTestContextOK("context Film inv: self.Reservation->product(self.tapes)->isEmpty()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp finalExp = (OperationCallExp) oclExpression;
            OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
            checkOperationCallExp(exp.getSource(), "product", "Set(Tuple(first : Reservation, second : Tape))");
        }

        [TestMethod]
        public void testNotDefinedOperation_01() {
            doTestContextNotOK("context Film inv: self.getTapes()->foo()", getCurrentMethodName());	
        }

        [TestMethod]
        public void testNotDefinedOperation_02() {
            doTestContextNotOK("context Film inv: self.getTapes()->size(1) > 0", getCurrentMethodName());	
        }

        [TestMethod]
        public void testNotAOperation_01() {
            doTestContextNotOK("context Tape inv: self.theFilm->abcde", getCurrentMethodName());	
        }

        [TestMethod]
        public void testSelect_08() {
            oclCompiler = new PSWOclCompiler(environment, tracker);
            CSTExpressionInOclCS rootNode = oclCompiler.compileOclExpression("Film::allInstances()->select(name.toInteger() > 10)",     
            getCurrentMethodName(), new StreamWriter(Console.OpenStandardOutput()));
            source = getCurrentMethodName();
            Assert.IsNotNull(rootNode);
            Assert.AreEqual("Set(Film)", rootNode.getAst().getBodyExpression().getType().getName());
        }

    }
}
