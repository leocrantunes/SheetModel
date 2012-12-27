using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class LetExpImpl : OclExpressionImpl, LetExp {

        private	VariableDeclaration	variable;
        private	OclExpression inE;
	
        /**
	 * 
	 */
        public LetExpImpl() {
        }

        public override String	ToString() {
            return	"let " + getVariable().ToString() + " in " + getIn().ToString(); 
        }

        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.expressions.ASTOclExpression#accept(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.IASTOclVisitor)
	 */
        public override void accept(IASTOclVisitor visitor) {
            try {
                ((VariableDeclarationImpl) getVariable()).accept(visitor);
                visitor.beginVisitLetExp(this);
                ((OclExpressionImpl) getIn()).accept(visitor);
                visitor.endVisitLetExp(this);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        /**
	 * @return Returns the in.
	 */
        public OclExpression getIn() {
            return inE;
        }
        /**
	 * @param in The in to set.
	 */
        public void setIn(OclExpression inE) {
            this.inE = inE;
        }
        /**
	 * @return Returns the variable.
	 */
        public VariableDeclaration getVariable() {
            return variable;
        }
        /**
	 * @param variable The variable to set.
	 */
        public void setVariable(VariableDeclaration variable) {
            this.variable = variable;
        }
	
        public override Object Clone() {
            LetExpImpl theClone = (LetExpImpl) base.Clone();
            theClone.variable = (VariableDeclaration) variable.Clone();
            theClone.inE = (OclExpression) inE.Clone();
		
            theClone.variable.setLetExp(theClone);
            theClone.inE.setLetExp(theClone);

            return	theClone;
        }
	
    }
}
