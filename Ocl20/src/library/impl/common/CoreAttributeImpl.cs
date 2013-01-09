using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.common
{
    public abstract class CoreAttributeImpl : CoreStructuralFeatureImpl, CoreAttribute, CoreEnumLiteral {

        private ExpressionInOcl initialValue = null;
        private ExpressionInOcl	derivedValue = null;
    
        /* (non-Javadoc)
	 * @see ocl20.CoreEnumLiteral#getTheEnumeration()
	 */
        public CoreEnumeration getTheEnumeration() {
            CoreClassifier featureOwner = getFeatureOwner();
            if (featureOwner != null && featureOwner.isEnumeration())
                return (CoreEnumeration) getFeatureOwner();
            else
                return	null;
        }

        public abstract void setTheEnumeration(CoreEnumeration newValue);

        /* (non-Javadoc)
	 * @see ocl20.CoreAttribute#getTheAssociationEnd()
	 */
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
	
        public virtual bool getSpecificIsDerived() {
            return	false;
        }

        /* (non-Javadoc)
	 * @see ocl20.common.CoreAttribute#getDeriveConstraint()
	 */
        public List<object> getDeriveConstraint() {
            // TODO Auto-generated method stub
            return null;
        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreAttribute#getInitConstraint()
	 */
        public List<object> getInitConstraint() {
            // TODO Auto-generated method stub
            return null;
        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreAttribute#setTheAssociationEnd(ocl20.common.CoreAssociationEnd)
	 */
        public void setTheAssociationEnd(CoreAssociationEnd newValue) {
            // TODO Auto-generated method stub
        }

    }
}