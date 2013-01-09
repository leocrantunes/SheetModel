/**
 * Class object instance interface.
 */

namespace Ocl20.uml13.iface.foundation.core
{
    public interface UmlClass : Classifier {
        /**
     * Returns the value of attribute isActive.
     * @return Value of attribute isActive.
     */
        bool isActive();
        /**
     * Sets the value of isActive attribute. See {@link #isActive} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setActive(bool newValue);
    }
}
