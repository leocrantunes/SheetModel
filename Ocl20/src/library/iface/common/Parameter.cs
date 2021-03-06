/**
 * Parameter object instance interface.
 */

using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.iface.common
{
    public interface Parameter : CoreModelElement {
        /**
     * Returns the value of attribute defaultValue.
     * @return Value of attribute defaultValue.
     */
        Expression getDefaultValue();
        /**
     * Sets the value of defaultValue attribute. See {@link #getDefaultValue} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setDefaultValue(Expression newValue);
        /**
     * Returns the value of attribute kind.
     * @return Value of attribute kind.
     */
        ParameterDirectionKind getKind();
        /**
     * Sets the value of kind attribute. See {@link #getKind} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setKind(ParameterDirectionKind newValue);
        /**
     * Returns the value of reference behavioralFeature.
     * @return Value of reference behavioralFeature.
     */
        CoreBehavioralFeature getBehavioralFeature();
        /**
     * Sets the value of reference behavioralFeature. See {@link #getBehavioralFeature} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setBehavioralFeature(CoreBehavioralFeature newValue);
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
    }
}
