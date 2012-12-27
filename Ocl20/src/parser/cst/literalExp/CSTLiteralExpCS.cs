using Ocl20.parser.controller;
using Ocl20.parser.cst.expression;

namespace Ocl20.parser.cst.literalExp
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
