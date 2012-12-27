/*
 * Created on Jun 28, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;


import ocl20.evaluation.OclValue;

/**
 * @author Administrator
 * 
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
abstract public class OclPrimitiveValue extends OclStaticValue {

	protected OclValue equal(OclValue arg) {
		if (arg instanceof OclNullValue) {
			return arg;
		} else {
			return super.equal(arg);
		}
	}

}
