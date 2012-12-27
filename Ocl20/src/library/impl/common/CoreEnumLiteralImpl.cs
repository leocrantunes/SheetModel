using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public abstract class CoreEnumLiteralImpl : CoreModelElementImpl, CoreEnumLiteral {
        /* (non-Javadoc)
	 * @see ocl20.CoreEnumLiteral#getTheEnumeration()
	 */
        public CoreEnumeration getTheEnumeration() {
            return (CoreEnumeration) getSpecificEnumeration();
        }

        public abstract void setTheEnumeration(CoreEnumeration newValue);

        protected CoreEnumeration getSpecificEnumeration() {
            return	null;
        }
    }
}
