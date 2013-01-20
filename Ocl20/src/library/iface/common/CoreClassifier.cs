/**
 * CoreClassifier object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.constraints;

namespace Ocl20.library.iface.common
{
    public interface CoreClassifier : CoreNamespace, OclConstraintOwner {
        
        CoreAttribute lookupAttribute(string name);
        CoreOperation lookupSameSignatureOperation(string name, List<object> paramTypes, CoreClassifier returnType);
        CoreOperation lookupOperation(string name, List<object> paramTypes);
        bool conformsTo(CoreClassifier c);
        ICollection<object> getAllAttributes();
        ICollection<object> getAllAttributesTransitiveClosure();
        List<object> getAllClassifierScopeAttributes();
        List<object> getAllDirectSubClasses();
        List<object> getAllSubClasses();
        bool isConcrete();
        List<object> getAllAncestors();
        List<object> getAllImplementedInterfaces();
        CoreClassifier getMostSpecificCommonSuperType(CoreClassifier otherClassifier);
        string getFullPathName();
        bool isEnumeration();
        List<object> getAllAssociationEnds();
        ICollection<object> getAllAssociationEndsTransitiveClosure();
        List<object> getAllAssociationClasses();
        CoreAssociationEnd lookupAssociationEnd(string name);
        CoreAssociationClass lookupAssociationClass(string name);
        List<object> getAllInterfaces();
        bool isClassifierDescendantOf(CoreClassifier superClass);
        void addInitConstraint(string elementName, OclInitConstraint constraint);
        OclInitConstraint getInitConstraint(string elementName);
        OclInitConstraint getLocalInitConstraint(string elementName);
        void addDeriveConstraint(string elementName, OclDeriveConstraint constraint);
        OclDeriveConstraint getDeriveConstraint(string elementName);
        OclDeriveConstraint getLocalDeriveConstraint(string elementName);
        void addInvariantConstraint(string name, OclInvariantConstraint constraint);
        List<object> getAllInvariants();
        OclInvariantConstraint getInvariant(string name);
        CoreAttribute addDefinedElement(string source, string name, CoreClassifier type);
        //void deleteAllConstraintsForSource(string sourceName);
        CoreOperation addDefinedOperation(string source, string name, List<object> paramNames, List<object> paramTypes, CoreClassifier returnType);
        void setDirty(bool dirty);
        List<object> getConstraint();
        List<object> getClassifierFeatures();
        bool isAbstract();
        List<Generalization> getGeneralization();
        void setGeneralization(List<Generalization> newValue);
        List<Generalization> getSpecialization();
        void setSpecialization(List<Generalization> newValue);
        List<object> getFeature();
        List<Dependency> getSupplierDependency();
        void setSupplierDependency(List<Dependency> newValue);
    }
}
