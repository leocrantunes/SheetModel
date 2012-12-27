using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.environment
{
    public class PSWNamedElement : NamedElement {
    
        private string name;
        private	bool mayBeImplicit;
        private	object referredElement;
        /**
	 * @param object
	 */
        public PSWNamedElement(string name, object referredElement, bool mayBeImplicit) {
            this.name = name;
            this.mayBeImplicit = mayBeImplicit;
            this.referredElement = referredElement;
        }

        /**
	 * @return Returns the mayBeImplicit.
	 */
        public bool isMayBeImplicit() {
            return mayBeImplicit;
        }
        /**
	 * @param mayBeImplicit The mayBeImplicit to set.
	 */
        public void setMayBeImplicit(bool mayBeImplicit) {
            this.mayBeImplicit = mayBeImplicit;
        }
        /**
	 * @return Returns the name.
	 */
        public string getName() {
            return name;
        }
        /**
	 * @param name The name to set.
	 */
        public void setName(string name) {
            this.name = name;
        }
	
        public CoreClassifier getType() {
            if (getReferredElement().GetType() == typeof(CoreClassifier)) {
                return (CoreClassifier) getReferredElement();
            } else if (getReferredElement().GetType() == typeof(VariableDeclaration)) {
                return ((VariableDeclaration) getReferredElement()).getType();
            } else {
                return null;
            }
        }
	
        /**
	 * @return Returns the referredElement.
	 */
        public object getReferredElement() {
            return referredElement;
        }
        /**
	 * @param referredElement The referredElement to set.
	 */
        public void setReferredElement(object referredElement) {
            this.referredElement = referredElement;
        }
    }
}
