using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.parser.semantics.types;
using CorePackage = Ocl20.library.iface.common.CorePackage;

namespace Ocl20.library.impl.common
{
    public class CorePackageImpl : CoreNamespaceImpl, CorePackage {

        /* (non-Javadoc)
	 * @see implocl20.CoreNamespaceImpl#elementShouldBeAddedToEnvironment(ocl20.CoreModelElement)
	 */
        public override List<object> getElementsForEnvironment()
        {
            throw new NotImplementedException();
        }

        protected override bool elementShouldBeAddedToEnvironment(CoreModelElement element) {
            return (element.getName() != null) &&
                   (element is CorePackageImpl || element is CoreClassifierImpl) &&
                   (! primitiveTypeRedeclaration(element) && (! isUseCaseElement(element)));
        }
	
        protected bool primitiveTypeRedeclaration(CoreModelElement element) {
            if (element is CoreClassifierImpl) {
                CoreClassifier cls = (CoreClassifier) element;
                if (OclTypesDefinition.isOclType(element.getName()) &&
                    ! "PrimitiveTypes".Equals(this.getName()) &&
                    cls.getModel().getEnvironmentWithoutParents().lookup(element.getName()) != null)
                    return	true;
            }
            return	false;
        }
	
        protected bool isUseCaseElement(CoreModelElement element) {
            if (element.getName().StartsWith("Operador")) {
                Console.WriteLine("has stereotype: " + ((CoreClassifier) element).hasStereotype("Actor"));
            }
            return ((element is CoreClassifierImpl) && ((CoreClassifier) element).hasStereotype("Actor"));
        }


        protected override CoreModelElement getSpecificOwnerElement()
        {
            return (CoreModelElement)getNamespace();
        }

        public override ICollection<object> getSpecificOwnedElements()
        {
            return this.getElemOwnedElements();
        }

    }
}
