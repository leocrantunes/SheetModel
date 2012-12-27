using OclParser.controller;

namespace OclParser.cst.literalExp
{
    public class CSTIntegerLiteralExpCS : CSTPrimitiveLiteralExpCS {
        private long integerSymbol;

        public CSTIntegerLiteralExpCS(OCLWorkbenchToken token) : base(token) {
            this.convertToken(token);
        }

        private void convertToken(OCLWorkbenchToken token) {
            long.TryParse(token.getText(), out integerSymbol);
        }

        public long getIntegerSymbol() {
            return integerSymbol;
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitIntegerLiteralExp(this);
        }
    }
}
