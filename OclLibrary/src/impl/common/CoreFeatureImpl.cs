using OclLibrary.iface.common;

namespace OclLibrary.impl.common
{
    public abstract class CoreFeatureImpl : CoreModelElementImpl, CoreFeature {
        public virtual CoreClassifier getFeatureOwner() {
            CoreModelElement owner = getSpecificOwnerElement();
            if (owner != null && owner.GetType() == typeof(CoreClassifier)) 
                return	(CoreClassifier) owner;
            else
                return	null;
        }

        public abstract void setFeatureOwner(CoreClassifier newValue);

        /* (non-Javadoc)
	 * @see ocl20.CoreFeature#isInstanceScope()
	 */
        public virtual bool isInstanceScope() {
            return getSpecificIsInstanceScope();
        }

        public virtual bool isOclDefined() {
            return	false;
        }
	
        public bool getSpecificIsInstanceScope() {
            return	true;
        }
    }
}
