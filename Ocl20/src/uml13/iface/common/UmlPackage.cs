/**
 * Package object instance interface.
 */

using Ocl20.uml13.iface.foundation;

namespace Ocl20.uml13.iface.common
{
    public interface UmlPackage {
        FoundationPackage getFoundation();
        ModelManagementPackage getModelManagement();
        //BehavioralElementsPackage getBehavioralElements();
    }
}
