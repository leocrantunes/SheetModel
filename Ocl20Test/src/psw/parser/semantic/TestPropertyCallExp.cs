using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.constraints;
using Ocl20.parser.cst;
using Ocl20.parser.cst.context;
using Ocl20.parser.typeChecker;

namespace Ocl20Test.psw.parser.semantic
{
    public abstract class TestPropertyCallExp : TestLiteralExp {

        protected	AssociationEndCallExp checkAssociationEndCallExp(OclExpression oclExpression, String roleName, String typeName ) {
            Assert.IsTrue(oclExpression is AssociationEndCallExp);
            AssociationEndCallExp exp = (AssociationEndCallExp) oclExpression;
            Assert.AreEqual(roleName, exp.getReferredAssociationEnd().getName());
            Assert.AreEqual(typeName, exp.getType().getName());
            return	exp;
        }

        protected	AssociationClassCallExp checkAssociationClassCallExp(OclExpression oclExpression, String roleName, String typeName ) {
            Assert.IsTrue(oclExpression is AssociationClassCallExp);
            AssociationClassCallExp exp = (AssociationClassCallExp) oclExpression;
            Assert.AreEqual(roleName, exp.getReferredAssociationClass().getName());
            Assert.AreEqual(typeName, exp.getType().getName());
            return	exp;
        }

        protected	AttributeCallExp checkAttributeCallExp(OclExpression oclExpression, String attName, String attType) {
            Assert.IsTrue(oclExpression is AttributeCallExp);
            AttributeCallExp attExp = (AttributeCallExp) oclExpression;
            Assert.AreEqual(attName, attExp.getReferredAttribute().getName());
            Assert.AreEqual(attType, attExp.getType().getName());
            return	attExp;
        }

        protected	OperationCallExp checkOperationCallExp(OclExpression oclExpression, String opName, String returnTypeName) {
            Assert.IsTrue(oclExpression is OperationCallExp);
            OperationCallExp opExp = (OperationCallExp) oclExpression;
            Assert.AreEqual(opName, opExp.getReferredOperation() != null ? opExp.getReferredOperation().getName() : opExp.getName());
//		Assert.AreEqual(returnTypeName, opExp.getReferredOperation() != null? opExp.getReferredOperation().getReturnType().getName() : opExp.getType().getName());
            Assert.AreEqual(returnTypeName, opExp.getType().getName());
            return	opExp;
        }



        protected	void checkImplicitSource(PropertyCallExp exp, String varName, String varType) {
            Assert.IsTrue(exp.getSource() is VariableExp);
            VariableExp varExp = (VariableExp) exp.getSource();
            Assert.AreEqual(varType, exp.getSource().getType().getName());
            Assert.AreEqual(varName, varExp.getReferredVariable().getVarName());
        }

        protected IteratorExp	checkIteratorExp(OclExpression oclExpression, String typeName, String name, String iteratorType, String iteratorName) {
            Assert.IsTrue(oclExpression is IteratorExp);
            IteratorExp exp = (IteratorExp) oclExpression;
            Assert.AreEqual(typeName, exp.getType().getName());
            Assert.AreEqual(name, exp.getName());
            Assert.AreEqual(1, exp.getIterators().Count);
            VariableDeclaration varDecl = (VariableDeclaration) exp.getIterators()[0];
            Assert.AreEqual(iteratorName, varDecl.getVarName());
            Assert.AreEqual(iteratorType, varDecl.getType().getName());
		
            return	exp;
        }

        protected PrimitiveLiteralExp	checkPrimitiveLiteralExp(OclExpression oclExpression) {
            Assert.IsTrue(oclExpression is PrimitiveLiteralExp);
            PrimitiveLiteralExp exp = (PrimitiveLiteralExp) oclExpression;
		
            return	exp;
        }

        protected CollectionLiteralExp	checkCollectionLiteralExp(OclExpression oclExpression, String type) {
            Assert.IsTrue(oclExpression is CollectionLiteralExp);
            CollectionLiteralExp exp = (CollectionLiteralExp) oclExpression;
            Assert.AreEqual(type, exp.getType().getName());
            return	exp;
        }

        protected void doTestContextNotOK(String expression, String testName) {
            source = testName;
            parseWithError(expression, testName);
        }

        protected List<object> doTestContextOK(String expression, String testName) {
            source = testName;
            CSTNode rootNode = parseOK(expression, testName);
            Assert.IsNotNull(rootNode);
            if (rootNode != null)
                return getConstraints(rootNode);
            else	
                return null;
        }

        protected virtual List<object> getConstraints(CSTNode rootNode) {
            if (rootNode is CSTClassifierContextDeclCS) {
                CSTClassifierContextDeclCS context = (CSTClassifierContextDeclCS) rootNode;
                List<object> constraints = context.getConstraintsNodesCS();
                return constraints;
            } else
            return	null;
        }

        protected List<object> doTestManyContextOK(String expression, String testName) {
            try {
                source = testName;
                oclCompiler = new PSWOclCompiler(environment, tracker);
                List<object> rootNode = oclCompiler.compileOclStream(expression, testName, new StreamWriter(Console.OpenStandardOutput()));
                Assert.IsNotNull(rootNode);
                Assert.IsTrue(rootNode.Count >= 1);
                return	rootNode;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        protected void doTestManyContextNotOK(String expression, String testName) {
            try {
                source = testName;
                oclCompiler = new PSWOclCompiler(environment, tracker);
                List<object> rootNode = oclCompiler.compileOclStream(expression, testName, new StreamWriter(Console.OpenStandardOutput()));
                if (oclCompiler.getErrorsCount() == 0)
                    throw new AssertFailedException();

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        protected OclExpression getConstraintExpression(List<object> constraints) {
            if (constraints.Count > 0) {
                CSTInvariantCS invariant =(CSTInvariantCS) constraints[0];
                CSTExpressionInOclCS expression = invariant.getExpressionNodeCS();
                return ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
            }
            else
                return	null;
        }
		
        protected override Type getSpecificNodeClass()
        {
            return typeof (CSTContextDeclarationCS);
        }
}

}
