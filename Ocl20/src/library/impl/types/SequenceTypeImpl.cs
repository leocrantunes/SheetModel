using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;

namespace Ocl20.library.impl.types
{
    public class SequenceTypeImpl : CollectionTypeImpl, SequenceType {
    
        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.SEQUENCE;
        }

        protected override String getCollectionTypeName() {
            return 	"Sequence";
        }
	
        public override String getReturnTypeForCollect() {
            return	"Sequence"; 
        }

        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createSequenceType(elementType);
        }
    }
}
