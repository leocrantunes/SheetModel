using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.common
{
    public class CoreAttributeImpl : CoreStructuralFeatureImpl, CoreAttribute, CoreEnumLiteral {

        private ExpressionInOcl initialValue;
        private ExpressionInOcl	derivedValue;

        public CoreAttributeImpl()
        {
            initialValue = null;
            derivedValue = null;
        }
    
        public CoreEnumeration getTheEnumeration() {
            CoreClassifier featureOwner = getFeatureOwner();
            if (featureOwner != null && featureOwner.isEnumeration())
                return (CoreEnumeration) getFeatureOwner();
            else
                return	null;
        }

        public void setTheEnumeration(CoreEnumeration newValue)
        {
            setFeatureOwner(newValue);
        }

        public CoreAssociationEnd getTheAssociationEnd() {
            return getSpecificAssociationEnd();
        }
	
        public bool isDerived() {
            return	this.derivedValue != null || getSpecificIsDerived() || this.getFeatureOwner().getDeriveConstraint(this.getName()) != null? true : false;
        }

        public ExpressionInOcl getDerivedValueExpression() {
            return this.derivedValue;
        }

        public ExpressionInOcl getInitialValueExpression() {
            return this.initialValue;
        }

        public void setDerivedValueExpression(ExpressionInOcl expression) {
            this.derivedValue = expression;
        }

        public void setInitialValueExpression(ExpressionInOcl expression) {
            this.initialValue = expression;
        }
    
        public override ICollection<object> getElemOwnedElements() {
            List<object> allOwnedElements = new List<object>();
            OclConstraint constraint;
		
            constraint = getFeatureOwner().getInitConstraint(this.getName());
            if (constraint != null)
                allOwnedElements.Add(constraint);
			
            constraint = getFeatureOwner().getDeriveConstraint(this.getName());
            if (constraint != null)
                allOwnedElements.Add(constraint);

            return 	allOwnedElements;
        }

        protected virtual CoreAssociationEnd getSpecificAssociationEnd() {
            return	null;
        }

        public List<object> getDeriveConstraint() {
            return null;
        }

        public List<object> getInitConstraint() {
            return null;
        }

        public void setTheAssociationEnd(CoreAssociationEnd newValue) 
        {}

        #region from uml13

        public override bool getSpecificIsInstanceScope()
        {
            return (getOwnerScope() == ScopeKindEnum.SK_INSTANCE);
        }

        public override CoreClassifier getSpecificType()
        {
            return getFeatureType();
        }

        public bool getSpecificIsDerived()
        {
            String name = getName();

            return (name[0] == '/');
        }
        
        public override String getName()
        {
            String name = base.getName();

            if (name == null)
                return name;
            else
                return (name[0] == '/') ?
                           name.Substring(1, name.Length - 1) :
                           name;
        }
        
        #endregion

    }
}