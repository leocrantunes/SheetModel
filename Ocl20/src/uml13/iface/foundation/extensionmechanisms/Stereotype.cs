/**
 * Stereotype object instance interface.
 */

using System;
using System.Collections.Generic;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.iface.foundation.extensionmechanisms
{
    public interface Stereotype : GeneralizableElement {
        /**
     * Returns the value of attribute icon.
     * @return Value of attribute icon.
     */
        //String getIcon();
        /**
     * Sets the value of icon attribute. See {@link #getIcon} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setIcon(String newValue);
        /**
     * Returns the value of attribute baseClass.
     * @return Value of attribute baseClass.
     */
        //String getBaseClass();
        /**
     * Sets the value of baseClass attribute. See {@link #getBaseClass} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setBaseClass(String newValue);
        /**
     * Returns the value of reference requiredTag.
     * @return Value of reference requiredTag.
     */
        //List<object> getRequiredTag();
        /**
     * Returns the value of reference extendedElement.
     * @return Value of reference extendedElement.
     */
        List<object> getExtendedElement();
        /**
     * Returns the value of reference stereotypeConstraint.
     * @return Value of reference stereotypeConstraint.
     */
        //List<object> getStereotypeConstraint();
    }
}
