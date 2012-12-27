using OclParser.controller;

namespace OclParser.cst.literalExp
{
    public class CSTInvalidLiteralExpCS  : CSTLiteralExpCS {

        public CSTInvalidLiteralExpCS(OCLWorkbenchToken token) : base(token) {
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitInvalidLiteralExp(this);
        }
    }
}