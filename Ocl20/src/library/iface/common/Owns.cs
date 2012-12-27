/**
 * owns association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface Owns {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param elemOwner Value of the first association end.
     * @param elemOwnedElements Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreModelElement elemOwner, CoreModelElement elemOwnedElements);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param elemOwner Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreModelElement getElemOwner(CoreModelElement elemOwnedElements);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param elemOwnedElements Required value of the second association end.
     * @return Collection of related objects.
     */
        List<object> getElemOwnedElements(CoreModelElement elemOwner);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param elemOwner Value of the first association end.
     * @param elemOwnedElements Value of the second association end.
     */
        bool add(CoreModelElement elemOwner, CoreModelElement elemOwnedElements);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param elemOwner Value of the first association end.
     * @param elemOwnedElements Value of the second association end.
     */
        bool remove(CoreModelElement elemOwner, CoreModelElement elemOwnedElements);
    }
}
