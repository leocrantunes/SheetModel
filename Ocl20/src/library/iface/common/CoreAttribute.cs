/**
 * CoreAttribute object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.iface.common
{
    public interface CoreAttribute : CoreStructuralFeature {
        /**
     * @return 
     */
        bool isDerived();
        /**
     * @return 
     */
        ExpressionInOcl getDerivedValueExpression();
        /**
     * @return 
     */
        ExpressionInOcl getInitialValueExpression();
        /**
     * @param expression 
     */
        void setDerivedValueExpression(ExpressionInOcl expression);
        /**
     * @param expression 
     */
        void setInitialValueExpression(ExpressionInOcl expression);
        /**
     * Returns the value of reference deriveConstraint.
     * @return Value of reference deriveConstraint.
     */
        List<object> getDeriveConstraint();
        /**
     * Returns the value of reference initConstraint.
     * @return Value of reference initConstraint.
     */
        List<object> getInitConstraint();
        /**
     * Returns the value of reference theAssociationEnd.
     * @return Value of reference theAssociationEnd.
     */
        CoreAssociationEnd getTheAssociationEnd();
        /**
     * Sets the value of reference theAssociationEnd. See {@link #getTheAssociationEnd} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setTheAssociationEnd(CoreAssociationEnd newValue);
    }
}
