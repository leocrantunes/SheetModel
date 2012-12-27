/*
 * Created on Oct 3, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.List;

import ocl20.evaluation.OclValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class OclNullValue extends OclUndefinedValue {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public OclNullValue() {
		setCoreType(OclTypesFactory.getType("OclVoid"));
	}
	
	public OclValue executeOperation(
			String opName,
			List arguments) {
		
			if (isArgumentUndefined(arguments)) {
				return	(OclValue) arguments.get(0);
			}

			try {
				if (opName.equals("oclIsNull")) {
					return	getOclValuesFactory().createBooleanValue(true);
				} else if (opName.equals("oclIsInvalid")) {
					return	getOclValuesFactory().createBooleanValue(false);
				} else if (opName.equals("=")) {
				   return	this.equal((OclValue) arguments.get(0));
				} else if (opName.equals("<>")) {
				   return	this.notEqual((OclValue) arguments.get(0));
				} else if (opName.equals("asSet")) {
					return	this.asSet();
				} else
					return	super.executeOperation(opName, arguments);
			} catch (RuntimeException e) {
				return	getOclValuesFactory().createNullValue();
			}
		}

		protected	OclValue	equal(OclValue arg) {
			return	getReturnValue(arg);
		}

		protected	OclValue	notEqual(OclValue arg) {
			return	getReturnValue(arg);
		}

		protected 	OclValue	asSet() {
			return OclValuesFactory.getInstance().createCollectionValue(OclTypesFactory.createSetType(this.getType()));
		}
		
		public String toString() {
			return "null";
		}
}
