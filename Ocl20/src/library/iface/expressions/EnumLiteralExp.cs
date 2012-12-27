/**
 * EnumLiteralExp object instance interface.
 */

using Ocl20.library.iface.common;

namespace Ocl20.library.iface.expressions
{
    public interface EnumLiteralExp : LiteralExp {
        /**
     * Returns the value of reference referredEnumLiteral.
     * @return Value of reference referredEnumLiteral.
     */
        CoreEnumLiteral getReferredEnumLiteral();
        /**
     * Sets the value of reference referredEnumLiteral. See {@link #getReferredEnumLiteral} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setReferredEnumLiteral(CoreEnumLiteral newValue);
    }
}
