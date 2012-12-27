

/**
 * CoreEnumeration object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreEnumeration : CoreClassifier {  // extends CoreDataType {
        /**
     * @param name 
     * @return 
     */
        CoreEnumLiteral lookupEnumLiteral(string name);
        /**
     * Returns the value of reference theLiterals.
     * @return Value of reference theLiterals.
     */
        List<object> getTheLiterals();
    }
}
