using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.expressions
{
    public interface VariableDeclaration : OclModelElement {
        /**
     * Returns the value of attribute varName.
     * @return Value of attribute varName.
     */
        string getVarName();
        /**
     * Sets the value of varName attribute. See {@link #getVarName} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setVarName(string newValue);
        /**
     * Returns the value of reference letExp.
     * @return Value of reference letExp.
     */
        LetExp getLetExp();
        /**
     * Sets the value of reference letExp. See {@link #getLetExp} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setLetExp(LetExp newValue);
        /**
     * Returns the value of reference type.
     * @return Value of reference type.
     */
        CoreClassifier getType();
        /**
     * Sets the value of reference type. See {@link #getType} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setType(CoreClassifier newValue);
        /**
     * Returns the value of reference baseExp.
     * @return Value of reference baseExp.
     */
        IterateExp getBaseExp();
        /**
     * Sets the value of reference baseExp. See {@link #getBaseExp} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setBaseExp(IterateExp newValue);
        /**
     * Returns the value of reference initExpression.
     * @return Value of reference initExpression.
     */
        OclExpression getInitExpression();
        /**
     * Sets the value of reference initExpression. See {@link #getInitExpression} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setInitExpression(OclExpression newValue);
        /**
     * Returns the value of reference tupleLiteralExp.
     * @return Value of reference tupleLiteralExp.
     */
        TupleLiteralExp getTupleLiteralExp();
        /**
     * Sets the value of reference tupleLiteralExp. See {@link #getTupleLiteralExp} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setTupleLiteralExp(TupleLiteralExp newValue);
        /**
     * Returns the value of reference loopExp.
     * @return Value of reference loopExp.
     */
        LoopExp getLoopExp();
        /**
     * Sets the value of reference loopExp. See {@link #getLoopExp} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setLoopExp(LoopExp newValue);
        /**
     * Returns the value of reference variableExp.
     * @return Value of reference variableExp.
     */
        List<object> getVariableExp();
    
        bool isFrozen();
        void setFrozen(bool value);
    
        object Clone();
    }
}
