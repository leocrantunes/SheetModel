using System;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;
using OclLibrary.iface.evaluation;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public abstract class OclExpressionImpl : OclModelElementImpl, OclExpression, IASTOclVisited {

        private	OclValue	value;
        private	CoreClassifier	type;
	
        private	PropertyCallExp appliedProperty;
        private	VariableDeclaration initializedVariable;
        private	LoopExp loopExp;
        private	NavigationCallExp navigationCallExp;
        private	OperationCallExp parentOperation;
        private	IfExp ifExp;
        private	IfExp thenClause;
        private	IfExp elseClause;
        private	LetExp letExp;
        private	CollectionRange collectionRange;
        private	CollectionItem	collectionItem;
	
        private	ExpressionInOcl  expressionInOcl;
	
        /**
	 * 
	 */
        public OclExpressionImpl() {
        }

        public bool isBooleanExpression() {
            return	getType().getName().Equals("Boolean");
        }
	
        public virtual bool conformsTo(CoreClassifier pType) {
            return getType().conformsTo(pType);
        }

        // only used in model property call context
        // should it migrate to subclass??
        public OperationCallExp withAtPre() {
            return  getFactory().createAtPreOperation(this);
        }
	
        public	OclValue	getValue() {
            return	value;
        }

        public	void	setValue(OclValue value) {
            this.value = value;
        }
	
        /* (non-Javadoc)
	 * @see impl.ocl20.common.CoreModelElementImpl#getElemOwner()
	 */
        public override OclModelElement getElemOwner() {
            if (getAppliedProperty() != null)
                return	getAppliedProperty();
            else if (getInitializedVariable() != null)
                return	getInitializedVariable();
            else if (getLoopExp() != null)
                return	getLoopExp();
            else if (getNavigationCallExp() != null)
                return	getNavigationCallExp();
            else if (getParentOperation() != null)
                return getParentOperation();
            else if (getIfExp() != null)
                return	getIfExp();
            else if (getThenClause() != null)
                return	getThenClause();
            else if (getElseClause() != null)
                return	getElseClause();
            else if (getLetExp() != null)
                return	getLetExp();
            else if (getCollectionRange() != null)
                return	getCollectionRange();
            else if (getCollectionItem() != null)
                return	getCollectionItem();
            else
                return	null;
        }
	
        public	abstract void	accept(IASTOclVisitor visitor);
	

	
        /**
	 * @return Returns the appliedProperty.
	 */
        public PropertyCallExp getAppliedProperty() {
            return appliedProperty;
        }
        /**
	 * @param appliedProperty The appliedProperty to set.
	 */
        public void setAppliedProperty(PropertyCallExp appliedProperty) {
            removeAllLinks();
            this.appliedProperty = appliedProperty;
        }
        /**
	 * @return Returns the collectionItem.
	 */
        public CollectionItem getCollectionItem() {
            return collectionItem;
        }
        /**
	 * @param collectionItem The collectionItem to set.
	 */
        public void setCollectionItem(CollectionItem collectionItem) {
            removeAllLinks();
            this.collectionItem = collectionItem;
        }
        /**
	 * @return Returns the collectionRange.
	 */
        public CollectionRange getCollectionRange() {
            return collectionRange;
        }
        /**
	 * @param collectionRange The collectionRange to set.
	 */
        public void setCollectionRange(CollectionRange collectionRange) {
            removeAllLinks();
            this.collectionRange = collectionRange;
        }
        /**
	 * @return Returns the elseClause.
	 */
        public IfExp getElseClause() {
            return elseClause;
        }
        /**
	 * @param elseClause The elseClause to set.
	 */
        public void setElseClause(IfExp elseClause) {
            removeAllLinks();
            this.elseClause = elseClause;
        }
        /**
	 * @return Returns the ifExp.
	 */
        public IfExp getIfExp() {
            return ifExp;
        }
        /**
	 * @param ifExp The ifExp to set.
	 */
        public void setIfExp(IfExp ifExp) {
            removeAllLinks();
            this.ifExp = ifExp;
        }
        /**
	 * @return Returns the initializedVariable.
	 */
        public VariableDeclaration getInitializedVariable() {
            return initializedVariable;
        }
        /**
	 * @param initializedVariable The initializedVariable to set.
	 */
        public void setInitializedVariable(VariableDeclaration initializedVariable) {
            removeAllLinks();
            this.initializedVariable = initializedVariable;
        }
        /**
	 * @return Returns the letExp.
	 */
        public LetExp getLetExp() {
            return letExp;
        }
        /**
	 * @param letExp The letExp to set.
	 */
        public void setLetExp(LetExp letExp) {
            removeAllLinks();
            this.letExp = letExp;
        }
        /**
	 * @return Returns the loopExp.
	 */
        public LoopExp getLoopExp() {
            return loopExp;
        }
        /**
	 * @param loopExp The loopExp to set.
	 */
        public void setLoopExp(LoopExp loopExp) {
            removeAllLinks();
            this.loopExp = loopExp;
        }
        /**
	 * @return Returns the navigationCallExp.
	 */
        public NavigationCallExp getNavigationCallExp() {
            return navigationCallExp;
        }
        /**
	 * @param navigationCallExp The navigationCallExp to set.
	 */
        public void setNavigationCallExp(NavigationCallExp navigationCallExp) {
            removeAllLinks();
            this.navigationCallExp = navigationCallExp;
        }
        /**
	 * @return Returns the parentOperation.
	 */
        public OperationCallExp getParentOperation() {
            return parentOperation;
        }
        /**
	 * @param parentOperation The parentOperation to set.
	 */
        public void setParentOperation(OperationCallExp parentOperation) {
            removeAllLinks();
            this.parentOperation = parentOperation;
        }
        /**
	 * @return Returns the thenClause.
	 */
        public IfExp getThenClause() {
            return thenClause;
        }
        /**
	 * @param thenClause The thenClause to set.
	 */
        public void setThenClause(IfExp thenClause) {
            removeAllLinks();
            this.thenClause = thenClause;
        }
        /**
	 * @return Returns the type.
	 */
        public CoreClassifier getType() {
            return type;
        }
        /* (non-Javadoc)
	 * @see ocl20.expressions.OclExpression#setType(ocl20.common.CoreClassifier)
	 */
        public void setType(CoreClassifier newValue) {
            this.type = newValue;
        }
	
	
        /**
	 * @return Returns the expressionInOcl.
	 */
        public ExpressionInOcl getExpressionInOcl() {
            return expressionInOcl;
        }
        /**
	 * @param expressionInOcl The expressionInOcl to set.
	 */
        public void setExpressionInOcl(ExpressionInOcl expressionInOcl) {
            this.expressionInOcl = expressionInOcl;
        }
	
        private	void	removeAllLinks() {
            appliedProperty = null;
            initializedVariable = null;
            loopExp  = null;
            navigationCallExp = null;
            parentOperation = null;
            ifExp  = null;
            thenClause  = null;
            elseClause  = null;
            letExp = null;
            collectionRange = null;
            collectionItem = null;
        }
	
        public override object Clone() {
            try  {
                OclExpressionImpl theClone = (OclExpressionImpl) base.Clone();
                if (value != null)
                    theClone.value = (OclValue) value.Clone();
                theClone.type = type;
			
                return	theClone;
            } catch (Exception) {
                return	null;
            }
        }
	
    }
}
