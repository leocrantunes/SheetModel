using System.Collections.Generic;
using Ocl20.parser.cst.context;
using Ocl20.parser.cst.expression;
using Ocl20.parser.cst.literalExp;
using Ocl20.parser.cst.type;

namespace Ocl20.parser.cst
{
    public interface CSTVisitor {
        void visitPackageDeclarationCS(CSTPackageDeclarationCS packageDeclaration);
        void visitClassifierContextDeclCS(CSTClassifierContextDeclCS classifierContextDeclaration);
        void visitAttrOrAssocContextDeclCSBegin(CSTAttrOrAssocContextCS attrAssocContextDeclaration);
        void visitAttrOrAssocContextDeclCSEnd(CSTAttrOrAssocContextCS attrAssocContextDeclaration);
        void visitOperationContextDeclCSBegin(CSTOperationContextCS operationContextDeclaration);
        void visitOperationContextDeclCSEnd(CSTOperationContextCS operationContextDeclaration);
        void visitOperationConstraint(CSTOperationConstraintCS operationConstraintExpression);
        void visitPreDecl(CSTPreDeclCS preDeclExpression);
        void visitPostDecl(CSTPostDeclCS preDeclExpression);
        void visitBodyDecl(CSTBodyDeclCS preDeclExpression);
        void visitInvariantCSBegin(CSTInvariantCS invariantDeclaration);
        void visitInvariantCSEnd(CSTInvariantCS invariantDeclaration);
        void visitDefVarExpressionBegin(CSTDefVarExpressionCS defVarDeclaration);
        void visitDefVarExpressionEnd(CSTDefVarExpressionCS defVarDeclaration);
        void visitDefOperationExpressionBegin(CSTDefOperationExpressionCS defVarDeclaration);
        void visitDefOperationExpressionEnd(CSTDefOperationExpressionCS defVarDeclaration);
        void visitExpressionInOcl(CSTExpressionInOclCS expression);
        void visitExpressionInOclEnd(CSTExpressionInOclCS expression);
        void visitBooleanLiteralExp(CSTBooleanLiteralExpCS literalExp);
        void visitStringLiteralExp(CSTStringLiteralExpCS literalExp);
        void visitIntegerLiteralExp(CSTIntegerLiteralExpCS literalExp);
        void visitRealLiteralExp(CSTRealLiteralExpCS literalExp);
        void visitNullLiteralExp(CSTNullLiteralExpCS literalExp);
        void visitInvalidLiteralExp(CSTInvalidLiteralExpCS literalExp);
        void visitCollectionTypeCS(CSTCollectionTypeCS type);
        void visitSimpleTypeCS(CSTSimpleTypeCS type);
        void visitTupleTypeCS(CSTTupleTypeCS type);
        void visitCollectionLiteralSinglePart(CSTCollectionLiteralSinglePartCS literalPart);
        void visitCollectionLiteralRange(CSTCollectionLiteralRangeCS literalPart);
        void visitCollectionLiteralExp(CSTCollectionLiteralExpCS literalExp);
        void visitVariableDeclaration(CSTVariableDeclarationCS variableDeclaration);
        void visitTupleLiteralExp(CSTTupleLiteralExpCS tupleLiteralExp);
        void visitClassifierAttributeCall(CSTClassifierAttributeCallExpCS classifierAttributeCallExp);
        void visitSimpleNameExp(CSTSimpleNameExpCS instanceAttributeCallExp);
        void visitArgumentCS(CSTArgumentCS expression);
        void visitInstanceOperationCallExp(CSTInstanceOperationCallExpCS expression);
        void visitClassOperationCallExp(CSTClassOperationCallExpCS expression);
        void visitNavigationExpressionBegin(CSTNavigationExpressionCS expression,CSTOclExpressionCS innerExpression);
        void visitNavigationExpression(CSTNavigationExpressionCS expression,CSTOclExpressionCS innerExpression);
        void visitNavigationExpressionEnd(CSTNavigationExpressionCS expression,List<object> innerExp, CSTOclExpressionCS callExpression);
        void visitNavigationOperator(CSTNavigationOperatorCS operatorNode);
        void visitUnaryExpression(CSTUnaryExpressionCS expression);
        void visitBinaryExpression(CSTBinaryExpressionCS expression);
        void visitIfExp(CSTIfExpCS expression);
        void visitLetExp(CSTLetExpCS expression);
        void visitBeginIteratorExp(CSTIteratorExpCS expression);
        void visitEndIteratorExp(CSTIteratorExpCS expression);
        void visitBeginIterateExp(CSTIterateExpCS expression);
        void visitEndIterateExp(CSTIterateExpCS expression);
    }
}
