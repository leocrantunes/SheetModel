/**
 * IfExp object instance interface.
 */
namespace OclLibrary.iface.expressions
{
    public interface IfExp : OclExpression {
        /**
     * Returns the value of reference condition.
     * @return Value of reference condition.
     */
        OclExpression getCondition();
        /**
     * Sets the value of reference condition. See {@link #getCondition} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setCondition(OclExpression newValue);
        /**
     * Returns the value of reference thenExpression.
     * @return Value of reference thenExpression.
     */
        OclExpression getThenExpression();
        /**
     * Sets the value of reference thenExpression. See {@link #getThenExpression} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setThenExpression(OclExpression newValue);
        /**
     * Returns the value of reference elseExpression.
     * @return Value of reference elseExpression.
     */
        OclExpression getElseExpression();
        /**
     * Sets the value of reference elseExpression. See {@link #getElseExpression} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setElseExpression(OclExpression newValue);
    }
}
