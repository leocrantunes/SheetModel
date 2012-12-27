/**
 * OclPreConstraint object instance interface.
 */

namespace OclLibrary.iface.constraints
{
    public interface OclPreConstraint : OclOperationConstraint {
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
     * Returns the value of reference owner.
     * @return Value of reference owner.
     */
        OclPrePostConstraint getOwner();
        /**
     * Sets the value of reference owner. See {@link #getOwner} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setOwner(OclPrePostConstraint newValue);
    }
}
