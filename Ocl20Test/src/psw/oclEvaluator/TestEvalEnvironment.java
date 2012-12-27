/*
 * Created on 07/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import ocl20.evaluation.OclValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalEnvironment extends TestEval{

	private	IEvalEnvironment	env;

	public	void	setUp() throws Exception {
		super.setUp();
		env = new EvalEnvironment();
	}
	
	public	void	testAdd() {
		OclValue value1 = new OclIntegerValue(10);
		OclValue value2 = new OclIntegerValue(20);
		
		env.add("value1", value1);
		env.add("value2", value2);
		assertEquals(2, env.getAll().size());
		
		env.add("value1", value2);
	}

	public	void	testAddAll() {
		OclValue value1 = new OclIntegerValue(10);
		OclValue value2 = new OclIntegerValue(20);
		IEvalEnvironment e1 = new EvalEnvironment();
		e1.add("value1", value1);
		e1.add("value2", value2);
		
		env.addAll(e1);
		assertEquals(2, env.getAll().size());
	}

	public	void	testGetValueOf() {
		OclValue value1 = new OclIntegerValue(10);
		OclValue value2 = new OclIntegerValue(20);
		
		env.add("value1", value1);
		env.add("value2", value2);

		assertEquals(10, ((OclIntegerValue) env.getValueOf("value1")).intValue() );
		assertEquals(20, ((OclIntegerValue) env.getValueOf("value2")).intValue() );
		
		try {
			env.getValueOf("value3");
			fail();
		} catch (IllegalArgumentException e) {
		}
	}

	public	void	testReplace() {
		OclValue value1 = new OclIntegerValue(10);
		OclValue value2 = new OclIntegerValue(20);
		
		env.add("value1", value1);
		env.add("value2", value2);

		env.replace("value1", new OclIntegerValue(30));
		assertEquals(30, ((OclIntegerValue) env.getValueOf("value1")).intValue() );
		env.replace("value2", new OclIntegerValue(40));
		assertEquals(40, ((OclIntegerValue) env.getValueOf("value2")).intValue() );

		try {
			env.replace("value3", new OclIntegerValue(50));
			fail();
		} catch (IllegalArgumentException e) {
		}

	}
	
	

}
