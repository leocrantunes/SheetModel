/*
 * Created on Dec 17, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */

import impl.ocl20.constraints.ExpressionInOclImpl;

import java.io.PrintWriter;
import java.util.Iterator;
import java.util.List;

import ocl20.expressions.AssociationClassCallExp;
import ocl20.expressions.AssociationEndCallExp;
import ocl20.expressions.AttributeCallExp;
import ocl20.expressions.CollectionLiteralExp;
import ocl20.expressions.IteratorExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;
import ocl20.expressions.PrimitiveLiteralExp;
import ocl20.expressions.PropertyCallExp;
import ocl20.expressions.VariableDeclaration;
import ocl20.expressions.VariableExp;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTClassifierContextDeclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTContextDeclarationCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTInvariantCS;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.PSWOclCompiler;


public abstract class TestPropertyCallExp extends TestLiteralExp {

	protected	AssociationEndCallExp checkAssociationEndCallExp(OclExpression oclExpression, String roleName, String typeName ) {
		assertTrue(oclExpression instanceof AssociationEndCallExp);
		AssociationEndCallExp exp = (AssociationEndCallExp) oclExpression;
		assertEquals(roleName, exp.getReferredAssociationEnd().getName());
		assertEquals(typeName, exp.getType().getName());
		return	exp;
	}

	protected	AssociationClassCallExp checkAssociationClassCallExp(OclExpression oclExpression, String roleName, String typeName ) {
		assertTrue(oclExpression instanceof AssociationClassCallExp);
		AssociationClassCallExp exp = (AssociationClassCallExp) oclExpression;
		assertEquals(roleName, exp.getReferredAssociationClass().getName());
		assertEquals(typeName, exp.getType().getName());
		return	exp;
	}

	protected	AttributeCallExp checkAttributeCallExp(OclExpression oclExpression, String attName, String attType) {
		assertTrue(oclExpression instanceof AttributeCallExp);
		AttributeCallExp attExp = (AttributeCallExp) oclExpression;
		assertEquals(attName, attExp.getReferredAttribute().getName());
		assertEquals(attType, attExp.getType().getName());
		return	attExp;
	}

	protected	OperationCallExp checkOperationCallExp(OclExpression oclExpression, String opName, String returnTypeName) {
		assertTrue(oclExpression instanceof OperationCallExp);
		OperationCallExp opExp = (OperationCallExp) oclExpression;
		assertEquals(opName, opExp.getReferredOperation() != null ? opExp.getReferredOperation().getName() : opExp.getName());
//		assertEquals(returnTypeName, opExp.getReferredOperation() != null? opExp.getReferredOperation().getReturnType().getName() : opExp.getType().getName());
		assertEquals(returnTypeName, opExp.getType().getName());
		return	opExp;
	}



	protected	void checkImplicitSource(PropertyCallExp exp, String varName, String varType) {
		assertTrue(exp.getSource() instanceof VariableExp);
		VariableExp varExp = (VariableExp) exp.getSource();
		assertEquals(varType, exp.getSource().getType().getName());
		assertEquals(varName, varExp.getReferredVariable().getVarName());
	}

	protected IteratorExp	checkIteratorExp(OclExpression oclExpression, String typeName, String name, String iteratorType, String iteratorName) {
		assertTrue(oclExpression instanceof IteratorExp);
		IteratorExp exp = (IteratorExp) oclExpression;
		assertEquals(typeName, exp.getType().getName());
		assertEquals(name, exp.getName());
		assertEquals(1, exp.getIterators().size());
		VariableDeclaration varDecl = (VariableDeclaration) exp.getIterators().iterator().next();
		assertEquals(iteratorName, varDecl.getVarName());
		assertEquals(iteratorType, varDecl.getType().getName());
		
		return	exp;
	}

	protected PrimitiveLiteralExp	checkPrimitiveLiteralExp(OclExpression oclExpression) {
		assertTrue(oclExpression instanceof PrimitiveLiteralExp);
		PrimitiveLiteralExp exp = (PrimitiveLiteralExp) oclExpression;
		
		return	exp;
	}

	protected CollectionLiteralExp	checkCollectionLiteralExp(OclExpression oclExpression, String type) {
		assertTrue(oclExpression instanceof CollectionLiteralExp);
		CollectionLiteralExp exp = (CollectionLiteralExp) oclExpression;
		assertEquals(type, exp.getType().getName());
		return	exp;
	}



	protected void doTestContextNotOK(String expression, String testName) {
		source = testName;
		parseWithError(expression, testName);
	}

	protected List doTestContextOK(String expression, String testName) {
		source = testName;
		CSTNode rootNode = parseOK(expression, testName);
		assertNotNull(rootNode);
		if (rootNode != null)
			return getConstraints(rootNode);
		else	
			return null;
	}

	protected List getConstraints(CSTNode rootNode) {
		if (rootNode instanceof CSTClassifierContextDeclCS) {
			CSTClassifierContextDeclCS context = (CSTClassifierContextDeclCS) rootNode;
			List constraints = context.getConstraintsNodesCS();
			return constraints;
		} else
			return	null;
	}

	protected List doTestManyContextOK(String expression, String testName) {
		try {
			source = testName;
			oclCompiler = new PSWOclCompiler(environment, tracker);
			List rootNode = oclCompiler.compileOclStream(expression, testName, new PrintWriter(System.out));
			assertNotNull(rootNode);
			assertTrue(rootNode.size() >= 1);
			return	rootNode;
		} catch (Exception e) {
			System.out.println(e.getMessage());
		}

		return null;
	}

	protected void doTestManyContextNotOK(String expression, String testName) {
		try {
			source = testName;
			oclCompiler = new PSWOclCompiler(environment, tracker);
			List rootNode = oclCompiler.compileOclStream(expression, testName, new PrintWriter(System.out));
			if (oclCompiler.getErrorsCount() == 0)
				fail();

		} catch (Exception e) {
			System.out.println(e.getMessage());
		}
	}

	
	protected OclExpression getConstraintExpression(List constraints) {
		Iterator iter = constraints.iterator();
		if (iter.hasNext()) {
			CSTInvariantCS invariant =(CSTInvariantCS) iter.next();
			CSTExpressionInOclCS expression = invariant.getExpressionNodeCS();
			return ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
		}
		else
			return	null;
	}
		

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestNodeCS#getSpecificNodeClass()
	 */
	protected Class getSpecificNodeClass() throws Exception {
		return CSTContextDeclarationCS.class;
	}

}
