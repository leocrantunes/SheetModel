using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.impl.foundation.core
{
    public class ModelElementImpl : CoreModelElementImpl, ModelElement
    {
        protected override string super_getName()
        {
            throw new System.NotImplementedException(); 
        }

        public override void setName(string newValue)
        {
            throw new System.NotImplementedException();
        }

        public Namespace getNamespace()
        {
            throw new System.NotImplementedException();
        }

        public void setNamespace(Namespace newValue)
        {
            throw new System.NotImplementedException();
        }

        public List<object> getClientDependency()
        {
            throw new System.NotImplementedException();
        }

        public List<object> getSupplierDependency()
        {
            throw new System.NotImplementedException();
        }

        public override void setElemOwner(CoreModelElement newValue)
        {
            throw new System.NotImplementedException();
        }

        public override List<object> getTheStereotypes()
        {
            throw new System.NotImplementedException();
        }

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getNamespace();
        }
	
        public override List<object> getSpecificOwnedElements() {
            return	new List<object>();
        }

    }
}
