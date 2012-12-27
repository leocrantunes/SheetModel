using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.utils;

namespace Ocl20.library.impl.common
{
    public abstract class CoreEnumerationImpl : CoreClassifierImpl, CoreEnumeration {

        public override bool isEnumeration() {
            return	true;
        }
	
        public override List<object> getTheLiterals() {
            return	adjustListResult(getSpecificEnumLiterals());
        }

        /* (non-Javadoc)
	 * @see implocl20.CoreClassifierImpl#isConcrete()
	 */
        public override bool isConcrete() {
            return false;
        }
	
        /* (non-Javadoc)
	 * @see implocl20.CoreClassifierImpl#getElemOwnedElements()
	 */
        public override ICollection<object> getElemOwnedElements() {
            HashSet<object>	resultAttr = new HashSet<object>();

            resultAttr.AddRange(getTheLiterals());
		
            return 	resultAttr;
        }
	
        protected List<object> getSpecificEnumLiterals() {
            return	new List<object>();
        }
    }
}
