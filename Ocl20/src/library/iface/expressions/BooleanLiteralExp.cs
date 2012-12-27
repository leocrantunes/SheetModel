/**
 * BooleanLiteralExp object instance interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface BooleanLiteralExp : PrimitiveLiteralExp {
        /**
     * Returns the value of attribute boolSymbol.
     * @return Value of attribute boolSymbol.
     */
        bool isBooleanSymbol();
        /**
     * Sets the value of boolSymbol attribute. See {@link #isboolSymbol} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setBooleanSymbol(bool newValue);
    }
}
