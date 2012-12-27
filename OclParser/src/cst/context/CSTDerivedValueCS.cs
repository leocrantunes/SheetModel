using OclParser.controller;

namespace OclParser.cst.context
{
    public class CSTDerivedValueCS : CSTInitDerivedValueCS {
        public CSTDerivedValueCS(
            OCLWorkbenchToken token,
            CSTExpressionInOclCS expression) : base (token, expression) {
            }
    }
}
