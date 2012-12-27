using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.types
{
    public abstract class OrderedSetTypeImpl : CollectionTypeImpl, OrderedSetType {

        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.ORDERED_SET;
        }

        protected override String getCollectionTypeName() {
            return 	"OrderedSet";
        }

        protected override String getReturnTypeForCollect() {
            return	"Sequence"; 
        }

        protected override bool areCollectionsCompatible(CoreClassifier c) {
            if (base.areCollectionsCompatible(c) || c.getName().StartsWith("Set(")) 
                return elementTypesConformance(c);
            else
                return false;
        }

        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createOrderedSetType(elementType);
        }
    }
}
