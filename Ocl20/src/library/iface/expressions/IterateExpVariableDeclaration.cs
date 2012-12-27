/**
 * IterateExpVariableDeclaration association proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface IterateExpVariableDeclaration {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param baseExp Value of the first association end.
     * @param result Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(IterateExp baseExp, VariableDeclaration result);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param baseExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        IterateExp getBaseExp(VariableDeclaration result);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param result Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        VariableDeclaration getResult(IterateExp baseExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param baseExp Value of the first association end.
     * @param result Value of the second association end.
     */
        bool add(IterateExp baseExp, VariableDeclaration result);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param baseExp Value of the first association end.
     * @param result Value of the second association end.
     */
        bool remove(IterateExp baseExp, VariableDeclaration result);
    }
}
