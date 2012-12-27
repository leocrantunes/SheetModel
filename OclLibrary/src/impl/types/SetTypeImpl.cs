using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.types
{
    public abstract class SetTypeImpl : CollectionTypeImpl, SetType {

        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.SET;
        }
	
        protected override String getCollectionTypeName() {
            return 	"Set";
        }
	
        protected override String getReturnTypeForCollect() {
            return	"Bag"; 
        }

        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createSetType(elementType);
        }
    }
}
