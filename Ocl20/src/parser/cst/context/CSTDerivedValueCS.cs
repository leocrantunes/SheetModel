using Ocl20.parser.controller;

namespace Ocl20.parser.cst.context
{
    public class CSTDerivedValueCS : CSTInitDerivedValueCS {
        public CSTDerivedValueCS(
            OCLWorkbenchToken token,
            CSTExpressionInOclCS expression) : base (token, expression) {
            }
    }
}
