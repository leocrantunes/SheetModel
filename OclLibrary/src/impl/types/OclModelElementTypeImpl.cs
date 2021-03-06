using System;
using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;
using OclLibrary.iface.types;
using Environment = OclLibrary.iface.environment.Environment;

namespace OclLibrary.impl.types
{
    public abstract class OclModelElementTypeImpl : OclModelElementType {

        public override String ToString() {
            return this.getReferredModelElement().getName();
        }

        public abstract void deleteAllConstraintsForSource(string sourceName);

        public abstract CoreModel getModel();
        public abstract bool hasStereotype(string name);

        public String getName() {
            return "OclModelElementType";
        }

        public abstract void setName(string newValue);
        public abstract ICollection<object> getElemOwnedElements();
        public abstract CoreModelElement getElemOwner();
        public abstract void setElemOwner(CoreModelElement newValue);
        public abstract List<object> getConstraintExpressionInOcl();
        public abstract List<object> getTheStereotypes();

        public abstract CoreAttribute lookupAttribute(string name);
        public abstract CoreOperation lookupSameSignatureOperation(string name, List<object> paramTypes, CoreClassifier returnType);
        public abstract CoreOperation lookupOperation(string name, List<object> paramTypes);

        public bool conformsTo(CoreClassifier c) {
            return (c.GetType()  == typeof(OclModelElementType)) || (c.getName().Equals("OclType"));
        }

        public abstract ICollection<object> getAllAttributes();
        public abstract ICollection<object> getAllAttributesTransitiveClosure();
        public abstract List<object> getAllClassifierScopeAttributes();
        public abstract List<object> getAllDirectSubClasses();
        public abstract List<object> getAllSubClasses();
        public abstract bool isConcrete();
        public abstract List<object> getAllAncestors();
        public abstract List<object> getAllImplementedInterfaces();
        public abstract CoreClassifier getMostSpecificCommonSuperType(CoreClassifier otherClassifier);
        public abstract string getFullPathName();
        public abstract bool isEnumeration();
        public abstract List<object> getAllAssociationEnds();
        public abstract ICollection<object> getAllAssociationEndsTransitiveClosure();
        public abstract List<object> getAllAssociationClasses();
        public abstract CoreAssociationEnd lookupAssociationEnd(string name);
        public abstract CoreAssociationClass lookupAssociationClass(string name);
        public abstract List<object> getAllInterfaces();
        public abstract bool isClassifierDescendantOf(CoreClassifier superClass);
        public abstract void addInitConstraint(string elementName, OclInitConstraint constraint);
        public abstract OclInitConstraint getInitConstraint(string elementName);
        public abstract OclInitConstraint getLocalInitConstraint(string elementName);
        public abstract void addDeriveConstraint(string elementName, OclDeriveConstraint constraint);
        public abstract OclDeriveConstraint getDeriveConstraint(string elementName);
        public abstract OclDeriveConstraint getLocalDeriveConstraint(string elementName);
        public abstract void addInvariantConstraint(string name, OclInvariantConstraint constraint);
        public abstract List<object> getAllInvariants();
        public abstract OclInvariantConstraint getInvariant(string name);
        public abstract CoreAttribute addDefinedElement(string source, string name, CoreClassifier type);

        public abstract CoreOperation addDefinedOperation(string source, string name, List<object> paramNames, List<object> paramTypes,
                                                          CoreClassifier returnType);

        public abstract void setDirty(bool dirty);
        public abstract List<object> getConstraint();
        public abstract List<object> getClassifierFeatures();

        public abstract List<object> getAllAssociations();
        public abstract Environment getEnvironmentWithParents();
        public abstract Environment getEnvironmentWithoutParents();
        public abstract List<object> getElementsForEnvironment();
        public abstract CoreModelElement getReferredModelElement();
        public abstract void setReferredModelElement(CoreModelElement newValue);
    }
}
