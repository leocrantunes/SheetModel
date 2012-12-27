using System.Collections.Generic;
using Ocl20.parser.cst.type;

namespace Ocl20.parser.cst.literalExp
{
    public class CSTCollectionLiteralExpCS : CSTLiteralExpCS {
        private List<object> literalParts;
        private CSTCollectionTypeIdentifierCS typeId;

        public CSTCollectionLiteralExpCS(
            CSTCollectionTypeIdentifierCS typeId,
            List<object> literalParts) : base(typeId.getToken()) {
            this.typeId = typeId;
            this.literalParts = literalParts;
            }

        /**
     * @return
     */
        public List<object> getLiteralPartsNodesCS() {
            return literalParts;
        }

        /**
     * @return
     */
        public CSTCollectionTypeIdentifierCS getTypeIdNodeCS() {
            return typeId;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            this.accept(literalParts, visitor);
            visitor.visitCollectionLiteralExp(this);
        }
    }
}
