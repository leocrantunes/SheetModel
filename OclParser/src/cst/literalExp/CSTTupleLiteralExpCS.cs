using System.Collections.Generic;
using OclParser.controller;

namespace OclParser.cst.literalExp
{
    public class CSTTupleLiteralExpCS : CSTLiteralExpCS {
        private List<object> tupleParts;

        public CSTTupleLiteralExpCS(
            OCLWorkbenchToken token,
            List<object> tupleParts) : base (token){
            this.tupleParts = tupleParts;
            }

        /**
     * @return
     */
        public List<object> getTuplePartsNodesCS() {
            return tupleParts;
        }

        public override void accept(CSTVisitor visitor) {
            this.accept(tupleParts, visitor);
            visitor.visitTupleLiteralExp(this);
        }
    }
}
