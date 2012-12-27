/*
 * Created on 22/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;


import java.io.PrintWriter;
import java.util.List;

import ocl20.common.CoreModel;
import ocl20.environment.Environment;

import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetamodelRepositoryFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFRepositoryReader;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.Uml13ModelsRepository;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTracker;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.PSWOclCompiler;
import junit.framework.TestCase;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
abstract public class TestNodeCS extends TestCase {
	protected static Environment	environment;
	protected static CoreModel 	model;
	String source;
	PSWOclCompiler oclCompiler;
	protected ConstraintSourceTracker tracker = new ConstraintSourceTrackerImpl();
	
//	static {
//		try {
//		Uml13ModelsRepository  modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
//		String extentName = "RoseExample";
//		modelRepository.importModel(extentName, "tests/resource/examples/myExampleRose.xml");
//		MOFRepositoryReader repository = modelRepository;
//		assertNotNull(model = repository.getModel(extentName));
//		environment = model.getEnvironmentWithoutParents();
//		} catch(Exception e) {
//			e.printStackTrace();
//		}
//		
//	}
	
	abstract protected	Class	getSpecificNodeClass() throws Exception;
	
	
	
	protected void setUp() throws Exception {
//		super.setUp();

		super.setUp();
		
		if (model == null) {
			Uml13ModelsRepository  modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
			String extentName = "RoseExample";
			modelRepository.importModel(extentName, "tests/resource/examples/myExampleRose.xml");
			MOFRepositoryReader repository = modelRepository;
			assertNotNull(model = repository.getModel(extentName));
			OclTypesFactory.setEnvironment(model);
		}
		
		environment = model.getEnvironmentWithoutParents();

	}
	
	protected void tearDown() {
		oclCompiler.deleteAllConstraintsForSource(source);
		tracker.clearAll();
	}

	protected CSTNode parseOK(String expression, String testName) {
		try {
			source = testName;
			oclCompiler = new PSWOclCompiler(environment, tracker);
			List nodes = oclCompiler.compileOclStream(expression, testName, new PrintWriter(System.out), getSpecificNodeClass());
			assertNotNull(nodes);
			assertTrue(nodes.size() >= 1);
			return	(CSTNode) nodes.get(0);
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
		return	null;
	}


	protected void parseWithError(String expression, String testName) {
		try {
			source = testName;
			oclCompiler = new PSWOclCompiler(environment, tracker);
			List nodes = oclCompiler.compileOclStream(expression, testName, new PrintWriter(System.out), getSpecificNodeClass());
			assertNull(nodes);
		} catch (Exception e) {
			System.out.println(e.getMessage());
		}
	}




//	protected Environment getEnvironment() throws MOFMetaModelRepositoryException {
//		Uml13ModelsRepository  modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
//		String extentName = "RoseExample";
//		modelRepository.importModel(extentName, "resource/myExampleRose.xml");
//		MOFRepositoryReader repository = modelRepository;
//		assertNotNull(model = repository.getModel(extentName));
//		return	model.getEnvironmentWithoutParents();
//	}

	protected String getCurrentMethodName() {
		return new Exception().getStackTrace()[1].getMethodName();
	}
}
