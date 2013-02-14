using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.modelreader;
using Ocl20Test.psw.parser.semantic;

namespace Ocl20Test.modelreader
{
    /// <summary>
    /// Summary description for TestModelReader
    /// </summary>
    [TestClass]
    public class TestModelReader : TestPropertyCallExp
    {
        public override ModelReader getReader()
        {
            return
                new VscdReader(
                    @"C:\Users\Leo\Documents\visual studio 2010\Projects\SheetModel_20121206\SheetModel\ModelMaker\Company.classdiagram");
        }

        [TestMethod]
        public void testAttributeCallExp_01()
        {
            List<object> constraints = doTestContextOK("context Empregado inv: nome = nome",
                                                       getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "nome", "String");
            checkImplicitSource(attExp, "self", "Empregado");
        }

        [TestMethod]
        public void testAttributeCallExp_02()
        {
            List<object> constraints = doTestContextOK("context Empregado inv: self.nome = nome",
                                                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "nome", "String");
            checkImplicitSource(attExp, "self", "Empregado");
        }

        [TestMethod]
        public void testDefAttributeCallExp_01()
        {
            doTestManyContextOK("context Empregado def: attrib : Real = 1000.0 context Empregado inv: self.salario > attrib",
                getCurrentMethodName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_01()
        {
            List<object> constraints = doTestContextOK("context Empregado inv: area = area ",
                                                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "area", "Area");
            checkImplicitSource(exp, "self", "Empregado");
        }

        [TestMethod]
        public void testAssociationEndCallExp_02()
        {
            List<object> constraints = doTestContextOK("context Area inv: empregados = empregados",
                                                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "empregados", "Set(Empregado)");
            checkImplicitSource(exp, "self", "Area");
        }

    }
}
