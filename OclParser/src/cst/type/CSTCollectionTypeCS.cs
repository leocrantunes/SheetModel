using System.Collections.Generic;
using OclParser.controller;

namespace OclParser.cst.type
{
    public class CSTCollectionTypeCS : CSTTypeCS {
        private CSTCollectionTypeIdentifierCS typeId;
        private CSTTypeCS elementType;

        public CSTCollectionTypeCS(
            CSTCollectionTypeIdentifierCS typeId,
            CSTTypeCS elementType) {
            this.typeId = typeId;
            this.elementType = elementType;
            }

        public override string getName() {
            return typeId.getName() + "(" + elementType.getName() + ")";
        }

        public override List<object> getAllSimpleTypesNodesCS() {
            return elementType.getAllSimpleTypesNodesCS();
        }

        /**
     * @return
     */
        public CSTTypeCS getElementTypeNodeCS() {
            return elementType;
        }

        /**
     * @return
     */
        public CSTCollectionTypeIdentifierCS getTypeIdNodeCS() {
            return typeId;
        }

        public override void accept(CSTVisitor visitor) {
            if (elementType != null) {
                elementType.accept(visitor);
                visitor.visitCollectionTypeCS(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return typeId.getToken();
        }
    }
}
