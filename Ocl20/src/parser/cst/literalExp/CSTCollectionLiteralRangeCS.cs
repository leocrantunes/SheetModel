using Ocl20.parser.controller;
using Ocl20.parser.cst.expression;

namespace Ocl20.parser.cst.literalExp
{
    public class CSTCollectionLiteralRangeCS : CSTCollectionLiteralPartCS {
        private CSTOclExpressionCS firstNodeCS;
        private CSTOclExpressionCS lastNodeCS;

        public CSTCollectionLiteralRangeCS(
            CSTOclExpressionCS first,
            CSTOclExpressionCS last) {
            this.firstNodeCS = first;
            this.lastNodeCS = last;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getFirstNodeCS() {
            return firstNodeCS;
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getLastNodeCS() {
            return lastNodeCS;
        }


        public string getLastNodeTypeName()
        {
            return lastNodeCS.getAst().getType().getName();
        }

        public string getFirstNodeTypeName()
        {
            return firstNodeCS.getAst().getType().getName();
        }

        public override void accept(CSTVisitor visitor) {
            if ((firstNodeCS != null) && (lastNodeCS != null)) {
                firstNodeCS.accept(visitor);
                lastNodeCS.accept(visitor);
                visitor.visitCollectionLiteralRange(this);
            }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return firstNodeCS.getToken();
        }
    }
}
