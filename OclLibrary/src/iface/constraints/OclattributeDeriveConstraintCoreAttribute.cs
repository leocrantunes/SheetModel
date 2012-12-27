/**
 * OCLAttributeDeriveConstraintCoreAttribute association proxy interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.constraints
{
    public interface OclattributeDeriveConstraintCoreAttribute {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param deriveConstraint Value of the first association end.
     * @param derivedAttribute Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclAttributeDeriveConstraint deriveConstraint, CoreAttribute derivedAttribute);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param deriveConstraint Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getDeriveConstraint(CoreAttribute derivedAttribute);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param derivedAttribute Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAttribute getDerivedAttribute(OclAttributeDeriveConstraint deriveConstraint);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param deriveConstraint Value of the first association end.
     * @param derivedAttribute Value of the second association end.
     */
        bool add(OclAttributeDeriveConstraint deriveConstraint, CoreAttribute derivedAttribute);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param deriveConstraint Value of the first association end.
     * @param derivedAttribute Value of the second association end.
     */
        bool remove(OclAttributeDeriveConstraint deriveConstraint, CoreAttribute derivedAttribute);
    }
}
