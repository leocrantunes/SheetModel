using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestOperationCallExp : TestPropertyCallExp {

        protected void checkOperationCallType(OclExpression oclExpression, String typeName, String operationName, Type sourceClass, String sourceType, Object[] argTypes) {
            Assert.IsTrue(oclExpression is OperationCallExp);
            OperationCallExp exp = (OperationCallExp) oclExpression;
			
            Assert.AreEqual(typeName, exp.getType().getName());
            Assert.IsTrue(exp.getReferredOperation().operationNameMatches(operationName));
		
            if (sourceClass != null) {
                Assert.IsTrue(sourceClass.IsInstanceOfType(exp.getSource()));
                Assert.AreEqual(exp.getSource().getType().getName(), sourceType);
            }
            else {
                Assert.IsFalse(exp.getReferredOperation().isInstanceScope());	
            }
            if (argTypes == null)
                Assert.AreEqual(0, exp.getArguments().Count);
            else
                Assert.AreEqual(argTypes.Length, exp.getArguments().Count);	
        }

        [TestMethod]
        public void testOperationCall_01() {
            List<object> constraints	= doTestContextOK("context Film inv: getRentalFee(1) = getRentalFee(1)",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
            checkImplicitSource(exp, "self", "Film");
        }

        [TestMethod]
        public void testOperationCall_02() {
            doTestContextNotOK("context Film inv: setDaysForReturn(1)",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testOperationCall_03() {
            List<object> constraints	= doTestContextOK("context SpecialFilm inv: getRentalFee(1) = getRentalFee(1)",     
                            	                  getCurrentMethodName());
				
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
            checkImplicitSource(exp, "self", "SpecialFilm");
        }

        [TestMethod]
        public void testOperationCall_04() {
            List<object> constraints	= doTestContextOK("context SpecialFilm inv: getTapes() = getTapes()",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getTapes", "Set(Tape)");
            checkImplicitSource(exp, "self", "SpecialFilm");
        }

        [TestMethod]
        public void testOperationCall_05() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getRentalFee(1) = self.getRentalFee(1)",     
                            	                  getCurrentMethodName());
				
            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
            checkImplicitSource(exp, "self", "Film");
        }

        [TestMethod]
        public void testOperationCall_06() {
            List<object> constraints	= doTestContextOK("context Tape inv: self.theFilm.getRentalFee(1) = self.theFilm.getRentalFee(1)",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
            checkAssociationEndCallExp(exp.getSource(), "theFilm", "Film");
        }

        [TestMethod]
        public void testOperationCall_07() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getTapes().number = self.getTapes().number",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Bag(Integer)", "collect", "Tape", "iterator");
            checkOperationCallExp(exp.getSource(), "getTapes", "Set(Tape)");
            checkAttributeCallExp(exp.getBody(), "number", "Integer");
            checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
        }

        [TestMethod]
        public void testOperationCall_08() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm = self.getTapes().theFilm",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            IteratorExp exp = checkIteratorExp(oclExpression, "Bag(Film)", "collect", "Tape", "iterator");
            checkOperationCallExp(exp.getSource(), "getTapes", "Set(Tape)");
            checkAssociationEndCallExp(exp.getBody(), "theFilm", "Film");
            checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
	
        }

        [TestMethod]
        public void testOperationCall_085() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm.getTapes() = self.getTapes().theFilm.getTapes()",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkIteratorExp(((IteratorExp)oclExpression).getSource(), "Bag(Film)", "collect", "Tape", "iterator");
            checkIteratorExp(oclExpression, "Bag(Tape)", "collect", "Film", "iterator");
		
        }

        [TestMethod]
        public void testOperationCall_09() {
            List<object> constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm.getTapes().number = self.getTapes().theFilm.getTapes().number",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            IteratorExp exp = checkIteratorExp(oclExpression, "Bag(Integer)", "collect", "Tape", "iterator");
            checkIteratorExp(exp.getSource(), "Bag(Tape)", "collect", "Film", "iterator");
            checkIteratorExp(((IteratorExp)exp.getSource()).getSource(), "Bag(Film)", "collect", "Tape", "iterator");
            checkAttributeCallExp(exp.getBody(), "number", "Integer");
            checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
        }

        [TestMethod]
        public void testOperationCall_10() {
            List<object> constraints	= doTestContextOK("context Film inv: Tape::tapesQty() = Tape::tapesQty()",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(((OperationCallExp) oclExpression).getSource(), "Integer", "tapesQty", null, null, null);
        }

        [TestMethod]
        public void testOperationCall_11() {
            List<object> constraints	= doTestContextOK("context Film inv: self.oclIsKindOf(Film)",     
                            	                  getCurrentMethodName());
					
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "oclIsKindOf", typeof(VariableExp), "Film", new Object[] { "Film" });
        }

        [TestMethod]
        public void testUnaryOperation_01() {
            List<object> constraints	= doTestContextOK("context Film inv: -10 = -10",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Integer", "-", typeof(IntegerLiteralExp), "Integer", null);			
        }

        [TestMethod]
        public void testUnaryOperation_02() {
            List<object> constraints	= doTestContextOK("context Film inv: not self.oclIsKindOf(Film)",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "not", typeof(OperationCallExp), "Boolean", null);			
        }

        [TestMethod]
        public void testMultiplicativeOperation_01() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 * 200 = 500",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Integer", "*", typeof(IntegerLiteralExp), "Integer", new Object[] { "Integer" });			
        }

        [TestMethod]
        public void testMultiplicativeOperation_02() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 * 200.50 = 200.3",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Real", "*", typeof(IntegerLiteralExp), "Integer", new Object[] { "Real" });			
        }

        [TestMethod]
        public void testMultiplicativeOperation_03() {
            Console.WriteLine("\n multiplicative operation 03");
            List<object> constraints	= doTestContextOK("context Film inv: -100 * -200.50 = 200.3",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Real", "*", typeof(OperationCallExp), "Integer", new Object[] { "Real" });			
        }

        [TestMethod]
        public void testAdditiveOperation_01() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 + 200  = 300",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Integer", "+", typeof(IntegerLiteralExp), "Integer", new Object[] { "Integer" });			

        }

        [TestMethod]
        public void testAdditiveOperation_02() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 * 300.32 + 200 * 500 / 200 = 300",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Real", "+", typeof(OperationCallExp), "Real", new Object[] { "Integer" });			
        }

        [TestMethod]
        public void testRelationalExpression_01() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 > 200",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", ">", typeof(IntegerLiteralExp), "Integer", new Object[] { "Integer" });			
        }

        [TestMethod]
        public void testRelationalPrecedence_01() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 > 200 and 200 < 100 or  100 = 200 xor 90 <> 67",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "xor", typeof(OperationCallExp), "Boolean", new Object[] { "Boolean" });			
        }

        [TestMethod]
        public void testRelationalPrecedence_02() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 > 200 and 200 < 100 or  100 = 200",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "or", typeof(OperationCallExp), "Boolean", new Object[] { "Boolean" });			
        }

        [TestMethod]
        public void testRelationalPrecedence_03() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 > 200 or 200 < 100 and  100 = 200",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "and", typeof(OperationCallExp), "Boolean", new Object[] { "Boolean" });			
        }

        [TestMethod]
        public void testRelationalPrecedence_04() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 > 200 or (200 < 100 and  100 = 200)",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "or", typeof(OperationCallExp), "Boolean", new Object[] { "Boolean" });			
        }

        [TestMethod]
        public void testRelationalPrecedence_05() {
            List<object> constraints	= doTestContextOK("context Film inv: (100 > 200 and 300 < 400) or (200 < 100 and  100 = 200)",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "or", typeof(OperationCallExp), "Boolean", new Object[] { "Boolean" });			
        }

        [TestMethod]
        public void testAdditivePrecedence_01() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 + 200 - 500 = 100",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Integer", "-", typeof(OperationCallExp), "Integer", new Object[] { "Integer" });			

        }

        [TestMethod]
        public void testAdditivePrecedence_02() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 - 200 + 500 = 100",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Integer", "+", typeof(OperationCallExp), "Integer", new Object[] { "Integer" });			

        }

        [TestMethod]
        public void testAdditivePrecedence_03() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 + (200 - 500) = 100",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Integer", "+", typeof(IntegerLiteralExp), "Integer", new Object[] { "Integer" });			
        }

        [TestMethod]
        public void testMultiplicativePrecedence_01() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 * 200 / 500 = 100",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Real", "/", typeof(OperationCallExp), "Integer", new Object[] { "Real" });			

        }

        [TestMethod]
        public void testMultiplicativePrecedence_02() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 / 200 * 500 = 100",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Real", "*", typeof(OperationCallExp), "Real", new Object[] { "Real" });			
        }

        [TestMethod]
        public void testMultiplicativePrecedence_03() {
            List<object> constraints	= doTestContextOK("context Film inv: 100 / (200 * 500) = 100",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Real", "/", typeof(IntegerLiteralExp), "Integer", new Object[] { "Real" });			
        }

        [TestMethod]
        public void testSetEquals_01() {
            List<object> constraints	= doTestContextOK("context Film inv: Set{1, 2, 3} = Set { 2, 3 }",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "=", typeof(CollectionLiteralExp), "Set(Integer)", new Object[] { "Set(Integer)" });			
        }

        [TestMethod]
        public void testSequenceEquals_01() {
            List<object> constraints	= doTestContextOK("context Film inv: Sequence{1, 2, 3} = Sequence { 2, 3 }",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallType(oclExpression, "Boolean", "=", typeof(CollectionLiteralExp), "Sequence(Integer)", new Object[] { "Sequence(Integer)" });			
        }

        [TestMethod]
        public void testSetDifference_01() {
            List<object> constraints	= doTestContextOK("context Film inv: Set{1, 2, 3} - Set { 2, 3 } = Set { 1, 2 }",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
            checkOperationCallType(oclExpression, "Set(Integer)", "-", typeof(CollectionLiteralExp), "Set(Integer)", new Object[] { "Set(Integer)" });			
        }

        [TestMethod]
        public void testError_01() {
            doTestContextNotOK("context Tape inv: self.theFilm.size()",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testError_02() {
            doTestContextNotOK("context context Film inv: Set{1, 2, 3} + Set { 2, 3 }",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassOperationCallError_01() {
            doTestContextNotOK("context Film inv: Film::getRentalFee(1) = 10",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassOperationCallError_02() {
            doTestContextNotOK("context Film inv: Film::getRentalFee1(1) = 10",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassOperationCallError_03() {
            doTestContextNotOK("context Film inv: name.Film::getRentalFee(1) = 10",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassOperationCall_01() {
            doTestContextOK("context Film inv: getRentalFee(Film::allInstances()->size()) = 10",     
                            getCurrentMethodName());
        }

    }
}