/**
 * A_stereotype_extendedElement association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.iface.foundation.extensionmechanisms
{
    public interface AStereotypeExtendedElement {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param stereotype Value of the first association end.
     * @param extendedElement Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(Stereotype stereotype, ModelElement extendedElement);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param stereotype Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        Stereotype getStereotype(ModelElement extendedElement);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param extendedElement Required value of the second association end.
     * @return Collection of related objects.
     */
        List<object> getExtendedElement(Stereotype stereotype);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param stereotype Value of the first association end.
     * @param extendedElement Value of the second association end.
     */
        bool add(Stereotype stereotype, ModelElement extendedElement);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param stereotype Value of the first association end.
     * @param extendedElement Value of the second association end.
     */
        bool remove(Stereotype stereotype, ModelElement extendedElement);
    }
}
