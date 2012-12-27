/**
 * environment package interface.
 */

namespace Ocl20.library.iface.environment
{
    public interface EnvironmentPackage {
        /**
     * Returns Environment class proxy object.
     * @return Environment class proxy object.
     */
        EnvironmentClass getEnvironment();
        /**
     * Returns EnvironmentParent association proxy object.
     * @return EnvironmentParent association proxy object.
     */
        EnvironmentParent getEnvironmentParent();
    }
}
