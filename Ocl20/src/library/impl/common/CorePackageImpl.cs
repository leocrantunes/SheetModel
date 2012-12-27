using System;
using Ocl20.library.iface.common;
using Ocl20.parser.semantics.types;

namespace Ocl20.library.impl.common
{
    public abstract class CorePackageImpl : CoreNamespaceImpl, CorePackage {

        /* (non-Javadoc)
	 * @see implocl20.CoreNamespaceImpl#elementShouldBeAddedToEnvironment(ocl20.CoreModelElement)
	 */
        protected override bool elementShouldBeAddedToEnvironment(CoreModelElement element) {
            return (element.getName() != null) &&
                   (element.GetType() == typeof(CorePackage) || element.GetType() == typeof(CoreClassifier)) &&
                   (! primitiveTypeRedeclaration(element) && (! isUseCaseElement(element)));
        }
	
        protected bool primitiveTypeRedeclaration(CoreModelElement element) {
            if (element.GetType() == typeof(CoreClassifier)) {
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
            return ((element.GetType() == typeof(CoreClassifier)) && ((CoreClassifier) element).hasStereotype("Actor"));
        }	
    }
}
