using System;
using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.iface.types;
using CoreAssociationEnd = OclLibrary.iface.expressions.CoreAssociationEnd;

namespace OclLibrary.impl.util
{
    public interface AstOclModelElementFactory {
        void resetTypes();

        CollectionType createSpecificCollectionType(
            CollectionKind collectionKind,
            CoreClassifier elementType);

        BagType createBagType(CoreClassifier elementType);

        SetType createSetType(CoreClassifier elementType);

        OrderedSetType createOrderedSetType(CoreClassifier elementType);

        SequenceType createSequenceType(CoreClassifier elementType);

        CollectionType createCollectionType(CoreClassifier elementType);
    
        CollectionType createCollectionType(
            String name,
            CoreClassifier elementType);

        TupleType createTupleType();
    
        TuplePartType  createTuplePartType(TupleType tupleType, String name, CoreClassifier type);
    
        VariableDeclaration createVariableDeclaration(
            String varName,
            CoreClassifier varType,
            CoreAssociationEnd varInitialization);

        BooleanLiteralExp createboolLiteralExp(
            bool symbol,
            CoreClassifier type);

        IntegerLiteralExp createIntegerLiteralExp(
            long symbol,
            CoreClassifier type);

        RealLiteralExp createRealLiteralExp(
            String symbol,
            CoreClassifier type);

        StringLiteralExp createStringLiteralExp(
            String symbol,
            CoreClassifier type);

        CollectionRange createCollectionRange(
            CoreAssociationEnd from,
            CoreAssociationEnd to);

        CollectionItem createCollectionItem(CoreAssociationEnd expression);

        CollectionLiteralExp createCollectionLiteralExp(
            List<object> parts,
            CollectionType type);

        TupleLiteralExp createTupleLiteralExp(
            List<object> tupleParts,
            CoreClassifier tupleType);

        LetExp createLetExp(
            VariableDeclaration variable,
            CoreAssociationEnd expression);

        IfExp createIfExp(
            CoreAssociationEnd conditionExp,
            CoreAssociationEnd thenExp,
            CoreAssociationEnd elseExp);

        EnumLiteralExp createEnumLiteralExp(CoreEnumLiteral enumLiteral);

        AttributeCallExp createAttributeCallExp(
            CoreAssociationEnd source,
            CoreAttribute attribute,
            bool isMarkedPre);

        AssociationEndCallExp createAssociationEndCallExp(
            CoreAssociationEnd source,
            CoreAssociationEnd referredAssociationEnd,
            CoreAssociationEnd navigationSource,
            List<object> qualifiers,
            bool isMarkedPre);

        AssociationClassCallExp createAssociationClassCallExp(
            CoreAssociationEnd source,
            CoreAssociationClass referredAssociationClass,
            CoreAssociationEnd navigationSource,
            List<object> qualifiers,
            bool isMarkedPre);

        OclTypeLiteralExp createOclTypeLiteralExp(OclModelElementType oclModelElementType);
    
        OclModelElementType createOclModelElementType(CoreModelElement referredElement);

        VariableExp createVariableExp(VariableDeclaration variable);

        OperationCallExp createOperationCallExp(
            CoreAssociationEnd source,
            CoreOperation operation,
            List<object> arguments,
            CoreClassifier returnType,
            bool isMarkedPre);

        OperationCallExp createOperationCallExp(
            CoreClassifier returnType,
            CoreAssociationEnd source,
            String opName,
            List<object> arguments, 
            bool isMarkedPre);

        IteratorExp createIteratorExp(
            String name,
            CoreClassifier type,
            CoreAssociationEnd source,
            CoreAssociationEnd body,
            List<object> iterators);

        IterateExp createIterateExp(
            CoreClassifier type,
            CoreAssociationEnd source,
            CoreAssociationEnd body,
            List<object> iterators,
            VariableDeclaration result);

        OperationCallExp createAtPreOperation(CoreAssociationEnd source);

        OperationCallExp createAsSetOperation(CoreAssociationEnd source);
    }
}
