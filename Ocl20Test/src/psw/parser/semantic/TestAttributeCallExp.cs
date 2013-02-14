using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.constraints;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestAttributeCallExp : TestPropertyCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testAttributeCallExp_01() {
            List<object> constraints = doTestContextOK("context Film inv: name = name",     
                                                       getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "name", "String");
            checkImplicitSource(attExp, "self", "Film");
        }

        [TestMethod]
        public void testAttributeCallExp_02() {
            List<object> constraints	= doTestContextOK("context Film inv: self.name = name",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "name", "String");
            checkImplicitSource(attExp, "self", "Film");
        }

        [TestMethod]
        public void testDefAttributeCallExp_01() {
            doTestManyContextOK("context Film def: attrib : Integer = 10  context Film inv: self.rentalFee > attrib",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifierAttribute_01() {
            List<object> constraints	= doTestContextOK("context Film inv: Rental::maxDaysToReturn > SpecialFilm::days",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
			
            Assert.IsTrue(((OperationCallExp)oclExpression).getSource() is AttributeCallExp);
            AttributeCallExp exp = (AttributeCallExp) ((OperationCallExp)oclExpression).getSource();
            Assert.AreEqual("Rental", exp.getReferredAttribute().getFeatureOwner().getName());
            Assert.AreEqual("maxDaysToReturn", exp.getReferredAttribute().getName());
            Assert.AreEqual("Integer", exp.getType().getName());
        }

        [TestMethod]
        public void testAttributeCall_Superclass() {
            doTestContextOK("context SpecialFilm inv: self.code = self.name",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testAttributeCall_Superclass_02() {
            List<object> constraints = doTestContextOK("context SpecialFilm inv: self.tapes[1].number > 10",     
                                               getCurrentMethodName());
				
            OclExpression oclExpression = getConstraintExpression(constraints);
			
            Assert.IsTrue(((OperationCallExp)oclExpression).getSource() is AttributeCallExp);
            AttributeCallExp exp = (AttributeCallExp) ((OperationCallExp)oclExpression).getSource();
            Assert.AreEqual("Tape", exp.getReferredAttribute().getFeatureOwner().getName());
            Assert.AreEqual("number", exp.getReferredAttribute().getName());
            Assert.AreEqual("Integer", exp.getType().getName());
        }

        [TestMethod]
        public void testClassifierAttribute_02() {
            doTestContextNotOK("context Rental inv: Rental::returned = true",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifierAttribute_03() {
            doTestContextNotOK("context Rental inv: Rental::return = true",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifierAttribute_04() {
            List<object> constraints	= doTestContextOK("context Rental inv: self.maxDaysToReturn > 10",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "maxDaysToReturn", "Integer");
            checkImplicitSource(attExp, "self", "Rental");
        }

        [TestMethod]
        public void testClassifierAttribute_045()
        {
            List<object> constraints =
                doTestManyContextOK("context Film::getSpecialFee(day:Integer) : Real pre: day > 10",
                                    getCurrentMethodName());
            CoreClassifier film = (CoreClassifier)environment.lookup("Film");
            List<object> parms = new List<object>();
            parms.Add((CoreClassifier) model.getEnvironmentWithoutParents().lookup("Integer"));
            CoreOperation operation = film.lookupOperation("getSpecialFee", parms);
            Assert.AreEqual(1, operation.getSpecifications().Count);
        }

        [TestMethod]
        public void testClassifierAttribute_05() {
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(0, operation.getSpecifications().Count);

            List<object> constraints	= 
                doTestManyContextOK("context Film::getTapes() : Set(Tape) post: self.rentalFee@pre = 10 ",     
                            	                      getCurrentMethodName());

            film = (CoreClassifier) environment.lookup("Film");
            operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(1, operation.getSpecifications().Count);
            OclPrePostConstraint	constraint = (OclPrePostConstraint) operation.getSpecifications()[0];
            OclPostConstraint post = (OclPostConstraint) constraint.getPostConditions()[0];

            OclExpression oclExpression = ((ExpressionInOclImpl) post.getExpression()).getBodyExpression();
		
            this.checkOperationCallExp(((OperationCallExp)oclExpression), "=", "Boolean");
            OperationCallExp  opCall = (OperationCallExp) ((OperationCallExp)oclExpression);
            AttributeCallExp attExp = checkAttributeCallExp(opCall.getSource(), "rentalFee", "Integer");
		
            opCall = (OperationCallExp) ((AttributeCallExp)opCall.getSource()).getSource();
            this.checkOperationCallExp(opCall, "atPre", "Film");
            checkImplicitSource(opCall, "self", "Film");
        }

        [TestMethod]
        public void testEnumerationLiteral_01() {
            List<object> constraints	= doTestContextOK("context Film inv: Situation::married = Situation::married",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
			
            Assert.IsTrue(((OperationCallExp)oclExpression).getSource() is EnumLiteralExp);
            EnumLiteralExp exp = (EnumLiteralExp) ((OperationCallExp)oclExpression).getSource();
            Assert.AreEqual("Situation", exp.getType().getName());
            Assert.AreEqual("married", exp.getReferredEnumLiteral().getName());
        }

        [TestMethod]
        public void testMoreThanOneNavigation_08() {
            List<object> constraints	= doTestContextOK("context Tape inv: self.theFilm.name = self.theFilm.name",     
                            	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "name", "String");
				
            Assert.IsTrue(attExp.getSource() is AssociationEndCallExp);
            AssociationEndCallExp exp = (AssociationEndCallExp) attExp.getSource();
            Assert.AreEqual("Film", exp.getType().getName());
        }

        [TestMethod]
        public void testVariableExp_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self = self",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
			
            Assert.IsTrue(((OperationCallExp)oclExpression).getSource() is VariableExp);
            VariableExp varExp = (VariableExp) ((OperationCallExp)oclExpression).getSource();
            Assert.AreEqual("self", varExp.getReferredVariable().getVarName());
            Assert.AreEqual("Film", varExp.getType().getName());
            Assert.AreEqual("Film", varExp.getReferredVariable().getType().getName());
            Assert.IsNull(varExp.getReferredVariable().getInitExpression());
        }

        [TestMethod]
        public void testAttributeCallExp_AttributesFromIterators_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->select(number > rentalFee)->size() = 0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testAttributeCallExp_AttributesFromIterators_02() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > self.rentalFee)->size() = 0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testAttributeCallExp_AttributesFromIterators_03() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > rentalFee)->size() = 0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testAttributeCallExp_AttributesFromIterators_04() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > t.theFilm.tapes->select(t2 | (t2.number > t.number) and (t2.number > self.rentalFee))->size())->size() = 0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testAttributeCallExp_AttributesFromIterators_05() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > t.theFilm.tapes->select(number > t.number and number > self.rentalFee)->size())->size() = 0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidAttributeCall01() {
            doTestContextNotOK("context Film inv: self.tapes->select(number > fee)->size() = 0",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidAttributeCall02() {
            doTestContextNotOK("context Film inv: self.tapes->select(number1 > rentalFee)->size() = 0",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidArrow_02() {
            doTestContextNotOK("context Film inv: self->name",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidAttributeExp_01() {
            doTestManyContextNotOK("context Film inv: self.tapes.num = 10  context Film inv: self.tapes->size()",     
                                   getCurrentMethodName());
        }
    }
}
