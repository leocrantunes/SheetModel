/**
 * Generalization object instance interface.
 */

using System;

namespace Ocl20.library.iface.common
{
    public interface Generalization : Relationship {
        /**
     * Returns the value of attribute discriminator.
     * @return Value of attribute discriminator.
     */
       String getDiscriminator();
        /**
     * Sets the value of discriminator attribute. See {@link #getDiscriminator} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setDiscriminator(String newValue);
        /**
     * Returns the value of reference child.
     * @return Value of reference child.
     */
        CoreClassifier getChild();
        /**
     * Sets the value of reference child. See {@link #getChild} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setChild(CoreClassifier newValue);
        /**
     * Returns the value of reference parent.
     * @return Value of reference parent.
     */
        CoreClassifier getParent();
        /**
     * Sets the value of reference parent. See {@link #getParent} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setParent(CoreClassifier newValue);
        /**
     * Returns the value of reference powertype.
     * @return Value of reference powertype.
     */
        CoreClassifier getPowertype();
        /**
     * Sets the value of reference powertype. See {@link #getPowertype} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setPowertype(CoreClassifier newValue);
    }
}
