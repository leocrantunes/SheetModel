using System.Collections.Generic;
using OclParser.cst.type;

namespace OclParser.cst.expression
{
    public class CSTIterateExpCS : CSTOclExpressionCS {
        private List<object> iterators;
        private CSTVariableDeclarationCS result;
        private CSTOclExpressionCS bodyExpression;

        public CSTIterateExpCS(
            List<object> iterators,
            CSTVariableDeclarationCS result,
            CSTOclExpressionCS bodyExpression) {
            this.iterators = iterators;
            this.result = result;
            this.bodyExpression = bodyExpression;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getBodyExpressionNodeCS() {
            return bodyExpression;
        }

        /**
     * @return
     */
        public List<object> getIteratorsNodesCS() {
            return iterators;
        }

        /**
     * @return
     */
        public CSTVariableDeclarationCS getResultNodeCS() {
            return result;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (iterators != null) {
                base.accept(iterators, visitor);
            }

            if ((result != null) && (bodyExpression != null)) {
                result.accept(visitor);
                visitor.visitBeginIterateExp(this);
                bodyExpression.accept(visitor);
                visitor.visitEndIterateExp(this);
            }
        }
    }
}
