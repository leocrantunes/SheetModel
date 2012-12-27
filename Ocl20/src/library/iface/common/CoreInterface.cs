/**
 * CoreInterface object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreInterface : CoreClassifier {
        /**
     * @return 
     */
        List<object> getAllImplementors();
        /**
     * @return 
     */
        List<object> getAllDirectImplementors();
    }
}
