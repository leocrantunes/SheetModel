using OclParser.controller;

namespace OclParser.cst.expression
{
    public class CSTIfExpCS : CSTOclExpressionCS {
        private CSTOclExpressionCS conditionExp;
        private CSTOclExpressionCS thenExp;
        private CSTOclExpressionCS elseExp;
        private OCLWorkbenchToken tokenIf;

        public CSTIfExpCS(
            OCLWorkbenchToken tokenIf,
            CSTOclExpressionCS conditionExp,
            CSTOclExpressionCS thenExp,
            CSTOclExpressionCS elseExp) {
            this.conditionExp = conditionExp;
            this.thenExp = thenExp;
            this.elseExp = elseExp;
            this.tokenIf = tokenIf;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getConditionExpNodeCS() {
            return conditionExp;
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getElseExpNodeCS() {
            return elseExp;
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getThenExpNodeCS() {
            return thenExp;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if ((conditionExp != null) && (thenExp != null) && (elseExp != null)) {
                visitor.visitIfExp(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return this.tokenIf;
        }
    }
}
