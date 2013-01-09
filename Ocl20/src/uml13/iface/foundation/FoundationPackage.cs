/**
 * Foundation package interface.
 */

using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.foundation.datatypes;
using Ocl20.uml13.iface.foundation.extensionmechanisms;

namespace Ocl20.uml13.iface.foundation
{
    public interface FoundationPackage {
        /**
     * Returns nested package DataTypes.
     * @return Proxy object related to nested package DataTypes.
     */
        DataTypesPackage getDataTypes();
        /**
     * Returns nested package Core.
     * @return Proxy object related to nested package Core.
     */
        CorePackage getCore();
        /**
     * Returns nested package ExtensionMechanisms.
     * @return Proxy object related to nested package ExtensionMechanisms.
     */
        ExtensionMechanismsPackage getExtensionMechanisms();
    }
}
