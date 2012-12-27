/**
 * OperationCallExpCoreOperation association proxy interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface OperationCallExpCoreOperation {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param operationCallExp Value of the first association end.
     * @param referredOperation Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OperationCallExp operationCallExp, CoreOperation referredOperation);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param operationCallExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getOperationCallExp(CoreOperation referredOperation);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredOperation Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreOperation getReferredOperation(OperationCallExp operationCallExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param operationCallExp Value of the first association end.
     * @param referredOperation Value of the second association end.
     */
        bool add(OperationCallExp operationCallExp, CoreOperation referredOperation);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param operationCallExp Value of the first association end.
     * @param referredOperation Value of the second association end.
     */
        bool remove(OperationCallExp operationCallExp, CoreOperation referredOperation);
    }
}
