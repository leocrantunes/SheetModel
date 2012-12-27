/**
 * ModelElement ExpressionInOCL association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface ModelElementExpressionInOcl {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param constraintExpressionInOcl Value of the first association end.
     * @param contextualElement Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(ExpressionInOcl constraintExpressionInOcl, CoreModelElement contextualElement);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param constraintExpressionInOcl Required value of the first association 
     * end.
     * @return Collection of related objects.
     */
        List<object> getConstraintExpressionInOcl(CoreModelElement contextualElement);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param contextualElement Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreModelElement getContextualElement(ExpressionInOcl constraintExpressionInOcl);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param constraintExpressionInOcl Value of the first association end.
     * @param contextualElement Value of the second association end.
     */
        bool add(ExpressionInOcl constraintExpressionInOcl, CoreModelElement contextualElement);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param constraintExpressionInOcl Value of the first association end.
     * @param contextualElement Value of the second association end.
     */
        bool remove(ExpressionInOcl constraintExpressionInOcl, CoreModelElement contextualElement);
    }
}
