using OclParser.controller;

namespace OclParser.cst.name
{
    public class CSTSimpleNameCS : CSTNameCS {
        private OCLWorkbenchToken token;

        public CSTSimpleNameCS(OCLWorkbenchToken token) {
            this.token = token;
        }

        public override OCLWorkbenchToken getToken() {
            return this.token;
        }

        public override string getName() {
            return token.getText();
        }

        public override string getLastName() {
            return getName();
        }
    }
}
