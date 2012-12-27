using Ocl20.library.iface.expressions;

namespace Ocl20.parser.cst.literalExp
{
    public abstract class CSTCollectionLiteralPartCS : CSTNode {
        private CollectionLiteralPart ast;

        /**
     * @return
     */
        public CollectionLiteralPart getAst()
        {
            return ast;
        }

        /**
     * @param part
     */
        public void setAst(CollectionLiteralPart part)
        {
            ast = part;
        }
    }
}
