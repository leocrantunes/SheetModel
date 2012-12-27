/**
 * CollectionLiteralExp object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.expressions
{
    public interface CollectionLiteralExp : LiteralExp {
        /**
     * Returns the value of attribute kind.
     * @return Value of attribute kind.
     */
        CollectionKind getKind();
        /**
     * Sets the value of kind attribute. See {@link #getKind} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setKind(CollectionKind newValue);
        /**
     * Returns the value of reference parts.
     * @return Value of reference parts.
     */
        List<CollectionLiteralPart> getParts();
    }
}
