using OclLibrary.iface.common;

namespace OclLibrary.impl.environment
{
    public interface NamedElement {
        /**
	     * @return 
	     */
        CoreClassifier getType();
        /**
	     * Returns the value of attribute name.
	     * @return Value of attribute name.
	     */
        string getName();
        /**
	     * Sets the value of name attribute. See {@link #getName} for description 
	     * on the attribute.
	     * @param newValue New value to be set.
	     */
        void setName(string newValue);
        /**
	     * Returns the value of attribute mayBeImplicit.
	     * @return Value of attribute mayBeImplicit.
	     */
        bool isMayBeImplicit();
        /**
	     * Sets the value of mayBeImplicit attribute. See {@link #isMayBeImplicit} 
	     * for description on the attribute.
	     * @param newValue New value to be set.
	     */
        void setMayBeImplicit(bool newValue);
        /**
	     * Returns the value of reference referredElement.
	     * @return Value of reference referredElement.
	     */
        object getReferredElement();
        /**
	     * Sets the value of reference referredElement. See {@link #getReferredElement} 
	     * for description on the reference.
	     * @param newValue New value to be set.
	     */
        void setReferredElement(object newValue);
    }
}
