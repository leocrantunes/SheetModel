using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.foundation.extensionmechanisms;

namespace Ocl20.uml13.impl.foundation.extensionmechanisms
{
    public abstract class StereotypeImpl : CoreModelElementImpl, Stereotype, CoreStereotype {

        public List<object> getTheExtendedElement() {
            return	getExtendedElement();
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract bool isAbstract();
        public abstract List<object> getGeneralizations();
        public abstract List<object> getSpecialization();
        public abstract List<object> getExtendedElement();
    }
}
