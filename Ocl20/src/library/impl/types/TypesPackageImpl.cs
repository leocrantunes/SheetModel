using Ocl20.library.iface.types;

namespace Ocl20.library.impl.types
{
    public class TypesPackageImpl : TypesPackage
    {
        public CollectionOperation getCollectionOperation()
        {
            return new CollectionOperationImpl();
        }

        public TuplePartType getTuplePartType()
        {
            return new TuplePartTypeImpl();
        }

        public OclModelElementType getOclModelElementType()
        {
            throw new System.NotImplementedException();
        }

        public OclType getOclType()
        {
            throw new System.NotImplementedException();
        }

        public VoidType getVoidType()
        {
            throw new System.NotImplementedException();
        }

        public OrderedSetType getOrderedSetType()
        {
            return new OrderedSetTypeImpl();
        }

        public BagType getBagType()
        {
            return new BagTypeImpl();
        }

        public SequenceType getSequenceType()
        {
            return new SequenceTypeImpl();
        }

        public SetType getSetType()
        {
            return new SetTypeImpl();
        }

        public CollectionType getCollectionType()
        {
            return new CollectionTypeImpl();
        }

        public TupleType getTupleType()
        {
            return new TupleTypeImpl();
        }

        public FactoryCollectionType getFactoryCollectionType()
        {
            throw new System.NotImplementedException();
        }

        public FactoryTupleType getFactoryTupleType()
        {
            throw new System.NotImplementedException();
        }

        public CollectionOperationCoreOperation getCollectionOperationCoreOperation()
        {
            throw new System.NotImplementedException();
        }

        public OclModelElementTypeCoreModelElement getOclModelElementTypeCoreModelElement()
        {
            throw new System.NotImplementedException();
        }

        public TupleTypeTuplePartType getTupleTypeTuplePartType()
        {
            throw new System.NotImplementedException();
        }

        public CollectionTypeElementTypes getCollectionTypeElementTypes()
        {
            throw new System.NotImplementedException();
        }
    }
}
