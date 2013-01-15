using Ocl20.library.iface.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.impl.common
{
    public class CoreFeatureImpl : CoreModelElementImpl, CoreFeature
    {
        private ScopeKind ownerScope;

        public CoreFeatureImpl() 
        {
            ownerScope = null;
        }

        public virtual CoreClassifier getFeatureOwner() {
            CoreModelElement owner = getSpecificOwnerElement();
            if (owner != null && owner.GetType() == typeof(CoreClassifierImpl)) 
                return	(CoreClassifier) owner;
            else
                return	null;
        }

        public virtual void setFeatureOwner(CoreClassifier newValue)
        {
            setFeatureOwner(newValue);
        }
        
        public ScopeKind getOwnerScope()
        {
            return ownerScope;
        }

        public void setOwnerScope(ScopeKind newValue)
        {
            ownerScope = newValue;
        }

        public virtual bool isInstanceScope() {
            return getSpecificIsInstanceScope();
        }

        public virtual bool isOclDefined() {
            return	false;
        }
	
        public virtual bool getSpecificIsInstanceScope() {
            return	true;
        }
    }
}
