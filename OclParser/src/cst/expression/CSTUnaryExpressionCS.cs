using OclParser.controller;

namespace OclParser.cst.expression
{
    public class CSTUnaryExpressionCS : CSTOclExpressionCS {
        private CSTOperatorCS operat;
        private CSTOclExpressionCS expression;

        public CSTUnaryExpressionCS(
            CSTOperatorCS operat,
            CSTOclExpressionCS expression) {
            this.operat = operat;
            this.expression = expression;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getExpressionNodeCS() {
            return expression;
        }

        /**
     * @return
     */
        public CSTOperatorCS getOperatorNodeCS() {
            return operat;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (expression != null) {
                expression.accept(visitor);
                visitor.visitUnaryExpression(this);
            }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return operat.getToken();
        }
    }
}
