/**
 * StringLiteralExp object instance interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface StringLiteralExp : PrimitiveLiteralExp {
        /**
     * Returns the value of attribute stringSymbol.
     * @return Value of attribute stringSymbol.
     */
        string getStringSymbol();
        /**
     * Sets the value of stringSymbol attribute. See {@link #getStringSymbol} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setStringSymbol(string newValue);
    }
}
