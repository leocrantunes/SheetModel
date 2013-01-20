using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;

namespace Ocl20.library.impl.types
{
    public class BagTypeImpl : CollectionTypeImpl, BagType {

        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.BAG;
        }

        protected override String getCollectionTypeName() {
            return 	"Bag";
        }
	
        public override String	getReturnTypeForCollect() {
            return	"Bag"; 
        }
	
        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createBagType(elementType);
        }

    }
}
