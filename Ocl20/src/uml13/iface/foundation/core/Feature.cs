/**
 * Feature object instance interface.
 */

using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Feature : ModelElement {
        /**
     * Returns the value of attribute ownerScope.
     * @return Value of attribute ownerScope.
     */
        ScopeKind getOwnerScope();
        /**
     * Sets the value of ownerScope attribute. See {@link #getOwnerScope} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        //void setOwnerScope(ScopeKind newValue);
        /**
     * Returns the value of reference owner.
     * @return Value of reference owner.
     */
        Classifier getOwner();
        /**
     * Sets the value of reference owner. See {@link #getOwner} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        //void setOwner(Classifier newValue);
    }
}
