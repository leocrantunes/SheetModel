using Ocl20.parser.controller;

namespace Ocl20.parser.cst.expression
{
    public class CSTBinaryExpressionCS : CSTOclExpressionCS {
        private CSTOclExpressionCS leftExpression;
        private CSTOperatorCS operat;
        private CSTOclExpressionCS rightExpression;

        public CSTBinaryExpressionCS(
            CSTOclExpressionCS leftExpression,
            CSTOperatorCS operat,
            CSTOclExpressionCS rightExpression) {
            this.leftExpression = leftExpression;
            this.operat = operat;
            this.rightExpression = rightExpression;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getLeftExpressionNodeCS() {
            return leftExpression;
        }

        /**
     * @return
     */
        public CSTOperatorCS getOperatorNodeCS() {
            return operat;
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getRightExpressionNodeCS() {
            return rightExpression;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if ((leftExpression != null) && (operat != null) &&
                (rightExpression != null)) {
                    leftExpression.accept(visitor);
                    rightExpression.accept(visitor);
                    visitor.visitBinaryExpression(this);
                }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return leftExpression.getToken();
        }
    }
}
