/**
 * CoreFeature object instance interface.
 */

using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.iface.common
{
    public interface CoreFeature : CoreModelElement {
       
        bool isInstanceScope();
        bool isOclDefined();
        CoreClassifier getFeatureOwner();
        void setFeatureOwner(CoreClassifier newValue);
        ScopeKind getOwnerScope();
        void setOwnerScope(ScopeKind newValue);
    }
}
