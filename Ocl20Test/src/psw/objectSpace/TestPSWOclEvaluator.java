/*
 * Created on 21/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.objectSpace;

import java.text.DateFormat;
import java.util.ArrayList;
import java.util.List;

import impl.ocl20.common.CoreModelImpl;
import ocl20.common.CoreModel;
import ocl20.environment.Environment;
import ocl20.evaluation.OclValue;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetamodelRepositoryFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.Uml13ModelsRepository;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclDateTimeValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclDateValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclInvalidValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclNullValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValuesFactory;
import junit.framework.TestCase;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
abstract public class TestPSWOclEvaluator extends TestCase {

	protected static Environment	environment = null;
	protected static CoreModel 	model = null;
	protected static Uml13ModelsRepository  modelRepository = null; 
	protected static String extentName = "RoseExample";
	
	/* (non-Javadoc)
	 * @see junit.framework.TestCase#setUp()
	 */
	
	
	protected void setUp()
		throws Exception {
		super.setUp();
		
		if (modelRepository == null) {
			modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
			modelRepository.importModel(extentName, "tests/resource/examples/myExampleRose.xml");
			assertNotNull(model = modelRepository.getModel(extentName));
			OclTypesFactory.setEnvironment(model);
		}
		
		try {
			((CoreModelImpl) model).setDirty(true);
			environment = model.getEnvironmentWithoutParents();
			} catch(Exception e) {
				e.printStackTrace();
			}
	}

	protected OclIntegerValue getInt(long	value) {
		return	OclValuesFactory.getInstance().createIntegerValue(value);
	}

	protected OclRealValue getReal(String value) {
		return	OclValuesFactory.getInstance().createRealValue(value);
	}

	protected OclBooleanValue getBoolean(boolean value) {
		return	OclValuesFactory.getInstance().createBooleanValue(value);
	}

	protected OclStringValue getString(String value) {
		return	OclValuesFactory.getInstance().createStringValue(value);
	}

	protected OclDateValue getDate(String value) throws Exception {
		return	OclValuesFactory.getInstance().createDateValue(DateFormat.getDateInstance().parse(value));
	}
	
	protected OclDateTimeValue getDateTime(String value) throws Exception {
		return	OclValuesFactory.getInstance().createDateTimeValue(DateFormat.getDateTimeInstance().parse(value));
	}

	
	protected OclNullValue getNull() {
		return	OclValuesFactory.getInstance().createNullValue();
	}

	protected OclInvalidValue getInvalid() {
		return	OclValuesFactory.getInstance().createInvalidValue();
	}

	protected void executeOperation(OclValue value, String op, OclValue expectedResult) {
		assertNotNull(value);
		OclValue result = (OclValue) value.executeOperation(op, null);
		checkResult(result, expectedResult);
	}

	protected void executeOperation(OclValue value, String op, OclValue arg, OclValue expectedResult) {
		assertNotNull(value);
		List args = new ArrayList();
		args.add(arg);
		OclValue result = (OclValue) value.executeOperation(op, args);
		checkResult(result, expectedResult);
	}

	protected void executeOperation(OclValue value, String op, OclValue arg1, OclValue arg2, OclValue expectedResult) {
		assertNotNull(value);
		List args = new ArrayList();
		args.add(arg1);
		args.add(arg2);
		OclValue result = (OclValue) value.executeOperation(op, args);
		checkResult(result, expectedResult);
	}

	protected void checkResult(OclValue result, OclValue expectedResult) {
		if (!expectedResult.oclIsUndefined())
			assertTrue(expectedResult.equals(result));
		else
			assertTrue(expectedResult == result);
	}
}
