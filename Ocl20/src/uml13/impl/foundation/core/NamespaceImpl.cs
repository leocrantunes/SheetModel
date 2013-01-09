using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.impl.foundation.core
{
    public abstract class NamespaceImpl : CoreNamespaceImpl, Namespace {

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getNamespace();
        }

        public override List<object> getSpecificOwnedElements() {
            return	getOwnedElement();
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract List<object> getOwnedElement();
    }
}
