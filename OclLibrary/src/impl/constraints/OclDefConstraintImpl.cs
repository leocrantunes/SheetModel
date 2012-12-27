using System;
using OclLibrary.iface.constraints;
using OclLibrary.impl.constraints;

public class OclDefConstraintImpl : OclClassifierConstraintImpl, OclDefConstraint {

	/**
	 * 
	 */
	public OclDefConstraintImpl() {
	}

	public String toString() {
		return	"def: " + this.getExpression().ToString();
	}
}

