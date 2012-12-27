/**
 * OclExpression object instance interface.
 */

using OclLibrary.iface.common;
using OclLibrary.iface.constraints;

namespace OclLibrary.iface.expressions
{
    public interface CoreAssociationEnd : OclModelElement {
        /**
     * Returns the value of reference collectionRange.
     * @return Value of reference collectionRange.
     */
        CollectionRange getCollectionRange();
        /**
     * Sets the value of reference collectionRange. See {@link #getCollectionRange} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setCollectionRange(CollectionRange newValue);
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
     * Returns the value of reference collectionItem.
     * @return Value of reference collectionItem.
     */
        CollectionItem getCollectionItem();
        /**
     * Sets the value of reference collectionItem. See {@link #getCollectionItem} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setCollectionItem(CollectionItem newValue);
        /**
     * Returns the value of reference expressionInOCL.
     * @return Value of reference expressionInOCL.
     */
        ExpressionInOcl getExpressionInOcl();
        /**
     * Sets the value of reference expressionInOCL. See {@link #getExpressionInOcl} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setExpressionInOcl(ExpressionInOcl newValue);
        /**
     * Returns the value of reference navigationCallExp.
     * @return Value of reference navigationCallExp.
     */
        NavigationCallExp getNavigationCallExp();
        /**
     * Sets the value of reference navigationCallExp. See {@link #getNavigationCallExp} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setNavigationCallExp(NavigationCallExp newValue);
        /**
     * Returns the value of reference parentOperation.
     * @return Value of reference parentOperation.
     */
        OperationCallExp getParentOperation();
        /**
     * Sets the value of reference parentOperation. See {@link #getParentOperation} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setParentOperation(OperationCallExp newValue);
        /**
     * Returns the value of reference elseClause.
     * @return Value of reference elseClause.
     */
        IfExp getElseClause();
        /**
     * Sets the value of reference elseClause. See {@link #getElseClause} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setElseClause(IfExp newValue);
        /**
     * Returns the value of reference ifExp.
     * @return Value of reference ifExp.
     */
        IfExp getIfExp();
        /**
     * Sets the value of reference ifExp. See {@link #getIfExp} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setIfExp(IfExp newValue);
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
     * Returns the value of reference thenClause.
     * @return Value of reference thenClause.
     */
        IfExp getThenClause();
        /**
     * Sets the value of reference thenClause. See {@link #getThenClause} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setThenClause(IfExp newValue);
        /**
     * Returns the value of reference appliedProperty.
     * @return Value of reference appliedProperty.
     */
        PropertyCallExp getAppliedProperty();
        /**
     * Sets the value of reference appliedProperty. See {@link #getAppliedProperty} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setAppliedProperty(PropertyCallExp newValue);
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
     * Returns the value of reference initializedVariable.
     * @return Value of reference initializedVariable.
     */
        VariableDeclaration getInitializedVariable();
        /**
     * Sets the value of reference initializedVariable. See {@link #getInitializedVariable} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setInitializedVariable(VariableDeclaration newValue);
    }
}
