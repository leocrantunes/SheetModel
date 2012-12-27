using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.iface.types;

namespace OclLibrary.impl.types
{
    public abstract class BagTypeImpl : CollectionTypeImpl, BagType {

        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.BAG;
        }

        protected override String getCollectionTypeName() {
            return 	"Bag";
        }
	
        protected override String	getReturnTypeForCollect() {
            return	"Bag"; 
        }
	
        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createBagType(elementType);
        }

    }
}
