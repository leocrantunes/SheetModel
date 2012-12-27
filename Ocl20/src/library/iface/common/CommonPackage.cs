/**
 * common package interface.
 */

namespace Ocl20.library.iface.common
{
    public interface CommonPackage {
        /**
     * Returns CoreStructuralFeature class proxy object.
     * @return CoreStructuralFeature class proxy object.
     */
        CoreStructuralFeatureClass getCoreStructuralFeature();
        /**
     * Returns CoreStereotype class proxy object.
     * @return CoreStereotype class proxy object.
     */
        CoreStereotypeClass getCoreStereotype();
        /**
     * Returns CorePrimitive class proxy object.
     * @return CorePrimitive class proxy object.
     */
        CorePrimitiveClass getCorePrimitive();
        /**
     * Returns CorePackage class proxy object.
     * @return CorePackage class proxy object.
     */
        CorePackageClass getCorePackage();
        /**
     * Returns CoreOperation class proxy object.
     * @return CoreOperation class proxy object.
     */
        CoreOperationClass getCoreOperation();
        /**
     * Returns CoreNamespace class proxy object.
     * @return CoreNamespace class proxy object.
     */
        CoreNamespaceClass getCoreNamespace();
        /**
     * Returns CoreModelElement class proxy object.
     * @return CoreModelElement class proxy object.
     */
        CoreModelElementClass getCoreModelElement();
        /**
     * Returns CoreModel class proxy object.
     * @return CoreModel class proxy object.
     */
        CoreModelClass getCoreModel();
        /**
     * Returns CoreInterface class proxy object.
     * @return CoreInterface class proxy object.
     */
        CoreInterfaceClass getCoreInterface();
        /**
     * Returns CoreFeature class proxy object.
     * @return CoreFeature class proxy object.
     */
        CoreFeatureClass getCoreFeature();
        /**
     * Returns CoreEnumeration class proxy object.
     * @return CoreEnumeration class proxy object.
     */
        CoreEnumerationClass getCoreEnumeration();
        /**
     * Returns CoreEnumLiteral class proxy object.
     * @return CoreEnumLiteral class proxy object.
     */
        CoreEnumLiteralClass getCoreEnumLiteral();
        /**
     * Returns CoreDataType class proxy object.
     * @return CoreDataType class proxy object.
     */
        CoreDataTypeClass getCoreDataType();
        /**
     * Returns CoreClassifier class proxy object.
     * @return CoreClassifier class proxy object.
     */
        CoreClassifierClass getCoreClassifier();
        /**
     * Returns CoreClass class proxy object.
     * @return CoreClass class proxy object.
     */
        CoreClassClass getCoreClass();
        /**
     * Returns CoreBehavioralFeature class proxy object.
     * @return CoreBehavioralFeature class proxy object.
     */
        CoreBehavioralFeatureClass getCoreBehavioralFeature();
        /**
     * Returns CoreAttribute class proxy object.
     * @return CoreAttribute class proxy object.
     */
        CoreAttributeClass getCoreAttribute();
        /**
     * Returns CoreAssociationEnd class proxy object.
     * @return CoreAssociationEnd class proxy object.
     */
        CoreAssociationEndClass getCoreAssociationEnd();
        /**
     * Returns CoreAssociationClass class proxy object.
     * @return CoreAssociationClass class proxy object.
     */
        CoreAssociationClassClass getCoreAssociationClass();
        /**
     * Returns CoreAssociation class proxy object.
     * @return CoreAssociation class proxy object.
     */
        CoreAssociationClass getCoreAssociation();
        /**
     * Returns AttachedStereotype association proxy object.
     * @return AttachedStereotype association proxy object.
     */
        AttachedStereotype getAttachedStereotype();
        /**
     * Returns FeatureOwnership association proxy object.
     * @return FeatureOwnership association proxy object.
     */
        FeatureOwnership getFeatureOwnership();
        /**
     * Returns FeatureType association proxy object.
     * @return FeatureType association proxy object.
     */
        FeatureType getFeatureType();
        /**
     * Returns ElementsForEnv association proxy object.
     * @return ElementsForEnv association proxy object.
     */
        ElementsForEnv getElementsForEnv();
        /**
     * Returns EnumerationLiterals association proxy object.
     * @return EnumerationLiterals association proxy object.
     */
        EnumerationLiterals getEnumerationLiterals();
        /**
     * Returns RattributeAssocEnd association proxy object.
     * @return RattributeAssocEnd association proxy object.
     */
        RattributeAssocEnd getRattributeAssocEnd();
        /**
     * Returns AssociationEndClassifier association proxy object.
     * @return AssociationEndClassifier association proxy object.
     */
        AssociationEndClassifier getAssociationEndClassifier();
        /**
     * Returns AssociationConnections association proxy object.
     * @return AssociationConnections association proxy object.
     */
        AssociationConnections getAssociationConnections();
        /**
     * Returns Owns association proxy object.
     * @return Owns association proxy object.
     */
        Owns getOwns();
    }
}
