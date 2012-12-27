/**
 * ExpressionInOCL class proxy interface.
 */

namespace Ocl20.library.iface.constraints
{
    public interface ExpressionInOclClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        ExpressionInOcl createExpressionInOcl();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @return The created instance object.
     */
        ExpressionInOcl createExpressionInOcl(string name);
    }
}
