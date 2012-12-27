/**
 * NavigationCallExp object instance interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.expressions
{
    public interface NavigationCallExp : ModelPropertyCallExp {
        /**
     * Returns the value of reference navigationSource.
     * @return Value of reference navigationSource.
     */
        common.CoreAssociationEnd getNavigationSource();
        /**
     * Sets the value of reference navigationSource. See {@link #getNavigationSource} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setNavigationSource(common.CoreAssociationEnd newValue);
        /**
     * Returns the value of reference qualifiers.
     * @return Value of reference qualifiers.
     */
        List<OclExpression> getQualifiers();
    }
}
