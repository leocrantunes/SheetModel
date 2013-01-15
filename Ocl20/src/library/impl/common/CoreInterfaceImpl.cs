using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.utils;

namespace Ocl20.library.impl.common
{
    public abstract class CoreInterfaceImpl : CoreClassifierImpl, CoreInterface {
        /* (non-Javadoc)
	 * @see ocl20.CoreInterface#getAllDirectImplementors()
	 */
        public List<object> getAllDirectImplementors() {
            return adjustCollectionResult(getSpecificAllDirectImplementors());
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreInterface#getAllImplementors()
	 */
        public List<object> getAllImplementors() {
            HashSet<object> result = new HashSet<object>();

            List<object> directImplementors = this.getAllDirectImplementors();
            result.AddRange(directImplementors);

            foreach (CoreClassifier cls in directImplementors) {
                result.AddRange(cls.getAllSubClasses());
            }

            List<object> directSubClasses = this.getAllDirectSubClasses();

            foreach (CoreInterface derivedInterface in directSubClasses) {
                result.AddRange(derivedInterface.getAllImplementors());
            }

            return new List<object>(result);
        }
	
        protected override bool getSpecificIsConcrete()
        {
            return false;
        }

        protected List<object> getSpecificAllDirectImplementors()
        {
            List<object> result = new List<object>();

            foreach (Dependency realization in getSupplierDependency()) {
                foreach (object i in realization.getClient()) {
                    result.Add(i);
                }
            }

            return result;
        }

        protected CoreAttribute createSpecificAttribute(String name, CoreClassifier type)
        {
            CoreElementFactory umlFactory = new CoreElementFactory();
            return (CoreAttribute)umlFactory.createSpecificAttribute(this, name, type);
        }

        protected CoreOperation createSpecificOperation(String name, List<object> paramNames, List<object> paramTypes, CoreClassifier returnType)
        {
            CoreElementFactory umlFactory = new CoreElementFactory();
            return (CoreOperation)umlFactory.createSpecificOperation(this, name, paramNames, paramTypes, returnType);
        }

        protected void createSpecificStereotype(CoreFeature feature, String stereotypeName)
        {
            CoreElementFactory umlFactory = new CoreElementFactory();
            umlFactory.createSpecificStereotype(this, feature, stereotypeName);
        }
    }
}
