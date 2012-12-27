using OclParser.controller;

namespace OclParser.cst.expression
{
    public class CSTNavigationOperatorCS : CSTNode {
        private OCLWorkbenchToken operat;

        public CSTNavigationOperatorCS(OCLWorkbenchToken operat) {
            this.operat = operat;
        }

        public string getOperator() {
            return operat.getText();
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            visitor.visitNavigationOperator(this);
        }

        public override OCLWorkbenchToken getToken() {
            return this.operat;
        }
    }
}
