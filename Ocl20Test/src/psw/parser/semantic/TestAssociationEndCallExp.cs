using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.constraints;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestAssociationEndCallExp : TestPropertyCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testAssociationEndCallExp_01()
        {
            List<object> constraints = doTestContextOK("context Tape inv: theFilm = theFilm ",
                                                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "theFilm", "Film");
            checkImplicitSource(exp, "self", "Tape");
        }

        [TestMethod]
        public void testAssociationEndCallExp_02() {
            List<object> constraints	= doTestContextOK("context Film inv: tapes = tapes",     
                              	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Set(Tape)");
            checkImplicitSource(exp, "self", "Film");
        }
        
        [TestMethod]
        public void testAssociationEndCallExp_03() {
            List<object> constraints	= doTestContextOK("context Rental inv: itens->size() > 0",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp) ((OperationCallExp)oclExpression).getSource()).getSource(), "itens", "OrderedSet(RentalItem)");
            checkImplicitSource(exp, "self", "Rental");
        }

        [TestMethod]
        public void testAssociationEndCallExp_04() {
            List<object> constraints	= doTestContextOK("context Tape inv: self.theFilm = theFilm",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "theFilm", "Film");
            checkImplicitSource(exp, "self", "Tape");
	
        }

        [TestMethod]
        public void testAssociationEndCallExp_05() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes = tapes",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Set(Tape)");
            checkImplicitSource(exp, "self", "Film");
        }

        [TestMethod]
        public void testAssociationEndCallExp_06() {
            List<object> constraints	= doTestContextOK("context RentalItem inv: Rental = Rental",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "Rental", "Rental");
            checkImplicitSource(exp, "self", "RentalItem");
        }

        [TestMethod]
        public void testAssociationEndCallExp_07() {
            List<object> constraints	= doTestContextOK("context RentalItem inv: self.Rental.Client = self.Rental.Client",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "Client", "Client");
        }

        [TestMethod]
        public void testAssociationEndCallExp_08() {
            List<object> constraints	= doTestContextOK("context RentalItem inv: self.Rental.itens = self.Rental.itens",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "itens", "OrderedSet(RentalItem)");
        }

        [TestMethod]
        public void testAssociationEndCallExp_09() {
            List<object> constraints	= doTestContextOK("context SpecialFilm inv: self.tapes = tapes",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Set(Tape)");
        }

        [TestMethod]
        public void testAssociationEndCallExp_10() {
            List<object> constraints	= doTestContextOK("context SpecialFilm inv: self.tapes[1] = tapes[2]",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Tape");
        }

        [TestMethod]
        public void testAssociationEndCallExp_11() {
            doTestContextNotOK("context SpecialFilm inv: self.tapes[true] = tapes[2]",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_12() {
            List<object> constraints	= doTestManyContextOK("context Film::getTapes() : Set(Tape) post: self.tapes[1]@pre = 10 ",     
                            	                      getCurrentMethodName());

            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(1, operation.getSpecifications().Count);
            OclPrePostConstraint	constraint = (OclPrePostConstraint) operation.getSpecifications()[0];
            OclPostConstraint post = (OclPostConstraint) constraint.getPostConditions()[0];

            OclExpression oclExpression = ((ExpressionInOclImpl) post.getExpression()).getBodyExpression();
		
            this.checkOperationCallExp(((OperationCallExp)oclExpression), "=", "Boolean");
            OperationCallExp  opCall = (OperationCallExp) ((OperationCallExp)oclExpression);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Tape");
				
            opCall = (OperationCallExp) ((AssociationEndCallExp)opCall.getSource()).getSource();
            this.checkOperationCallExp(opCall, "atPre", "Film");
            checkImplicitSource(opCall, "self", "Film");
		
        }

        [TestMethod]
        public void testAssociationEndCallExp_13() {
            List<object> constraints	= doTestManyContextOK("context Film::getTapes() : Set(Tape) post: tapes[1]@pre = 10 ",     
                            	                      getCurrentMethodName());
		
            doTestManyContextNotOK("context Film inv: tapes[1]@pre = 10 ",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_14() {
            doTestContextOK("context Rental inv: self.itens->select(i | i.Rental = self) = Set{}",     
                            getCurrentMethodName());
            doTestContextOK("context Rental inv: self.itens->select(Rental = self) = Set{}",     
                            getCurrentMethodName());
            doTestContextOK("context Rental inv: self.itens->select(Rental.itens = itens) = Set{}",     
                            getCurrentMethodName());
            doTestContextNotOK("context Rental inv: self.itens->select(i | i.client = self) = Set{}",     
                               getCurrentMethodName());
        }


    }
}
