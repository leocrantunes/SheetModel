/**
 * Core package interface.
 */

using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface CorePackage {

        /**
         * Returns Element class proxy object.
         * @return Element class proxy object.
         */
        Element getElement();
        /**
         * Returns ModelElement class proxy object.
         * @return ModelElement class proxy object.
         */
        ModelElement getModelElement();
        /**
         * Returns GeneralizableElement class proxy object.
         * @return GeneralizableElement class proxy object.
         */
        GeneralizableElement getGeneralizableElement();
        /**
         * Returns Namespace class proxy object.
         * @return Namespace class proxy object.
         */
        Namespace getNamespace();
        /**
         * Returns Classifier class proxy object.
         * @return Classifier class proxy object.
         */
        Classifier getClassifier();
        /**
         * Returns UmlClass class proxy object.
         * @return UmlClass class proxy object.
         */
        UmlClass getUmlClass();
        /**
         * Returns DataType class proxy object.
         * @return DataType class proxy object.
         */
        DataType getDataType();
        /**
         * Returns Feature class proxy object.
         * @return Feature class proxy object.
         */
        Feature getFeature();
        /**
         * Returns StructuralFeature class proxy object.
         * @return StructuralFeature class proxy object.
         */
        StructuralFeature getStructuralFeature();
        /**
         * Returns AssociationEnd class proxy object.
         * @return AssociationEnd class proxy object.
         */
        AssociationEnd getAssociationEnd();
        /**
         * Returns Interface class proxy object.
         * @return Interface class proxy object.
         */
        Interface getInterface();
        /**
         * Returns Constraint class proxy object.
         * @return Constraint class proxy object.
         */
        //Constraint getConstraint();
        /**
         * Returns Relationship class proxy object.
         * @return Relationship class proxy object.
         */
        Relationship getRelationship();
        /**
         * Returns UmlAssociation class proxy object.
         * @return UmlAssociation class proxy object.
         */
        UmlAssociation getUmlAssociation();
        /**
         * Returns Attribute class proxy object.
         * @return Attribute class proxy object.
         */
        Attribute getAttribute();
        /**
         * Returns BehavioralFeature class proxy object.
         * @return BehavioralFeature class proxy object.
         */
        BehavioralFeature getBehavioralFeature();
        /**
         * Returns Operation class proxy object.
         * @return Operation class proxy object.
         */
        Operation getOperation();
        /**
         * Returns Parameter class proxy object.
         * @return Parameter class proxy object.
         */
        Parameter getParameter();
        /**
         * Returns Method class proxy object.
         * @return Method class proxy object.
         */
        //Method getMethod();
        /**
         * Returns Generalization class proxy object.
         * @return Generalization class proxy object.
         */
        Generalization getGeneralization();
        /**
         * Returns AssociationClass class proxy object.
         * @return AssociationClass class proxy object.
         */
        AssociationClass getAssociationClass();
        /**
         * Returns Dependency class proxy object.
         * @return Dependency class proxy object.
         */
        Dependency getDependency();
        /**
         * Returns Abstraction class proxy object.
         * @return Abstraction class proxy object.
         */
        //AbstractionClass getAbstraction();
        /**
         * Returns PresentationElement class proxy object.
         * @return PresentationElement class proxy object.
         */
        //PresentationElementClass getPresentationElement();
        /**
         * Returns Usage class proxy object.
         * @return Usage class proxy object.
         */
        //UsageClass getUsage();
        /**
         * Returns Binding class proxy object.
         * @return Binding class proxy object.
         */
        //BindingClass getBinding();
        /**
         * Returns Component class proxy object.
         * @return Component class proxy object.
         */
        //ComponentClass getComponent();
        /**
         * Returns Node class proxy object.
         * @return Node class proxy object.
         */
        //NodeClass getNode();
        /**
         * Returns Permission class proxy object.
         * @return Permission class proxy object.
         */
        //PermissionClass getPermission();
        /**
         * Returns Comment class proxy object.
         * @return Comment class proxy object.
         */
        //CommentClass getComment();
        /**
         * Returns Flow class proxy object.
         * @return Flow class proxy object.
         */
        //FlowClass getFlow();
        /**
         * Returns ElementResidence class proxy object.
         * @return ElementResidence class proxy object.
         */
        //ElementResidenceClass getElementResidence();
        /**
         * Returns TemplateParameter class proxy object.
         * @return TemplateParameter class proxy object.
         */
        //TemplateParameter getTemplateParameter();
        /**
         * Returns AAssociationConnection association proxy object.
         * @return AAssociationConnection association proxy object.
         */
        //AAssociationConnection getAAssociationConnection();
        /**
         * Returns AOwnerFeature association proxy object.
         * @return AOwnerFeature association proxy object.
         */
        //AOwnerFeature getAOwnerFeature();
        /**
         * Returns ASpecificationMethod association proxy object.
         * @return ASpecificationMethod association proxy object.
         */
        //ASpecificationMethod getASpecificationMethod();
        /**
         * Returns AStructuralFeatureType association proxy object.
         * @return AStructuralFeatureType association proxy object.
         */
        //AStructuralFeatureType getAStructuralFeatureType();
        /**
         * Returns ANamespaceOwnedElement association proxy object.
         * @return ANamespaceOwnedElement association proxy object.
         */
        //ANamespaceOwnedElement getANamespaceOwnedElement();
        /**
         * Returns ABehavioralFeatureParameter association proxy object.
         * @return ABehavioralFeatureParameter association proxy object.
         */
        //ABehavioralFeatureParameter getABehavioralFeatureParameter();
        /**
         * Returns AParameterType association proxy object.
         * @return AParameterType association proxy object.
         */
        //AParameterType getAParameterType();
        /**
         * Returns AChildGeneralization association proxy object.
         * @return AChildGeneralization association proxy object.
         */
        //AChildGeneralization getAChildGeneralization();
        /**
         * Returns AParentSpecialization association proxy object.
         * @return AParentSpecialization association proxy object.
         */
        //AParentSpecialization getAParentSpecialization();
        /**
         * Returns AQualifierAssociationEnd association proxy object.
         * @return AQualifierAssociationEnd association proxy object.
         */
        //AQualifierAssociationEnd getAQualifierAssociationEnd();
        /**
         * Returns ATypeAssociationEnd association proxy object.
         * @return ATypeAssociationEnd association proxy object.
         */
        //ATypeAssociationEnd getATypeAssociationEnd();
        /**
         * Returns AParticipantSpecification association proxy object.
         * @return AParticipantSpecification association proxy object.
         */
        //AParticipantSpecification getAParticipantSpecification();
        /**
         * Returns AClientClientDependency association proxy object.
         * @return AClientClientDependency association proxy object.
         */
        //AClientClientDependency getAClientClientDependency();
        /**
         * Returns AConstrainedElementConstraint association proxy object.
         * @return AConstrainedElementConstraint association proxy object.
         */
        //AConstrainedElementConstraint getAConstrainedElementConstraint();
        /**
         * Returns ASupplierSupplierDependency association proxy object.
         * @return ASupplierSupplierDependency association proxy object.
         */
        //ASupplierSupplierDependency getASupplierSupplierDependency();
        /**
         * Returns APresentationSubject association proxy object.
         * @return APresentationSubject association proxy object.
         */
        //APresentationSubject getAPresentationSubject();
        /**
         * Returns ADeploymentLocationResident association proxy object.
         * @return ADeploymentLocationResident association proxy object.
         */
        //ADeploymentLocationResident getADeploymentLocationResident();
        /**
         * Returns ATargetFlowTarget association proxy object.
         * @return ATargetFlowTarget association proxy object.
         */
        //ATargetFlowTarget getATargetFlowTarget();
        /**
         * Returns ASourceFlowSource association proxy object.
         * @return ASourceFlowSource association proxy object.
         */
        //ASourceFlowSource getASourceFlowSource();
        /**
         * Returns ADefaultElementTemplateParameter3 association proxy object.
         * @return ADefaultElementTemplateParameter3 association proxy object.
         */
        //ADefaultElementTemplateParameter3 getADefaultElementTemplateParameter3();
        /**
         * Returns ABindingArgument association proxy object.
         * @return ABindingArgument association proxy object.
         */
        //ABindingArgument getABindingArgument();
        /**
         * Returns APowertypePowertypeRange association proxy object.
         * @return APowertypePowertypeRange association proxy object.
         */
        //APowertypePowertypeRange getAPowertypePowertypeRange();
        /**
         * Returns ACommentAnnotatedElement association proxy object.
         * @return ACommentAnnotatedElement association proxy object.
         */
        //ACommentAnnotatedElement getACommentAnnotatedElement();
        /**
         * Returns AResidentElementResidence association proxy object.
         * @return AResidentElementResidence association proxy object.
         */
        //AResidentElementResidence getAResidentElementResidence();
        /**
         * Returns AImplementationLocationResidentElement association proxy object.
         * @return AImplementationLocationResidentElement association proxy object.
         */
        //AImplementationLocationResidentElement getAImplementationLocationResidentElement();
        /**
         * Returns AModelElementTemplateParameter association proxy object.
         * @return AModelElementTemplateParameter association proxy object.
         */
        //AModelElementTemplateParameter getAModelElementTemplateParameter();
        /**
         * Returns AModelElement2TemplateParameter2 association proxy object.
         * @return AModelElement2TemplateParameter2 association proxy object.
         */
        //AModelElement2TemplateParameter2 getAModelElement2TemplateParameter2();
    }
}
