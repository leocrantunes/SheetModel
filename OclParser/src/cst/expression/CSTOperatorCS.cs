using OclParser.controller;

namespace OclParser.cst.expression
{
    public class CSTOperatorCS : CSTNode {
        private OCLWorkbenchToken operat;

        public CSTOperatorCS(OCLWorkbenchToken operat) {
            this.operat = operat;
        }

        public string getOperator() {
            return operat.getText();
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return operat;
        }
    }
}
