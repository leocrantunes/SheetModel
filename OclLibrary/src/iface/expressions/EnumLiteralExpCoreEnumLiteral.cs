/**
 * EnumLiteralExpCoreEnumLiteral association proxy interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface EnumLiteralExpCoreEnumLiteral {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param enumLiteralExp Value of the first association end.
     * @param referredEnumLiteral Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(EnumLiteralExp enumLiteralExp, CoreEnumLiteral referredEnumLiteral);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param enumLiteralExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getEnumLiteralExp(CoreEnumLiteral referredEnumLiteral);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredEnumLiteral Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreEnumLiteral getReferredEnumLiteral(EnumLiteralExp enumLiteralExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param enumLiteralExp Value of the first association end.
     * @param referredEnumLiteral Value of the second association end.
     */
        bool add(EnumLiteralExp enumLiteralExp, CoreEnumLiteral referredEnumLiteral);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param enumLiteralExp Value of the first association end.
     * @param referredEnumLiteral Value of the second association end.
     */
        bool remove(EnumLiteralExp enumLiteralExp, CoreEnumLiteral referredEnumLiteral);
    }
}
