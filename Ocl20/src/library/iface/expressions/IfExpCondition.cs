

/**
 * IfExpCondition association proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface IfExpCondition {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param ifExp Value of the first association end.
     * @param condition Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(IfExp ifExp, OclExpression condition);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param ifExp Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        IfExp getIfExp(OclExpression condition);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param condition Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getCondition(IfExp ifExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param ifExp Value of the first association end.
     * @param condition Value of the second association end.
     */
        bool add(IfExp ifExp, OclExpression condition);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param ifExp Value of the first association end.
     * @param condition Value of the second association end.
     */
        bool remove(IfExp ifExp, OclExpression condition);
    }
}
