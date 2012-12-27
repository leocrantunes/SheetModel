using System.Collections.Generic;
using OclLibrary.iface.environment;
using OclParser.cst.context;
using OclParser.cst.expression;
using OclParser.cst.literalExp;
using OclParser.cst.type;

namespace OclParser.typeChecker
{
    public class OCLDefinedFeaturesVisitor : OCLSemanticAnalyzerVisitor {

        private	bool isOperationContext = false;
	
        /**
	 * @param context
	 * @param tracker
	 */
        public OCLDefinedFeaturesVisitor(Environment context, ConstraintSourceTracker tracker) : base(context, tracker) {
        }

        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitArgumentCS(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTArgumentCS)
	 */
        public override void visitArgumentCS(CSTArgumentCS argumentNodeCS) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitAttrOrAssocContextDeclCSBegin(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTAttrOrAssocContextCS)
	 */
        public void visitAttrOrAssocContextDeclCSBegin(CSTAttrOrAssocContextCS attrAssocContextDeclaration) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitAttrOrAssocContextDeclCSEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTAttrOrAssocContextCS)
	 */
        public void visitAttrOrAssocContextDeclCSEnd(CSTAttrOrAssocContextCS attrAssocContextDeclaration) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBeginIterateExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTIterateExpCS)
	 */
        public void visitBeginIterateExp(CSTIterateExpCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBeginIteratorExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTIteratorExpCS)
	 */
        public void visitBeginIteratorExp(CSTIteratorExpCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBinaryExpression(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTBinaryExpressionCS)
	 */
        public void visitBinaryExpression(CSTBinaryExpressionCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBodyDecl(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTBodyDeclCS)
	 */
        public void visitBodyDecl(CSTBodyDeclCS bodyDeclExpression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBooleanLiteralExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTBooleanLiteralExpCS)
	 */
        public void visitBooleanLiteralExp(CSTBooleanLiteralExpCS literalExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitClassifierAttributeCall(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTClassifierAttributeCallExpCS)
	 */
        public void visitClassifierAttributeCall(CSTClassifierAttributeCallExpCS classifierAttributeCallExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitClassifierContextDeclCS(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTClassifierContextDeclCS)
	 */
        public void visitClassifierContextDeclCS(CSTClassifierContextDeclCS classifierContextDeclaration) {
            base.visitClassifierContextDeclCS(classifierContextDeclaration);
            isOperationContext = false;
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitClassOperationCallExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTClassOperationCallExpCS)
	 */
        public void visitClassOperationCallExp(CSTClassOperationCallExpCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitCollectionLiteralExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTCollectionLiteralExpCS)
	 */
        public void visitCollectionLiteralExp(CSTCollectionLiteralExpCS literalExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitCollectionLiteralRange(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTCollectionLiteralRangeCS)
	 */
        public void visitCollectionLiteralRange(CSTCollectionLiteralRangeCS literalPart) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitCollectionLiteralSinglePart(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTCollectionLiteralSinglePartCS)
	 */
        public void visitCollectionLiteralSinglePart(CSTCollectionLiteralSinglePartCS literalPart) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitCollectionTypeCS(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.type.CSTCollectionTypeCS)
	 */
        public void visitCollectionTypeCS(CSTCollectionTypeCS typeCstNode) {
            base.visitCollectionTypeCS(typeCstNode);
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitDefOperationExpressionBegin(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTDefOperationExpressionCS)
	 */
        public void visitDefOperationExpressionBegin(CSTDefOperationExpressionCS defOperationDeclaration) {
            base.visitDefOperationExpressionBegin(defOperationDeclaration);
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitDefOperationExpressionEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTDefOperationExpressionCS)
	 */
        public void visitDefOperationExpressionEnd(CSTDefOperationExpressionCS defOperationDeclaration) {
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            CSTOperationCS operation = defOperationDeclaration.getOperationNodeCS();
            checkForWrongOperationName(operation.getNameNodeCS());

            string operationName = operation.getNameNodeCS().toString();
            List<object> paramTypes = getParamTypesForOperation(operation, currentEnvironment, defOperationDeclaration);
            List<object> paramNames = getParamNamesForOperation(operation, currentEnvironment, defOperationDeclaration);
        
            CoreClassifier returnType;
            if (operation.getTypeNodeCS() != null)
                returnType = (CoreClassifier) operation.getTypeNodeCS().getAst();
            else
                returnType = null;

            checkForOperationRedefinition(contextClassifier.lookupSameSignatureOperation(
                operationName, paramTypes, returnType), operation,
                                          contextClassifier.getName());

            contextClassifier.addDefinedOperation(defOperationDeclaration.getToken().getFilename(),
                                                  operationName, paramNames, paramTypes, returnType);
            constraintSourceTracker.addOwnerToSource(defOperationDeclaration.getToken().getFilename(), contextClassifier);
        
            stackOfEnvironments.pop();

        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitDefVarExpressionBegin(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTDefVarExpressionCS)
	 */
        public void visitDefVarExpressionBegin(CSTDefVarExpressionCS defVarDeclaration) {
            base.visitDefVarExpressionBegin(defVarDeclaration);
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitDefVarExpressionEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTDefVarExpressionCS)
	 */
        public void visitDefVarExpressionEnd(CSTDefVarExpressionCS defVarDeclaration) {
            if (! isOperationContext) {
                Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
                string varName = defVarDeclaration.getNameAsString();

                checkForFeatureRedefinition(contextClassifier, varName,
                                            defVarDeclaration.getNameNodeCS());

                try {
                    contextClassifier.addDefinedElement(defVarDeclaration.getToken().getFilename(),
                                                        varName,
                                                        defVarDeclaration.getTypeNodeCS().getAst());
                    constraintSourceTracker.addOwnerToSource(defVarDeclaration.getToken().getFilename(), contextClassifier);
                } catch (NameClashException e) {
                    generateSemanticException(defVarDeclaration, e.getMessage());
                }
            }
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitEndIterateExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTIterateExpCS)
	 */
        public void visitEndIterateExp(CSTIterateExpCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitEndIteratorExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTIteratorExpCS)
	 */
        public void visitEndIteratorExp(CSTIteratorExpCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitExpressionInOcl(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS)
	 */
        public void visitExpressionInOcl(CSTExpressionInOclCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitExpressionInOclEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS)
	 */
        public void visitExpressionInOclEnd(CSTExpressionInOclCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitIfExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTIfExpCS)
	 */
        public void visitIfExp(CSTIfExpCS expressionNode) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitInstanceOperationCallExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTInstanceOperationCallExpCS)
	 */
        public void visitInstanceOperationCallExp(CSTInstanceOperationCallExpCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitIntegerLiteralExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTIntegerLiteralExpCS)
	 */
        public void visitIntegerLiteralExp(CSTIntegerLiteralExpCS literalExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitInvariantCSBegin(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTInvariantCS)
	 */
        public void visitInvariantCSBegin(CSTInvariantCS invariantDeclaration) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitInvariantCSEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTInvariantCS)
	 */
        public void visitInvariantCSEnd(CSTInvariantCS invariantDeclaration) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitLetExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTLetExpCS)
	 */
        public void visitLetExp(CSTLetExpCS expressionNode) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitNavigationExpression(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTNavigationExpressionCS, br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTOclExpressionCS)
	 */
        public void visitNavigationExpression(CSTNavigationExpressionCS expressionCstNode,
                                              CSTOclExpressionCS currentExpressionCstNode) {
                                              }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitNavigationExpressionBegin(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTNavigationExpressionCS, br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTOclExpressionCS)
	 */
        public void visitNavigationExpressionBegin(CSTNavigationExpressionCS expressionCstNode, 
                                                   CSTOclExpressionCS currentExpressionCstNode) {
                                                   }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitNavigationExpressionEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTNavigationExpressionCS, java.util.List, br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTOclExpressionCS)
	 */
        public void visitNavigationExpressionEnd(CSTNavigationExpressionCS expressionCstNode, List<object> innerExpCstNodes,
                                                 CSTOclExpressionCS currentExpressionCstNode) {
                                                 }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitNavigationOperator(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTNavigationOperatorCS)
	 */
        public void visitNavigationOperator(CSTNavigationOperatorCS operatorCstNode) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitOperationConstraint(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTOperationConstraintCS)
	 */
        public void visitOperationConstraint(CSTOperationConstraintCS operationConstraintExpression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitOperationContextDeclCSBegin(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTOperationContextCS)
	 */
        public void visitOperationContextDeclCSBegin(CSTOperationContextCS operationContextDeclaration) {
            isOperationContext = true;
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitOperationContextDeclCSEnd(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTOperationContextCS)
	 */
        public void visitOperationContextDeclCSEnd(CSTOperationContextCS operationContextDeclaration) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitPackageDeclarationCS(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTPackageDeclarationCS)
	 */
        public void visitPackageDeclarationCS(CSTPackageDeclarationCS packageDeclaration) {
            base.visitPackageDeclarationCS(packageDeclaration);
            isOperationContext = false;
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitPostDecl(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTPostDeclCS)
	 */
        public void visitPostDecl(CSTPostDeclCS preDeclExpression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitPreDecl(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTPreDeclCS)
	 */
        public void visitPreDecl(CSTPreDeclCS preDeclExpression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitRealLiteralExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTRealLiteralExpCS)
	 */
        public void visitRealLiteralExp(CSTRealLiteralExpCS literalExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitSimpleNameExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTSimpleNameExpCS)
	 */
        public void visitSimpleNameExp(CSTSimpleNameExpCS simpleNameExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitSimpleTypeCS(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.type.CSTSimpleTypeCS)
	 */
        public void visitSimpleTypeCS(CSTSimpleTypeCS typeCstNode) {
            base.visitSimpleTypeCS(typeCstNode);
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitStringLiteralExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTStringLiteralExpCS)
	 */
        public void visitStringLiteralExp(CSTStringLiteralExpCS literalExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitTupleLiteralExp(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTTupleLiteralExpCS)
	 */
        public void visitTupleLiteralExp(CSTTupleLiteralExpCS tupleLiteralExp) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitTupleTypeCS(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.type.CSTTupleTypeCS)
	 */
        public void visitTupleTypeCS(CSTTupleTypeCS typeCstNode) {
            base.visitTupleTypeCS(typeCstNode);
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitUnaryExpression(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTUnaryExpressionCS)
	 */
        public void visitUnaryExpression(CSTUnaryExpressionCS expression) {
        }
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitVariableDeclaration(br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.type.CSTVariableDeclarationCS)
	 */
        public void visitVariableDeclaration(CSTVariableDeclarationCS variableDeclarationCstNode) {
            base.visitVariableDeclaration(variableDeclarationCstNode);
        }
    }
}