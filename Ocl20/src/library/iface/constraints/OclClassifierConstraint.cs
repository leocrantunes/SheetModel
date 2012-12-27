/**
 * OclClassifierConstraint object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface OclClassifierConstraint : OclConstraint {
        /**
     * Returns the value of reference contextualClassifier.
     * @return Value of reference contextualClassifier.
     */
        CoreClassifier getContextualClassifier();
        /**
     * Sets the value of reference contextualClassifier. See {@link #getContextualClassifier} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setContextualClassifier(CoreClassifier newValue);
    }
}
