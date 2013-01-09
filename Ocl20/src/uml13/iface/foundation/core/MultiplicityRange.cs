/**
 * MultiplicityRange object instance interface.
 */

namespace Ocl20.uml13.iface.foundation.core
{
    public interface MultiplicityRange {
        /**
     * Returns the value of attribute lower.
     * @return Value of attribute lower.
     */
        int getLower();
        /**
     * Sets the value of lower attribute. See {@link #getLower} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setLower(int newValue);
        /**
     * Returns the value of attribute upper.
     * @return Value of attribute upper.
     */
        int getUpper();
        /**
     * Sets the value of upper attribute. See {@link #getUpper} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setUpper(int newValue);
        /**
     * Returns the value of reference multiplicity.
     * @return Value of reference multiplicity.
     */
        Multiplicity getMultiplicity();
        /**
     * Sets the value of reference multiplicity. See {@link #getMultiplicity} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setMultiplicity(Multiplicity newValue);
    }
}
