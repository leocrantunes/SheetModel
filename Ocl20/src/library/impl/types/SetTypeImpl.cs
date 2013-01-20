using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.iface.util;

namespace Ocl20.library.impl.types
{
    public class SetTypeImpl : CollectionTypeImpl, SetType {

        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.SET;
        }
	
        protected override String getCollectionTypeName() {
            return 	"Set";
        }
	
        public override String getReturnTypeForCollect() {
            return	"Bag"; 
        }

        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createSetType(elementType);
        }
    }
}
