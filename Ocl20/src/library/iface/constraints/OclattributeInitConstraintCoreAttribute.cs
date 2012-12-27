/**
 * OCLAttributeInitConstraintCoreAttribute association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.constraints
{
    public interface OclattributeInitConstraintCoreAttribute {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param initConstraint Value of the first association end.
     * @param initializedAttribute Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclAttributeInitConstraint initConstraint, CoreAttribute initializedAttribute);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param initConstraint Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getInitConstraint(CoreAttribute initializedAttribute);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param initializedAttribute Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAttribute getInitializedAttribute(OclAttributeInitConstraint initConstraint);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param initConstraint Value of the first association end.
     * @param initializedAttribute Value of the second association end.
     */
        bool add(OclAttributeInitConstraint initConstraint, CoreAttribute initializedAttribute);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param initConstraint Value of the first association end.
     * @param initializedAttribute Value of the second association end.
     */
        bool remove(OclAttributeInitConstraint initConstraint, CoreAttribute initializedAttribute);
    }
}
