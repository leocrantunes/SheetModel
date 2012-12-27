/**
 * OclModelElement object instance interface.
 */

using OclLibrary.iface.util;

namespace OclLibrary.iface.expressions
{
    public interface OclModelElement { //extends CoreModelElement {
        /**
     * Returns the value of reference factory.
     * @return Value of reference factory.
     */
        AstOclModelElementFactory getFactory();
        /**
     * Sets the value of reference factory. See {@link #getFactory} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setFactory(AstOclModelElementFactory newValue);
    
        string getName();
        void setName(string name);
    }
}
