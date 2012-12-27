/*
 * Created on 13/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.ocl20;

import java.util.ArrayList;
import java.util.List;

import junit.framework.TestCase;
import impl.ocl20.util.AstOclConstraintFactoryManager;
import impl.ocl20.util.AstOclModelElementFactoryManager;
import impl.ocl20.util.AstOclConstraintFactory;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.common.CoreOperation;
import ocl20.constraints.ExpressionInOcl;
import ocl20.constraints.OclAttributeDeriveConstraint;
import ocl20.constraints.OclAttributeInitConstraint;
import ocl20.constraints.OclBodyConstraint;
import ocl20.constraints.OclConstraint;
import ocl20.constraints.OclInvariantConstraint;
import ocl20.constraints.OclPrePostConstraint;
import ocl20.expressions.BooleanLiteralExp;
import ocl20.expressions.IntegerLiteralExp;
import ocl20.expressions.RealLiteralExp;
import ocl20.util.AstOclModelElementFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetamodelRepositoryFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFRepositoryReader;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.Uml13ModelsRepository;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestOclConstraints extends TestCase {

	
	public TestOclConstraints(String arg0) throws Exception {
		super(arg0);
	}

	protected	static MOFRepositoryReader repository;
	protected	static CoreModel	  umlModel = null;
	protected static Uml13ModelsRepository  modelRepository = null;
	protected static String extentName = "RoseExample";

	
	public void setUp() throws Exception {
		if (modelRepository == null) {
			try {
				modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
				modelRepository.importModel(extentName, "tests/resource/examples/myExampleRose.xml");
				repository = modelRepository;
			} catch (Exception e) {
				e.printStackTrace();
			}
		}	
		setUmlModelsRepository();
	}
	
	public void setUmlModelsRepository() throws MOFMetaModelRepositoryException {
		assertNotNull(umlModel = repository.getModel(extentName));

	}

	protected CoreClassifier getClassifier(String name) {
		return	(CoreClassifier) umlModel.getEnvironmentWithoutParents().lookup(name);
	}

	public void testAttributeInit() {
		AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
		ocl20.util.AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
		IntegerLiteralExp exp1 = factory2.createIntegerLiteralExp(100, getClassifier("Integer"));
		ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
		OclAttributeInitConstraint constraint = (OclAttributeInitConstraint) factory1.createAttributeInitConstraint("test.ocl", getClassifier("Film"), getClassifier("Film").lookupAttribute("rentalFee"), 
				expInOcl);

		assertEquals("init: 100", constraint.toString());
		
		OclConstraint c = getClassifier("Film").getInitConstraint("rentalFee");
		assertEquals(constraint, c);
		
		CoreAttribute attrib = getClassifier("Film").lookupAttribute("rentalFee");
		ExpressionInOcl exp = attrib.getInitialValueExpression();
		assertEquals(exp, expInOcl);
		
		getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
		assertNull(getClassifier("Film").getInitConstraint("rentalFee"));
	}
	
	public void testAttributeDerive() {
		AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
		AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
		IntegerLiteralExp exp1 = factory2.createIntegerLiteralExp(100, getClassifier("Integer"));
		ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
		OclAttributeDeriveConstraint constraint = (OclAttributeDeriveConstraint) factory1.createAttributeDeriveConstraint("test.ocl", getClassifier("Film"), getClassifier("Film").lookupAttribute("rentalFee"), 
				expInOcl);

		assertEquals("derive: 100", constraint.toString());
		
		OclConstraint c = getClassifier("Film").getDeriveConstraint("rentalFee");
		assertEquals(constraint, c);
		
		CoreAttribute attrib = getClassifier("Film").lookupAttribute("rentalFee");
		assertNotNull(attrib);
		assertEquals(expInOcl, attrib.getDerivedValueExpression());
		assertTrue(attrib.isDerived());
		
		getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
		assertNull(getClassifier("Film").getDeriveConstraint("rentalFee"));
	}

	public void testInvariant() {
		AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
		AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
		BooleanLiteralExp exp1 = factory2.createBooleanLiteralExp(true, getClassifier("Boolean"));
		ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
		OclInvariantConstraint constraint = (OclInvariantConstraint) factory1.createInvariantConstraint("test.ocl", "myInvariant", getClassifier("Film"), expInOcl);

		assertEquals("inv myInvariant: true", constraint.toString());
		
		assertTrue(getClassifier("Film").getAllInvariants().contains(constraint));
		
		getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
		assertFalse(getClassifier("Film").getAllInvariants().contains(constraint));
	}

	public void testBody() {
		AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
		AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
		RealLiteralExp exp1 = factory2.createRealLiteralExp("200.50", getClassifier("Real"));
		ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
		
		List params = new ArrayList();
		params.add(getClassifier("Integer"));
		CoreOperation oper = getClassifier("Film").lookupOperation("getRentalFee", params);
		
		OclBodyConstraint constraint = (OclBodyConstraint) factory1.createBodyConstraint("test.ocl", oper, expInOcl, null);

		assertEquals("body: 200.50", constraint.toString());
		
		assertEquals(constraint, oper.getBodyDefinition());
		assertEquals(expInOcl, oper.getBodyDefinition().getExpression());
		
		oper.deleteAllConstraintsForSource("test.ocl");
		assertNull(oper.getBodyDefinition());
	}
	
	public void testPrePost() {
		AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
		AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
		BooleanLiteralExp exp1 = factory2.createBooleanLiteralExp(true, getClassifier("Boolean"));
		ExpressionInOcl expInOcl1 = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);

		BooleanLiteralExp exp2 = factory2.createBooleanLiteralExp(false, getClassifier("Boolean"));
		ExpressionInOcl expInOcl2 = factory1.createExpressionInOcl("name", getClassifier("Film"), exp2);

		List params = new ArrayList();
		params.add(getClassifier("Integer"));
		CoreOperation oper = getClassifier("Film").lookupOperation("getRentalFee", params);
		
		OclPrePostConstraint constraint = (OclPrePostConstraint) factory1.createPrePostConstraint("test.ocl", oper);
		OclConstraint preConstraint = factory1.createPreConstraint(constraint, "test.ocl", "myPre", oper, expInOcl1);
		OclConstraint postConstraint = factory1.createPostConstraint(constraint, "test.ocl", "myPost", oper, expInOcl2);
		
		assertEquals("pre myPre: true", preConstraint.toString());
		assertEquals("post myPost: false", postConstraint.toString());
		
		assertTrue(oper.getSpecifications().contains(constraint));
		OclPrePostConstraint example = (OclPrePostConstraint) oper.getSpecifications().iterator().next();
		assertTrue(example.getPreConditions().contains(preConstraint));
		assertTrue(example.getPostConditions().contains(postConstraint));
		
		oper.deleteAllConstraintsForSource("test.ocl");
		assertEquals(0, oper.getSpecifications().size());
	}
	
	public void testDefAttribute() {
		try {
			getClassifier("Film").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
		} catch(Exception e) {
			e.printStackTrace();
			fail();
		}
		
		try {
			getClassifier("Film").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
			fail();
		} catch(Exception e) {
		}

		try {
			getClassifier("Product").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
			fail();
		} catch(Exception e) {
		}

		try {
			getClassifier("Film").addDefinedElement("test.ocl", "name", getClassifier("Integer"));
			fail();
		} catch(Exception e) {
		}

		CoreAttribute attr = getClassifier("Film").lookupAttribute("myNewAttr");
		assertEquals("Integer", attr.getFeatureType().getName());
		assertTrue(attr.isOclDefined());
		
		CoreAttribute nameAttr = getClassifier("Film").lookupAttribute("name");
		assertEquals("String", nameAttr.getFeatureType().getName());
		assertFalse(nameAttr.isOclDefined());
		
		getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
		assertNull(getClassifier("Film").lookupAttribute("myNewAttr"));
	}

	public void testDefOperation() {
		List paramNames = new ArrayList();
		paramNames.add("param1");
		
		List paramTypes = new ArrayList();
		paramTypes.add(getClassifier("Real"));
		
		try {
			getClassifier("Film").addDefinedOperation("test.ocl", "myNewOper", paramNames, paramTypes, getClassifier("Integer"));
		} catch(Exception e) {
			e.printStackTrace();
			fail();
		}
		
		CoreOperation oper = getClassifier("Film").lookupOperation("myNewOper", paramTypes);
		assertEquals("Integer", oper.getReturnType().getName());
		assertTrue(oper.isOclDefined());
		List args = new ArrayList();
		args.add(getClassifier("Integer"));
		assertTrue(oper.hasMatchingSignature(args));
		
		getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
		assertNull(getClassifier("Film").lookupOperation("myNewOper", paramTypes));
	}
	
	
}
