/*
 * Created on Jul 10, 2004
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
public interface OclOrderedCollection {
	public OclOrderedCollection insertAt(OclIntegerValue index, OclValue value);
	public OclValue  at(OclIntegerValue i);
	public OclIntegerValue size();
}
