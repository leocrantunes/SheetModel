using System.Collections.Generic;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.impl.foundation.core
{
    public abstract class UmlAssociationImpl : CoreAssociationImpl, UmlAssociation {
    
        public override List<object> getSpecificAssociationEnds() {
            return	getConnection();
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract List<object> getConnection();
        public abstract bool isAbstract();
        public abstract List<object> getGeneralizations();
        public abstract List<object> getSpecialization();
    }
}
