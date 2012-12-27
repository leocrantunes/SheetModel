using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.environment;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.iface.util;

namespace Ocl20.library.iface
{
    public interface Ocl20Package {
        /**
     * Returns nested package CustomMof.
     * @return Proxy object related to nested package CustomMof.
     */
        //CustomMofPackage getCustomMof();
        /**
     * Returns nested package Util.
     * @return Proxy object related to nested package Util.
     */
        UtilPackage getUtil();
        /**
     * Returns nested package Environment.
     * @return Proxy object related to nested package Environment.
     */
        EnvironmentPackage getEnvironment();
        /**
     * Returns nested package Constraints.
     * @return Proxy object related to nested package Constraints.
     */
        ConstraintsPackage getConstraints();
        /**
     * Returns nested package Expressions.
     * @return Proxy object related to nested package Expressions.
     */
        ExpressionsPackage getExpressions();
        /**
     * Returns nested package Types.
     * @return Proxy object related to nested package Types.
     */
        TypesPackage getTypes();
        /**
     * Returns nested package Common.
     * @return Proxy object related to nested package Common.
     */
        CommonPackage getCommon();
    }
}
