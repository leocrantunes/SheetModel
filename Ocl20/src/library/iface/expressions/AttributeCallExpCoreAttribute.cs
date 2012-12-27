/**
 * AttributeCallExpCoreAttribute association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.expressions
{
    public interface AttributeCallExpCoreAttribute {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param attributeCallExp Value of the first association end.
     * @param referredAttribute Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(AttributeCallExp attributeCallExp, CoreAttribute referredAttribute);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param attributeCallExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getAttributeCallExp(CoreAttribute referredAttribute);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredAttribute Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAttribute getReferredAttribute(AttributeCallExp attributeCallExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param attributeCallExp Value of the first association end.
     * @param referredAttribute Value of the second association end.
     */
        bool add(AttributeCallExp attributeCallExp, CoreAttribute referredAttribute);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param attributeCallExp Value of the first association end.
     * @param referredAttribute Value of the second association end.
     */
        bool remove(AttributeCallExp attributeCallExp, CoreAttribute referredAttribute);
    }
}
