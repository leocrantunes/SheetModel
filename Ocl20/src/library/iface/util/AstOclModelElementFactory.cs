/**
 * AstOclModelElementFactory object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.iface.util
{
    public interface AstOclModelElementFactory {
        /**
     */
        void resetTypes();
        /**
     * @param collectionKind 
     * @param elementType 
     * @return 
     */
        CollectionType createSpecificCollectionType(CollectionKind collectionKind, CoreClassifier elementType);
        /**
     * @param elementType 
     * @return 
     */
        BagType createBagType(CoreClassifier elementType);
        /**
     * @param elementType 
     * @return 
     */
        SetType createSetType(CoreClassifier elementType);
        /**
     * @param elementType 
     * @return 
     */
        OrderedSetType createOrderedSetType(CoreClassifier elementType);
        /**
     * @param elementType 
     * @return 
     */
        SequenceType createSequenceType(CoreClassifier elementType);
        /**
     * @param elementType 
     * @return 
     */
        CollectionType createCollectionType(CoreClassifier elementType);
        /**
     * @param name 
     * @param elementType 
     * @return 
     */
        CollectionType createCollectionType(string name, CoreClassifier elementType);
        /**
     * @param tupleType 
     * @param name 
     * @param type 
     * @return 
     */
        TuplePartType createTuplePartType(TupleType tupleType, string name, CoreClassifier type);
        /**
     * @param varName 
     * @param varType 
     * @param varInitialization 
     * @return 
     */
        VariableDeclaration createVariableDeclaration(string varName, CoreClassifier varType, OclExpression varInitialization);
        /**
     * @param symbol 
     * @param type 
     * @return 
     */
        BooleanLiteralExp createBooleanLiteralExp(bool symbol, CoreClassifier type);
        /**
     * @param symbol 
     * @param type 
     * @return 
     */
        IntegerLiteralExp createIntegerLiteralExp(long symbol, CoreClassifier type);
        /**
     * @param symbol 
     * @param type 
     * @return 
     */
        RealLiteralExp createRealLiteralExp(string symbol, CoreClassifier type);
        /**
     * @param symbol 
     * @param typE 
     * @return 
     */
        StringLiteralExp createStringLiteralExp(string symbol, CoreClassifier typE);
        /**
     * @param symbol 
     * @param typE 
     * @return 
     */
        NullLiteralExp createNullLiteralExp(CoreClassifier typE);
        /**
     * @param symbol 
     * @param typE 
     * @return 
     */
        InvalidLiteralExp createInvalidLiteralExp(CoreClassifier typE);
        /**
     * @param from 
     * @param to 
     * @return 
     */
        CollectionRange createCollectionRange(OclExpression from, OclExpression to);
        /**
     * @param expression 
     * @return 
     */
        CollectionItem createCollectionItem(OclExpression expression);
        /**
     * @param parts 
     * @param type 
     * @return 
     */
        CollectionLiteralExp createCollectionLiteralExp(List<object> parts, CollectionType type);
        /**
     * @param tupleParts 
     * @param tupleType 
     * @return 
     */
        TupleLiteralExp createTupleLiteralExp(List<object> tupleParts, CoreClassifier tupleType);
        /**
     * @param variable 
     * @param expression 
     * @return 
     */
        LetExp createLetExp(VariableDeclaration variable, OclExpression expression);
        /**
     * @param conditionExp 
     * @param thenExp 
     * @param elseExp 
     * @return 
     */
        IfExp createIfExp(OclExpression conditionExp, OclExpression thenExp, OclExpression elseExp);
        /**
     * @param enumLiteral 
     * @return 
     */
        EnumLiteralExp createEnumLiteralExp(CoreEnumLiteral enumLiteral);
        /**
     * @param source 
     * @param attribute 
     * @param isMarkedPre 
     * @return 
     */
        AttributeCallExp createAttributeCallExp(OclExpression source, CoreAttribute attribute, bool isMarkedPre);
        /**
     * @param source 
     * @param referredAssociationEnd 
     * @param navigationSource 
     * @param qualifiers 
     * @param isMarkedPre 
     * @return 
     */
        AssociationEndCallExp createAssociationEndCallExp(OclExpression source, CoreAssociationEnd referredAssociationEnd, CoreAssociationEnd navigationSource, List<object> qualifiers, bool isMarkedPre);
        /**
     * @param source 
     * @param referredAssociationClass 
     * @param navigationSource 
     * @param qualifiers 
     * @param isMarkedPre 
     * @return 
     */
        AssociationClassCallExp createAssociationClassCallExp(OclExpression source, CoreAssociationClass referredAssociationClass, CoreAssociationEnd navigationSource, List<object> qualifiers, bool isMarkedPre);
        /**
     * @param oclModelElementType 
     * @return 
     */
        OclTypeLiteralExp createOclTypeLiteralExp(OclModelElementType oclModelElementType);
        /**
     * @param referredElement 
     * @return 
     */
        OclModelElementType createOclModelElementType(CoreModelElement referredElement);
        /**
     * @param variable 
     * @return 
     */
        VariableExp createVariableExp(VariableDeclaration variable);
        /**
     * @param source 
     * @param operation 
     * @param arguments 
     * @param returnType 
     * @param isMarkedPre 
     * @return 
     */
        OperationCallExp createOperationCallExp(OclExpression source, CoreOperation operation, List<object> arguments, CoreClassifier returnType, bool isMarkedPre);
        /**
     * @param returnType 
     * @param source 
     * @param opName 
     * @param arguments 
     * @param isMarkedPre 
     * @return 
     */
        OperationCallExp createOperationCallExp(CoreClassifier returnType, OclExpression source, string opName, List<object> arguments, bool isMarkedPre);
        /**
     * @param name 
     * @param type 
     * @param source 
     * @param body 
     * @param iterators 
     * @return 
     */
        IteratorExp createIteratorExp(string name, CoreClassifier type, OclExpression source, OclExpression body, List<object> iterators);
        /**
     * @param type 
     * @param source 
     * @param body 
     * @param iterators 
     * @param result 
     * @return 
     */
        IterateExp createIterateExp(CoreClassifier type, OclExpression source, OclExpression body, List<object> iterators, VariableDeclaration result);
        /**
     * @param source 
     * @return 
     */
        OperationCallExp createAtPreOperation(OclExpression source);
        /**
     * @param source 
     * @return 
     */
        OperationCallExp createAsSetOperation(OclExpression source);
        /**
     * @return 
     */
        TupleType createTupleType();
        /**
     * @param jmiOperation 
     * @param owner 
     * @return 
     */
        CollectionOperation createCollectionOperation(CoreOperation jmiOperation, CoreClassifier owner);
    }
}
