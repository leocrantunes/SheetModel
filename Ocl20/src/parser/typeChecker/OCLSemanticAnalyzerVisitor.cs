using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.iface.util;
using Ocl20.library.impl.common;
using Ocl20.library.impl.constraints;
using Ocl20.library.impl.environment;
using Ocl20.library.impl.expressions;
using Ocl20.library.impl.types;
using Ocl20.library.impl.util;
using Ocl20.parser.cst;
using Ocl20.parser.cst.context;
using Ocl20.parser.cst.expression;
using Ocl20.parser.cst.literalExp;
using Ocl20.parser.cst.name;
using Ocl20.parser.cst.type;
using Ocl20.parser.exception;
using Ocl20.parser.semantics.types;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;
using CorePackage = Ocl20.library.iface.common.CorePackage;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.parser.typeChecker
{
    public class OCLSemanticAnalyzerVisitor : CSTVisitor {
        protected static string OP_ARROW = "->";

        //protected	static string ITERATOR_STEREOTYPE = "ITERATOR";
        protected Environment initialContext;
        protected Stack<object> stackOfEnvironments;
        protected Stack<object> stackOfExpressions;
        protected Stack<object> stackOfOperators;
        protected int lastClassifierIndex;
        protected CoreClassifier contextClassifier;
        protected CoreOperation contextOperation = null;
        protected AstOclModelElementFactory astFactory;
        protected AstOclConstraintFactory	oclConstraintFactory;
        protected bool atPreAllowed = false;
        protected bool shouldInsertSelfVariable = false;
        protected ConstraintSourceTracker constraintSourceTracker;

        protected string getCurrentMethodName() {
            return new StackTrace().GetFrame(1).GetMethod().Name;
        }

        private	static	bool compilingForTwoSnapshotsEvaluation = false; 
	
        public static	void	setCompilingForTwoSnapshotsEvaluation(bool status) {
            compilingForTwoSnapshotsEvaluation = status;
        }
	
        public OCLSemanticAnalyzerVisitor(
            Environment context,
            ConstraintSourceTracker tracker) {
            this.initialContext = context;

            stackOfEnvironments = new Stack<object>();
            stackOfExpressions = new Stack<object>();
            stackOfOperators = new Stack<object>();

            stackOfEnvironments.Push(context);
            astFactory = AstOclModelElementFactoryManager.getInstance(((EnvironmentImpl) context).getOclPackage());
            oclConstraintFactory = AstOclConstraintFactoryManager.getInstance(((EnvironmentImpl) context).getOclPackage());
            setSourceTracker(tracker);
            atPreAllowed = compilingForTwoSnapshotsEvaluation;
            }

        public void setSourceTracker(ConstraintSourceTracker tracker) {
            constraintSourceTracker = tracker;
        }

        public virtual void visitPackageDeclarationCS(
            CSTPackageDeclarationCS packageDeclaration)  {
            clearAllStacks();
            stackOfEnvironments.Push(this.initialContext);

            CorePackage corePackage = getPackageForName(packageDeclaration.getNameNodeCS());
            stackOfEnvironments.Push(corePackage.getEnvironmentWithParents());
            }

        public virtual void visitClassifierContextDeclCS(
            CSTClassifierContextDeclCS classifierContextDeclaration)
        {
    	
            resetStack(lastClassifierIndex);
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            contextClassifier = getClassifierForName(currentEnvironment,
                                                     classifierContextDeclaration.getNameNodeCS().ToString(),
                                                     classifierContextDeclaration.getNameNodeCS());
            contextOperation = null;
                
            stackOfEnvironments.Push(contextClassifier.getEnvironmentWithParents());
            lastClassifierIndex = stackOfEnvironments.Count;
            shouldInsertSelfVariable = true;
        
        }

        public virtual void visitInvariantCSBegin(CSTInvariantCS invariantDeclaration)
        {
            atPreAllowed = false;

        }

        public virtual void visitInvariantCSEnd(CSTInvariantCS invariantDeclaration)
        {

            string invariantName = invariantDeclaration.getNameAsString();

            checkForInvariantExpressionNotBoolean(invariantDeclaration);
            checkForInvariantNameRedefinition(invariantName, contextClassifier,
                                              invariantDeclaration.getNameNodeCS());

            OclInvariantConstraint constraint = (OclInvariantConstraint) oclConstraintFactory
                                                                             .createInvariantConstraint(invariantDeclaration.getToken().getFilename(),
                                                                                                        invariantName, contextClassifier,
                                                                                                        invariantDeclaration.getExpressionInOCL());

            constraint.setSourceNodeCS(invariantDeclaration);
        
//        contextClassifier.addInvariantConstraint(invariantName, constraint);
            constraintSourceTracker.addOwnerToSource(constraint, contextClassifier);
        }

        public virtual void visitAttrOrAssocContextDeclCSBegin(
            CSTAttrOrAssocContextCS attrAssocContextDeclaration)
        {
            resetStack(lastClassifierIndex);
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            atPreAllowed = false;
		
            contextClassifier = getClassifierForName(currentEnvironment,
                                                     attrAssocContextDeclaration.getClassifierName(), attrAssocContextDeclaration.getPathNameNodeCS());

            checkForInitOrDerivedConstraintAlreadyDefined(attrAssocContextDeclaration.getFeatureName(),
                                                          contextClassifier, attrAssocContextDeclaration.getPathNameNodeCS());
            checkForFeatureTypeMismatch(attrAssocContextDeclaration);

            stackOfEnvironments.Push(contextClassifier.getEnvironmentWithParents());
            lastClassifierIndex = stackOfEnvironments.Count;
            shouldInsertSelfVariable = !isClassScopeFeature(contextClassifier,
                                                            attrAssocContextDeclaration.getFeatureName(), attrAssocContextDeclaration);
        }

        public virtual void visitAttrOrAssocContextDeclCSEnd(
            CSTAttrOrAssocContextCS attrAssocContextDeclaration)
        {

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            string classifierName = attrAssocContextDeclaration.getClassifierName();
            string featureName = attrAssocContextDeclaration.getFeatureName();
            contextClassifier = getClassifierForName(currentEnvironment,
                                                     classifierName, attrAssocContextDeclaration.getPathNameNodeCS());

            CoreModelElement property = getProperty(contextClassifier, featureName,
                                                    attrAssocContextDeclaration);

            CoreClassifier expectedFeatureType = getTypeForProperty(property);

            checkForExpressionTypeMismatch(((ExpressionInOclImpl) attrAssocContextDeclaration.getExpressionInOCL()).getBodyExpression().getType(),
                                           expectedFeatureType, attrAssocContextDeclaration);

            if (attrAssocContextDeclaration.isInitConstraint()) {
                OclInitConstraint constraint = (OclInitConstraint) oclConstraintFactory
                                                                       .createModelElementInitConstraint(attrAssocContextDeclaration.getToken().getFilename(),
                                                                                                         contextClassifier, property,
                                                                                                         attrAssocContextDeclaration.getExpressionInOCL());
                constraint.setSourceNodeCS(attrAssocContextDeclaration);
//            contextClassifier.addInitConstraint(featureName, constraint);
                constraintSourceTracker.addOwnerToSource(constraint,
                                                         contextClassifier);
            } else {
                OclDeriveConstraint constraint = (OclDeriveConstraint) oclConstraintFactory
                                                                           .createModelElementDeriveConstraint(attrAssocContextDeclaration.getToken().getFilename(),
                                                                                                               contextClassifier, property,
                                                                                                               attrAssocContextDeclaration.getExpressionInOCL());
                constraint.setSourceNodeCS(attrAssocContextDeclaration);
//            contextClassifier.addDeriveConstraint(featureName, constraint);
                constraintSourceTracker.addOwnerToSource(constraint,
                                                         contextClassifier);
            }
        }

        public virtual void visitOperationContextDeclCSBegin(
            CSTOperationContextCS operationContextDeclaration)
        {
            resetStack(lastClassifierIndex);
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            CSTNameCS name = operationContextDeclaration.getOperationNodeCS()
                                                        .getNameNodeCS();
            CSTOperationCS operation = operationContextDeclaration.getOperationNodeCS();

            checkForWrongOperationContextName(name, operation);

            CSTPathNameCS pathName = (CSTPathNameCS) name;
            string classifierName = pathName.getAllButLastName();
            string operationName = pathName.getLastName();

            CoreClassifier classifier = getClassifierForName(currentEnvironment,
                                                             classifierName,
                                                             operationContextDeclaration.getOperationNodeCS().getNameNodeCS());
            List<object> paramTypes = getParamTypesForOperation(operation,
                                                                currentEnvironment, operationContextDeclaration);
            CoreClassifier returnType = (operation.getTypeNodeCS() != null)
                                            ? operation.getTypeNodeCS()
                                                       .getAst()
                                            : null;

            CoreOperation declaredOperation = classifier.lookupSameSignatureOperation(operationName,
                                                                                      paramTypes, returnType);
            checkForOperationExistence(declaredOperation, operation, classifierName);
            checkArgumentNames(declaredOperation, operationContextDeclaration);

            contextOperation = declaredOperation;
            contextClassifier = classifier;
            stackOfEnvironments.Push(contextClassifier.getEnvironmentWithParents());
            lastClassifierIndex = stackOfEnvironments.Count;

            Environment nestedEnvironment = currentEnvironment.nestedEnvironment();
            stackOfEnvironments.Push(nestedEnvironment);

            if (returnType != null) {
                insertResultVariableInEnvironment(nestedEnvironment, returnType);
            }

            for (int i = 0; i < operation.getParametersNodesCS()
                                         .Count; i++) {
                                             insertVariableInEnvironment(nestedEnvironment,
                                                                         astFactory.createVariableDeclaration((string) operationContextDeclaration.getParameterNames()[i],
                                                                                                              (CoreClassifier) paramTypes[i], null), false);
                                         }

            shouldInsertSelfVariable = contextOperation.isInstanceScope();
        }

        public virtual void visitOperationContextDeclCSEnd(
            CSTOperationContextCS operationContextDeclaration)
        {

            List<object> bodyDefinition = operationContextDeclaration.getBodyDefinitionNodesCS();
            List<object> preConditions = operationContextDeclaration.getPreConditionsNodesCS();
            List<object> postConditions = operationContextDeclaration.getPostConditionsNodesCS();

            List<string> parameterNames = operationContextDeclaration.getParameterNames();

            if (bodyDefinition.Count > 0) {
                checkForMoreThanOneBody(operationContextDeclaration);
                checkForIncompatibleBodyType(contextOperation,
                                             ((CSTBodyDeclCS) bodyDefinition[0]).getExpressionInOCL().getBodyExpression().getType(),
                                             operationContextDeclaration);

                OclBodyConstraint body = oclConstraintFactory.createBodyConstraint(operationContextDeclaration.getToken().getFilename(),
                                                                                   contextOperation,
                                                                                   ((CSTBodyDeclCS) bodyDefinition[0]).getExpressionInOCL(),
                                                                                   operationContextDeclaration.getParameterNames());
//            contextOperation.setBodyDefinition(body);
                body.setSourceNodeCS((CSTBodyDeclCS) bodyDefinition[0]);
                constraintSourceTracker.addOwnerToSource(body, contextOperation);
            }

            if ((preConditions.Count > 0) || (postConditions.Count > 0)) {
                OclPrePostConstraint constraint = oclConstraintFactory.createPrePostConstraint(
                    operationContextDeclaration.getToken().getFilename(), contextOperation);
                createPreConstraints(constraint, operationContextDeclaration.getToken().getFilename(),
                                     contextOperation, preConditions);
                createPostConstraints(constraint, operationContextDeclaration.getToken().getFilename(),
                                      contextOperation, postConditions);
//            contextOperation.addOperationSpecification(constraint);
                constraintSourceTracker.addOwnerToSource(constraint,
                                                         contextOperation);
            }

            ((EnvironmentImpl) stackOfEnvironments.Pop()).release();

        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitOperationConstraint(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.context.CSTOperationConstraintCS)
     */
        public virtual void visitOperationConstraint(
            CSTOperationConstraintCS operationConstraintExpression)
        {

            if ((operationConstraintExpression is CSTPreDeclCS) ||
                (operationConstraintExpression is CSTPostDeclCS)) {
                    checkForOperationConstraintNotBoolean(operationConstraintExpression);
                }

        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitPreDecl(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.context.CSTPreDeclCS)
     */
        public virtual void visitPreDecl(CSTPreDeclCS preDeclExpression)
        {

            atPreAllowed = false;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitPostDecl(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.context.CSTPostDeclCS)
     */
        public virtual void visitPostDecl(CSTPostDeclCS preDeclExpression)
        {
            atPreAllowed = true;

        }

        public virtual void visitBodyDecl(CSTBodyDeclCS bodyDeclExpression)
        {

            atPreAllowed = false;
            checkForNonQueryOperation(bodyDeclExpression, contextOperation);
            checkForBodyAlreadyDefined(bodyDeclExpression, contextOperation);
        
        }

        public virtual void visitDefVarExpressionBegin(
            CSTDefVarExpressionCS defVarDeclaration)  {
    	
            atPreAllowed = contextOperation == null ? false : true;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTVisitor#visitDefVarExpression(br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTDefVarExpressionCS)
     */
        public virtual void visitDefVarExpressionEnd(
            CSTDefVarExpressionCS defVarDeclaration)  {
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            string varName = defVarDeclaration.getNameAsString();

//        checkForFeatureRedefinition(contextClassifier, varName,
//            defVarDeclaration.getNameNodeCS());

//        try {
//            contextClassifier.addDefinedElement(defVarDeclaration.getToken().getFilename(),
//                varName,
//                getClassifierForType(currentEnvironment,
//                    defVarDeclaration.getTypeNodeCS(), defVarDeclaration));
//            constraintSourceTracker.addOwnerToSource(defVarDeclaration.getToken().getFilename(), contextClassifier);
            
            if (defVarDeclaration.getExpressionNodeCS().getAst() != null) {
        
                checkForExpressionTypeMismatch(((ExpressionInOclImpl) defVarDeclaration.getExpressionNodeCS().getAst()).getBodyExpression().getType(),
                                               defVarDeclaration.getTypeNodeCS().getAst(), defVarDeclaration);

                if (contextOperation == null) {
                    CoreModelElement property = getProperty(contextClassifier,
                                                            varName, defVarDeclaration);
                
                    OclDeriveConstraint constraint = (OclDeriveConstraint) oclConstraintFactory
                                                                               .createModelElementDeriveConstraint(defVarDeclaration.getToken().getFilename(),
                                                                                                                   contextClassifier, property,
                                                                                                                   defVarDeclaration.getExpressionNodeCS().getAst());
                    constraint.setSourceNodeCS(defVarDeclaration);
                    constraintSourceTracker.addOwnerToSource(constraint,
                                                             contextClassifier);
                } else {
                    VariableDeclaration var = contextOperation.lookupLocalVariable(varName);
                    if (var == null && currentEnvironment.lookupVariable(varName) == null) {
                        var = astFactory.createVariableDeclaration(varName,
                                                                   defVarDeclaration.getTypeNodeCS().getAst(), 
                                                                   defVarDeclaration.getExpressionNodeCS().getOclExpressionNodeCS().getAst());
                        insertVariableInEnvironment(currentEnvironment,
                                                    var, false);
                        contextOperation.addLocalVariable(defVarDeclaration.getToken().getFilename(), var);
                    } else {
                        generateSemanticException(defVarDeclaration, "there is already a variable with this name: {0}", new object[] {defVarDeclaration.getNameAsString()});
                    }
                }
            }     
//        } catch (NameClashException e) {
//            generateSemanticException(defVarDeclaration, e.getMessage());
//        }
            }

        public virtual void visitDefOperationExpressionBegin(
            CSTDefOperationExpressionCS defOperationDeclaration)
        {
            atPreAllowed = false;
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            Environment nestedEnvironment = currentEnvironment.nestedEnvironment();
            stackOfEnvironments.Push(nestedEnvironment);

            CSTOperationCS operation = defOperationDeclaration.getOperationNodeCS();

            List<object> parameters = operation.getParametersNodesCS();

            for (int i = 0; i < parameters.Count; i++) {
                CSTVariableDeclarationCS parameter = (CSTVariableDeclarationCS) parameters[i];
                insertVariableInEnvironment(nestedEnvironment,
                                            astFactory.createVariableDeclaration(parameter.getNameNodeCS().ToString(),
                                                                                 getClassifierForName(currentEnvironment,
                                                                                                      parameter.getTypeNodeCS().getName(),
                                                                                                      defOperationDeclaration), null), false);
            }
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTVisitor#visitDefOperationExpression(br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTDefOperationExpressionCS)
     */
        public virtual void visitDefOperationExpressionEnd(
            CSTDefOperationExpressionCS defOperationDeclaration)
        {

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            CSTOperationCS operation = defOperationDeclaration.getOperationNodeCS();

            checkForWrongOperationName(operation.getNameNodeCS());

            string operationName = operation.getNameNodeCS()
                                            .ToString();
            List<object> paramTypes = getParamTypesForOperation(operation,
                                                                currentEnvironment, defOperationDeclaration);
            List<object> paramNames = getParamNamesForOperation(operation,
                                                                currentEnvironment, defOperationDeclaration);
        
            CoreClassifier returnType;
            if (operation.getTypeNodeCS() != null)
                returnType = (CoreClassifier) operation.getTypeNodeCS()
                                                       .getAst();
            else
                returnType = null;

//        checkForOperationRedefinition(contextClassifier.lookupSameSignatureOperation(
//                operationName, paramTypes, returnType), operation,
//            contextClassifier.getName());
//
//        contextClassifier.addDefinedOperation(defOperationDeclaration.getToken().getFilename(),
//            operationName, paramNames, paramTypes, returnType);
//        constraintSourceTracker.addOwnerToSource(defOperationDeclaration.getToken().getFilename(), contextClassifier);
        
            CoreOperation definedOperation = contextClassifier.lookupSameSignatureOperation(operationName,
                                                                                            paramTypes, returnType);
        
            if (definedOperation != null && defOperationDeclaration.getExpressionNodeCS().getAst() != null) {
                checkForIncompatibleBodyType(definedOperation,
                                             defOperationDeclaration.getExpressionNodeCS().getAst().getBodyExpression().getType(),
                                             defOperationDeclaration);

                OclBodyConstraint body = oclConstraintFactory
                    .createBodyConstraint(defOperationDeclaration.getToken().getFilename(),
                                          definedOperation,
                                          defOperationDeclaration.getExpressionNodeCS().getAst(),
                                          defOperationDeclaration.getParameterNames());
                body.setSourceNodeCS(defOperationDeclaration);
//                definedOperation.setBodyDefinition(body);
                constraintSourceTracker.addOwnerToSource(body, contextClassifier);
            }

            ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
        }

        //	**************************************************************
        // 	ExpressionInOCL
        //	**************************************************************

        public virtual void visitExpressionInOcl(CSTExpressionInOclCS expression)
        {
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            Environment nestedEnvironment = currentEnvironment.nestedEnvironment();

            if (shouldInsertSelfVariable) {
                insertSelfVariableInEnvironment(nestedEnvironment, contextClassifier);
            }

            stackOfEnvironments.Push(nestedEnvironment);
            stackOfExpressions.Clear();
            stackOfOperators.Clear();
        }

        public virtual void visitExpressionInOclEnd(CSTExpressionInOclCS expression)
        {
            expression.setAst(createExpressionInOCL(contextClassifier,
                                                    expression.getOclExpressionNodeCS().getAst()));
            ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
        }

        //	**************************************************************
        // 	Types
        //	**************************************************************
        public virtual void visitSimpleTypeCS(CSTSimpleTypeCS typeCstNode)
        {
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            typeCstNode.setAst(getClassifierForName(currentEnvironment,
                                                    typeCstNode.getName(), typeCstNode));
        }

        public virtual void visitCollectionTypeCS(CSTCollectionTypeCS typeCstNode)
        {
            typeCstNode.setAst(astFactory.createSpecificCollectionType(
                typeCstNode.getTypeIdNodeCS().getCollectionKind(),
                typeCstNode.getElementTypeNodeCS().getAst()));
        }

        public virtual void visitTupleTypeCS(CSTTupleTypeCS typeCstNode)
        {
            typeCstNode.setAst(astFactory.createTupleType());
            addTuplePartsTypes((TupleType) typeCstNode.getAst(),
                               typeCstNode.getVariableDeclarationNodesCS());
        }

        protected void addTuplePartsTypes(
            TupleType tupleType,
            List<object> tupleParts)  {
            foreach (CSTVariableDeclarationCS varDecl in tupleParts) {
                if (varDecl != null) {
                    checkIfTuplePartNameAlreadyDefined(tupleType,
                                                       varDecl.getAst().getVarName(), varDecl);
                    ((TupleTypeImpl) tupleType).addElement(varDecl.getAst().getVarName(),
                                                           varDecl.getAst().getType());
                }
            }
        }

        // ************************************************************** 
        // Variable Declaration
        // **************************************************************
        public virtual void visitVariableDeclaration(
            CSTVariableDeclarationCS variableDeclarationCstNode)
        {
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            CoreClassifier oclVoidType = getClassifierForName(currentEnvironment,
                                                              "OclVoid", variableDeclarationCstNode.getNameNodeCS());

            CSTOclExpressionCS initExpression = variableDeclarationCstNode.getExpressionNodeCS();
            CSTTypeCS varType = variableDeclarationCstNode.getTypeNodeCS();

            if ((initExpression != null) && (initExpression.getAst() != null) && (varType != null)) {
                checkForTypeConformance(initExpression.getAst(), varType.getAst(),
                                        variableDeclarationCstNode);
            }

            variableDeclarationCstNode.setAst(astFactory.createVariableDeclaration(
                variableDeclarationCstNode.getNameNodeCS().ToString(),
                (varType != null) ? varType.getAst()
                    : ((initExpression != null && initExpression.getAst() != null)
                           ? initExpression.getAst()
                                           .getType()
                           : oclVoidType),
                (initExpression != null) ? initExpression.getAst()
                    : null));
        }

        // ************************************************************** 
        // Literal Expressions
        // **************************************************************
        public virtual void visitBooleanLiteralExp(CSTBooleanLiteralExpCS literalExp)
        {

            literalExp.setAst(astFactory.createBooleanLiteralExp(
                literalExp.getBooleanSymbol(),
                getPrimitiveType(literalExp, "Boolean")));
        }

        public virtual void visitIntegerLiteralExp(CSTIntegerLiteralExpCS literalExp)
        {

            literalExp.setAst(astFactory.createIntegerLiteralExp(
                literalExp.getIntegerSymbol(),
                getPrimitiveType(literalExp, "Integer")));
        }

        public virtual void visitRealLiteralExp(CSTRealLiteralExpCS literalExp)
        {

            literalExp.setAst(astFactory.createRealLiteralExp(
                literalExp.getRealSymbol(), getPrimitiveType(literalExp, "Real")));
        }

        public virtual void visitStringLiteralExp(CSTStringLiteralExpCS literalExp)
        {

            literalExp.setAst(astFactory.createStringLiteralExp(
                literalExp.getStringSymbol(),
                getPrimitiveType(literalExp, "String")));
        }

        public void visitNullLiteralExp(CSTNullLiteralExpCS literalExp)
        {
            literalExp.setAst(astFactory.createNullLiteralExp(
                getPrimitiveType(literalExp, "OclVoid")));
        }

        public void visitInvalidLiteralExp(CSTInvalidLiteralExpCS literalExp)
        {
            literalExp.setAst(astFactory.createInvalidLiteralExp(
                getPrimitiveType(literalExp, "OclInvalid")));
        }
    
    
        protected CoreClassifier getPrimitiveType(
            CSTLiteralExpCS literalExp,
            string typeName)  {

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            CoreClassifier result = getClassifierForName(currentEnvironment, typeName, literalExp);
            return	result;
            }

        public virtual void visitCollectionLiteralRange(
            CSTCollectionLiteralRangeCS literalPart)  {

            checkForRangeElementNotInteger(literalPart);
            literalPart.setAst(astFactory.createCollectionRange(
                literalPart.getFirstNodeCS().getAst(),
                literalPart.getLastNodeCS().getAst()));
            }

        public virtual void visitCollectionLiteralSinglePart(
            CSTCollectionLiteralSinglePartCS literalPart)
        {
    	
            literalPart.setAst(astFactory.createCollectionItem(
                literalPart.getExpressionNodeCS().getAst()));
        }

        public virtual void visitCollectionLiteralExp(CSTCollectionLiteralExpCS literalExp)
        {

            List<object> collectionLiteralParts = new List<object>();
            CoreClassifier type = null;

            if (literalExp.getLiteralPartsNodesCS()
                          .Count > 0) {
                              foreach (CSTCollectionLiteralPartCS part in literalExp.getLiteralPartsNodesCS()) {
                                  if (part != null) {
                                      collectionLiteralParts.Add(part.getAst());
                                      type = (type == null) ? part.getAst().getType()
                                                 : type.getMostSpecificCommonSuperType(part.getAst().getType());
                                  }
                              }
                          } else { // empty set

                              Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
                              type = getClassifierForName(currentEnvironment, "OclVoid",
                                                          literalExp);
                          }

            literalExp.setAst(astFactory.createCollectionLiteralExp(
                collectionLiteralParts,
                astFactory.createSpecificCollectionType(
                    literalExp.getTypeIdNodeCS().getCollectionKind(), type)));
        
        }

        public virtual void visitTupleLiteralExp(CSTTupleLiteralExpCS tupleLiteralExp)
        {

            List<object> tupleParts = new List<object>();

            foreach (CSTVariableDeclarationCS varDecl in tupleLiteralExp.getTuplePartsNodesCS()) {
                                                    if ((varDecl != null) && (varDecl.getAst() != null)) {
                                                        tupleParts.Add(varDecl.getAst());
                                                    }
                                                }

            if (tupleLiteralExp.getTuplePartsNodesCS()
                               .Count == tupleParts.Count) {
                                   tupleLiteralExp.setAst(astFactory.createTupleLiteralExp(
                                       tupleParts, astFactory.createTupleType()));
                                   addTuplePartsTypes((TupleType) tupleLiteralExp.getAst().getType(), tupleLiteralExp.getTuplePartsNodesCS());
                               }

        }

        //	**************************************************************
        //     Expressions
        //	**************************************************************

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitUnaryExpression(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTUnaryExpressionCS)
     */
        public virtual void visitUnaryExpression(CSTUnaryExpressionCS expression)
        {

            expression.setAst(matchOperatorExpression(null,
                                                      expression.getOperatorNodeCS(), expression.getExpressionNodeCS().getAst(),
                                                      expression));
        
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitMultiplicativeExpression(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTMultiplicativeExpressionCS)
     */
        public virtual void visitBinaryExpression(CSTBinaryExpressionCS expression)
        {

            if ((expression.getLeftExpressionNodeCS()
                           .getAst() != null) &&
                (expression.getOperatorNodeCS() != null) &&
                (expression.getRightExpressionNodeCS()
                           .getAst() != null)) {
                               expression.setAst(matchOperatorExpression(
                                   expression.getLeftExpressionNodeCS().getAst(),
                                   expression.getOperatorNodeCS(),
                                   expression.getRightExpressionNodeCS().getAst(), expression));
                           }
        }

        public OperationCallExp matchOperatorExpression(
            OclExpression leftExpression,
            CSTOperatorCS operatorNode,
            OclExpression rightExpression,
            CSTNode node)  {

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            OclExpression source;

            List<object> paramTypes = new List<object>();
            List<object> arguments = new List<object>();

            if (leftExpression != null) {
                source = leftExpression;
                arguments.Add(rightExpression);
                paramTypes.Add(rightExpression.getType());
            } else {
                source = rightExpression;
            }

            CoreOperation operat = source.getType()
                                         .lookupOperation(operatorNode.getOperator(),
                                                          paramTypes);
            checkForOperatorExistence(operat, operatorNode, paramTypes,
                                      source.getType().getName());

            CoreClassifier returnType = getOperationReturnType(operat,
                                                               paramTypes, currentEnvironment, node);

            OperationCallExp result = astFactory.createOperationCallExp(source, operat,
                                                                        arguments, returnType, false);
        
            return	result;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitIfExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTIfExpCS)
     */
        public virtual void visitIfExp(CSTIfExpCS expressionNode)
        {

            expressionNode.getConditionExpNodeCS().accept(this);
			
            stackOfExpressions.Push(null);
            stackOfOperators.Push(null);
			
            expressionNode.getThenExpNodeCS().accept(this);
			
            stackOfExpressions.Push(null);
            stackOfOperators.Push(null);
			
            expressionNode.getElseExpNodeCS().accept(this);

            stackOfExpressions.Pop(); // pops null - then expression 
            stackOfExpressions.Pop(); // pops null - else expression

            stackOfOperators.Pop();
            stackOfOperators.Pop();

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();

            CoreClassifier booleanType = getClassifierForName(currentEnvironment,
                                                              "Boolean", expressionNode);
			
            if (expressionNode.getConditionExpNodeCS()
                              .getAst()
                              .getType() == booleanType) {
                                  expressionNode.setAst(astFactory.createIfExp(
                                      expressionNode.getConditionExpNodeCS().getAst(),
                                      expressionNode.getThenExpNodeCS().getAst(),
                                      expressionNode.getElseExpNodeCS().getAst()));
                              } else {
                                  generateSemanticException(expressionNode,
                                                            "condition expression must be of Boolean type");
                              }
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitLetExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTLetExpCS)
     */
        public virtual void visitLetExp(CSTLetExpCS expressionNode)
        {
            CSTVariableDeclarationCS decl = null;
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            Environment nestedEnv = currentEnvironment.nestedEnvironment();

            try {
                stackOfEnvironments.Push(nestedEnv);

                foreach (CSTVariableDeclarationCS varDecl in expressionNode.getVarDeclarationsNodesCS())
                {

                    decl = varDecl;
                                                       if (varDecl != null) {
                                                           varDecl.accept(this);
                                                           if (varDecl.getAst() != null) {
                                                               if (nestedEnv.lookup(varDecl.getAst().getVarName()) == null) {
                                                                   nestedEnv.addElement(varDecl.getAst().getVarName(),
                                                                                        varDecl.getAst(), false);
                                                               }
                                                               else {    	
                                                                   generateSemanticException(varDecl,
                                                                                             "duplicate name found in variable definition {0}",
                                                                                             new object[] { varDecl.getAst()
                                                                                                                   .getVarName() });
                                                               }
                                                           }
                                                       }
                                                   }

                expressionNode.getExpressionNodeCS().accept(this);

                OclExpression inExpression = expressionNode.getExpressionNodeCS()
                                                           .getAst();

                if (inExpression == null) {
                    generateSemanticException(decl,
                                              "invalid call to a non query operation on the <in> part of a let expression",
                                              null);
                }
			
                List<object> varDeclarations = expressionNode.getVarDeclarationsNodesCS();

                for (int i = varDeclarations.Count - 1; i >= 0; i--) {
                    decl = (CSTVariableDeclarationCS) varDeclarations[i];

                    if ((decl != null) && (decl.getAst() != null)) {
                        expressionNode.setAst(astFactory.createLetExp(
                            decl.getAst(), inExpression));
                        inExpression = expressionNode.getAst();
                    }
                }
            } catch (NullNameException e) {
                Console.WriteLine(e.StackTrace);
            } catch (NameClashException e) {
                generateSemanticException(decl,
                                          "duplicate name found in variable definition {0}",
                                          new object[] { decl.getAst()
                                                                .getVarName() });
            } finally {
                ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
            }
        }


        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitNavigationExpression(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNavigationExpressionCS)
     */
        public virtual void visitNavigationExpressionBegin(
            CSTNavigationExpressionCS expressionCstNode,
            CSTOclExpressionCS currentExpressionCstNode)
        {

            visitNavigationExpression(expressionCstNode, currentExpressionCstNode);
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitNavigationExpression(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNavigationExpressionCS)
     */
        public virtual void visitNavigationExpression(
            CSTNavigationExpressionCS expressionCstNode,
            CSTOclExpressionCS currentExpressionCstNode)
        {

            expressionCstNode.setAst(currentExpressionCstNode.getAst());

            if (currentExpressionCstNode.getAst() != null) {
                stackOfExpressions.Push(currentExpressionCstNode.getAst());
            }
        }

        public virtual void visitNavigationExpressionEnd(
            CSTNavigationExpressionCS expressionCstNode,
            List<object> innerExpCstNodes,
            CSTOclExpressionCS currentExpressionCstNode) {

            for (int i = 0; i < innerExpCstNodes.Count; i++) {
                CSTOclExpressionCS innerExp = (CSTOclExpressionCS) innerExpCstNodes[i];

                if (innerExp != null && innerExp.getAst() != null) {
                    stackOfExpressions.Pop();
                }

                stackOfOperators.Pop();
            }

            if (currentExpressionCstNode.getAst() != null) {
                stackOfExpressions.Pop(); // pop call expression
            }
            }

        public virtual void visitNavigationOperator(
            CSTNavigationOperatorCS operatorCstNode) {

            stackOfOperators.Push(operatorCstNode.getOperator());
            }

        /* (non-Javadoc)
    * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitSimpleNameExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTSimpleNameExpCS)
    */
        public virtual void visitSimpleNameExp(CSTSimpleNameExpCS simpleNameExp)
        {

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            OclExpression currentExpression = getCurrentExpression();

            if (currentExpression == null) {
                bool matchSimpleName = matchVariableExpression(simpleNameExp,
                                                               currentEnvironment) ||
                                       matchImplicitAttributeCall(simpleNameExp, currentEnvironment) ||
                                       matchImplicitAssociationEndCall(simpleNameExp,
                                                                       currentEnvironment) ||
                                       matchImplicitAssociationClassCall(simpleNameExp,
                                                                         currentEnvironment) ||
                                       matchClassifierLiteral(simpleNameExp, currentEnvironment);

                if (!matchSimpleName) {
                    generateSemanticException(simpleNameExp,
                                              "{0} is not defined in this context",
                                              new object[] { simpleNameExp.getNameAsString() });
                }
            } else {
                string operat = getCurrentOperator();
                checkForInvalidOperator(simpleNameExp, operat);

                CoreClassifier classifier = currentExpression.getType();

                if (classifier is CollectionTypeImpl) {
                    if (!matchIteratorExp(simpleNameExp, currentExpression)) {
                        generateSemanticException(simpleNameExp,
                                                  "{0} is not a feature of {1}",
                                                  new object[] {
                                                      simpleNameExp.getNameAsString(),
                                                      ((CollectionType) classifier).getElementType()
                                                                                   .getName()
                                                  });
                    }
                } else {
                    if (!matchAttributeCall(simpleNameExp, currentExpression) &&
                        !matchAssociationEndCall(simpleNameExp,
                                                 currentExpression) &&
                        !matchAssociationClassCall(simpleNameExp,
                                                   currentExpression)) {
                                                       generateSemanticException(simpleNameExp, "{0} is not a feature of {1}", new object[] { simpleNameExp.getNameAsString(), classifier.getName() }); 
                                                   }
                }
            }
        }


        public virtual void visitArgumentCS(CSTArgumentCS argumentNodeCS)
        {
    	
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            Environment envToBePushed = currentEnvironment;
            OclExpression currentExpression = getCurrentExpression();

            try {
                if ((currentExpression != null) &&
                    currentExpression.getType() is CollectionTypeImpl) {
                        envToBePushed = currentEnvironment.nestedEnvironment();

                        CoreClassifier elementType = ((CollectionType) currentExpression.getType()).getElementType();
                        insertVariableInEnvironment(envToBePushed, astFactory.createVariableDeclaration("iterator",
                                                                                                        elementType, null), true);
                    }

                stackOfEnvironments.Push(envToBePushed);
                stackOfExpressions.Push(null);
                stackOfOperators.Push(null);

                argumentNodeCS.getExpressionNodeCS()
                              .accept(this);

                argumentNodeCS.setAst(argumentNodeCS.getExpressionNodeCS().getAst());
            } finally {
                stackOfOperators.Pop();
                stackOfExpressions.Pop();
                ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
            }
        }

        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitClassifierAttributeCall(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTClassifierAttributeCallExpCS)
	 */
        public virtual void visitClassifierAttributeCall(
            CSTClassifierAttributeCallExpCS classifierAttributeCallExp)
        {

            Environment currentEnvironment = this.initialContext;

            CoreModelElement element = (CoreModelElement) currentEnvironment.lookupPathName(classifierAttributeCallExp.getClassifierName());

            bool match = matchEnumerationLiteral(element, classifierAttributeCallExp) ||
                         matchClassifierAttributeCallExp(element, classifierAttributeCallExp) ||
                         matchPathNameClassifierLiteral(classifierAttributeCallExp, currentEnvironment);
        }

        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitInstanceOperationCallExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTInstanceOperationCallExpCS)
	 */
        public virtual void visitClassOperationCallExp(
            CSTClassOperationCallExpCS expression)  {

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            OclExpression currentExpression = getCurrentExpression();

            if (currentExpression != null) {
                generateSemanticException(expression,
                                          "class operation call not expected here");
            }

            string classifierName = expression.getClassifierName();
            string operationName = expression.getOperationName();

            CoreClassifier classifier = getClassifierForName(currentEnvironment,
                                                             classifierName, expression.getPathNameNodeCS());

            List<object> argumentTypes = expression.getArgumentsTypes();
            CoreOperation oper = classifier.lookupOperation(operationName,
                                                            argumentTypes);

            OclExpression source = null;


            if (oper != null) {
                CoreClassifier returnType = getOperationReturnType(oper,
                                                                   argumentTypes, currentEnvironment,
                                                                   expression.getPathNameNodeCS());

                checkAtPreNotAllowed(expression.getIsMarkedPre(), expression);
			
                if (oper.isInstanceScope()) {
                    checkIfNotOpCallToSuperclass(expression, oper);
                }
			
                // return type for class operations such as "allInstances"
                if ((returnType != null) &&
                    returnType.getName()
                              .Equals("Set<T>") &&
                    (argumentTypes.Count == 0)) {
                        returnType = astFactory.createSetType(classifier);
                    }

                expression.setAst(astFactory.createOperationCallExp(
                    astFactory.createOclTypeLiteralExp(
                        (astFactory.createOclModelElementType(classifier))), oper,
                    expression.getArgumentsAst(), returnType,
                    expression.getIsMarkedPre()));
            } else {
                generateSemanticException(expression,
                                          "operation {0} is not defined in classifier {1}",
                                          new object[] {getOperationFullName(operationName, argumentTypes) , classifierName });
            }
            }

	
        public void checkIfNotOpCallToSuperclass(
            CSTClassOperationCallExpCS expression, CoreOperation operation)  {
            if (! contextClassifier.isClassifierDescendantOf(operation.getFeatureOwner())) {
                generateSemanticException(expression,
                                          "illegal call to instance operation {0}::{1}",
                                          new object[] {expression.getClassifierName(), getOperationFullName(expression.getOperationName(), expression.getArgumentsTypes()) });
            }
            }
	
	
        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitInstanceOperationCallExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTInstanceOperationCallExpCS)
     */
        public virtual void visitInstanceOperationCallExp(CSTInstanceOperationCallExpCS expression)  {
            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            OclExpression currentExpression = getCurrentExpression();

            if (isCollectionOperatorPending(currentExpression)) {
                matchCollectionOperation(expression, currentExpression);
            } else {
                if ((currentExpression != null) &&
                    currentExpression.getType() is CollectionTypeImpl) {
                        matchIteratorCollectOperationCallExp(expression,
                                                             currentEnvironment, currentExpression);
                    } else {
                        matchOperationCallExp(expression, currentEnvironment,
                                              currentExpression);
                    }
            }
        }


        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBeginIteratorExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.expression.CSTIteratorExpCS)
     */
        public virtual void visitBeginIteratorExp(CSTIteratorExpCS expression)
        {
            OclExpression currentExpression = getCurrentExpression();

            // check if left ocl expression is a collection
            if (isImplicitSetAccess(currentExpression)) {
                currentExpression = generateWithAsSetExpression(currentExpression);
            }

            checkForInvalidIteratorSource(currentExpression.getType(), expression);

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            Environment nestedEnv = currentEnvironment.nestedEnvironment();
            List<object> expIterators;
            CoreClassifier elementType = ((CollectionType) currentExpression.getType()).getElementType();

            List<object> iterators = expression.getVariablesNodesCS();

            if (iterators.Count == 0) {
                expIterators = new List<object>();
                expIterators.Add(astFactory.createVariableDeclaration("iterator", elementType, null));
            } else {
                checkForInvalidNumberOfIterators(expression);
                expIterators = getIteratorsAst(iterators, elementType);
            }

            foreach (VariableDeclaration variable in expIterators) {
                insertVariableInEnvironment(nestedEnv, variable, true);
            }

            stackOfEnvironments.Push(nestedEnv);

            IteratorExp iteratorExp = astFactory.createIteratorExp(expression.getIteratorOperationNodeCS().getOperationName(), null,
                                                                   currentExpression, null, expIterators);
            expression.setAst(iteratorExp);
            stackOfExpressions.Push(null);
        }

        protected List<object> getIteratorsAst(
            List<object> iterators,
            CoreClassifier elementType)  {

            List<object> expIterators = new List<object>();

            for (int i = 0; i < iterators.Count; i++) {
                CSTVariableDeclarationCS iterator = (CSTVariableDeclarationCS) iterators[i];

                if (iterator.getExpressionNodeCS() != null) {
                    generateSemanticException(iterator,
                                              "iterator {0} cannot have an initialization expression",
                                              new object[] { iterator.getNameNodeCS()
                                                                     .getName() });
                }

                if (iterator.getTypeNodeCS() == null) {
                    iterator.getAst()
                            .setType(elementType);
                } else if (!elementType.conformsTo(iterator.getAst().getType())) {
                    generateSemanticException(iterator,
                                              "element type {0} does not conform to iterator type {1}",
                                              new object[] {
                                                  elementType.getName(),
                                                  iterator.getAst()
                                                          .getType()
                                                          .getName()
                                              });
                }

                expIterators.Add(iterator.getAst());
            }
            return expIterators;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitEndIteratorExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.expression.CSTIteratorExpCS)
     */
        public virtual void visitEndIteratorExp(CSTIteratorExpCS expression)
        {

            stackOfExpressions.Pop();

            OclExpression currentExpression = getCurrentExpression();

            string iteratorName = expression.getIteratorOperationNodeCS()
                                            .getOperationName();

            // CollectionType collection = (CollectionType) currentExpression.getType();
            CollectionType collection = (CollectionType) ((IteratorExp) expression
                                                                            .getAst()).getSource()
                                                                                      .getType();
            List<object> paramTypes = new List<object>();
            if (expression.getBodyExpressionNodeCS() != null && expression.getBodyExpressionNodeCS().getAst() != null)
                paramTypes.Add(expression.getBodyExpressionNodeCS().getAst().getType());

            CoreOperation operation = collection.lookupOperation(iteratorName,
                                                                 paramTypes);

            if (operation != null) {
                IteratorExp iteratorExp = (IteratorExp) expression.getAst();
                iteratorExp.setType(((CollectionOperationImpl) operation).getReturnType(
                    paramTypes));
                iteratorExp.setBody(expression.getBodyExpressionNodeCS().getAst());
            } else {
                generateSemanticException(expression,
                                          "wrong expression type in iterator argument");
            }

            ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitBeginIterateExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.expression.CSTIterateExpCS)
     */
        public virtual void visitBeginIterateExp(CSTIterateExpCS expression)
        {
            // check if left ocl expression is a collection
            OclExpression currentExpression = getCurrentExpression();

            if (!(currentExpression.getType() is CollectionTypeImpl)) {
                generateSemanticException(expression,
                                          "left operand of iterate should be a collection");
            }

            Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
            Environment nestedEnv = currentEnvironment.nestedEnvironment();
            CoreClassifier elementType = ((CollectionType) currentExpression.getType()).getElementType();

            List<object> expIterators;
            List<object> iterators = expression.getIteratorsNodesCS();

            if ((iterators == null) || (iterators.Count == 0)) {
                expIterators = new List<object>();
                expIterators.Add(astFactory.createVariableDeclaration("iterator",
                                                                      elementType, null));
            } else {
                expIterators = getIteratorsAst(iterators, elementType);
            }

            foreach (VariableDeclaration varDecl in expIterators) {
                insertVariableInEnvironment(nestedEnv, varDecl, true);
            }

            insertVariableInEnvironment(nestedEnv, expression.getResultNodeCS().getAst(),
                                        true);

            stackOfEnvironments.Push(nestedEnv);

            expression.setAst(astFactory.createIterateExp(expression.getResultNodeCS().getAst().getType(),
                                                          currentExpression, null, expIterators,
                                                          expression.getResultNodeCS().getAst()));
            stackOfExpressions.Push(null);
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor#visitEndIterateExp(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.expression.CSTIterateExpCS)
     */
        public virtual void visitEndIterateExp(CSTIterateExpCS expression)
        {

            stackOfExpressions.Pop();

            IterateExp iterateExp = (IterateExp) expression.getAst();

            CoreClassifier bodyType = expression.getBodyExpressionNodeCS()
                                                .getAst()
                                                .getType();

            if (bodyType.conformsTo(expression.getResultNodeCS().getAst().getType())) {
                iterateExp.setBody(expression.getBodyExpressionNodeCS().getAst());
            } else {
                generateSemanticException(expression,
                                          "body expression type {0} does not conform to result type {1}",
                                          new object[] {
                                              bodyType.getName(),
                                              expression.getResultNodeCS()
                                                        .getAst()
                                                        .getType()
                                                        .getName()
                                          });
            }

            ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
        }

        protected bool isCollectionOperatorPending(
            OclExpression currentExpression) {
            return (currentExpression != null) &&
                   getCurrentOperator()
                       .Equals(OP_ARROW);
            }

        protected void matchCollectionOperation(
            CSTInstanceOperationCallExpCS expression,
            OclExpression currentExpression)  {
        	
            if (isImplicitSetAccess(currentExpression)) {
                currentExpression = generateWithAsSetExpression(currentExpression);
            } else if (!(currentExpression.getType() is CollectionTypeImpl)) {
                generateSemanticException(expression,
                                          "collection type expected in left operand for -> operator, found: {0}",
                                          new object[] { currentExpression.getType()
                                                                          .getName() });
            }

            checkAtPreNotAllowed(expression.getIsMarkedPre(), expression);

            string operationName = expression.getOperationName();
            List<object> argumentTypes = expression.getArgumentsTypes();

            CollectionType collectionType = (CollectionType) currentExpression.getType();
            CoreOperation operation = collectionType.lookupOperation(operationName,
                                                                     argumentTypes);

            if (operation != null) {
                CoreClassifier returnType = ((CollectionOperationImpl) operation).getReturnType(argumentTypes);
                expression.setAst(astFactory.createOperationCallExp(
                    currentExpression, operation, expression.getArgumentsAst(),
                    returnType, expression.getIsMarkedPre()));
            } else {
                generateSemanticException(expression.getSimpleNameNodeCS(),
                                          "operation {0} not found in collection {1}",
                                          new object[] {
                                              getOperationFullName(operationName, argumentTypes),
                                              collectionType.getName()
                                          });
            }

            // what is after the arrow?
            // operation without iterator stereotype => match operation call
            // operation with iterator stereotype => match iterator exp 
            }

        protected string getOperationFullName(
            string operationName,
            List<object> argumentTypes) {

            StringBuilder result = new StringBuilder();

            result.Append(operationName);
            result.Append("(");

            bool isFirst = true;

            foreach (CoreClassifier type in argumentTypes) {
                if (!isFirst) {
                    result.Append(", ");
                }

                result.Append(type.getName());
                isFirst = false;
            }

            result.Append(")");

            return result.ToString();
            }

        protected bool isImplicitSetAccess(OclExpression expression)
        {
            return (!(expression is LiteralExpImpl) &&
                    !(expression.getType() is CollectionTypeImpl));
        }

        protected OclExpression generateWithAsSetExpression(
            OclExpression currentExpression) {
            return astFactory.createAsSetOperation(currentExpression);
            }


        protected bool matchVariableExpression(CSTSimpleNameExpCS simpleNameExp, Environment currentEnvironment) {
            VariableDeclaration variable = currentEnvironment.lookupVariable(simpleNameExp.getNameAsString());
        
            if (variable != null) {
                simpleNameExp.setAst(astFactory.createVariableExp(variable));
                return	true;
            } else {
                return	false;
            }
        }

        protected bool matchClassifierLiteral(
            CSTSimpleNameExpCS simpleNameExp,
            Environment currentEnvironment)  {

            CoreModelElement c = (CoreModelElement) currentEnvironment.lookupPathName(simpleNameExp.getNameAsString());
            bool isClassifierLiteral = ((c != null) && c is CoreClassifierImpl);

            if (isClassifierLiteral) {
                simpleNameExp.setAst(astFactory.createOclTypeLiteralExp(
                    (astFactory.createOclModelElementType((CoreClassifier) c))));
            }

            return isClassifierLiteral;
            }

        protected bool matchImplicitAttributeCall(
            CSTSimpleNameExpCS simpleNameExp,
            Environment currentEnvironment)  {

            string attrName = simpleNameExp.getNameAsString();
            CoreAttribute attribute = currentEnvironment.lookupImplicitAttribute(attrName);

            if (attribute != null) {
                checkAtPreNotAllowed(simpleNameExp.getIsMarkedPre(), simpleNameExp);

                VariableDeclaration source = currentEnvironment.lookupSourceForImplicitFeature(attrName);
                simpleNameExp.setAst(astFactory.createAttributeCallExp(
                    astFactory.createVariableExp(source), attribute,
                    simpleNameExp.getIsMarkedPre()));
                if (attribute.isOclDefined()) {
                    OclDefinedAttributeImpl definedAttribute = (OclDefinedAttributeImpl) attribute;
                    constraintSourceTracker.addDependantToSource(definedAttribute.getSource(), simpleNameExp.getToken().getFilename());			                    
                }
            }
            return attribute != null;
            }

        protected bool matchAttributeCall(CSTSimpleNameExpCS simpleNameExp, OclExpression source)  {
            ModelPropertyCallExp attributeCallExp = matchAttributeCallExp(source,
                                                                          simpleNameExp);
            bool isAttributeCallExp = (attributeCallExp != null);

            if (isAttributeCallExp) {
                simpleNameExp.setAst(attributeCallExp);
            }
            return isAttributeCallExp;
        }

        protected ModelPropertyCallExp matchAttributeCallExp(
            OclExpression source,
            CSTSimpleNameExpCS cstNode)  {

            CoreAttribute attribute = source.getType()
                                            .lookupAttribute(cstNode.getNameAsString());

            if (attribute != null) {
                checkAtPreNotAllowed(cstNode.getIsMarkedPre(), cstNode);

                if (attribute.isOclDefined()) {
                    OclDefinedAttributeImpl definedAttribute = (OclDefinedAttributeImpl) attribute;
                    constraintSourceTracker.addDependantToSource(definedAttribute.getSource(), cstNode.getToken().getFilename());			                    
                }
                return astFactory.createAttributeCallExp(source, attribute,
                                                         cstNode.getIsMarkedPre());
            } else {
                return null;
            }
            }

        protected bool matchImplicitAssociationEndCall(
            CSTSimpleNameExpCS simpleNameExp,
            Environment currentEnvironment)  {
            string simpleName = simpleNameExp.getNameAsString();
        
            CoreAssociationEnd associationEnd = currentEnvironment.lookupImplicitAssociationEnd(simpleName);

            if (associationEnd != null) {
                checkAtPreNotAllowed(simpleNameExp.getIsMarkedPre(), simpleNameExp);

                if (simpleNameExp.getArgumentsNodesCS()
                                 .Count > 0) {
                                     checkArgumentsWithQualifiers(associationEnd, simpleNameExp);
                                 }
            
                VariableDeclaration source = currentEnvironment.lookupSourceForImplicitFeature(simpleName);
            
                simpleNameExp.setAst(astFactory.createAssociationEndCallExp(
                    astFactory.createVariableExp(source), associationEnd,
                    null, simpleNameExp.getArgumentsAst(), simpleNameExp.getIsMarkedPre()));
            } else {
            }
        
            return associationEnd != null;
            }

        protected bool matchAssociationEndCall(
            CSTSimpleNameExpCS simpleNameExp,
            OclExpression source)  {

            ModelPropertyCallExp associationEndCallExp = matchAssociationEndCallExp(source,
                                                                                    simpleNameExp);
            bool isAssociationEndCallExp = (associationEndCallExp != null);

            if (isAssociationEndCallExp) {
                simpleNameExp.setAst(associationEndCallExp);
            }
            return isAssociationEndCallExp;
            }

        protected ModelPropertyCallExp matchAssociationEndCallExp(
            OclExpression source,
            CSTSimpleNameExpCS cstNode)  {

            CoreAssociationEnd associationEnd = source.getType()
                                                      .lookupAssociationEnd(cstNode.getNameAsString());

            if (associationEnd != null) {
                checkAtPreNotAllowed(cstNode.getIsMarkedPre(), cstNode);

                if (cstNode.getArgumentsNodesCS()
                           .Count > 0) {
                               checkArgumentsWithQualifiers(associationEnd, cstNode);
                           }
                return astFactory.createAssociationEndCallExp(source,
                                                              associationEnd, null, cstNode.getArgumentsAst(),
                                                              cstNode.getIsMarkedPre());
            } else {
                return null;
            }
            }

        protected bool matchImplicitAssociationClassCall(
            CSTSimpleNameExpCS simpleNameExp,
            Environment currentEnvironment)  {

            string simpleName = simpleNameExp.getNameAsString();
            CoreAssociationClass associationClass = currentEnvironment.lookupImplicitAssociationClass(simpleName);

            if (associationClass != null) {
                CoreAssociationEnd associationEnd = null;
                List<object> qualifiers = new List<object>();
                checkAtPreNotAllowed(simpleNameExp.getIsMarkedPre(), simpleNameExp);

                VariableDeclaration source = currentEnvironment.lookupSourceForImplicitFeature(simpleName);

                if (simpleNameExp.getArgumentsNodesCS()
                                 .Count > 0) {
                                     associationEnd = checkArgumentsForAssociationClass(simpleNameExp);
                                     qualifiers.AddRange(simpleNameExp.getArgumentsAst());
                                 } else {
                                     checkForAmbiguousAssociationClassNavigation(associationClass,
                                                                                 source.getType(), simpleNameExp);
                                 }

                simpleNameExp.setAst(astFactory.createAssociationClassCallExp(
                    astFactory.createVariableExp(source), associationClass,
                    associationEnd, qualifiers, false));
            }

            return associationClass != null;
            }

        protected bool matchAssociationClassCall(
            CSTSimpleNameExpCS simpleNameExp,
            OclExpression source)  {

            ModelPropertyCallExp associationClassCallExp = matchAssociationClassCallExp(source,
                                                                                        simpleNameExp);
            bool isAssociationClassCallExp = (associationClassCallExp != null);

            if (isAssociationClassCallExp) {
                simpleNameExp.setAst(associationClassCallExp);
            }
            return isAssociationClassCallExp;
            }

        protected ModelPropertyCallExp matchAssociationClassCallExp(
            OclExpression source,
            CSTSimpleNameExpCS cstNode)  {

            CoreAssociationClass associationClass = source.getType()
                                                          .lookupAssociationClass(cstNode.getNameAsString());

            if (associationClass != null) {
                checkAtPreNotAllowed(cstNode.getIsMarkedPre(), cstNode);

                CoreAssociationEnd associationEnd = null;
                List<object> qualifiers = new List<object>();

                if (cstNode.getArgumentsNodesCS()
                           .Count > 0) {
                               associationEnd = checkArgumentsForAssociationClass(cstNode);
                               qualifiers.AddRange(cstNode.getArgumentsAst());
                           } else {
                               checkForAmbiguousAssociationClassNavigation(associationClass,
                                                                           source.getType(), cstNode);
                           }
                return astFactory.createAssociationClassCallExp(source,
                                                                associationClass, associationEnd, qualifiers,
                                                                cstNode.getIsMarkedPre());
            } else {
                return null;
            }
            }

        protected bool matchEnumerationLiteral(
            CoreModelElement element,
            CSTClassifierAttributeCallExpCS classifierAttributeCallExp)
        {

            if ((element != null) && (element is CoreClassifierImpl && ((CoreClassifier) element).isEnumeration())) {
                CoreEnumeration enumeration = (CoreEnumeration) element;
                checkForEnumerationLiteralWithAtPre(enumeration,
                                                    classifierAttributeCallExp);

                CoreEnumLiteral enumLiteral = getEnumerationLiteral(enumeration,
                                                                    classifierAttributeCallExp.getFeatureName(),
                                                                    classifierAttributeCallExp.getPathNameNodeCS());
                classifierAttributeCallExp.setAst(astFactory.createEnumLiteralExp(
                    enumLiteral));
                return true;
            } else {
                return false;
            }
        }

        protected bool matchClassifierAttributeCallExp(
            CoreModelElement element,
            CSTClassifierAttributeCallExpCS classifierAttributeCallExp)
        {

            bool result = false;

            if ((element != null) && (element is CoreClassifierImpl)) {
                CoreClassifier classifier = (CoreClassifier) element;
                CoreAttribute attribute = classifier.lookupAttribute(classifierAttributeCallExp.getFeatureName());

                if (attribute != null) {
                    checkForInvalidInstanceAttribute(attribute,
                                                     classifierAttributeCallExp.getPathNameNodeCS());
                    checkAtPreNotAllowed(classifierAttributeCallExp.getIsMarkedPre(),
                                         classifierAttributeCallExp);

                    classifierAttributeCallExp.setAst(astFactory.createAttributeCallExp(
                        astFactory.createOclTypeLiteralExp(
                            (astFactory.createOclModelElementType(classifier))), attribute,
                        classifierAttributeCallExp.getIsMarkedPre()));
                
                    result = true;
                }
            }
            return result;
        }

        protected bool matchPathNameClassifierLiteral(
            CSTClassifierAttributeCallExpCS classifierAttributeCallExp,
            Environment currentEnvironment)  {

            CoreClassifier c = this.getClassifierForName(currentEnvironment,
                                                         classifierAttributeCallExp.getPathNameNodeCS().ToString(),
                                                         classifierAttributeCallExp.getPathNameNodeCS());
            classifierAttributeCallExp.setAst(astFactory.createOclTypeLiteralExp(
                (astFactory.createOclModelElementType(c))));
            return true;
            }

        protected void matchOperationCallExp(
            CSTInstanceOperationCallExpCS expression,
            Environment currentEnvironment,
            OclExpression currentExpression)  {

            CoreOperation oper;
            string operationName = expression.getOperationName();
            List<object> argumentTypes = expression.getArgumentsTypes();

            if (currentExpression == null) {
                oper = currentEnvironment.lookupImplicitOperation(operationName,
                                                                  argumentTypes);
            } else {
                CoreClassifier classifier = currentExpression.getType();
                oper = classifier.lookupOperation(operationName, argumentTypes);
            }

            if (oper != null) {
                checkAtPreNotAllowed(expression.getIsMarkedPre(), expression);
                checkOperationCallAllowed(expression);

                CoreClassifier returnType = getOperationReturnType(oper,
                                                                   argumentTypes, currentEnvironment,
                                                                   expression.getSimpleNameNodeCS());

                OclExpression sourceExpression;

                if (currentExpression == null) {
                    VariableDeclaration source = currentEnvironment.lookupSourceForImplicitOperation(operationName,
                                                                                                     argumentTypes);
                    sourceExpression = astFactory.createVariableExp(source);
                } else {
                    sourceExpression = currentExpression;
                }

                if (oper.isOclDefined()) {
                    OclDefinedOperationImpl definedOperation = (OclDefinedOperationImpl) oper;
                    constraintSourceTracker.addDependantToSource(definedOperation.getSource(), expression.getToken().getFilename());			                    
                }
                createOperationCallExp(expression, sourceExpression, oper, returnType);
            } else {
                generateSemanticException(expression.getSimpleNameNodeCS(),
                                          "operation {0} not found in this context",
                                          new object[] { getOperationFullName(operationName, argumentTypes) });
            }
            }

        protected void	createOperationCallExp(CSTInstanceOperationCallExpCS expression, OclExpression sourceExpression, CoreOperation oper, CoreClassifier returnType)  {
            expression.setAst(astFactory.createOperationCallExp(
                sourceExpression, oper, expression.getArgumentsAst(),
                returnType, expression.getIsMarkedPre()));
        }
    
        protected void	checkOperationCallAllowed(CSTInstanceOperationCallExpCS expression)  {
            // default: is allowed
        }
    
        protected bool matchIteratorExp(
            CSTSimpleNameExpCS simpleNameExp,
            OclExpression source)  {

            CoreClassifier sourceType = source.getType();

            checkForNestedCollectionSource(simpleNameExp, sourceType);

            CoreClassifier elementType = ((CollectionType) sourceType).getElementType();
            VariableExp defaultVariableExp = astFactory.createVariableExp(astFactory.createVariableDeclaration("iterator",
                                                                                                               elementType, null));

            List<object> iterators = new List<object>();

            ModelPropertyCallExp modelPropertyCallExp;

            CoreClassifier expType = null;

            modelPropertyCallExp = matchAttributeCallExp(defaultVariableExp,
                                                         simpleNameExp);

            if (modelPropertyCallExp != null) {
                AttributeCallExpImpl attCallExp = (AttributeCallExpImpl) modelPropertyCallExp;
                expType = attCallExp.getExpressionType(source,
                                                       getFeatureType(attCallExp.getReferredAttribute()));
            } else {
                modelPropertyCallExp = matchAssociationEndCallExp(defaultVariableExp,
                                                                  simpleNameExp);

                if (modelPropertyCallExp != null) {
                    AssociationEndCallExpImpl assocCallExp = (AssociationEndCallExpImpl) modelPropertyCallExp;
                    expType = assocCallExp.getExpressionType(source,
                                                             assocCallExp.getReferredAssociationEnd());
                } else {
                    modelPropertyCallExp = matchAssociationClassCallExp(defaultVariableExp,
                                                                        simpleNameExp);

                    if (modelPropertyCallExp != null) {
                        AssociationClassCallExpImpl assocClassCallExp = (AssociationClassCallExpImpl) modelPropertyCallExp;
                        expType = assocClassCallExp.getExpressionType(source,
                                                                      assocClassCallExp.getReferredAssociationClass());
                    }
                }
            }

            if (modelPropertyCallExp != null) {
                iterators.Add(astFactory.createVariableDeclaration("iterator",
                                                                   elementType, null));

                simpleNameExp.setAst(astFactory.createIteratorExp("collect", expType,
                                                                  source, modelPropertyCallExp, iterators));
                return true;
            } else {
                return false;
            }
            }

        protected void matchIteratorCollectOperationCallExp(
            CSTInstanceOperationCallExpCS expression,
            Environment currentEnvironment,
            OclExpression currentExpression)  {

            CoreClassifier sourceType = currentExpression.getType();

            checkForNestedCollectionSource(expression.getSimpleNameNodeCS(), sourceType);

            CoreClassifier elementType = ((CollectionType) sourceType).getElementType();
            CoreOperation oper = elementType.lookupOperation(expression.getOperationName(),
                                                             expression.getArgumentsTypes());

            if (oper != null) {
                List<object> iterators = new List<object>();
                iterators.Add(astFactory.createVariableDeclaration("iterator",
                                                                   elementType, null));

                CoreClassifier returnType = getOperationReturnType(oper,
                                                                   expression.getArgumentsTypes(), currentEnvironment,
                                                                   expression.getSimpleNameNodeCS());

                checkAtPreNotAllowed(expression.getIsMarkedPre(), expression);

                VariableExp defaultVariableExp = astFactory.createVariableExp(astFactory.createVariableDeclaration("iterator",
                                                                                                                   elementType, null));

                OclExpression body = astFactory.createOperationCallExp(defaultVariableExp,
                                                                       oper, expression.getArgumentsAst(), returnType,
                                                                       expression.getIsMarkedPre());
                CoreClassifier type = ((OperationCallExpImpl) body).getExpressionType(currentExpression,
                                                                                      returnType);

                IteratorExp iteratorExp = astFactory.createIteratorExp("collect", type,
                                                                       currentExpression, body, iterators);
                expression.setAst(iteratorExp);
            } else {
                generateSemanticException(expression.getSimpleNameNodeCS(),
                                          "operation {0} not defined in collection type",
                                          new object[] { expression.getOperationName() });
            }
            }

        protected CoreClassifier getOperationReturnType(
            CoreOperation oper,
            List<object> arguments,
            Environment currentEnvironment,
            CSTNode expression)  {

            CoreClassifier	result = null;
            CoreClassifier returnType = oper.getReturnType();

            if ((returnType != null) &&
                OclTypesDefinition.typeNeedsToBeParsed(returnType.getName())) {
                    result = parseType(currentEnvironment,
                                       returnType.getName());
                } else if ((returnType != null) && returnType.getName()
                                                             .Equals("<T>") &&
                           (arguments.Count == 1) &&
                           arguments[0] is OclModelElementTypeImpl) {
                               if (((OclModelElementType) arguments[0]).getReferredModelElement() is CoreClassifierImpl)
                                   result = (CoreClassifier) ((OclModelElementType) arguments[0]).getReferredModelElement();
                               else {
                                   generateSemanticException(expression,
                                                             "{0} is not a valid classifier name",
                                                             new object[] { oper.getName() });
                               }
                           } else if (returnType == null && oper.getIsQuery()) {
                               generateSemanticException(expression,
                                                         "return type for operation {0} is void or not defined",
                                                         new object[] { oper.getName() });
                           }

            if (result == null && returnType != null)
                result = getOperationReturnType(oper);
        
            return	result;
            }

        protected OclExpression getCurrentExpression() {

            OclExpression currentExpression;

            if (stackOfExpressions.Count == 0) {
                currentExpression = null;
            } else {
                currentExpression = (OclExpression) stackOfExpressions.Peek();
            }
            return currentExpression;
        }

        protected string getCurrentOperator() {
            return (string) stackOfOperators.Peek();
        }

        protected CorePackage getPackageForName(CSTNameCS packageDeclaration)
        {

            object modelElement = this.initialContext.lookupPathName(packageDeclaration.getName());

            if ((modelElement != null) && (modelElement is CorePackageImpl)) {
                return (CorePackage) modelElement;
            } else {
                generateSemanticException(packageDeclaration,
                                          Messages.getString(
                                              "OCLSemanticAnalyzerVisitor.package_#{0}#_not_found_in_model_1"),
                                          new object[] { packageDeclaration.getName() });

                return null;
            }
        }

        protected CoreClassifier getClassifierForName(
            Environment currentEnvironment,
            string classifierName,
            CSTNode node)  {

            CoreModelElement classifier;
    	
            if (OclTypesDefinition.typeNeedsToBeParsed(classifierName)) {
                classifier = parseType(currentEnvironment,
                                       classifierName);
                return (CoreClassifier) classifier;
            } else {
                classifier = (CoreModelElement) currentEnvironment.lookupPathName(classifierName);
                if ((classifier != null) && (classifier is CoreClassifierImpl)) {
                    return (CoreClassifier) classifier;
                } else {
                    generateSemanticException(node,
                                              Messages.getString(
                                                  "OCLSemanticAnalyzerVisitor.classifier_#{0}#_not_defined_in_model_1"),
                                              new object[] { classifierName });
                    return null;
                }
            }
            }

/*
    protected CoreClassifier getExpectedFeatureType(
        CoreClassifier classifier,
        string featureName,
        CSTNode node)  {
        CoreClassifier expectedFeatureType = null;

        CoreAttribute attribute = classifier.lookupAttribute(featureName);

        if (attribute != null) {
            expectedFeatureType = attribute.getType();
        } else {
            CoreAssociationEnd associationEnd = classifier.lookupAssociationEnd(featureName);

            if (associationEnd != null) {
                expectedFeatureType = associationEnd.isOneMultiplicity() ? associationEnd.getParticipant() 
                   																                              : astFactory.createSpecificCollectionType(CollectionKind.OclSet, associationEnd.getParticipant()); 
            } else {
                generateSemanticException(node,
                    Messages.getString(
                        "OCLSemanticAnalyzerVisitor.#{0}#_is_neither_an_attribute_nor_an_association_end_of_#{1}#_2"),
                    new object[] { featureName, classifier.getName() });
            }
        }

        return expectedFeatureType;
    }
*/
        protected	CoreClassifier	getTypeForProperty(CoreModelElement	property) {

            CoreClassifier result = null;
    	
            if (property is CoreAttributeImpl) {
                CoreAttribute	attr = (CoreAttribute) property;
                Environment currentEnvironment = (Environment) stackOfEnvironments.Peek();
                result = (CoreClassifier) currentEnvironment.lookup(attr.getFeatureType().getName());
            } else if (property is CoreAssociationEndImpl) {
                CoreAssociationEnd	associationEnd = (CoreAssociationEnd) property;
                result = associationEnd.isOneMultiplicity() ? associationEnd.getTheParticipant()																										  
                             : astFactory.createSpecificCollectionType(CollectionKindEnum.SET, associationEnd.getTheParticipant());
            } else {
                result = null;
            }
		
            return	result;
        }

        protected bool isClassScopeFeature(
            CoreClassifier classifier,
            string featureName,
            CSTNode node) {

            bool result;
            CoreAttribute attribute = classifier.lookupAttribute(featureName);

            if (attribute != null) {
                result = ! attribute.isInstanceScope();
            } else {
                result = false;
            }
        
            return	result;
            }

        protected CoreModelElement getProperty(
            CoreClassifier classifier,
            string featureName,
            CSTNode node)  {
            CoreModelElement	result = null;
    	
            CoreAttribute attribute = classifier.lookupAttribute(featureName);

            if (attribute != null) {
                result = attribute;
            } else {
                CoreAssociationEnd associationEnd = classifier.lookupAssociationEnd(featureName);

                if (associationEnd != null) {
                    result = associationEnd;
                } else {
                    generateSemanticException(node,
                                              Messages.getString(
                                                  "OCLSemanticAnalyzerVisitor.#{0}#_is_neither_an_attribute_nor_an_association_end_of_#{1}#_2"),
                                              new object[] { featureName, classifier.getName() });

                    result = null;
                }
            }
            return	result;
            }

        protected CoreClassifier getClassifierForType(
            Environment currentEnvironment,
            CSTTypeCS type,
            CSTNode node)  {
            return (type != null)
                       ? getClassifierForName(currentEnvironment, type.getName(), node)
                       : null;
            }

        protected List<object> getParamTypesForOperation(
            CSTOperationCS operation,
            Environment currentEnvironment,
            CSTNode node)  {

            List<object> paramTypes = new List<object>();

            List<object> opParameters = operation.getParametersNodesCS();

            if (opParameters != null) {
                foreach (CSTVariableDeclarationCS parameter in opParameters) {
                    CoreClassifier parameterType = parameter.getTypeNodeCS()
                                                            .getAst();
                    paramTypes.Add(parameterType);
                }
            }

            return paramTypes;
            }

        protected List<object> getParamNamesForOperation(
            CSTOperationCS operation,
            Environment currentEnvironment,
            CSTNode node)  {

            List<object> paramNames = new List<object>();

            List<object> opParameters = operation.getParametersNodesCS();

            if (opParameters != null) {
                foreach (CSTVariableDeclarationCS parameter in opParameters) {
                    string	name = parameter.getNameNodeCS().getName();
                    paramNames.Add(name);
                }
            }
            return paramNames;
            }


        protected CoreEnumLiteral getEnumerationLiteral(
            CoreEnumeration enumeration,
            string enumLiteral,
            CSTNode node)  {
            CoreEnumLiteral literal = enumeration.lookupEnumLiteral(enumLiteral);

            if (literal != null) {
                return literal;
            } else {
                generateSemanticException(node,
                                          "{0} is not a literal defined in Enumeration {1}",
                                          new object[] { enumLiteral, enumeration.getName() });

                return null;
            }
            }

        protected void checkForFeatureRedefinition(
            CoreClassifier classifier,
            string varName,
            CSTNode node)  {

            if ((classifier.lookupAttribute(varName) != null) ||
                (classifier.lookupAssociationEnd(varName) != null)) {
                    generateSemanticException(node,
                                              Messages.getString(
                                                  "OCLSemanticAnalyzerVisitor.redefinition_of_#{0}#_in_classifier_#{1}#_4"),
                                              new object[] { varName, classifier.getName() });
                }
            }

        protected void checkForInvariantExpressionNotBoolean(
            CSTInvariantCS invariantDeclaration)  {

            if (! ((OclExpressionImpl) ((ExpressionInOclImpl) invariantDeclaration.getExpressionInOCL())
                                           .getBodyExpression())
                      .isBooleanExpression()) {
                          generateSemanticException(invariantDeclaration,
                                                    "invariant expression should be of type Boolean");
                      }
            }

        protected void checkForOperationConstraintNotBoolean(
            CSTOperationConstraintCS operationConstraint)
        {

            if (! ((OclExpressionImpl) ((ExpressionInOclImpl) operationConstraint.getExpressionInOCL())
                                           .getBodyExpression())
                      .isBooleanExpression()) {
                          generateSemanticException(operationConstraint,
                                                    "pre or post condition expression should be of type boolean");
                      }
        }

        protected void checkForInvariantNameRedefinition(
            string invariantName,
            CoreClassifier classifier,
            CSTNode node)  {

            if ((invariantName != null) &&
                (classifier.getInvariant(invariantName) != null)) {
                    generateSemanticException(node,
                                              Messages.getString(
                                                  "OCLSemanticAnalyzerVisitor.redefinition_of_invariant_#{0}#_for_classifier_#{1}#_5"),
                                              new object[] { invariantName, classifier.getName() });
                }
            }

        protected void checkForInitOrDerivedConstraintAlreadyDefined(
            string featureName,
            CoreClassifier classifier,
            CSTPathNameCS node)  {

            if ((classifier.getLocalInitConstraint(featureName) != null) ||
                (classifier.getLocalDeriveConstraint(featureName) != null)) {
                    generateSemanticException(node,
                                              Messages.getString(
                                                  "OCLSemanticAnalyzerVisitor.init_or_derived_expression_already_defined_for_#{0}#_6"),
                                              new object[] { node.getName() });
                }
            }

        protected void checkForIncompatibleBodyType(
            CoreOperation contextOperation,
            CoreClassifier type,
            CSTNode node)  {

            if (contextOperation.getReturnType() == null) {
                generateSemanticException(node,
                                          "operation {0} cannot have a body expression since it has no defined return type",
                                          new object[] { contextOperation.getName() });
            }

            if (! type.conformsTo(getOperationReturnType(contextOperation))) {
                generateSemanticException(node,
                                          "body expression type {0} does not conform to operation return type {1}",
                                          new object[] {
                                              type.getName(),
                                              contextOperation.getReturnType()
                                                              .getName()
                                          });
            }
            }

        protected void checkForFeatureTypeMismatch(
            CSTAttrOrAssocContextCS attrAssocContextDeclaration)  {

            CoreClassifier declaredFeatureType;
            CoreClassifier expectedFeatureType;

            if (attrAssocContextDeclaration.getTypeNodeCS() != null) {
                CoreModelElement element = getProperty(contextClassifier,
                                                       attrAssocContextDeclaration.getFeatureName(), attrAssocContextDeclaration.getPathNameNodeCS());
                expectedFeatureType = getTypeForProperty(element);
                declaredFeatureType = attrAssocContextDeclaration.getTypeNodeCS()
                                                                 .getAst();
																		
                if ((declaredFeatureType != null) && ! (expectedFeatureType.getFullPathName().Equals(declaredFeatureType.getFullPathName()))) {
                    generateSemanticException(attrAssocContextDeclaration.getTypeNodeCS(),
                                              Messages.getString(
                                                  "OCLSemanticAnalyzerVisitor.wrong_attribute_or_association_end_type__expected_#{0}#_but_found_#{1}#_1"),
                                              new object[] {
                                                  expectedFeatureType.getName(), declaredFeatureType.getName()
                                              });
                }
            }
            }

        protected void checkForExpressionTypeMismatch(
            CoreClassifier featureType,
            CoreClassifier expectedFeatureType,
            CSTNode node)  {

            if ((featureType != null) &&
                !featureType.conformsTo(expectedFeatureType)) {
                    generateSemanticException(node,
                                              "expression type {0} does not conform to feature type {1}",
                                              new object[] {
                                                  featureType.getName(), expectedFeatureType.getName()
                                              });
                }
            }

        protected void checkForOperationExistence(
            CoreOperation operationFound,
            CSTOperationCS operation,
            string classifierName)  {

            if (operationFound == null) {
                generateSemanticException(operation,
                                          Messages.getString(
                                              "OCLSemanticAnalyzerVisitor.operation_#{0}#_not_defined_in_classifier_#{1}#_2"),
                                          new object[] { operation.getFullName(), classifierName });
            }
            }

        protected void checkArgumentNames(
            CoreOperation declaredOperation,
            CSTOperationContextCS operation)  {
            if (declaredOperation != null) {
                object [] declaredParameters = declaredOperation.getParametersNamesExceptReturn().ToArray();
                for (int i = 0; i < operation.getParameterNames().Count; i++) {
                    if (! operation.getParameterNames()[i].Equals(declaredParameters[i])) {
                        generateSemanticException(operation,
                                                  "name mismatch in parameter {0} - operation: {1}",
                                                  new object[] { operation.getParameterNames()[i], declaredOperation.getFullSignatureAsString() });
                    }
                }
            }
            }

        protected void checkForOperatorExistence(
            CoreOperation operationFound,
            CSTOperatorCS operat,
            List<object> paramTypes,
            string classifierName)  {

            if (operationFound == null) {
                generateSemanticException(operat,
                                          "operator {0} not defined for classifier {1}",
                                          new object[] {
                                              getOperationFullName(operat.getOperator(), paramTypes),
                                              classifierName
                                          });
            }
            }

        protected void checkForOperationRedefinition(
            CoreOperation operationFound,
            CSTOperationCS operation,
            string classifierName)  {

            if (operationFound != null) {
                generateSemanticException(operation,
                                          Messages.getString(
                                              "OCLSemanticAnalyzerVisitor.redefinition_of_operation_#{0}#_in_classifier_#{1}#_7"),
                                          new object[] { operation.getFullName(), classifierName });
            }
            }

        protected void checkForWrongOperationContextName(
            CSTNameCS name,
            CSTOperationCS operation)  {

            if (!(name is CSTPathNameCS)) {
                generateSemanticException(name,
                                          Messages.getString(
                                              "OCLSemanticAnalyzerVisitor.classifier_not_defined_for_the_operation_#{0}#_1"),
                                          new object[] { operation.getFullName() });
            }
            }

        protected void checkForWrongOperationName(CSTNameCS name)
        {

            if (name is CSTPathNameCS) {
                generateSemanticException(name,
                                          Messages.getString(
                                              "OCLSemanticAnalyzerVisitor.wrong_operation_name_#{0}#__path_name_not_expected_3"),
                                          new object[] { name.ToString() });
            }
        }

        protected void checkForNonQueryOperation(
            CSTNode operationContextDeclaration,
            CoreOperation operation)  {

            if (! operation.getIsQuery()) {
                generateSemanticException(operationContextDeclaration,
                                          "non query operation {0} can not have a body definition",
                                          new object[] { operation.getName() });
            }
        
            }
    
        protected void checkForMoreThanOneBody(
            CSTOperationContextCS operationContextDeclaration)
        {

            if (operationContextDeclaration.getBodyDefinitionNodesCS()
                                           .Count > 1) {
                                               generateSemanticException(operationContextDeclaration,
                                                                         "Only one body should be specified for an operation.");
                                           }
        }

        protected void checkForBodyAlreadyDefined(
            CSTNode operationContextDeclaration,
            CoreOperation operation)  {

            if (operation.getBodyDefinition() != null || operation.getActionBody() != null) {
                generateSemanticException(operationContextDeclaration,
                                          "Operation {0} already has a body definition",
                                          new object[] { operation.getName() });
            }
            }

        protected void checkForTypeConformance(
            OclExpression initExpression,
            CoreClassifier target,
            CSTNode node)  {

            if (! ((OclExpressionImpl)initExpression).conformsTo(target)) {
                generateSemanticException(node,
                                          "expression type {0} does not conform to type {1} in variable initialization",
                                          new object[] {
                                              initExpression.getType()
                                                            .getName(), target.getName()
                                          });
            }
            }

        protected void checkIfTuplePartNameAlreadyDefined(
            TupleType tupleType,
            string tupleName,
            CSTNode node)  {

            if (tupleType.lookupAttribute(tupleName) != null) {
                generateSemanticException(node,
                                          "duplicate name found in tuple definition: {0} ",
                                          new object[] { tupleName });
            }
            }

        protected void checkForInvalidOperator(
            CSTNode node,
            string operat)  {

            if (operat.Equals(OP_ARROW)) {
                generateSemanticException(node,
                                          "collection operation expected following ->");
            }
            }

        protected void checkForEnumerationLiteralWithAtPre(
            CoreEnumeration enumeration,
            CSTClassifierAttributeCallExpCS node)  {
            if (node.getIsMarkedPre()) {

                generateSemanticException(node,
                                          "@pre should not be used in enumeration literal expression {0}",
                                          new object[] { node.getPathNameNodeCS()
                                                             .getName() });
            }
            }

        protected void checkForRangeElementNotInteger(
            CSTCollectionLiteralRangeCS literalPart)  {

            if (!"Integer".Equals(literalPart.getFirstNodeTypeName()) ||
                !"Integer".Equals(literalPart.getLastNodeTypeName())) {
                    generateSemanticException(literalPart,
                                              "range must be defined as an interval of integer numbers");
                }

            }

        protected void checkForAmbiguousAssociationClassNavigation(
            CoreAssociationClass associationClass,
            CoreClassifier type,
            CSTNode node)  {

            if (associationClass.lookupAssociationEnd(type) == null) {
                generateSemanticException(node,
                                          "Ambiguous navigation in association class {0}",
                                          new object[] { associationClass.getName() });
            }

            }

        protected void checkForUnexpectedOperator(CSTNode node)
        {

            if (getCurrentOperator() != null) {
                generateSemanticException(node,
                                          "if not expected in navigation expression");
            }

        }

        protected void checkAtPreNotAllowed(
            bool isMarkedPre,
            CSTNode node)  {

            if (isMarkedPre && ( !atPreAllowed)) {
                generateSemanticException(node,
                                          "@pre can be used only in post conditions");
            }

            }

        protected void checkArgumentsWithQualifiers(
            CoreAssociationEnd associationEnd,
            CSTSimpleNameExpCS simpleNameExp)  {

            List<object> definedQualifiers = associationEnd.getTheQualifiers();
            List<object> arguments = simpleNameExp.getArgumentsAst();

            if (definedQualifiers == null) {
                generateSemanticException(simpleNameExp,
                                          "qualifiers not expected in association end");
            }

            if (definedQualifiers.Count != arguments.Count) {
                generateSemanticException(simpleNameExp,
                                          "wrong number of qualifier arguments");
            }

            for (int i = 0; i < arguments.Count; i++) {
                OclExpressionImpl argument = (OclExpressionImpl) arguments[i];
                CoreAttribute qualifier = (CoreAttribute) definedQualifiers[i];

                if (!argument.conformsTo(getFeatureType(qualifier))) {
                    generateSemanticException(simpleNameExp,
                                              "argument type {0} does not conform to qualifier type {1}",
                                              new object[] {
                                                  argument.getType()
                                                          .getName(), qualifier.getFeatureType()
                                                                               .getName()
                                              });
                }
            }
            }

        protected void checkForInvalidInstanceAttribute(
            CoreAttribute attribute,
            CSTNode cstNode)  {

            if (attribute.isInstanceScope()) {
                generateSemanticException(cstNode,
                                          "{0} is an instance attribute of {1}",
                                          new object[] { attribute.getName(), contextClassifier.getName() });
            }
            }

        protected CoreAssociationEnd checkArgumentsForAssociationClass(
            CSTSimpleNameExpCS simpleNameExp)  {

            OclExpression argument = (OclExpression) simpleNameExp.getArgumentsAst()[0];

            if (argument is AssociationEndCallExp) {
                return ((AssociationEndCallExp) argument).getReferredAssociationEnd();
            } else {
                generateSemanticException(simpleNameExp,
                                          "invalid role name {0} for qualifier in association class navigation",
                                          new object[] { simpleNameExp.getNameAsString() });

                return null;
            }
            }

        protected void checkForNestedCollectionSource(
            CSTNode node,
            CoreClassifier sourceType)  {

            CollectionType collectionType = (CollectionType) sourceType;
            CoreClassifier elementType = collectionType.getElementType();

            if (elementType is CollectionTypeImpl) {
                generateSemanticException(node,
                                          "invalid source for property call expression: {0}",
                                          new object[] { elementType.getName() });
            }
            }

        protected void checkForInvalidIteratorSource(
            CoreClassifier sourceType,
            CSTNode node)  {

            if (!(sourceType is CollectionTypeImpl)) {
                generateSemanticException(node,
                                          "left operand of an iterator expected to be a collection, but found {0}",
                                          new object[] { sourceType.getName() });
            }
            }

        protected void checkForInvalidNumberOfIterators(CSTIteratorExpCS expression)
        {
            List<object> iterators = expression.getVariablesNodesCS();

            if (expression.getIteratorOperationNodeCS()
                          .getOperationName()
                          .Equals("forAll")) {
                              if (iterators.Count > 2) {
                                  generateSemanticException(expression.getIteratorOperationNodeCS(),
                                                            "at most two iterators are allowed in forAll expression");
                              }
                          } else {
                              if (iterators.Count > 1) {
                                  generateSemanticException(expression.getIteratorOperationNodeCS(),
                                                            "only one iterator allowed in {0} operation",
                                                            new object[] {
                                                                expression.getIteratorOperationNodeCS()
                                                                          .getOperationName()
                                                            });
                              }
                          }
        }

        protected void generateSemanticException(
            CSTNode node,
            string errorMessage,
            object[] parametersForErrorMessage)  {

            throw new OCLSemanticException(node, string.Format(errorMessage, parametersForErrorMessage));
            }

        protected void generateSemanticException(
            CSTNode node,
            string errorMessage)  {
            throw new OCLSemanticException(node, errorMessage);
            }

        protected void resetStack(int index) {
            if (index > 0) {
                for (int i = index - 1; i <= stackOfEnvironments.Count; i++)
                    ((EnvironmentImpl) stackOfEnvironments.Pop()).release();
            }

            stackOfExpressions.Clear();
            stackOfOperators.Clear();
        }
	
        protected	void	clearAllStacks() {
            stackOfEnvironments.Clear();
            stackOfExpressions.Clear();
            stackOfOperators.Clear();
        }

        protected void insertSelfVariableInEnvironment(
            Environment currentEnvironment,
            CoreClassifier classifier) {

            if (classifier != null) {
                insertVariableInEnvironment(currentEnvironment, astFactory.createVariableDeclaration("self",
                                                                                                     classifier, null), true);
            }
            }

        protected void insertResultVariableInEnvironment(
            Environment currentEnvironment,
            CoreClassifier classifier) {

            if (classifier != null) {
                insertVariableInEnvironment(currentEnvironment, astFactory.createVariableDeclaration("result",
                                                                                                     classifier, null), false);
            }

            }

        protected void insertVariableInEnvironment(
            Environment environment,
            VariableDeclaration variable,
            bool mayBeImplicit) {

            try {
                environment.addElement(variable.getVarName(), variable, mayBeImplicit);
            } catch (NullNameException e) {
                Console.WriteLine(e.StackTrace);
            } catch (NameClashException e) {
                Console.WriteLine(e.StackTrace);
            }
            }

        protected List<object> createPreConstraints(
            OclPrePostConstraint owner,
            string source,
            CoreOperation contextOperation,
            List<object> constraints) {

            List<object> result = new List<object>();

            foreach (CSTPreDeclCS constraintDecl in constraints) {
                OclConstraint oclConstraint;
                result.Add(oclConstraint = oclConstraintFactory.createPreConstraint(owner, source,
                                                                                    constraintDecl.getNameAsString(), contextOperation,
                                                                                    constraintDecl.getExpressionInOCL()));
                oclConstraint.setSourceNodeCS(constraintDecl);
            }

            return result;
            }

        protected List<object> createPostConstraints(
            OclPrePostConstraint owner,
            string source,
            CoreOperation contextOperation,
            List<object> constraints) {

            List<object> result = new List<object>();

            foreach (CSTPostDeclCS constraintDecl in constraints) {
                OclConstraint	oclConstraint;
                result.Add(oclConstraint = oclConstraintFactory.createPostConstraint(owner, source,
                                                                                     constraintDecl.getNameAsString(), contextOperation,
                                                                                     constraintDecl.getExpressionInOCL()));
                oclConstraint.setSourceNodeCS(constraintDecl);
            }

            return result;
            }

        protected ExpressionInOcl createExpressionInOCL(
            CoreClassifier contextClassifier,
            OclExpression oclExpression) {

            if (oclExpression != null) {
                return	oclConstraintFactory.createExpressionInOcl(
                    "", contextClassifier,
                    oclExpression);
            } else {
                return null;
            }
            }
    
        public CoreClassifier parseType(
            Environment environment,
            string name) {

            CSTNode node = null;

            PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
            CoreClassifier result = oclCompiler.parseType(environment, name);
   		
            return	result;
            }

        public CoreClassifier getOperationReturnType(CoreOperation operation) {

            CoreClassifier result = null;
    	
            if (OclTypesDefinition.typeNeedsToBeParsed(operation.getReturnType().getName())) {
                CoreClassifier owner = (CoreClassifier) operation.getElemOwner();
                if (owner != null) {
                    Environment globalEnvironment = owner.getModel().getEnvironmentWithoutParents();
                    result = parseType(globalEnvironment, operation.getReturnType().getName());
                } else {
                    result = operation.getModel() != null ? operation.getModel().toOclType(operation.getReturnType()) : operation.getReturnType(); 
                }
            }		
            else {
                result = operation.getModel() != null ? operation.getModel().toOclType(operation.getReturnType()) : operation.getReturnType();
            }		
          
            return	result;
        }
    
        public CoreClassifier getFeatureType(CoreAttribute attribute) {

            CoreClassifier result = null;
    	
            if (OclTypesDefinition.typeNeedsToBeParsed(attribute.getFeatureType())) {
                CoreClassifier owner = (CoreClassifier) attribute.getElemOwner();
                if (owner != null) {
                    Environment globalEnvironment = owner.getModel().getEnvironmentWithoutParents();
                    result = parseType(globalEnvironment, attribute.getFeatureType().getName());
                } else {
                    result = attribute.getModel() != null ? attribute.getModel().toOclType(attribute.getFeatureType()) : attribute.getFeatureType();
                }
            }		
            else {
                result = attribute.getModel() != null ? attribute.getModel().toOclType(attribute.getFeatureType()) : attribute.getFeatureType();
            }		
        
        
            return	result;
        }

    
    
    }
}
