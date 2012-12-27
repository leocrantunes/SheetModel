using System;
using System.Reflection;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.constraints
{
    public class ExpressionInOclImpl : ExpressionInOcl, ICloneable {

        private	OclExpression		bodyExpression;
        private	OclConstraint		constraint;
        private	CoreModelElement	contextualElement;
        private	String	name;
	
        /**
	 * 
	 */
        public ExpressionInOclImpl() {
        }

        public override String ToString() {
            return	getBodyExpression().ToString();
        }
	
        /**
	 * @return Returns the bodyExpression.
	 */
        public OclExpression getBodyExpression() {
            return bodyExpression;
        }
        /**
	 * @param bodyExpression The bodyExpression to set.
	 */
        public void setBodyExpression(OclExpression bodyExpression) {
            this.bodyExpression = bodyExpression;
        }
	
	
	
        /**
	 * @return Returns the constraint.
	 */
        public OclConstraint getConstraint() {
            return constraint;
        }
        /**
	 * @param constraint The constraint to set.
	 */
        public void setConstraint(OclConstraint constraint) {
            this.constraint = constraint;
        }
        /**
	 * @return Returns the contextualElement.
	 */
        public CoreModelElement getContextualElement() {
            return contextualElement;
        }
        /**
	 * @param contextualElement The contextualElement to set.
	 */
        public void setContextualElement(CoreModelElement contextualElement) {
            this.contextualElement = contextualElement;
        }
        /**
	 * @return Returns the name.
	 */
        public String getName() {
            return name;
        }
        /**
	 * @param name The name to set.
	 */
        public void setName(String name) {
            this.name = name;
        }
	
        public Object Clone() {
            try {
                ExpressionInOclImpl theClone = (ExpressionInOclImpl)Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, this.GetType().FullName).Unwrap();
            
                theClone.bodyExpression = (OclExpression) bodyExpression.Clone();
                theClone.contextualElement = contextualElement;
                theClone.name = name;

                return	theClone;
            } catch (Exception) {
                return	null;
            }
        }
    }
}
