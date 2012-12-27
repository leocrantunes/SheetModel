using Ocl20.parser.controller;

namespace Ocl20.parser.cst.literalExp
{
    public class CSTInvalidLiteralExpCS  : CSTLiteralExpCS {

        public CSTInvalidLiteralExpCS(OCLWorkbenchToken token) : base(token) {
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitInvalidLiteralExp(this);
        }
    }
}