using System;
using System.Reflection;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.util;

namespace Ocl20.library.impl.expressions
{
    public abstract class OclModelElementImpl : OclModelElement, ICloneable {

        private	AstOclModelElementFactory factory;
        private	String	name;

        public OclModelElementImpl() {
        }
	
        public abstract OclModelElement getElemOwner();
	
        /* (non-Javadoc)
	 * @see ocl20.expressions.OclModelElement#getFactory()
	 */
        public AstOclModelElementFactory getFactory() {
            return factory;
        }
        /* (non-Javadoc)
	 * @see ocl20.expressions.OclModelElement#setFactory(ocl20.util.AstOclModelElementFactory)
	 */
        public void setFactory(AstOclModelElementFactory newValue) {
            this.factory = newValue;
        }

	
        /**
	 * @return Returns the name.
	 */
        public virtual String getName() {
            return name;
        }
        /**
	 * @param name The name to set.
	 */
        public void setName(string name) {
            this.name = name;
        }

        public virtual object Clone() {
            try {
                OclModelElementImpl theClone = 
                    (OclModelElementImpl) Activator.CreateInstance(
                        Assembly.GetExecutingAssembly().FullName, this.GetType().FullName).Unwrap();
                theClone.factory = factory;
                if (name != null)
                    theClone.name = name;
                else 
                    theClone.name = null;
                return	theClone;
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return	null;
            }
        }
    }
}
