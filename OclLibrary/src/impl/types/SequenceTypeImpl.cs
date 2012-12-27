using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.types
{
    public abstract class SequenceTypeImpl : CollectionTypeImpl, SequenceType {
    
        public override CollectionKind getCollectionKind() {
            return	CollectionKindEnum.SEQUENCE;
        }

        protected override String getCollectionTypeName() {
            return 	"Sequence";
        }
	
        protected override String getReturnTypeForCollect() {
            return	"Sequence"; 
        }

        public override CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	getFactory().createSequenceType(elementType);
        }
    }
}
