/**
 * Attribute object instance interface.
 */

using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Attribute : StructuralFeature {
        /**
     * Returns the value of attribute initialValue.
     * @return Value of attribute initialValue.
     */
        //Expression getInitialValue();
        /**
     * Sets the value of initialValue attribute. See {@link #getInitialValue} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        //void setInitialValue(Expression newValue);
        /**
     * Returns the value of reference associationEnd.
     * @return Value of reference associationEnd.
     */
        AssociationEnd getAssociationEnd();
        /**
     * Sets the value of reference associationEnd. See {@link #getAssociationEnd} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        //void setAssociationEnd(AssociationEnd newValue);
    }
}
