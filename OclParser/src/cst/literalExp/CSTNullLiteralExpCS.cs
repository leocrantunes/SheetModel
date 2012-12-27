using OclParser.controller;

namespace OclParser.cst.literalExp
{
    public class CSTNullLiteralExpCS : CSTLiteralExpCS {

        public CSTNullLiteralExpCS(OCLWorkbenchToken token) : base(token) {
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitNullLiteralExp(this);
        }
    }
}