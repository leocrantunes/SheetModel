using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.types;
using Ocl20.library.impl.common;

namespace Ocl20.library.impl.types
{
    public class OclModelElementTypeImpl : CoreClassifierImpl, OclModelElementType
    {
        private CoreModelElement referredModelElement;

        public override String ToString() {
            return this.getReferredModelElement().getName();
        }

        public override String getName() {
            return "OclModelElementType";
        }

        public override bool conformsTo(CoreClassifier c) {
            return (c is OclModelElementTypeImpl) || (c.getName().Equals("OclType"));
        }

        public CoreModelElement getReferredModelElement()
        {
            return referredModelElement;
        }

        public void setReferredModelElement(CoreModelElement newValue)
        {
            referredModelElement = newValue;
        }
    }
}
