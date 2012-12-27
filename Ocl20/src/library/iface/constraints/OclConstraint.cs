/**
 * OclConstraint object instance interface.
 */

namespace Ocl20.library.iface.constraints
{
    public interface OclConstraint { // extends javax.jmi.reflect.RefObject {
        /**
     * Returns the value of attribute source.
     * @return Value of attribute source.
     */
        string getSource();
        /**
     * Sets the value of source attribute. See {@link #getSource} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setSource(string newValue);
        /**
     * Returns the value of reference expression.
     * @return Value of reference expression.
     */
        ExpressionInOcl getExpression();
        /**
     * Sets the value of reference expression. See {@link #getExpression} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setExpression(ExpressionInOcl newValue);
    
        /**
	 * @return Returns the sourceNodeCS.
	 */
        object getSourceNodeCS();
        /**
	 * @param sourceNodeCS The sourceNodeCS to set.
	 */
        void setSourceNodeCS(object sourceNodeCS);
    }
}
