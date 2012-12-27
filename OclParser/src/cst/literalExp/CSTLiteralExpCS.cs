using OclParser.controller;
using OclParser.cst.expression;

namespace OclParser.cst.literalExp
{
    public abstract class CSTLiteralExpCS : CSTOclExpressionCS {
        private OCLWorkbenchToken token;

        public CSTLiteralExpCS(OCLWorkbenchToken token) {
            this.token = token;
        }

        public override OCLWorkbenchToken getToken() {
            return this.token;
        }
    }
}
