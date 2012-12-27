using OclParser.controller;

namespace OclParser.cst.literalExp
{
    public class CSTBooleanLiteralExpCS : CSTPrimitiveLiteralExpCS {
        bool booleanSymbol;

        public CSTBooleanLiteralExpCS(OCLWorkbenchToken token) : base(token) {
            this.convertToken(token);
        }

        private void convertToken(OCLWorkbenchToken token) {
            bool.TryParse(token.getText(), out booleanSymbol);
        }

        public bool getBooleanSymbol() {
            return booleanSymbol;
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitBooleanLiteralExp(this);
        }
    }
}
