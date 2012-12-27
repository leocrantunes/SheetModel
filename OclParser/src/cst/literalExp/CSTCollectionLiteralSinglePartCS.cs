using OclParser.cst.expression;

namespace OclParser.cst.literalExp
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
