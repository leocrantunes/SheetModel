/**
 * expressions package interface.
 */

namespace Ocl20.library.iface.expressions
{
    public interface ExpressionsPackage {
        /**
     * Returns OclTypeLiteralExp class proxy object.
     * @return OclTypeLiteralExp class proxy object.
     */
        OclTypeLiteralExpClass getOclTypeLiteralExp();
        /**
     * Returns TupleLiteralExp class proxy object.
     * @return TupleLiteralExp class proxy object.
     */
        TupleLiteralExpClass getTupleLiteralExp();
        /**
     * Returns LetExp class proxy object.
     * @return LetExp class proxy object.
     */
        LetExpClass getLetExp();
        /**
     * Returns EnumLiteralExp class proxy object.
     * @return EnumLiteralExp class proxy object.
     */
        EnumLiteralExpClass getEnumLiteralExp();
        /**
     * Returns CollectionItem class proxy object.
     * @return CollectionItem class proxy object.
     */
        CollectionItemClass getCollectionItem();
        /**
     * Returns CollectionRange class proxy object.
     * @return CollectionRange class proxy object.
     */
        CollectionRangeClass getCollectionRange();
        /**
     * Returns CollectionLiteralPart class proxy object.
     * @return CollectionLiteralPart class proxy object.
     */
        CollectionLiteralPartClass getCollectionLiteralPart();
        /**
     * Returns CollectionLiteralExp class proxy object.
     * @return CollectionLiteralExp class proxy object.
     */
        CollectionLiteralExpClass getCollectionLiteralExp();
        /**
     * Returns BooleanLiteralExp class proxy object.
     * @return BooleanLiteralExp class proxy object.
     */
        boolLiteralExpClass getboolLiteralExp();
        /**
     * Returns StringLiteralExp class proxy object.
     * @return StringLiteralExp class proxy object.
     */
        StringLiteralExpClass getStringLiteralExp();
        /**
     * Returns RealLiteralExp class proxy object.
     * @return RealLiteralExp class proxy object.
     */
        RealLiteralExpClass getRealLiteralExp();
        /**
     * Returns IntegerLiteralExp class proxy object.
     * @return IntegerLiteralExp class proxy object.
     */
        IntegerLiteralExpClass getIntegerLiteralExp();
        /**
     * Returns NumericLiteralExp class proxy object.
     * @return NumericLiteralExp class proxy object.
     */
        NumericLiteralExpClass getNumericLiteralExp();
        /**
     * Returns PrimitiveLiteralExp class proxy object.
     * @return PrimitiveLiteralExp class proxy object.
     */
        PrimitiveLiteralExpClass getPrimitiveLiteralExp();
        /**
     * Returns OperationCallExp class proxy object.
     * @return OperationCallExp class proxy object.
     */
        OperationCallExpClass getOperationCallExp();
        /**
     * Returns AssociationClassCallExp class proxy object.
     * @return AssociationClassCallExp class proxy object.
     */
        AssociationClassCallExpClass getAssociationClassCallExp();
        /**
     * Returns AssociationEndCallExp class proxy object.
     * @return AssociationEndCallExp class proxy object.
     */
        AssociationEndCallExpClass getAssociationEndCallExp();
        /**
     * Returns NavigationCallExp class proxy object.
     * @return NavigationCallExp class proxy object.
     */
        NavigationCallExpClass getNavigationCallExp();
        /**
     * Returns AttributeCallExp class proxy object.
     * @return AttributeCallExp class proxy object.
     */
        AttributeCallExpClass getAttributeCallExp();
        /**
     * Returns IterateExp class proxy object.
     * @return IterateExp class proxy object.
     */
        IterateExpClass getIterateExp();
        /**
     * Returns IteratorExp class proxy object.
     * @return IteratorExp class proxy object.
     */
        IteratorExpClass getIteratorExp();
        /**
     * Returns LoopExp class proxy object.
     * @return LoopExp class proxy object.
     */
        LoopExpClass getLoopExp();
        /**
     * Returns ModelPropertyCallExp class proxy object.
     * @return ModelPropertyCallExp class proxy object.
     */
        ModelPropertyCallExpClass getModelPropertyCallExp();
        /**
     * Returns OclMessageExp class proxy object.
     * @return OclMessageExp class proxy object.
     */
        OclMessageExpClass getOclMessageExp();
        /**
     * Returns VariableExp class proxy object.
     * @return VariableExp class proxy object.
     */
        VariableExpClass getVariableExp();
        /**
     * Returns IfExp class proxy object.
     * @return IfExp class proxy object.
     */
        IfExpClass getIfExp();
        /**
     * Returns LiteralExp class proxy object.
     * @return LiteralExp class proxy object.
     */
        LiteralExpClass getLiteralExp();
        /**
     * Returns PropertyCallExp class proxy object.
     * @return PropertyCallExp class proxy object.
     */
        PropertyCallExpClass getPropertyCallExp();
        /**
     * Returns VariableDeclaration class proxy object.
     * @return VariableDeclaration class proxy object.
     */
        VariableDeclarationClass getVariableDeclaration();
        /**
     * Returns OclExpression class proxy object.
     * @return OclExpression class proxy object.
     */
        OclExpressionClass getOclExpression();
        /**
     * Returns OclModelElement class proxy object.
     * @return OclModelElement class proxy object.
     */
        OclModelElementClass getOclModelElement();
        /**
     * Returns FactoryOclModelElement association proxy object.
     * @return FactoryOclModelElement association proxy object.
     */
        FactoryOclModelElement getFactoryOclModelElement();
        /**
     * Returns TupleLiteralExpTuplePart association proxy object.
     * @return TupleLiteralExpTuplePart association proxy object.
     */
        TupleLiteralExpTuplePart getTupleLiteralExpTuplePart();
        /**
     * Returns LetExpOclExpression association proxy object.
     * @return LetExpOclExpression association proxy object.
     */
        LetExpOclExpression getLetExpOclExpression();
        /**
     * Returns LetExpVariableDeclaration association proxy object.
     * @return LetExpVariableDeclaration association proxy object.
     */
        LetExpVariableDeclaration getLetExpVariableDeclaration();
        /**
     * Returns EnumLiteralExpCoreEnumLiteral association proxy object.
     * @return EnumLiteralExpCoreEnumLiteral association proxy object.
     */
        EnumLiteralExpCoreEnumLiteral getEnumLiteralExpCoreEnumLiteral();
        /**
     * Returns CollectionLiteralPartCoreClassifier association proxy object.
     * @return CollectionLiteralPartCoreClassifier association proxy object.
     */
        CollectionLiteralPartCoreClassifier getCollectionLiteralPartCoreClassifier();
        /**
     * Returns CollectionItemOclExpression association proxy object.
     * @return CollectionItemOclExpression association proxy object.
     */
        CollectionItemOclExpression getCollectionItemOclExpression();
        /**
     * Returns CollectionRangeFirst association proxy object.
     * @return CollectionRangeFirst association proxy object.
     */
        CollectionRangeFirst getCollectionRangeFirst();
        /**
     * Returns CollectionRangeLast association proxy object.
     * @return CollectionRangeLast association proxy object.
     */
        CollectionRangeLast getCollectionRangeLast();
        /**
     * Returns CollectionLiteralExpLiteralPart association proxy object.
     * @return CollectionLiteralExpLiteralPart association proxy object.
     */
        CollectionLiteralExpLiteralPart getCollectionLiteralExpLiteralPart();
        /**
     * Returns IfExpThen association proxy object.
     * @return IfExpThen association proxy object.
     */
        IfExpThen getIfExpThen();
        /**
     * Returns IfExpElse association proxy object.
     * @return IfExpElse association proxy object.
     */
        IfExpElse getIfExpElse();
        /**
     * Returns IfExpCondition association proxy object.
     * @return IfExpCondition association proxy object.
     */
        IfExpCondition getIfExpCondition();
        /**
     * Returns NavigationCallExpCoreAssociationEnd association proxy object.
     * @return NavigationCallExpCoreAssociationEnd association proxy object.
     */
        NavigationCallExpCoreAssociationEnd getNavigationCallExpCoreAssociationEnd();
        /**
     * Returns NavigationCallExpOclExpression association proxy object.
     * @return NavigationCallExpOclExpression association proxy object.
     */
        NavigationCallExpOclExpression getNavigationCallExpOclExpression();
        /**
     * Returns OperationCallExpOclExpression association proxy object.
     * @return OperationCallExpOclExpression association proxy object.
     */
        OperationCallExpOclExpression getOperationCallExpOclExpression();
        /**
     * Returns OperationCallExpCoreOperation association proxy object.
     * @return OperationCallExpCoreOperation association proxy object.
     */
        OperationCallExpCoreOperation getOperationCallExpCoreOperation();
        /**
     * Returns AssociationClassCallExpCoreAssociationClass association proxy object.
     * @return AssociationClassCallExpCoreAssociationClass association proxy object.
     */
        AssociationClassCallExpCoreAssociationClass getAssociationClassCallExpCoreAssociationClass();
        /**
     * Returns AssociationEndCallExpCoreAssociationEnd association proxy object.
     * @return AssociationEndCallExpCoreAssociationEnd association proxy object.
     */
        AssociationEndCallExpCoreAssociationEnd getAssociationEndCallExpCoreAssociationEnd();
        /**
     * Returns AttributeCallExpCoreAttribute association proxy object.
     * @return AttributeCallExpCoreAttribute association proxy object.
     */
        AttributeCallExpCoreAttribute getAttributeCallExpCoreAttribute();
        /**
     * Returns IterateExpVariableDeclaration association proxy object.
     * @return IterateExpVariableDeclaration association proxy object.
     */
        IterateExpVariableDeclaration getIterateExpVariableDeclaration();
        /**
     * Returns LoopExpVariableDeclaration association proxy object.
     * @return LoopExpVariableDeclaration association proxy object.
     */
        LoopExpVariableDeclaration getLoopExpVariableDeclaration();
        /**
     * Returns VariableExpVariableDeclaration association proxy object.
     * @return VariableExpVariableDeclaration association proxy object.
     */
        VariableExpVariableDeclaration getVariableExpVariableDeclaration();
        /**
     * Returns LoopExpOclExpression association proxy object.
     * @return LoopExpOclExpression association proxy object.
     */
        LoopExpOclExpression getLoopExpOclExpression();
        /**
     * Returns PropertyCallExpOclExpression association proxy object.
     * @return PropertyCallExpOclExpression association proxy object.
     */
        PropertyCallExpOclExpression getPropertyCallExpOclExpression();
        /**
     * Returns VariableDeclarationCoreClassifier association proxy object.
     * @return VariableDeclarationCoreClassifier association proxy object.
     */
        VariableDeclarationCoreClassifier getVariableDeclarationCoreClassifier();
        /**
     * Returns VariableDeclarationOclExpression association proxy object.
     * @return VariableDeclarationOclExpression association proxy object.
     */
        VariableDeclarationOclExpression getVariableDeclarationOclExpression();
        /**
     * Returns OclExpressionClassifier association proxy object.
     * @return OclExpressionClassifier association proxy object.
     */
        OclExpressionClassifier getOclExpressionClassifier();
    }
}
