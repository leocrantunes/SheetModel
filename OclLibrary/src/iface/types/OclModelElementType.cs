/**
 * OclModelElementType object instance interface.
 */

using OclLibrary.iface.common;

namespace OclLibrary.iface.types
{
    public interface OclModelElementType : CoreClassifier {
        /**
     * Returns the value of reference referredModelElement.
     * @return Value of reference referredModelElement.
     */
        CoreModelElement getReferredModelElement();
        /**
     * Sets the value of reference referredModelElement. See {@link #getReferredModelElement} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setReferredModelElement(CoreModelElement newValue);
    }
}
