using Ocl20.parser.controller;

namespace Ocl20.parser.cst.expression
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
