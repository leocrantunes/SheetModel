using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestAssociationClassCallExp : TestAttributeCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testAssocClass_01() {
            List<object> constraints = doTestContextOK("context SpecialFilm inv: self.dist = self.dist",     
                                                       getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "dist", "Set(Distributor)");
        }

        [TestMethod]
        public void testAssocClass_02() {
            List<object> constraints	= doTestContextOK("context SpecialFilm inv: self.Allocation = self.Allocation",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "Allocation", "Set(Allocation)");
        }

        [TestMethod]
        public void testAssocClass_03() {
            List<object> constraints	= doTestContextOK("context Allocation inv: self.dist= self.dist",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "dist", "Distributor");
        }

        [TestMethod]
        public void testAssocClass_04() {
            List<object> constraints	= doTestContextOK("context Distributor inv: self.Allocation = self.Allocation",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "Allocation", "Set(Allocation)");
        }

        [TestMethod]
        public void testAssocClass_05() {
            List<object> constraints	= doTestContextOK("context Person inv: EmployeeRanking[self.bosses] = EmployeeRanking[self.bosses]",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "EmployeeRanking", "Set(EmployeeRanking)");
        }

        [TestMethod]
        public void testAssocClass_06() {
            List<object> constraints	= doTestContextOK("context Person inv: EmployeeRanking[bosses] = EmployeeRanking[employees]",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "EmployeeRanking", "Set(EmployeeRanking)");
        }

        [TestMethod]
        public void testQualifier_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes[1] = self.tapes[2]",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp attExp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Tape");
            Assert.AreEqual(1, attExp.getQualifiers().Count);
        }

        [TestMethod]
        public void testQualifier_02() {
            doTestContextNotOK("context Film inv: self.tapes[true] = self.tapes[2]",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidArrow_01() {
            doTestContextNotOK("context Rental inv: self.itens->rental",     
                               getCurrentMethodName());
        }
    }
}
