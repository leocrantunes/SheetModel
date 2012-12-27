/**
 * ExpressionInOCL object instance interface.
 */

using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.iface.constraints
{
    public interface ExpressionInOcl { // extends javax.jmi.reflect.RefObject {
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
     * Returns the value of reference contextualElement.
     * @return Value of reference contextualElement.
     */
        CoreModelElement getContextualElement();
        /**
     * Sets the value of reference contextualElement. See {@link #getContextualElement} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setContextualElement(CoreModelElement newValue);
        /**
     * Returns the value of reference constraint.
     * @return Value of reference constraint.
     */
        OclConstraint getConstraint();
        /**
     * Sets the value of reference constraint. See {@link #getConstraint} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setConstraint(OclConstraint newValue);
    
        OclExpression getBodyExpression();
	
        void setBodyExpression(OclExpression bodyExpression);

        Object Clone();
    }
}
