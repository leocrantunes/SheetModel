/**
 * elementsForEnv association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface ElementsForEnv {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param elemNamespace Value of the first association end.
     * @param elementsForEnvironment Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreNamespace elemNamespace, CoreModelElement elementsForEnvironment);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param elemNamespace Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreNamespace getElemNamespace(CoreModelElement elementsForEnvironment);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param elementsForEnvironment Required value of the second association 
     * end.
     * @return Collection of related objects.
     */
        List<object> getElementsForEnvironment(CoreNamespace elemNamespace);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param elemNamespace Value of the first association end.
     * @param elementsForEnvironment Value of the second association end.
     */
        bool add(CoreNamespace elemNamespace, CoreModelElement elementsForEnvironment);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param elemNamespace Value of the first association end.
     * @param elementsForEnvironment Value of the second association end.
     */
        bool remove(CoreNamespace elemNamespace, CoreModelElement elementsForEnvironment);
    }
}
