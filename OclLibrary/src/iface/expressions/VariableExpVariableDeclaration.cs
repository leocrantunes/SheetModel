

/**
 * VariableExpVariableDeclaration association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.expressions
{
    public interface VariableExpVariableDeclaration {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param variableExp Value of the first association end.
     * @param referredVariable Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(VariableExp variableExp, VariableDeclaration referredVariable);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param variableExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getVariableExp(VariableDeclaration referredVariable);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredVariable Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        VariableDeclaration getReferredVariable(VariableExp variableExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param variableExp Value of the first association end.
     * @param referredVariable Value of the second association end.
     */
        bool add(VariableExp variableExp, VariableDeclaration referredVariable);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param variableExp Value of the first association end.
     * @param referredVariable Value of the second association end.
     */
        bool remove(VariableExp variableExp, VariableDeclaration referredVariable);
    }
}
