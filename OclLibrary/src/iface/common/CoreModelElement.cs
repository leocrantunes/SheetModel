/**
 * CoreModelElement object instance interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OclLibrary.iface.common
{
    public interface CoreModelElement {
        /**
     * @return 
     */
        CoreModel getModel();
        /**
     * @param name 
     * @return 
     */
        bool hasStereotype(string name);
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
     * Returns the value of reference elemOwnedElements.
     * @return Value of reference elemOwnedElements.
     */
        ICollection<object> getElemOwnedElements();
        /**
     * Returns the value of reference elemOwner.
     * @return Value of reference elemOwner.
     */
        CoreModelElement getElemOwner();
        /**
     * Sets the value of reference elemOwner. See {@link #getElemOwner} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setElemOwner(CoreModelElement newValue);
        /**
     * Returns the value of reference constraintExpressionInOCL.
     * @return Value of reference constraintExpressionInOCL.
     */
        List<object> getConstraintExpressionInOcl();
        /**
     * Returns the value of reference theStereotypes.
     * @return Value of reference theStereotypes.
     */
        List<object> getTheStereotypes();
    }
}
