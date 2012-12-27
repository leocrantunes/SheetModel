

/**
 * LetExpVariableDeclaration association proxy interface.
 */
namespace OclLibrary.iface.expressions
{
    public interface LetExpVariableDeclaration {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param letExp Value of the first association end.
     * @param variable Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(LetExp letExp, VariableDeclaration variable);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param letExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        LetExp getLetExp(VariableDeclaration variable);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param variable Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        VariableDeclaration getVariable(LetExp letExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param letExp Value of the first association end.
     * @param variable Value of the second association end.
     */
        bool add(LetExp letExp, VariableDeclaration variable);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param letExp Value of the first association end.
     * @param variable Value of the second association end.
     */
        bool remove(LetExp letExp, VariableDeclaration variable);
    }
}
