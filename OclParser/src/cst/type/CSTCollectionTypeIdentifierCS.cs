// TODO : Rever Collection Kind

using OclParser.controller;

namespace OclParser.cst.type
{
    public class CSTCollectionTypeIdentifierCS : CSTNode    {
        private OCLWorkbenchToken token;
        private string[] collectionTypeNames = {
            "Set", "Bag", "Sequence", "OrderedSet", "Collection"
        };
    
        //private CollectionKindEnum[] collectionKinds = {
        //    CollectionKindEnum.SET, CollectionKindEnum.BAG,
        //   CollectionKindEnum.SEQUENCE, CollectionKindEnum.ORDERED_SET,
        //    CollectionKindEnum.COLLECTION
        //};

        public CSTCollectionTypeIdentifierCS(OCLWorkbenchToken token) {
            this.token = token;
        }

        //public CollectionKindEnum getCollectionKind() {
        //    for (int i = 0; i < collectionTypeNames.Length; i++) {
        //        if (getName()
        //                    .Equals(collectionTypeNames[i])) {
        //            return collectionKinds[i];
        //        }
        //    }

        //    return CollectionKindEnum.COLLECTION;
        //}

        public string getName() {
            return getToken().getText();
        }

        public override OCLWorkbenchToken getToken() {
            return this.token;
        }
    }
}
