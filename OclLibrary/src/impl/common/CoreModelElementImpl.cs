using System;
using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.impl.common
{
    public abstract class CoreModelElementImpl : CoreModelElement, IComparable {
        protected abstract String super_getName();
	
        public virtual String getName() {
            return	super_getName();
        }

        public abstract void setName(string newValue);

        /* (non-Javadoc)
	 * @see java.lang.Comparable#compareTo(java.lang.Object)
	 */
        public int CompareTo(Object arg0) {
            int	compareClassNames = this.GetType().FullName.CompareTo(arg0.GetType().FullName);
		
            if (compareClassNames == 0)
                if (this.getName() != null && ((CoreModelElement) arg0).getName() != null)
                    return	this.getName().ToUpper().CompareTo(((CoreModelElement) arg0).getName().ToUpper());
                else
                    return	-1;
            else
                return	compareClassNames;			
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreModelElement#getElemOwner()
	 */
        public virtual CoreModelElement getElemOwner() {
            return	getSpecificOwnerElement();
        }

        public abstract void setElemOwner(CoreModelElement newValue);

        /* (non-Javadoc)
	 * @see ocl20.CoreModelElement#getElemOwnedElements()
	 */
        public virtual ICollection<object> getElemOwnedElements() {
            return	getSpecificOwnedElements();
        }
	
        public CoreModel getModel() {
            if (this.GetType() == typeof(CoreModel))
                return	(CoreModel) this;
            else {
                CoreModelElement ns = this.getElemOwner();
                while ((ns != null) && ! (ns.GetType() == typeof(CoreModel))) {
                    ns = ns.getElemOwner();
                }
                return	(CoreModel) ns;
            }
        }

        public bool hasStereotype(String stereotypeName) {
            if (getSpecificHasDirectStereotype()) {
                foreach (CoreStereotype stereotype in getSpecificStereotypes()) {
                    if (stereotypeName != null && stereotypeName.ToUpper().Equals(stereotype.getName().ToUpper()))
                        return	true;
                }
            } else {
                foreach (CoreStereotype stereotype in getModel().getAllStereotypes()) {
                    if (stereotype.getTheExtendedElement().Contains(this) && stereotypeName != null && stereotypeName.ToUpper().Equals(stereotype.getName().ToUpper()))
                        return	true;
                }
            }
		
            return false;
        }
	
	
        protected CoreModelElement getSpecificOwnerElement() {
            return	null;
        }

        protected List<object> getSpecificOwnedElements() {
            return	new List<object>();
        }
	
        protected bool getSpecificHasDirectStereotype() {
            return	false;
        }
	
        protected List<object> getSpecificStereotypes() {
            return	new List<object>();
        }
	
        protected List<object> adjustCollectionResult(List<object> aCollection) {
            if (aCollection != null)
                return	aCollection;
            else
                return	new List<object>();
        }

        protected List<object> adjustListResult(List<object> aCollection) {
            if (aCollection != null)
                return	new List<object>(aCollection);
            else
                return	new List<object>();
        }
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreModelElement#getConstraintExpressionInOcl()
	 */
        public List<object> getConstraintExpressionInOcl() {
            // TODO Auto-generated method stub
            return new List<object>();
        }

        public abstract List<object> getTheStereotypes();
    }
}
