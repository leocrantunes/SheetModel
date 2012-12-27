/**
 * RealLiteralExp object instance interface.
 */

using System;

namespace OclLibrary.iface.expressions
{
    public interface RealLiteralExp : NumericLiteralExp {
        /**
     * Returns the value of attribute realSymbol.
     * @return Value of attribute realSymbol.
     */
        string getRealSymbol();
        /**
     * Sets the value of realSymbol attribute. See {@link #getRealSymbol} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        void setRealSymbol(String newValue);
    }
}
