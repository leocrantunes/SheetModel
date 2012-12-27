/**
 * preownership association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.constraints
{
    public interface Preownership {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param owner Value of the first association end.
     * @param preConditions Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclPrePostConstraint owner, OclPreConstraint preConditions);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param owner Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        OclPrePostConstraint getOwner(OclPreConstraint preConditions);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param preConditions Required value of the second association end.
     * @return Collection of related objects.
     */

        List<object> getPreConditions(OclPrePostConstraint owner);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param owner Value of the first association end.
     * @param preConditions Value of the second association end.
     */
        bool add(OclPrePostConstraint owner, OclPreConstraint preConditions);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param owner Value of the first association end.
     * @param preConditions Value of the second association end.
     */
        bool remove(OclPrePostConstraint owner, OclPreConstraint preConditions);
    }
}
