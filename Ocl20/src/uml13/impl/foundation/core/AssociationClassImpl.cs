using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.impl.foundation.core
{
    public abstract class AssociationClassImpl : CoreAssociationClassImpl, AssociationClass {
         
        public override List<object> getSpecificAssociationEnds() {
            return	getConnection();
        }

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getNamespace();
        }
    


        protected void createSpecificStereotype(CoreFeature feature, String stereotypeName)  {
            CoreElementFactory umlFactory = new CoreElementFactory();
            umlFactory.createSpecificStereotype(this, feature, stereotypeName);
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract List<object> getOwnedElement();
        public abstract List<object> getConnection();
        public abstract List<object> getFeature();
        public abstract List<object> getParticipant();
        public abstract bool isActive();
        public abstract void setActive(bool newValue);
        public abstract bool isAbstract();
        public abstract List<object> getGeneralizations();
        public abstract List<object> getSpecialization();
    }
}
