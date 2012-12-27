/**
 * PropertyCallExpOclExpression association proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface PropertyCallExpOclExpression {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param appliedProperty Value of the first association end.
     * @param source Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(PropertyCallExp appliedProperty, OclExpression source);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param appliedProperty Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        PropertyCallExp getAppliedProperty(OclExpression source);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param source Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclExpression getSource(PropertyCallExp appliedProperty);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param appliedProperty Value of the first association end.
     * @param source Value of the second association end.
     */
        bool add(PropertyCallExp appliedProperty, OclExpression source);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param appliedProperty Value of the first association end.
     * @param source Value of the second association end.
     */
        bool remove(PropertyCallExp appliedProperty, OclExpression source);
    }
}
