/**
 * RealLiteralExp class proxy interface.
 */

using OclLibrary.iface.expressions;

public interface RealLiteralExpClass {
    /**
     * The default factory operation used to create an instance object.
     * @return The created instance object.
     */
    RealLiteralExp createRealLiteralExp();
    /**
     * Creates an instance object having attributes initialized by the passed 
     * values.
     * @param name 
     * @param realSymbol 
     * @return The created instance object.
     */
    RealLiteralExp createRealLiteralExp(string name, double realSymbol);
}
