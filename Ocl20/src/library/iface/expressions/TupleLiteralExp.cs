/**
 * TupleLiteralExp object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.expressions
{
    public interface TupleLiteralExp : LiteralExp {
        /**
     * Returns the value of reference tuplePart.
     * @return Value of reference tuplePart.
     */
        List<VariableDeclaration> getTuplePart();
    }
}
