using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class IfExpImpl : OclExpressionImpl, IfExp {

        private	OclExpression	condition;
        private	OclExpression	thenExpression;
        private	OclExpression	elseExpression;
	
        public IfExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitIfExpBegin(this);
            ((OclExpressionImpl) getCondition()).accept(visitor);
            visitor.visitIfExpThenBegin(this);
            // shouldn't it evaluate only one of the expressions (then or else)?
            ((OclExpressionImpl) getThenExpression()).accept(visitor);
            visitor.visitIfExpElseBegin(this);
            ((OclExpressionImpl) getElseExpression()).accept(visitor);
            visitor.visitIfExp(this);
        }

        public override String	ToString() {
            return "if " + getCondition().ToString() + " then "	+ getThenExpression().ToString() + " else " + getElseExpression().ToString() + " endif";
        }
	
	
	
        /**
	 * @return Returns the condition.
	 */
        public OclExpression getCondition() {
            return condition;
        }
        /**
	 * @param condition The condition to set.
	 */
        public void setCondition(OclExpression condition) {
            this.condition = condition;
        }
        /**
	 * @return Returns the elseExpression.
	 */
        public OclExpression getElseExpression() {
            return elseExpression;
        }
        /**
	 * @param elseExpression The elseExpression to set.
	 */
        public void setElseExpression(OclExpression elseExpression) {
            this.elseExpression = elseExpression;
        }
        /**
	 * @return Returns the thenExpression.
	 */
        public OclExpression getThenExpression() {
            return thenExpression;
        }
        /**
	 * @param thenExpression The thenExpression to set.
	 */
        public void setThenExpression(OclExpression thenExpression) {
            this.thenExpression = thenExpression;
        }
	
        public override Object Clone() {
            IfExpImpl theClone = (IfExpImpl) base.Clone();
            theClone.condition = (OclExpression) condition.Clone();
            theClone.thenExpression = (OclExpression) thenExpression.Clone();
            theClone.elseExpression = (OclExpression) elseExpression.Clone();
		
            ((OclExpressionImpl) theClone.condition).setIfExp(theClone);
            ((OclExpressionImpl) theClone.thenExpression).setIfExp(theClone);
            ((OclExpressionImpl) theClone.elseExpression).setIfExp(theClone);

            return	theClone;
        }
	
    }
}
