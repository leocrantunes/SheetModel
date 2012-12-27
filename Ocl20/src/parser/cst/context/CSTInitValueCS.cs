using Ocl20.parser.controller;

namespace Ocl20.parser.cst.context
{
    public class CSTInitValueCS : CSTInitDerivedValueCS {
        private OCLWorkbenchToken token;

        public CSTInitValueCS(
            OCLWorkbenchToken token,
            CSTExpressionInOclCS expression) : base(token, expression) {
            }
    }
}
