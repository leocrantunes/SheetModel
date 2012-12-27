using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.impl.expressions;
using CoreAssociationEnd = OclLibrary.iface.common.CoreAssociationEnd;

public class AssociationClassCallExpImpl : NavigationCallExpImpl, AssociationClassCallExp {

	private	CoreAssociationClass	referredAssociationClass;
	
	/**
	 * @param object
	 */
//	public AssociationClassCallExpImpl(StorableObject object) {
//		super(object);
//	}

	public AssociationClassCallExpImpl() {
	}

	public CoreClassifier getExpressionType(OclExpression source, CoreAssociationEnd associationEnd, CoreAssociationClass associationClass) {
		return	base.getExpressionType(source.getType(), associationEnd, associationClass);
	}
	
	protected override String getSpecificString() {
		return	this.getReferredAssociationClass().getName();
	}
	
	
	/**
	 * @return Returns the referredAssociationClass.
	 */
	public CoreAssociationClass getReferredAssociationClass() {
		return referredAssociationClass;
	}
	/**
	 * @param referredAssociationClass The referredAssociationClass to set.
	 */
	public void setReferredAssociationClass(
			CoreAssociationClass referredAssociationClass) {
		this.referredAssociationClass = referredAssociationClass;
	}
	
	public override Object Clone() {
		AssociationClassCallExpImpl theClone = (AssociationClassCallExpImpl) base.Clone();
		theClone.referredAssociationClass = referredAssociationClass;
		return	theClone;
	}
}
