/**
 * CollectionLiteralPart object instance interface.
 */

using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface CollectionLiteralPart : OclModelElement {
        /**
     * Returns the value of reference type.
     * @return Value of reference type.
     */
        CoreClassifier getType();
        /**
     * Sets the value of reference type. See {@link #getType} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setType(CoreClassifier newValue);
        /**
     * Returns the value of reference literalExp.
     * @return Value of reference literalExp.
     */
        CollectionLiteralExp getLiteralExp();
        /**
     * Sets the value of reference literalExp. See {@link #getLiteralExp} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setLiteralExp(CollectionLiteralExp newValue);
    }
}
