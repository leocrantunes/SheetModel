using Ocl20.library.iface.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.impl.common
{
    public class CoreFeatureImpl : CoreModelElementImpl, CoreFeature
    {
        private ScopeKind ownerScope;

        public CoreFeatureImpl() 
        {
            ownerScope = ScopeKindEnum.SK_INSTANCE;
        }

        public virtual CoreClassifier getFeatureOwner() {
            CoreModelElement owner = getElemOwner();
            if (owner != null && owner is CoreClassifierImpl) 
                return	(CoreClassifier) owner;
            else
                return	null;
        }

        public virtual void setFeatureOwner(CoreClassifier newValue)
        {
            setElemOwner(newValue);
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
