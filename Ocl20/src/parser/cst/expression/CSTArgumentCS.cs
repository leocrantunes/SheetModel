using Ocl20.parser.controller;

namespace Ocl20.parser.cst.expression
{
    public class CSTArgumentCS : CSTOclExpressionCS {
        private CSTOclExpressionCS expressionNodeCS;

        public CSTArgumentCS(CSTOclExpressionCS expression) {
            this.expressionNodeCS = expression;
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getExpressionNodeCS() {
            return expressionNodeCS;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (expressionNodeCS != null) {
                visitor.visitArgumentCS(this);
            }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return expressionNodeCS.getToken();
        }
    }
}
