using System.Collections.Generic;

namespace Ocl20.library.iface.environment
{
    public interface EnvironmentParent {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param children Value of the first association end.
     * @param parent Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(Environment children, Environment parent);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param children Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getChildren(Environment parent);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param parent Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        Environment getParent(Environment children);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param children Value of the first association end.
     * @param parent Value of the second association end.
     */
        bool add(Environment children, Environment parent);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param children Value of the first association end.
     * @param parent Value of the second association end.
     */
        bool remove(Environment children, Environment parent);
    }
}
