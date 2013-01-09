/**
 * StructuralFeature object instance interface.
 */

using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface StructuralFeature : Feature {
        /**
     * Returns the value of attribute multiplicity.
     * @return Value of attribute multiplicity.
     */
        Multiplicity getMultiplicity();
        /**
     * Sets the value of multiplicity attribute. See {@link #getMultiplicity} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setMultiplicity(Multiplicity newValue);
        /**
     * Returns the value of attribute changeability.
     * @return Value of attribute changeability.
     */
        ChangeableKind getChangeability();
        /**
     * Sets the value of changeability attribute. See {@link #getChangeability} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setChangeability(ChangeableKind newValue);
        /**
     * Returns the value of attribute targetScope.
     * @return Value of attribute targetScope.
     */
        ScopeKind getTargetScope();
        /**
     * Sets the value of targetScope attribute. See {@link #getTargetScope} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        void setTargetScope(ScopeKind newValue);
        /**
     * Returns the value of reference type.
     * @return Value of reference type.
     */
        Classifier getType();
        /**
     * Sets the value of reference type. See {@link #getType} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setType(Classifier newValue);
    }
}
