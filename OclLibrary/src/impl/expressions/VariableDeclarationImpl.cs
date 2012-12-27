using System;
using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.impl.types;

namespace OclLibrary.impl.expressions
{
    public class VariableDeclarationImpl : OclModelElementImpl, VariableDeclaration {

        private	string	varName;
        private	CoreClassifier	type;
        private	OclExpression initExpression;
        private	bool		frozen = true;
	
        private	IterateExp	baseExp;
        private	LoopExp	loopExp;
        private	LetExp	letExp;
        private	TupleLiteralExp	tupleLiteralExp;
        private	List<object> variableExp;
	
        public VariableDeclarationImpl() {
            variableExp = new List<object>();
        }

        public void accept(IASTOclVisitor visitor) {
            if (getInitExpression() != null)
                ((OclExpressionImpl) getInitExpression()).accept(visitor);
            visitor.visitVariableDeclaration(this);

            // adjust type if Set{}, Bag{}, Sequence{}, OrderedSet{}
            OclExpression initExp = getInitExpression();
            if (initExp != null && initExp.getType().GetType() == typeof(CollectionType) && "OclVoid".Equals(((CollectionTypeImpl) initExp.getType()).getInnerMostElementType().getName())) {
                initExp.setType(type);
            }
        }

        /* (non-Javadoc)
	 * @see java.lang.Comparable#compareTo(java.lang.object)
	 */
        public int compareTo(object arg0) {
            return 0;
        }
	
        public override string getName() {
            return	getVarName();
        }

        public	string	toString() {
            string	result;
		
            result = this.getVarName();
            if (this.getType() != null) {
                result += " : " + this.getType().getName();
            }
		
            if (this.getInitExpression() != null) {
                result += " = " + this.getInitExpression().ToString();
            }
		
            return	result;
        }

        /* (non-Javadoc)
	 * @see impl.CoreModelElementImpl#getElemOwner()
	 */
        public override OclModelElement getElemOwner() {
            if (getBaseExp() != null)
                return	getBaseExp();
            else if (getLoopExp() != null)
                return	getLoopExp();
            else if (getLetExp() != null)
                return	getLetExp();
            else if (getTupleLiteralExp() != null)
                return	getTupleLiteralExp();
            else
                return	null;
        }
	
	
	
        /**
	 * @return Returns the baseExp.
	 */
        public IterateExp getBaseExp() {
            return baseExp;
        }
        /**
	 * @param baseExp The baseExp to set.
	 */
        public void setBaseExp(IterateExp baseExp) {
            this.baseExp = baseExp;
        }
        /**
	 * @return Returns the initExpression.
	 */
        public OclExpression getInitExpression() {
            return initExpression;
        }
        /**
	 * @param initExpression The initExpression to set.
	 */
        public void setInitExpression(OclExpression initExpression) {
            this.initExpression = initExpression;
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
            this.letExp = letExp;
        }
        /**
	 * @return Returns the loopExpr.
	 */
        public LoopExp getLoopExp() {
            return loopExp;
        }
        /**
	 * @param loopExpr The loopExpr to set.
	 */
        public void setLoopExp(LoopExp loopExp) {
            this.loopExp = loopExp;
        }
        /**
	 * @return Returns the type.
	 */
        public CoreClassifier getType() {
            return type;
        }
        /**
	 * @param type The type to set.
	 */
        public void setType(CoreClassifier type) {
            this.type = type;
        }
        /**
	 * @return Returns the varName.
	 */
        public string getVarName() {
            return varName;
        }
        /**
	 * @param varName The varName to set.
	 */
        public void setVarName(string varName) {
            this.varName = varName;
        }
	
	
        /**
	 * @return Returns the tupleLiteralExp.
	 */
        public TupleLiteralExp getTupleLiteralExp() {
            return tupleLiteralExp;
        }
        /**
	 * @param tupleLiteralExp The tupleLiteralExp to set.
	 */
        public void setTupleLiteralExp(TupleLiteralExp tupleLiteralExp) {
            this.tupleLiteralExp = tupleLiteralExp;
        }
	
	
        /**
	 * @return Returns the variableExp.
	 */
        public List<object> getVariableExp() {
            return variableExp;
        }
	
        /**
	 * @param variableExp The variableExp to set.
	 */
        public void addVariableExp(VariableExp variableExp) {
            this.variableExp.Add(variableExp);
        }
	
        public void removeVariableExp(VariableExp variableExp) {
            this.variableExp.Remove(variableExp);
        }
	
        /* (non-Javadoc)
	 * @see VariableDeclaration#isFrozen()
	 */
        public bool isFrozen() {
            return frozen;
        }
	
        /* (non-Javadoc)
	 * @see VariableDeclaration#setFrozen(bool)
	 */
        public void setFrozen(bool value) {
            frozen = value;
        }
	
        public override object Clone() {
            try {
                VariableDeclarationImpl	theClone = (VariableDeclarationImpl) base.Clone();

                if (varName != null)
                    theClone.varName = varName;
                else
                    theClone.varName = null;
			
                theClone.type = type;
			
                if (initExpression != null) {
                    theClone.initExpression = (OclExpression) initExpression.Clone();
                    theClone.initExpression.setInitializedVariable(theClone);
                } else 
                    theClone.initExpression = null;
			
                theClone.frozen = frozen;
			
                return	theClone;
            } catch (Exception) {
                return	null;
            }
        }	
    }
}
