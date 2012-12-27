using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class VariableExpImpl : OclExpressionImpl, VariableExp {

        private	VariableDeclaration	referredVariable;
	
        /**
	 * @param object
	 */
        public VariableExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            try {
                ((VariableDeclarationImpl) getReferredVariable()).accept(visitor);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            visitor.visitVariableExp(this);
        }
	
        public String	toString() {
            return	this.getReferredVariable().getVarName();
        }

        /**
	 * @return Returns the referredVariable.
	 */
        public VariableDeclaration getReferredVariable() {
            return referredVariable;
        }
	
        /**
	 * @param referredVariable The referredVariable to set.
	 */
        public void setReferredVariable(VariableDeclaration referredVariable) {
            this.referredVariable = referredVariable;
        }
	
        public override Object Clone() {
            VariableExpImpl  theClone = (VariableExpImpl) base.Clone();
            // this variable should not be cloned here, but it is ok regarding our purposes for the moment.
            theClone.referredVariable = (VariableDeclaration) referredVariable.Clone();
            ((VariableDeclarationImpl) theClone.referredVariable).addVariableExp(theClone);
            return	theClone;
        }
    }
}
