/**
 * OclInvariantConstraint object instance interface.
 */

using OclLibrary.iface.constraints;

public interface OclInvariantConstraint : OclClassifierConstraint {
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

    object Clone();
}
