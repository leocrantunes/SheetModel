using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;

namespace Ocl20.library.impl.types
{
    public abstract class OrderedSetTypeImpl : CollectionTypeImpl, OrderedSetType {

        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.ORDERED_SET;
        }

        protected override String getCollectionTypeName() {
            return 	"OrderedSet";
        }

        public override String getReturnTypeForCollect() {
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
