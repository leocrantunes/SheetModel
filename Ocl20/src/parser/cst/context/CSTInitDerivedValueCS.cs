using Ocl20.parser.controller;

namespace Ocl20.parser.cst.context
{
    public abstract class CSTInitDerivedValueCS : CSTNode {
        private CSTExpressionInOclCS expressionNodeCS;
        private OCLWorkbenchToken token;

        public CSTInitDerivedValueCS(
            OCLWorkbenchToken token,
            CSTExpressionInOclCS expression) {
            this.token = token;
            this.expressionNodeCS = expression;
            }

        public CSTExpressionInOclCS getExpressionNodeCS() {
            return this.expressionNodeCS;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (expressionNodeCS != null) {
                expressionNodeCS.accept(visitor);
            }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return token;
        }
    }
}
