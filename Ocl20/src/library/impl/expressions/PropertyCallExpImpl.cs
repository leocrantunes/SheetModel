using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public abstract class PropertyCallExpImpl : OclExpressionImpl, PropertyCallExp {

        private	OclExpression source;
	
        public PropertyCallExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            ((OclExpressionImpl) getSource()).accept(visitor);
        }
	
	
        /**
	 * @return Returns the source.
	 */
        public OclExpression getSource() {
            return source;
        }
        /**
	 * @param source The source to set.
	 */
        public void setSource(OclExpression source) {
            this.source = source;
        }
	
        public override Object Clone() {
            PropertyCallExpImpl theClone = (PropertyCallExpImpl) base.Clone();
		
            if (source != null) {
                theClone.source = (OclExpression) source.Clone();
                ((OclExpressionImpl) theClone.source).setAppliedProperty(theClone);
            } else 
                theClone.source = null;
		
            return	theClone;
        }
    }
}
