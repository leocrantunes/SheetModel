/**
 * CoreInterface object instance interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OclLibrary.iface.common
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
