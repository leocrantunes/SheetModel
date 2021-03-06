using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.utils;

namespace OclLibrary.impl.common
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
	
        protected List<object> getSpecificAllDirectImplementors() {
            return	new	List<object>();
        }
    }
}
