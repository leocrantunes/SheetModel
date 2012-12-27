/**
 * OclExpressionClassifier association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.expressions
{
    public interface OclExpressionClassifier {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param expressions Value of the first association end.
     * @param type Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclExpression expressions, CoreClassifier type);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param expressions Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getExpressions(CoreClassifier type);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param type Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreClassifier getType(OclExpression expressions);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param expressions Value of the first association end.
     * @param type Value of the second association end.
     */
        bool add(OclExpression expressions, CoreClassifier type);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param expressions Value of the first association end.
     * @param type Value of the second association end.
     */
        bool remove(OclExpression expressions, CoreClassifier type);
    }
}
