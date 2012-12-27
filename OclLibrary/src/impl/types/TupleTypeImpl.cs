using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OclLibrary.iface.common;
using OclLibrary.iface.types;
using OclLibrary.iface.util;
using OclLibrary.impl.common;
using Environment = OclLibrary.iface.environment.Environment;

public abstract class TupleTypeImpl : CoreClassifierImpl, TupleType {
	private	List<object> tupleParts;

	/**
	 * 
	 */
	public TupleTypeImpl() {
		tupleParts = new List<object>();
	}

	public void addElement(String name, CoreClassifier type) {
		tupleParts.Add(getFactory().createTuplePartType(this, name, type));
	}

		/* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#conformsTo(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
	 */
	public override bool conformsTo(CoreClassifier c) {
		if (c.getName().Equals("OclAny"))
			return	true;

		if (c.GetType() != typeof(TupleType)) {
			return	false;
		}

		TupleType	typeToMatch = (TupleType) c;
		
		if (this.getTupleParts().Count !=  typeToMatch.getTupleParts().Count) {
			return	false;
		}

		List<object> tupleTypes = new List<object>(getTupleParts());
		
		for (int i = 0; i < tupleTypes.Count; i++) {
			TuplePartType element = (TuplePartType) tupleTypes[i];
			CoreAttribute elementFound = typeToMatch.lookupAttribute(element.getName());
			if (elementFound == null || ! element.getFeatureType().conformsTo(elementFound.getFeatureType()))
				return	false;				 
		}
		return	true;
	}

	/* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#lookupAttribute(java.lang.String)
	 */
	public override CoreAttribute lookupAttribute(String attName) {
		foreach (TuplePartType element in getTupleParts()) {
			if (element.getName().Equals(attName))
				return	element;
		}
		
		return	null;
	}
	
	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getAllAttributes()
	 */
	public override ICollection<object> getAllAttributes() {
		return new List<object>(getTupleParts());
	}


	/* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement#getName()
	 */
	public override String getName() {
		StringBuilder partsNames = new StringBuilder();
		
		List<object> tupleTypes = new List<object>(getTupleParts());
		
		if (tupleTypes.Count > 0) { 
			appendTuplePart(partsNames, (TuplePartType) tupleTypes[0]);
		}
		
		for (int i = 1; i < getTupleParts().Count; i++ ) {
			partsNames.Append(", ");
			appendTuplePart(partsNames, (TuplePartType) tupleTypes[i]);
		}
		
		return "Tuple" + "(" + partsNames.ToString() + ")";		
	}

	private	void   appendTuplePart(StringBuilder partsNames, TuplePartType tuplePart) {
		partsNames.Append(tuplePart.getName());
		partsNames.Append(" : ");
		partsNames.Append(tuplePart.getFeatureType().getName());
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getMostSpecificCommonSuperType(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
	 */
	public override CoreClassifier getMostSpecificCommonSuperType(CoreClassifier otherClassifier) {
		if (this.conformsTo(otherClassifier)) {
			return	otherClassifier;
		} else if (otherClassifier.conformsTo(this)) {
			return	this;
		} else
			return (CoreClassifier) OclTypesDefinition.getType("OclAny");
	}


	public String toString() {
		return	this.getName();
	}		
	
	/* (non-Javadoc)
	 * @see ocl20.types.TupleType#getTupleParts()
	 */
    public abstract AstOclModelElementFactory getFactory();
    public abstract void setFactory(AstOclModelElementFactory newValue);

    public List<object> getTupleParts() {
		return	tupleParts;
	}
	
	public override Environment getEnvironmentWithoutParents() {
		return	OclTypesDefinition.getType("OclAny").getEnvironmentWithoutParents();
	}

}
