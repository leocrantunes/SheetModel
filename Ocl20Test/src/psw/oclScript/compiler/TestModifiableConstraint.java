package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import impl.ocl20.constraints.OclModifiableAttributesDefinition;
import impl.ocl20.constraints.OclModifiableDeclarationConstraint;
import impl.ocl20.constraints.OclModifiableInstancesDefinition;
import impl.ocl20.constraints.OclModifiableLinksDefinition;
import impl.ocl20.constraints.OclModifiableTypeDefinition;

import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActions;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;

public class TestModifiableConstraint extends TestActions {
	protected	PSWOclScriptCompiler oclScriptCompiler;
	
	public TestModifiableConstraint() {
		super();
	}
	
	public void		setUp() throws Exception {
		modelRepository = null;
		super.setUp();
		oclScriptCompiler = new PSWOclScriptCompiler(environment, new ConstraintSourceTrackerImpl());
	}
	
	public	void	testModifiableConstraint_01() throws Exception {
		
		String definitions1 =
				"context SpecialFilm::computeFee() : Integer \r\n " +
				" modifiable test:  Film";
		;

		String definitions2 =
			"context SpecialFilm::computeFee() : Integer \r\n " +
			" modifiable test:  SpecialFilm";
	;

		
		List result = oclScriptCompiler.compileOclStream(definitions1, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

		CoreClassifier c = getClassifier("SpecialFilm");
		CoreOperation operation = c.lookupOperation("computeFee", new ArrayList());
		assertNotNull(operation);

		assertEquals(1, operation.getModifiableConstraints().size());
		
		OclModifiableDeclarationConstraint constraint = (OclModifiableDeclarationConstraint) operation.getModifiableConstraints().get(0);
		assertEquals("test", constraint.getName());
		assertEquals(1, constraint.getTypeDefinition().size());
		assertEquals(0, constraint.getInstancesDefinition().size());
		assertEquals(0, constraint.getAttributesDefinition().size());
		assertEquals(0, constraint.getLinksDefinition().size());
		
		assertEquals("Film", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(0)).getClassifier().getName());
		
		result = oclScriptCompiler.compileOclStream(definitions2, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

		c = getClassifier("SpecialFilm");
		operation = c.lookupOperation("computeFee", new ArrayList());
		assertNotNull(operation);

		assertEquals(1, operation.getModifiableConstraints().size());
		
		constraint = (OclModifiableDeclarationConstraint) operation.getModifiableConstraints().get(0);
		assertEquals("test", constraint.getName());
		assertEquals(1, constraint.getTypeDefinition().size());
		assertEquals(0, constraint.getInstancesDefinition().size());
		assertEquals(0, constraint.getAttributesDefinition().size());
		assertEquals(0, constraint.getLinksDefinition().size());
		
		assertEquals("SpecialFilm", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(0)).getClassifier().getName());

		
		result = oclScriptCompiler.compileOclStream(definitions2, "ocldefinitions 2", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

		c = getClassifier("SpecialFilm");
		operation = c.lookupOperation("computeFee", new ArrayList());
		assertNotNull(operation);

		assertEquals(2, operation.getModifiableConstraints().size());
		
		constraint = (OclModifiableDeclarationConstraint) operation.getModifiableConstraints().get(0);
		assertEquals("test", constraint.getName());
		assertEquals(1, constraint.getTypeDefinition().size());
		assertEquals(0, constraint.getInstancesDefinition().size());
		assertEquals(0, constraint.getAttributesDefinition().size());
		assertEquals(0, constraint.getLinksDefinition().size());
		
		assertEquals("SpecialFilm", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(0)).getClassifier().getName());
	}

	
	
	public	void	testModifiableConstraint_02() throws Exception {
		
		String definitions1 =
				"context SpecialFilm::computeFee() : Integer \r\n " +
				" modifiable test:  Film, SpecialFilm";
		;

		List result = oclScriptCompiler.compileOclStream(definitions1, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

		CoreClassifier c = getClassifier("SpecialFilm");
		CoreOperation operation = c.lookupOperation("computeFee", new ArrayList());
		assertNotNull(operation);

		assertEquals(1, operation.getModifiableConstraints().size());
		
		OclModifiableDeclarationConstraint constraint = (OclModifiableDeclarationConstraint) operation.getModifiableConstraints().get(0);
		assertEquals("test", constraint.getName());
		assertEquals(2, constraint.getTypeDefinition().size());
		assertEquals(0, constraint.getInstancesDefinition().size());
		assertEquals(0, constraint.getAttributesDefinition().size());
		assertEquals(0, constraint.getLinksDefinition().size());
		
		assertEquals("Film", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(0)).getClassifier().getName());
		assertEquals("SpecialFilm", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(1)).getClassifier().getName());
	}

	
	public	void	testModifiableConstraint_03() throws Exception {
		
		String definitions1 =
				"context SpecialFilm::computeFee() : Integer \r\n " +
				" modifiable test:  Film, SpecialFilm::allInstances(), Film::allInstances().rentalFee \r\n" +
				"modifiable test2: Film, links(SpecialFilm::allInstances(), dist), links(SpecialFilm::allInstances(), dist, Distributor::allInstances())"
		;

		List result = oclScriptCompiler.compileOclStream(definitions1, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

		CoreClassifier c = getClassifier("SpecialFilm");
		CoreOperation operation = c.lookupOperation("computeFee", new ArrayList());
		assertNotNull(operation);

		assertEquals(2, operation.getModifiableConstraints().size());
		
		OclModifiableDeclarationConstraint constraint = (OclModifiableDeclarationConstraint) operation.getModifiableConstraints().get(0);
		assertEquals("test", constraint.getName());
		
		assertEquals(1, constraint.getTypeDefinition().size());
		assertEquals(1, constraint.getInstancesDefinition().size());
		assertEquals(1, constraint.getAttributesDefinition().size());
		assertEquals(0, constraint.getLinksDefinition().size());
		
		assertEquals("Film", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(0)).getClassifier().getName());
		assertEquals("rentalFee", ((OclModifiableAttributesDefinition) constraint.getAttributesDefinition().get(0)).getAttribute().getName());
		assertEquals("Set(Film)", ((OclModifiableAttributesDefinition) constraint.getAttributesDefinition().get(0)).getInstancesExpression().getType().getName());
		assertEquals("Set(SpecialFilm)", ((OclModifiableInstancesDefinition) constraint.getInstancesDefinition().get(0)).getInstancesExpression().getType().getName());
		
		constraint = (OclModifiableDeclarationConstraint) operation.getModifiableConstraints().get(1);
		assertEquals(1, constraint.getTypeDefinition().size());
		assertEquals(0, constraint.getInstancesDefinition().size());
		assertEquals(0, constraint.getAttributesDefinition().size());
		assertEquals(2, constraint.getLinksDefinition().size());

		assertEquals("Film", ((OclModifiableTypeDefinition) constraint.getTypeDefinition().get(0)).getClassifier().getName());
		assertEquals("Set(SpecialFilm)", ((OclModifiableLinksDefinition) constraint.getLinksDefinition().get(0)).getSourceInstancesExpression().getType().getName());
		assertEquals("dist", ((OclModifiableLinksDefinition) constraint.getLinksDefinition().get(0)).getAssociationEnd().getName());
		assertNull(((OclModifiableLinksDefinition) constraint.getLinksDefinition().get(0)).getTargetInstancesExpression());

		assertEquals("Set(SpecialFilm)", ((OclModifiableLinksDefinition) constraint.getLinksDefinition().get(1)).getSourceInstancesExpression().getType().getName());
		assertEquals("Set(Distributor)", ((OclModifiableLinksDefinition) constraint.getLinksDefinition().get(1)).getTargetInstancesExpression().getType().getName());
		assertEquals("dist", ((OclModifiableLinksDefinition) constraint.getLinksDefinition().get(1)).getAssociationEnd().getName());

	}

}
