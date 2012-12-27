/**
 * CoreClassifier object instance interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.constraints;
using OclLibrary.impl.constraints;

namespace OclLibrary.iface.common
{
    public interface CoreClassifier : CoreNamespace, OclConstraintOwner {
        /**
     * @param name 
     * @return 
     */
        CoreAttribute lookupAttribute(string name);
        /**
     * @param name 
     * @param paramTypes 
     * @param returnType 
     * @return 
     */
        CoreOperation lookupSameSignatureOperation(string name, List<object> paramTypes, CoreClassifier returnType);
        /**
     * @param name 
     * @param paramTypes 
     * @return 
     */
        CoreOperation lookupOperation(string name, List<object> paramTypes);
        /**
     * @param c 
     * @return 
     */
        bool conformsTo(CoreClassifier c);
        /**
     * @return 
     */
        ICollection<object> getAllAttributes();
        /**
     * @return 
     */
        ICollection<object> getAllAttributesTransitiveClosure();

        /**
     * @return 
     */
        List<object> getAllClassifierScopeAttributes();
        /**
     * @return 
     */
        List<object> getAllDirectSubClasses();
        /**
     * @return 
     */
        List<object> getAllSubClasses();
        /**
     * @return 
     */
        bool isConcrete();
        /**
     * @return 
     */
        List<object> getAllAncestors();
        /**
     * @return 
     */
        List<object> getAllImplementedInterfaces();
        /**
     * @param otherClassifier 
     * @return 
     */
        CoreClassifier getMostSpecificCommonSuperType(CoreClassifier otherClassifier);
        /**
     * @return 
     */
        string getFullPathName();
        /**
     * @return 
     */
        bool isEnumeration();
        /**
     * @return 
     */
        List<object> getAllAssociationEnds();
        /**
     * @return 
     */
        ICollection<object> getAllAssociationEndsTransitiveClosure();

        /**
     * @return 
     */
        List<object> getAllAssociationClasses();
        /**
     * @param name 
     * @return 
     */
        CoreAssociationEnd lookupAssociationEnd(string name);
        /**
     * @param name 
     * @return 
     */
        CoreAssociationClass lookupAssociationClass(string name);
        /**
     * @return 
     */
        List<object> getAllInterfaces();
        /**
     * @param superClass 
     * @return 
     */
        bool isClassifierDescendantOf(CoreClassifier superClass);
        /**
     * @param elementName 
     * @param constraint 
     */
        void addInitConstraint(string elementName, OclInitConstraint constraint);
        /**
     * @param elementName 
     * @return 
     */
        OclInitConstraint getInitConstraint(string elementName);
        /**
     * @param elementName 
     * @return 
     */
        OclInitConstraint getLocalInitConstraint(string elementName);
        /**
     * @param elementName 
     * @param constraint 
     */
        void addDeriveConstraint(string elementName, OclDeriveConstraint constraint);
        /**
     * @param elementName 
     * @return 
     */
        OclDeriveConstraint getDeriveConstraint(string elementName);
        /**
     * @param elementName 
     * @return 
     */
        OclDeriveConstraint getLocalDeriveConstraint(string elementName);
        /**
     * @param name 
     * @param constraint 
     */
        void addInvariantConstraint(string name, OclInvariantConstraint constraint);
        /**
     * @return 
     */
        List<object> getAllInvariants();
        /**
     * @param name 
     * @return 
     */
        OclInvariantConstraint getInvariant(string name);
        /**
     * @param source 
     * @param name 
     * @param type 
     * @return 
     */
        CoreAttribute addDefinedElement(string source, string name, CoreClassifier type);
        /**
     * @param sourceName 
     */
        //void deleteAllConstraintsForSource(string sourceName);
        /**
     * @param source 
     * @param name 
     * @param paramNames 
     * @param paramTypes 
     * @param returnType 
     * @return 
     */
        CoreOperation addDefinedOperation(string source, string name, List<object> paramNames, List<object> paramTypes, CoreClassifier returnType);
        /**
     * @param dirty 
     */
        void setDirty(bool dirty);
        /**
     * Returns the value of reference constraint.
     * @return Value of reference constraint.
     */
        List<object> getConstraint();
        /**
     * Returns the value of reference classifierFeatures.
     * @return Value of reference classifierFeatures.
     */
        List<object> getClassifierFeatures();
    }
}
