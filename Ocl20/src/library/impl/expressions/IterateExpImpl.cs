using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class IterateExpImpl : LoopExpImpl, IterateExp {

        private	VariableDeclaration 	result;
	
        public IterateExpImpl() {
        }

        public override String ToString() {
            return	this.getSource().ToString() + "->iterate(" + getIteratorsAsString() + " ; " + getResult().ToString() + " | " + getBody().ToString() +  ")";
        }


        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.expressions.core.ASTLoopExp#accept(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.IASTOclVisitor)
	 */
        public override void accept(IASTOclVisitor visitor) {
            try {
                base.accept(visitor);
                ((VariableDeclarationImpl) getResult()).accept(visitor);
                visitor.visitIterateExp(this);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }
	
	
        /**
	 * @return Returns the result.
	 */
        public VariableDeclaration getResult() {
            return result;
        }
        /**
	 * @param result The result to set.
	 */
        public void setResult(VariableDeclaration result) {
            this.result = result;
        }
	
        public override Object Clone() {
            IterateExpImpl theClone = (IterateExpImpl) base.Clone();
            theClone.result = (VariableDeclaration) result.Clone();
            theClone.result.setLoopExp(theClone);
            return	theClone;
        }
    }
}
