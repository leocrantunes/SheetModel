using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.modelmanagement;

namespace Ocl20.uml13.impl.modelmanagement
{
    public class ModelImpl : CoreModelImpl, Model {
        
        protected override string super_getName()
        {
            throw new NotImplementedException();
        }

        public override void setName(string newValue)
        {
            throw new NotImplementedException();
        }

        public Namespace getNamespace()
        {
            throw new NotImplementedException();
        }

        public override void setElemOwner(CoreModelElement newValue)
        {
            throw new NotImplementedException();
        }

        public override List<object> getTheStereotypes()
        {
            throw new NotImplementedException();
        }

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getNamespace();
        }

        public override List<object> getSpecificOwnedElements() {
            return	getOwnedElement();
        }

        public void setNamespace(Namespace newValue)
        {
            throw new NotImplementedException();
        }

        public List<object> getClientDependency()
        {
            throw new NotImplementedException();
        }

        public List<object> getSupplierDependency()
        {
            throw new NotImplementedException();
        }

        public List<object> getOwnedElement()
        {
            throw new NotImplementedException();
        }
        
        public override List<object> getElementsForEnvironment()
        {
            throw new NotImplementedException();
        }

        public bool isAbstract()
        {
            throw new NotImplementedException();
        }

        public List<object> getGeneralizations()
        {
            throw new NotImplementedException();
        }

        public List<object> getSpecialization()
        {
            throw new NotImplementedException();
        }
    }
}
