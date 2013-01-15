/**
 * CoreModelElement object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreModelElement {

        CoreModel getModel();
        bool hasStereotype(string name);
        string getName();
        void setName(string newValue);
        ICollection<object> getElemOwnedElements();
        void setElemOwnedElements(List<object> newValue);
        CoreModelElement getElemOwner();
        void setElemOwner(CoreModelElement newValue);
        List<object> getConstraintExpressionInOcl();
        List<CoreStereotype> getTheStereotypes();
        void setTheStereotypes(List<CoreStereotype> newValue);
        CoreNamespace getNamespace();
        void setNamespace(CoreNamespace newValue);
        List<object> getConnection();
        void setConnection(List<object> newValue);
        List<object> getClientDependency();
        void setClientDependency(List<object> newValue);
    }
}
