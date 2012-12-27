/**
 * VariableDeclaration class proxy interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface VariableDeclarationClass {
        /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
        VariableDeclaration createVariableDeclaration();
        /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @param varName 
     * @return The created instance object.
     */
        VariableDeclaration createVariableDeclaration(string name, string varName);
    }
}
