/*
 * Created on May 5, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.List;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.IObjectSpace;

import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.evaluation.OclValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public interface OclObjectValue extends OclValue {
	OclValue	getValueOf(CoreAttribute	attribute);
	List getValueOf(CoreAssociationEnd   assocEnd);
	void	setValueOf(CoreAttribute	attribute, OclValue	value);
	OclValue	getValueOf(String	attribute);
	void	setValueOf(String	attribute, OclValue	value);
	GUID	getGUID();
	CoreClassifier getClassifier();
	IObjectSpace getObjSpace();
	OclValue executeSuperclassOperation(CoreOperation oper, List args);
	
	public List getEvaluatedPreConditions();
	public List getEvaluatedPostConditions();
}
