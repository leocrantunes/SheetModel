using OclParser.controller;

namespace OclParser.cst.expression
{
    public class CSTIteratorOperationCS : CSTNode {
        private OCLWorkbenchToken token;

        public CSTIteratorOperationCS(OCLWorkbenchToken token) {
            this.token = token;
        }

        public string getOperationName() {
            return token.getText();
        }

        public override OCLWorkbenchToken getToken() {
            return this.token;
        }
    }
}
