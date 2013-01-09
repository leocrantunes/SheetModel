/**
 * GeneralizableElement object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface GeneralizableElement : ModelElement {
        /**
     * Returns the value of attribute isRoot.
     * @return Value of attribute isRoot.
     */
        //bool isRoot();
        /**
     * Sets the value of isRoot attribute. See {@link #isRoot} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setRoot(bool newValue);
        /**
     * Returns the value of attribute isLeaf.
     * @return Value of attribute isLeaf.
     */
        //bool isLeaf();
        /**
     * Sets the value of isLeaf attribute. See {@link #isLeaf} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setLeaf(bool newValue);
        /**
     * Returns the value of attribute isAbstract.
     * @return Value of attribute isAbstract.
     */
        bool isAbstract();
        /**
     * Sets the value of isAbstract attribute. See {@link #isAbstract} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setAbstract(bool newValue);
        /**
     * Returns the value of reference generalization.
     * @return Value of reference generalization.
     */
        List<object> getGeneralizations();
        /**
     * Returns the value of reference specialization.
     * @return Value of reference specialization.
     */
        List<object> getSpecialization();
    }
}
