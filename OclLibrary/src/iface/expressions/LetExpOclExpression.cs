/**
 * LetExpOclExpression association proxy interface.
 */

namespace OclLibrary.iface.expressions
{
    public interface LetExpOclExpression {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param letExp Value of the first association end.
     * @param in Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(LetExp letExp, OclExpression inR);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param letExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        LetExp getLetExp(OclExpression inR);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param in Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getIn(LetExp letExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param letExp Value of the first association end.
     * @param in Value of the second association end.
     */
        bool add(LetExp letExp, OclExpression inR);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param letExp Value of the first association end.
     * @param in Value of the second association end.
     */
        bool remove(LetExp letExp, OclExpression inR);
    }
}
