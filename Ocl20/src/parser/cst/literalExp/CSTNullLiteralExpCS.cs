using Ocl20.parser.controller;

namespace Ocl20.parser.cst.literalExp
{
    public class CSTNullLiteralExpCS : CSTLiteralExpCS {

        public CSTNullLiteralExpCS(OCLWorkbenchToken token) : base(token) {
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitNullLiteralExp(this);
        }
    }
}