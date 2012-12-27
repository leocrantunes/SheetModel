/**
 * OperationCallExpOclExpression association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.expressions
{
    public interface OperationCallExpOclExpression {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param parentOperation Value of the first association end.
     * @param arguments Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OperationCallExp parentOperation, OclExpression arguments);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param parentOperation Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OperationCallExp getParentOperation(OclExpression arguments);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param arguments Required value of the second association end.
     * @return List of related objects.
     */
        List<object> getArguments(OperationCallExp parentOperation);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param parentOperation Value of the first association end.
     * @param arguments Value of the second association end.
     */
        bool add(OperationCallExp parentOperation, OclExpression arguments);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param parentOperation Value of the first association end.
     * @param arguments Value of the second association end.
     */
        bool remove(OperationCallExp parentOperation, OclExpression arguments);
    }
}
