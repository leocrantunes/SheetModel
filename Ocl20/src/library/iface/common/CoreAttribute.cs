/**
 * CoreAttribute object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.iface.common
{
    public interface CoreAttribute : CoreStructuralFeature {

        bool isDerived();
        ExpressionInOcl getDerivedValueExpression();
        ExpressionInOcl getInitialValueExpression();
        void setDerivedValueExpression(ExpressionInOcl expression);
        void setInitialValueExpression(ExpressionInOcl expression);
        List<object> getDeriveConstraint();
        List<object> getInitConstraint();
        CoreAssociationEnd getTheAssociationEnd();
        void setTheAssociationEnd(CoreAssociationEnd newValue);
    }
}
