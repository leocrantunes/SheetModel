using Ocl20.parser.cst.expression;

namespace Ocl20.parser.cst.literalExp
{
    public class CSTCollectionLiteralSinglePartCS : CSTCollectionLiteralPartCS {
        private CSTOclExpressionCS expressionNodeCS;

        public CSTCollectionLiteralSinglePartCS(CSTOclExpressionCS expression) {
            this.expressionNodeCS = expression;
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getExpressionNodeCS() {
            return expressionNodeCS;
        }

        public override void accept(CSTVisitor visitor) {
            if (expressionNodeCS != null) {
                expressionNodeCS.accept(visitor);
                visitor.visitCollectionLiteralSinglePart(this);
            }
        }
    }
}
