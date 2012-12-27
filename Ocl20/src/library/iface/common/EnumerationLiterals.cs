/**
 * EnumerationLiterals association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface EnumerationLiterals {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param theEnumeration Value of the first association end.
     * @param theLiterals Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreEnumeration theEnumeration, CoreEnumLiteral theLiterals);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param theEnumeration Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreEnumeration getTheEnumeration(CoreEnumLiteral theLiterals);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param theLiterals Required value of the second association end.
     * @return List of related objects.
     */
        List<object> getTheLiterals(CoreEnumeration theEnumeration);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param theEnumeration Value of the first association end.
     * @param theLiterals Value of the second association end.
     */
        bool add(CoreEnumeration theEnumeration, CoreEnumLiteral theLiterals);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param theEnumeration Value of the first association end.
     * @param theLiterals Value of the second association end.
     */
        bool remove(CoreEnumeration theEnumeration, CoreEnumLiteral theLiterals);
    }
}
