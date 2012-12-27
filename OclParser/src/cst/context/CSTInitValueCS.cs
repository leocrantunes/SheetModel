using OclParser.controller;

namespace OclParser.cst.context
{
    public class CSTInitValueCS : CSTInitDerivedValueCS {
        private OCLWorkbenchToken token;

        public CSTInitValueCS(
            OCLWorkbenchToken token,
            CSTExpressionInOclCS expression) : base(token, expression) {
            }
    }
}
