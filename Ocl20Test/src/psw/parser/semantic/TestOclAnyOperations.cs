using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestOclAnyOperations : TestPropertyCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testEqual() {
            List<object> constraints	= doTestContextOK("context Tape inv: self = self",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallExp(oclExpression, "=", "Boolean");
        }

        [TestMethod]
        public void testNotEqual() {
            List<object> constraints	= doTestContextOK("context Tape inv: self <> self",     
                              	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallExp(oclExpression, "<>", "Boolean");
        }

        [TestMethod]
        public void testOclIsNew() {
            List<object> constraints = doTestContextOK("context Tape inv: self.oclIsNew()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallExp(oclExpression, "oclIsNew", "Boolean");
        }

        [TestMethod]
        public void testOclIsUndefined() {
            List<object> constraints = doTestContextOK("context Tape inv: Set{1, 2}->asSequence()->first().oclIsUndefined()",     
                            	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkOperationCallExp(oclExpression, "oclIsUndefined", "Boolean");
        }

        [TestMethod]
        public void testOclAsType() {
            List<object> constraints = doTestContextOK("context Tape inv: self.theFilm.oclAsType(SpecialFilm).lateReturnFee > 40.0",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testOclIsTypeOf() {
            List<object> constraints = doTestContextOK("context Tape inv: self.theFilm.oclIsTypeOf(SpecialFilm)",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testOclIsKindOf() {
            List<object> constraints = doTestContextOK("context Tape inv: self.theFilm.oclIsKindOf(SpecialFilm)",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testAllInstances() {
            List<object> constraints = doTestContextOK("context Tape def: allTapes : Set(Tape) = Tape::allInstances()",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testAllInstances_02() {
            List<object> constraints = doTestContextOK("context Tape inv : 20 = Tape::allInstances()->size()",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testOclAnyType() {
            List<object> constraints = doTestContextOK("context Tape inv: let x : OclAny = self in x.oclIsUndefined()",     
                            	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testOclVoid() {
            List<object> constraints = doTestContextOK("context Tape inv: let a : Set(OclVoid) = Set{} in a->forAll(x : OclVoid | x.oclIsUndefined())",     
                            	                  getCurrentMethodName());
        }
    }
}
