using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.modelmanagement;

namespace Ocl20.uml13.impl.foundation.core
{
    public abstract class UmlPackageImpl : CorePackageImpl, UmlPackage {

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) this.getNamespace();
        }

        public override List<object> getSpecificOwnedElements() {
            return	getOwnedElement();
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract List<object> getOwnedElement();
        public abstract bool isAbstract();
        public abstract List<object> getGeneralizations();
        public abstract List<object> getSpecialization();
    }
}
