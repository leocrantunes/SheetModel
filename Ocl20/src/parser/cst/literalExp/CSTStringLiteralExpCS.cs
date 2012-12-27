using Ocl20.parser.controller;

namespace Ocl20.parser.cst.literalExp
{
    public class CSTStringLiteralExpCS : CSTPrimitiveLiteralExpCS {
        private string stringSymbol;

        public CSTStringLiteralExpCS(OCLWorkbenchToken token) : base(token) {
            this.convertToken(token);
        }

        private void convertToken(OCLWorkbenchToken token) {
            stringSymbol = this.getStringWithoutQuotationMarks(token.getText());
        }

        public string getStringSymbol() {
            return stringSymbol;
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitStringLiteralExp(this);
        }

        private string getStringWithoutQuotationMarks(string source) {
            return source.Substring(1, source.Length - 1);
        }
    }
}
