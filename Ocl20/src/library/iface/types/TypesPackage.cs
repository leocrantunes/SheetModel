/**
 * types package interface.
 */

namespace Ocl20.library.iface.types
{
    public interface TypesPackage {
        /**
     * Returns CollectionOperation class proxy object.
     * @return CollectionOperation class proxy object.
     */
        CollectionOperation getCollectionOperation();
        /**
     * Returns TuplePartType class proxy object.
     * @return TuplePartType class proxy object.
     */
        TuplePartType getTuplePartType();
        /**
     * Returns OclModelElementType class proxy object.
     * @return OclModelElementType class proxy object.
     */
        OclModelElementType getOclModelElementType();
        /**
     * Returns OclType class proxy object.
     * @return OclType class proxy object.
     */
        OclType getOclType();
        /**
     * Returns VoidType class proxy object.
     * @return VoidType class proxy object.
     */
        VoidType getVoidType();
        /**
     * Returns OrderedSetType class proxy object.
     * @return OrderedSetType class proxy object.
     */
        OrderedSetType getOrderedSetType();
        /**
     * Returns BagType class proxy object.
     * @return BagType class proxy object.
     */
        BagType getBagType();
        /**
     * Returns SequenceType class proxy object.
     * @return SequenceType class proxy object.
     */
        SequenceType getSequenceType();
        /**
     * Returns SetType class proxy object.
     * @return SetType class proxy object.
     */
        SetType getSetType();
        /**
     * Returns CollectionType class proxy object.
     * @return CollectionType class proxy object.
     */
        CollectionType getCollectionType();
        /**
     * Returns TupleType class proxy object.
     * @return TupleType class proxy object.
     */
        TupleType getTupleType();
        /**
     * Returns FactoryCollectionType association proxy object.
     * @return FactoryCollectionType association proxy object.
     */
        FactoryCollectionType getFactoryCollectionType();
        /**
     * Returns FactoryTupleType association proxy object.
     * @return FactoryTupleType association proxy object.
     */
        FactoryTupleType getFactoryTupleType();
        /**
     * Returns CollectionOperationCoreOperation association proxy object.
     * @return CollectionOperationCoreOperation association proxy object.
     */
        CollectionOperationCoreOperation getCollectionOperationCoreOperation();
        /**
     * Returns OclModelElementTypeCoreModelElement association proxy object.
     * @return OclModelElementTypeCoreModelElement association proxy object.
     */
        OclModelElementTypeCoreModelElement getOclModelElementTypeCoreModelElement();
        /**
     * Returns TupleTypeTuplePartType association proxy object.
     * @return TupleTypeTuplePartType association proxy object.
     */
        TupleTypeTuplePartType getTupleTypeTuplePartType();
        /**
     * Returns CollectionTypeElementTypes association proxy object.
     * @return CollectionTypeElementTypes association proxy object.
     */
        CollectionTypeElementTypes getCollectionTypeElementTypes();
    }
}
