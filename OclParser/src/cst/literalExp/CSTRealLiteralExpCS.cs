using OclParser.controller;

namespace OclParser.cst.literalExp
{
    public class CSTRealLiteralExpCS : CSTPrimitiveLiteralExpCS {
        private string realSymbol;

        public CSTRealLiteralExpCS(OCLWorkbenchToken token) : base(token) {
            this.convertToken(token);
        }

        private void convertToken(OCLWorkbenchToken token) {
            realSymbol = token.getText();
        }

        public string getRealSymbol() {
            return realSymbol;
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitRealLiteralExp(this);
        }
    }
}
