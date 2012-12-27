header {
	using System.Collections.Generic;
	using OclParser.controller;
	using OclParser.cst.context;
	using OclParser.cst.expression;
	using OclParser.cst.literalExp;
	using OclParser.cst.name;
	using OclParser.cst.type;
}

options {
    language = "CSharp";
	namespace = "oclparser.antlrcs";
}

class OCLScriptParser extends Parser;
options {
    exportVocab = OCLScript;
    defaultErrorHandler = true;	
	buildAST = false;
    k = 3;  // lookahead number of tokens
}

expressionStream returns [List<object> declarations] 
{
	declarations = new List<object>();
	CSTPackageDeclarationCS packageDecl = null;
	CSTContextDeclarationCS contextDecl = null;
}	
:
	(
	packageDecl = packageDeclarationCS { declarations.Add(packageDecl); }
	|
	contextDecl = contextDeclarationCS { declarations.Add(contextDecl); }
	)* 
	EOF
		;

/*********************************************
packageDeclarationCS :
	KEYW_PACKAGE
		nameCS
		(contextDeclarationCS)*
	KEYW_ENDPACKAGE
		;
**********************************************/
packageDeclarationCS returns [CSTPackageDeclarationCS decl] 
{
	decl = null;
	CSTNameCS name = null;
	CSTContextDeclarationCS contextDecl = null;
}
:
	KEYW_PACKAGE
		name = nameCS { decl = new CSTPackageDeclarationCS(name); }
		(contextDecl = contextDeclarationCS { decl.addContextDeclaration(contextDecl); } )*
	KEYW_ENDPACKAGE
		;


/*********************************************
contextDeclarationCS :
	classifierContextDeclCS |
	attrOrAssocContextCS |
	operationContextDeclCS
		;
**********************************************/

contextDeclarationCS returns [CSTContextDeclarationCS decl] 
{ 
    decl = null;
} 
:
	(KEYW_CONTEXT pathNameCS COLON) => decl = attrOrAssocContextCS 		|
	(KEYW_CONTEXT pathNameCS KEYW_INIT) => decl = attrOrAssocContextCS 	|
	(KEYW_CONTEXT pathNameCS KEYW_DERIVE) => decl = attrOrAssocContextCS |
	(KEYW_CONTEXT nameCS KEYW_INV) => decl = classifierContextDeclCS	|
	(KEYW_CONTEXT nameCS KEYW_DEF) => decl = classifierContextDeclCS	|
	decl = operationContextDeclCS
		;


/*********************************************
attrOrAssocContextCS  :
	KEYW_CONTEXT
	pathNameCS
	(COLON typeCS)?
	initOrDerivedValueCS
		;
**********************************************/
attrOrAssocContextCS  returns [CSTAttrOrAssocContextCS decl] 
{
	decl = null;
	CSTPathNameCS pathName = null;
	CSTTypeCS type = null;
	CSTInitDerivedValueCS value = null;
}
:
	KEYW_CONTEXT
	pathName = pathNameCS
	(COLON	type = typeCS)?
	value = initOrDerivedValueCS
	{ decl = new CSTAttrOrAssocContextCS(pathName, type, value); }
		;

/*********************************************
initOrDerivedValueCS :
	initValueCS 
	|
	derValueCS 
	;
**********************************************/
	
initOrDerivedValueCS returns [CSTInitDerivedValueCS value]
{
	value = null;
}
:
	( value = initValueCS 	)
	|
	( value = derValueCS 	)
	;	
	
	
/*********************************************
initValueCS:
	KEYW_INIT 
	COLON
	expressionInOCLCS 
		;
**********************************************/

initValueCS returns [CSTInitValueCS value]
{
	value = null;
	CSTExpressionInOclCS expression = null;
}
:
	token : KEYW_INIT 
	COLON
	expression = expressionInOCLCS { value = new CSTInitValueCS((OCLWorkbenchToken) token, expression); }
		;


/*********************************************
derValueCS:
	KEYW_DERIVE
	COLON
	expressionInOCLCS
		;
**********************************************/
	
derValueCS returns [CSTDerivedValueCS value] 
{
	value = null;
	CSTExpressionInOclCS expression = null;
}
:
	token : KEYW_DERIVE
	COLON
	expression = expressionInOCLCS { value = new CSTDerivedValueCS((OCLWorkbenchToken) token, expression); }
		;


/*********************************************
classifierContextDeclCS 
	KEYW_CONTEXT
	nameCS
	invOrDefCS
		;
**********************************************/
classifierContextDeclCS returns [CSTClassifierContextDeclCS decl] 
{
	decl = null;
	CSTNameCS name = null;
	List<object> constraintsList = null;
}
:
	KEYW_CONTEXT
	name = nameCS { decl = new CSTClassifierContextDeclCS(name); } 
	constraintsList = invOrDefCS { decl.addConstraints(constraintsList); }
		;

/*********************************************
invOrDefCS :
	(invCS | defCS)+
		;
**********************************************/
invOrDefCS returns [List<object> constraints]	
{
	constraints = new List<object>();
	CSTConstraintDefinitionCS constraint = null;
}
:
	(
	constraint = invCS { constraints.Add(constraint); }
	| 
	constraint = defCS { constraints.Add(constraint); }
	)+
		;

/*********************************************
invCS  :
	KEYW_INV 
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
invCS  returns [CSTInvariantCS invariant] 
{
	invariant = null;
	CSTSimpleNameCS	invariantName = null;
	CSTExpressionInOclCS expression = null;
}
:
	token : KEYW_INV 
	(invariantName = simpleNameCS)?
	COLON { invariant = new CSTInvariantCS((OCLWorkbenchToken) token, invariantName); }
	expression = expressionInOCLCS { invariant.setExpressionNodeCS(expression); }
		;


/*********************************************
defCS:
	KEYW_DEF
	(simpleNameCS)?
	COLON
	defExpressionCS
		;
**********************************************/
defCS returns [CSTDefCS defDeclaration]
{
	defDeclaration = null;
	CSTDefExpressionCS expression = null;
	CSTNameCS name = null;
}
:
	token : KEYW_DEF
	(name = simpleNameCS)?
	COLON
	expression = defExpressionCS
	{ defDeclaration = new CSTDefCS((OCLWorkbenchToken) token, name, expression); }
		;

/*********************************************
defExpressionCS :
	defVarExpressionCS |
	defOperationExpressionCS
		;	
**********************************************/
defExpressionCS returns [CSTDefExpressionCS defExpression] 
{
	defExpression = null;
}
:
	defExpression = defVarExpressionCS |
	defExpression = defOperationExpressionCS
		;	
	
/*********************************************
defVarExpressionCS:
	simpleNameCS 
	(COLON typeCS)? 
	OP_EQUAL
	expressionInOCLCS
		;
**********************************************/

defVarExpressionCS returns [CSTDefVarExpressionCS defVarExpression]
{
	defVarExpression = null;
	CSTNameCS name = null;
	CSTTypeCS type = null;
	CSTExpressionInOclCS expression = null;
}	
:
	name = simpleNameCS 
	COLON type = typeCS 
	OP_EQUAL
	expression = expressionInOCLCS 
	{ defVarExpression = new CSTDefVarExpressionCS(name, type, expression); }
		;


/*********************************************
defOperationExpressionCS :
	operationCS
	OP_EQUAL
	expressionInOCLCS 
		;
**********************************************/

defOperationExpressionCS returns [CSTDefOperationExpressionCS defOperationExpression] 
{
	defOperationExpression = null;
	CSTOperationCS operation = null;
	CSTExpressionInOclCS expression = null;
}
:
	operation = operationCS
	OP_EQUAL
	expression = expressionInOCLCS 
	{ defOperationExpression = new CSTDefOperationExpressionCS(operation, expression); }
		;


/*********************************************
operationContextDeclCS:
	KEYW_CONTEXT
	operationCS
	prePostOrBodyDeclCS
		;
**********************************************/
operationContextDeclCS returns [CSTOperationContextCS operationDecl] 
{
	operationDecl = null;
	CSTOperationCS operation = null;
	List<object> decl = null;
}
:
	KEYW_CONTEXT
	operation = operationCS
	decl = prePostOrBodyDeclCS
	{ operationDecl = new CSTOperationContextCS(operation, decl); }
		;


/*********************************************
prePostOrBodyDeclCS:
	(preDeclCS | postDeclCS | bodyDeclCS)+
		;
**********************************************/
prePostOrBodyDeclCS returns [List<object> declarations] 
{
	declarations = new List<object>();
	CSTDefVarExpressionCS defVar = null;
	CSTOperationConstraintCS decl = null;
}
:
	(token : KEYW_DEF
		COLON
	  defVar = defVarExpressionCS { declarations.Add(defVar); })*

	(decl = preDeclCS { declarations.Add(decl); }
	| 
	decl = postDeclCS { declarations.Add(decl); }
	| 
	decl = bodyDeclCS { declarations.Add(decl); }
	)+
		;

/*********************************************
preDeclCS:
	KEYW_PRE
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
preDeclCS returns [CSTPreDeclCS decl] 
{
	decl = null;
	CSTNameCS name = null;
	CSTExpressionInOclCS expression = null;
}
:
	token : KEYW_PRE
	(name = simpleNameCS)?
	COLON
	expression = expressionInOCLCS
	{ decl = new CSTPreDeclCS((OCLWorkbenchToken) token, name, expression); }
		;


/*********************************************
postDeclCS:
	KEYW_POST
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
postDeclCS returns [CSTPostDeclCS decl]
{
	decl = null;
	CSTNameCS name = null;
	CSTExpressionInOclCS expression = null;
}
:
	token : KEYW_POST
	(name = simpleNameCS)?
	COLON
	expression = expressionInOCLCS
	{ decl = new CSTPostDeclCS((OCLWorkbenchToken) token, name, expression); }
		;


/*********************************************
bodyDeclCS:
	KEYW_BODY
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
bodyDeclCS returns [CSTBodyDeclCS decl] 
{
	decl = null;
	CSTNameCS name = null;
	CSTExpressionInOclCS expression = null;
}
:
	token : KEYW_BODY
	(name = simpleNameCS)?
	COLON
	expression = expressionInOCLCS
	{ decl = new CSTBodyDeclCS((OCLWorkbenchToken) token, name, expression); }
		;

/*********************************************
operationCS:
	nameCS
	LEFT_PAR
	(parametersCS)?
	RIGHT_PAR
	(COLON typeCS)?
		;
**********************************************/
operationCS returns [CSTOperationCS operation] 
{
	operation = null;
	CSTNameCS name = null;
	List<object> parameters = new List<object>();
	CSTTypeCS type = null;
}
:
	name = nameCS
	LEFT_PAR
	(parameters = parametersCS)?
	RIGHT_PAR
	(COLON type = typeCS)?
	{ operation = new CSTOperationCS(name, parameters, type); }
		;

/*********************************************
parametersCS:
	onlyTypeDefinedVariableDeclarationCS 
	(COMMA onlyTypeDefinedVariableDeclarationCS)*
		;
**********************************************/
parametersCS returns [List<object> parameters]
{
	parameters = new List<object>();
	CSTVariableDeclarationCS variableDeclaration = null;
}
:
	variableDeclaration = onlyTypeDefinedVariableDeclarationCS { parameters.Add(variableDeclaration); }
	(
	COMMA variableDeclaration = onlyTypeDefinedVariableDeclarationCS 
	{ parameters.Add(variableDeclaration); }
	)*
		;




expressionInOCLCS returns[CSTExpressionInOclCS expression]
{
	expression = null;
	CSTOclExpressionCS oclExpression = null;
}
:
	(oclExpression = letExpCS
	|	
	oclExpression = impliesExpressionCS)

	{ expression = new CSTExpressionInOclCS(oclExpression); }
		;

				   
/*********************************************
**********************************************/
letExpCS returns [CSTOclExpressionCS resultExpression] 
{
	resultExpression = null;
	CSTVariableDeclarationCS varDeclaration = null;
	CSTOclExpressionCS	oclExpression = null;
	List<object>	varDeclarations = new List<object>();
}
:
	KEYW_LET 
	varDeclaration = initializedVariableDeclarationCS  	{ varDeclarations.Add(varDeclaration); }
	(	
		COMMA 
		varDeclaration = initializedVariableDeclarationCS  	{ varDeclarations.Add(varDeclaration); }
	)* 
	KEYW_IN 
	(oclExpression = impliesExpressionCS | oclExpression = letExpCS)
	{ resultExpression = new CSTLetExpCS(varDeclarations, oclExpression); }
		;



//###########################################################################
//
//					EXPRESSIONS WITH OPERATOR PRECEDENCE
//
//###########################################################################

/*********************************************
**********************************************/
impliesExpressionCS returns [CSTOclExpressionCS resultExpression]
{
	resultExpression = null;
	CSTOperatorCS operat = null;
	CSTOclExpressionCS	rightExpression = null;
}
:
	resultExpression = logicalExpressionCS
	(operat = impliesOperatorCS
	rightExpression = logicalExpressionCS
		{ resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression); }
	)*
		;


/*********************************************
**********************************************/
			
logicalExpressionCS returns [CSTOclExpressionCS resultExpression]
{
	resultExpression = null;
	CSTOperatorCS operat = null;
	CSTOclExpressionCS	rightExpression = null;
}
: 
	resultExpression = equalityExpressionCS 
	(operat = logicalOperatorCS 
	rightExpression = equalityExpressionCS
		{ resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression); }
	
	)*
		;

/*********************************************
**********************************************/

equalityExpressionCS returns [CSTOclExpressionCS resultExpression]
{
	resultExpression = null;
	CSTOperatorCS operat = null;
	CSTOclExpressionCS	rightExpression = null;
}
: 
	resultExpression = relationalExpressionCS 
	(operat = equalityOperatorCS 
	rightExpression = relationalExpressionCS
		{ resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression); }
	)*
		;


/*********************************************
**********************************************/

relationalExpressionCS returns [CSTOclExpressionCS resultExpression]
{
	resultExpression = null;
	CSTOperatorCS operat = null;
	CSTOclExpressionCS	rightExpression = null;
}
: 
	resultExpression = additiveExpressionCS 
	(operat = relationalOperatorCS 
	rightExpression = additiveExpressionCS
		{ resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression); }
	)*
		;


/*********************************************
**********************************************/
additiveExpressionCS returns [CSTOclExpressionCS resultExpression]
{
	resultExpression = null;
	CSTOperatorCS operat = null;
	CSTOclExpressionCS	rightExpression = null;
}
: 
	resultExpression = multiplicativeExpressionCS 
	(operat = addOperatorCS 
	rightExpression = multiplicativeExpressionCS
		{ resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression); }
	)*
		;


/*********************************************
**********************************************/
multiplicativeExpressionCS returns [CSTOclExpressionCS resultExpression]
{
	resultExpression = null;
	CSTOperatorCS operat = null;
	CSTOclExpressionCS	rightExpression = null;
}
: 
	resultExpression = unaryExpressionCS 
	(operat = multiplyOperatorCS
	rightExpression = unaryExpressionCS
		{ resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression); }
	)*
		;


/*********************************************
**********************************************/
unaryExpressionCS returns[CSTOclExpressionCS resultExpression] 
{
	resultExpression = null;
	CSTOperatorCS unaryOperator = null;
	CSTOclExpressionCS expression = null;
}
: 
	(	
		unaryOperator = unaryOperatorCS 
		( expression = oclExpressionCS  |
		  expression = parenthesisExpressionCS )
	)
	 {  resultExpression = new CSTUnaryExpressionCS(unaryOperator, expression); }
	|
     (	resultExpression = oclExpressionCS )
    |
     (	resultExpression = parenthesisExpressionCS )
		;


parenthesisExpressionCS returns [CSTOclExpressionCS resultExpression] 
{
	resultExpression = null;
	CSTOperatorCS unaryOperator = null;
	CSTOclExpressionCS expression = null;
	
	CSTOclExpressionCS callExp = null;
	CSTOclExpressionCS innerNavigation = null;
	CSTNavigationExpressionCS navigationExpression = null;
	CSTNavigationOperatorCS operat = null;
	
}
: 
   	LEFT_PAR (resultExpression = impliesExpressionCS | resultExpression = letExpCS) RIGHT_PAR
   	(
	  (OP_ARROW KEYW_ITERATE LEFT_PAR) => opArrow1 : OP_ARROW  innerNavigation = iterateExpCS
		    { 	if (navigationExpression == null) {
		    		navigationExpression = new CSTNavigationExpressionCS(resultExpression);
 		    		resultExpression = navigationExpression;
	    	}
	    	navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow1), innerNavigation); 
  		    }
	  |
	  (OP_ARROW iteratorOperationCS)   => opArrow2 : OP_ARROW  innerNavigation = iteratorExpCS
		    { 	if (navigationExpression == null) {
  		    		navigationExpression = new CSTNavigationExpressionCS(resultExpression);
  		    		resultExpression = navigationExpression;
  		    	}
		    	navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow2), innerNavigation); 
		    }
	  |
	  (
  		operat = navigationOperatorCS
		innerNavigation = partTwoExpCS
		  	{ 	if (navigationExpression == null) {
  		    		navigationExpression = new CSTNavigationExpressionCS(resultExpression);
  		    		resultExpression = navigationExpression;
  		    	}
		  		navigationExpression.addInnerNavigation(operat, innerNavigation); 
		  	}
 	  )	
	)*
    	
		;

//###########################################################################
//
//							OPERATORS
//
//###########################################################################


/*********************************************
**********************************************/
impliesOperatorCS returns [CSTOperatorCS operat]
{
	operat = null;
}
:
	opImplies : KEYW_IMPLIES	{ operat = new CSTOperatorCS((OCLWorkbenchToken) opImplies); }
		;

/*********************************************
**********************************************/
logicalOperatorCS returns [CSTOperatorCS operat]
{
	operat = null;
}
:
     (
     	opAnd : KEYW_AND 		{ operat = new CSTOperatorCS((OCLWorkbenchToken) opAnd); }
    	| 
	    opOr : KEYW_OR			{ operat = new CSTOperatorCS((OCLWorkbenchToken) opOr); }
    	| 
	    opXor : KEYW_XOR		{ operat = new CSTOperatorCS((OCLWorkbenchToken) opXor); }
     )
     	;


/*********************************************
**********************************************/
relationalOperatorCS returns [CSTOperatorCS operat] 
{
	operat = null;
}
:
     (
	    opLessThan : OP_LESS_THAN			{ operat = new CSTOperatorCS((OCLWorkbenchToken) opLessThan); }
    	| 
	    opGreaterThan : OP_GREATER_THAN		{ operat = new CSTOperatorCS((OCLWorkbenchToken) opGreaterThan); }
    	| 
	    opLessOrEqual : OP_LESS_OR_EQ		{ operat = new CSTOperatorCS((OCLWorkbenchToken) opLessOrEqual); }
    	| 
	    opGreaterOrEqual : OP_GREATER_OR_EQ	{ operat = new CSTOperatorCS((OCLWorkbenchToken) opGreaterOrEqual); }
     )
	    ;


equalityOperatorCS returns [CSTOperatorCS operat] 
{
	operat = null;
}
:
     (
     	opEqual : OP_EQUAL 			{ operat = new CSTOperatorCS((OCLWorkbenchToken) opEqual); }
    	| 
	    opNotEqual : OP_NOTEQUAL	{ operat = new CSTOperatorCS((OCLWorkbenchToken) opNotEqual); }
     )
     ;    


/*********************************************
**********************************************/

addOperatorCS returns [CSTOperatorCS operat]
{
	operat = null;
}
:
     (
     	opPlus : OP_PLUS 	{ operat = new CSTOperatorCS((OCLWorkbenchToken) opPlus); }
     	| 
	    opMinus : OP_MINUS	{ operat = new CSTOperatorCS((OCLWorkbenchToken) opMinus); }
     )
     	;


/*********************************************
**********************************************/
multiplyOperatorCS returns [CSTOperatorCS operat]
{
	operat = null;
}
:
     (
     	opMultiply : OP_MULTIPLY { operat = new CSTOperatorCS((OCLWorkbenchToken) opMultiply); }
     	| 
	    opDivide : OP_DIVIDE  	 { operat = new CSTOperatorCS((OCLWorkbenchToken) opDivide); }
     )
    	;


/*********************************************
**********************************************/
unaryOperatorCS returns [CSTOperatorCS operat]
{
	operat = null;
}
:
     (
     	opMinus : OP_MINUS  { operat = new CSTOperatorCS((OCLWorkbenchToken) opMinus); }
	    | 
	    opNot : KEYW_NOT 	{ operat = new CSTOperatorCS((OCLWorkbenchToken) opNot); }
     )
	    ;

     
     





//###########################################################################
//
//					MODEL PROPERTY CALL EXPRESSIONS
//
//###########################################################################



oclExpressionCS returns[CSTOclExpressionCS expression]
{
	expression = null;
}
:
	expression = ifExpCS |
	expression = navigationExpCS 
		;



/*********************************************
**********************************************/

ifExpCS returns [CSTOclExpressionCS resultExpression] 
{
	resultExpression = null;
	CSTOclExpressionCS conditionExpression = null;
	CSTOclExpressionCS thenExpression = null;
	CSTOclExpressionCS elseExpression = null;
}
:
	tokenIf : KEYW_IF 
	(conditionExpression = impliesExpressionCS | conditionExpression = letExpCS)
	KEYW_THEN 
	(thenExpression = impliesExpressionCS | thenExpression = letExpCS)
	KEYW_ELSE 
	(elseExpression = impliesExpressionCS | elseExpression = letExpCS)
	KEYW_ENDIF
	{ resultExpression = new CSTIfExpCS((OCLWorkbenchToken) tokenIf, conditionExpression, thenExpression, elseExpression); }
		;






//###########################################################################
//
//					MODEL PROPERTY CALL EXPRESSIONS
//
//###########################################################################

/*********************************************
navigationExpCS :

	partOneExpCS
	(
	 (OP_ARROW KEYW_ITERATE LEFT_PAR) => iterateExpCS
	  |
	 (OP_ARROW simpleNameCS LEFT_PAR variableDeclarationCS) => iteratorExpCS
	  |
	 (navigationOperatorCS	partTwoExpCS)	
	)*
	;
**********************************************/

navigationExpCS returns [CSTOclExpressionCS expression]
{
	expression = null;
	CSTOclExpressionCS callExp = null;
	CSTOclExpressionCS innerNavigation = null;
	CSTNavigationExpressionCS navigationExpression = null;
	CSTNavigationOperatorCS operat = null;
}
:
	callExp = partOneExpCS
	 	{ navigationExpression = new CSTNavigationExpressionCS(callExp);
	  	  expression = navigationExpression;
	  	}
	
	(
		  (OP_ARROW KEYW_ITERATE LEFT_PAR) => opArrow1 : OP_ARROW  innerNavigation = iterateExpCS
  		    { navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow1), innerNavigation); }
		  |
		  (OP_ARROW iteratorOperationCS)   => opArrow2 : OP_ARROW  innerNavigation = iteratorExpCS
		    { navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow2), innerNavigation); }
		  |
		  (
	  		operat = navigationOperatorCS
			innerNavigation = partTwoExpCS
		  	{ navigationExpression.addInnerNavigation(operat, innerNavigation); }
		  )	
	)*
	{
	  if (innerNavigation == null) 
		expression = callExp;
	}  	
	;


navigationOperatorCS returns [CSTNavigationOperatorCS operat]
{
	operat = null;
}
:
	opDot : OP_DOT		{ operat = new CSTNavigationOperatorCS((OCLWorkbenchToken) opDot); }
	|
	opArrow : OP_ARROW	{ operat = new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow); }
	;


/*********************************************
partOneExpCS :
	(simpleNameCS LEFT_PAR) => instanceOperationCallExpCS |
	(pathNameCS LEFT_PAR) => classOperationCallExpCS |
	(simpleNameCS isMarkedPreCS LEFT_PAR) => instanceOperationCallExpCS |
	(pathNameCS isMarkedPreCS LEFT_PAR) => classOperationCallExpCS |
	simpleNameExpCS |
	classifierAttributeCallExpCS |
	literalExpCS
		;
**********************************************/
partOneExpCS returns [CSTOclExpressionCS expression]
{
	expression = null;
}
:
	(simpleNameCS LEFT_PAR) => expression = instanceOperationCallExpCS |
	(pathNameCS LEFT_PAR) => expression = classOperationCallExpCS |
	(simpleNameCS isMarkedPreCS LEFT_PAR) => expression = instanceOperationCallExpCS |
	(pathNameCS isMarkedPreCS LEFT_PAR) => expression = classOperationCallExpCS |
	expression = simpleNameExpCS |
	expression = classifierAttributeCallExpCS |
	expression = literalExpCS
		;


/*********************************************
partTwoExpCS :
	(simpleNameCS LEFT_PAR) => instanceOperationCallExpCS |
	(simpleNameCS isMarkedPreCS LEFT_PAR) => instanceOperationCallExpCS |
	simpleNameExpCS 
		;
**********************************************/

partTwoExpCS returns [CSTOclExpressionCS expression]
{
	expression = null;
}
:
	(simpleNameCS LEFT_PAR) => expression = instanceOperationCallExpCS |
	(simpleNameCS isMarkedPreCS LEFT_PAR) => expression = instanceOperationCallExpCS |
	expression = simpleNameExpCS 
		;




/*********************************************
simpleNameExpCS :
	simpleNameCS 
	(LEFT_BRACKET 
	 arguments = argumentsCS
	 RIGHT_BRACKET)?	 
	(isMarkedPreCS)?
		;
**********************************************/
simpleNameExpCS returns [CSTSimpleNameExpCS callExpCS] 
{
	callExpCS = null;
	CSTSimpleNameCS name = null;
	bool isMarkedPre = false;
	List<object> arguments = new List<object>();
}
:
	name = simpleNameCS 
	(LEFT_BRACKET 
	 arguments = argumentsCS
	 RIGHT_BRACKET)?	 
	(isMarkedPre = isMarkedPreCS)?
	{ callExpCS = new CSTSimpleNameExpCS(name, arguments, isMarkedPre); }
		;


/*********************************************
classifierAttributeCallExpCS :
	pathNameCS
	(isMarkedPreCS)?
		;
**********************************************/

classifierAttributeCallExpCS returns [CSTClassifierAttributeCallExpCS callExpCS] 
{
	callExpCS = null;
	CSTPathNameCS name = null;
	bool isMarkedPre = false;
}
:
	name = pathNameCS
	(isMarkedPre = isMarkedPreCS)?
	{ callExpCS = new CSTClassifierAttributeCallExpCS(name, isMarkedPre); }
		;


/*********************************************
instanceOperationCallExpCS :
	simpleNameCS
	(isMarkedPreCS)?
	LEFT_PAR
	(argumentsCS)?
	RIGHT_PAR
		;
**********************************************/
instanceOperationCallExpCS returns [CSTOclExpressionCS expression]
{
	expression = null;
	CSTSimpleNameCS simpleName = null;
	List<object> arguments = new List<object>();
	bool isMarkedPre = false;
}
:
	simpleName = simpleNameCS
	(isMarkedPre = isMarkedPreCS)?
	LEFT_PAR
	(arguments = argumentsCS)?
	RIGHT_PAR
	{ expression = new CSTInstanceOperationCallExpCS(simpleName, arguments, isMarkedPre); }
		;



/*********************************************
classOperationCallExpCS :
	pathNameCS
	(isMarkedPreCS)?
	LEFT_PAR
	(argumentsCS)?
	RIGHT_PAR
		;
**********************************************/
classOperationCallExpCS returns [CSTOclExpressionCS expression]
{
	expression = null;
	CSTPathNameCS pathName = null;
	bool isMarkedPre = false;
	List<object> arguments = new List<object>();
}
:
	pathName = pathNameCS
	(isMarkedPre = isMarkedPreCS)?
	LEFT_PAR
	(arguments = argumentsCS)?
	RIGHT_PAR
	{ expression = new CSTClassOperationCallExpCS(pathName, arguments, isMarkedPre); }
		;



/*********************************************
isMarkedPreCS :
	AT 
	KEYW_PRE
		;
**********************************************/
isMarkedPreCS returns [bool isMarkedPre]
{
	isMarkedPre = false;
}
:
	AT 
	KEYW_PRE
	{ isMarkedPre = true; }
		;
		
		
/*********************************************
argumentsCS :
	impliesExpressionCS
	(COMMA argumentsCS)?
		;
**********************************************/
argumentsCS returns [List<object> arguments]
{
	arguments = new List<object>();
	CSTOclExpressionCS expression = null;
}
:
	(expression = impliesExpressionCS | expression = letExpCS) { arguments.Add(new CSTArgumentCS(expression)); }
	(
		COMMA 
		(expression = impliesExpressionCS | expression = letExpCS)
		{ arguments.Add(new CSTArgumentCS(expression)); }
	)*
		;
		


/*********************************************
iteratorExpCS :
	  iteratorOperationCS
	  LEFT_PAR
 	  variableDeclarationCS  
	  (COMMA  variableDeclarationCS)?
	  VERT_BAR
	  impliesExpression
	  RIGHT_PAR
		;
**********************************************/
iteratorExpCS returns [CSTIteratorExpCS expression]
{
	expression = null;
	CSTVariableDeclarationCS varDecl;
	List<object> iterators = new List<object>();
	CSTOclExpressionCS bodyExpression = null;
	CSTIteratorOperationCS iteratorOperation = null;
}
:
	  iteratorOperation = iteratorOperationCS
	  LEFT_PAR
	  (
  	  	  ((simpleNameCS COLON) | (simpleNameCS VERT_BAR) | (simpleNameCS COMMA)) =>
	  	  ( iterators = iteratorsCS	
	  	  	VERT_BAR
		    (bodyExpression = impliesExpressionCS | bodyExpression = letExpCS)
		  )
		  |
 		    (bodyExpression = impliesExpressionCS | bodyExpression = letExpCS)
	  )
	  RIGHT_PAR
	  { expression = new CSTIteratorExpCS(iteratorOperation, iterators, bodyExpression); }
		;



iteratorsCS returns [List<object> iterators]
{
	iterators = new List<object>();
	CSTNameCS name = null;
	CSTTypeCS type1 = null;
	CSTTypeCS type2 = null;
}
:
	name = simpleNameCS 
	(COLON type1 = typeCS)? 
		{ iterators.Add(new CSTVariableDeclarationCS(name, type1, null)); }
    (	COMMA 
		name = simpleNameCS
		(COLON type2 = typeCS)?
		{ iterators.Add(new CSTVariableDeclarationCS(name, type2, null)); }
	)? 
	;



/*********************************************
iterateExpCS:
	  KEYW_ITERATE
	  LEFT_PAR
 	  variableDeclarationCS 
 	  (SEMI_COLON  variableDeclarationCS)?
	  VERT_BAR
	  navigationExpCS
	  RIGHT_PAR
		;

**********************************************/

iterateExpCS returns [CSTIterateExpCS expression]
{
	expression = null;
	List<object> iterators = null;
	CSTVariableDeclarationCS result = null;
	CSTOclExpressionCS bodyExpression = null;
}
:
	  KEYW_ITERATE
	  LEFT_PAR
	  (
	 	  (simpleNameCS COLON typeCS SEMI_COLON) => (iterators = iteratorsCS SEMI_COLON result = initializedVariableDeclarationCS) 
 		  |
  	 	  (simpleNameCS COLON typeCS COMMA) => (iterators = iteratorsCS SEMI_COLON result = initializedVariableDeclarationCS) 
  	 	  |
	 	  (simpleNameCS SEMI_COLON) => (iterators = iteratorsCS SEMI_COLON result = initializedVariableDeclarationCS)
	 	  |	  
  	 	  (simpleNameCS COMMA) => (iterators = iteratorsCS SEMI_COLON result = initializedVariableDeclarationCS) 
  	 	  |
	 	  (result = initializedVariableDeclarationCS)
	  )	  
	  VERT_BAR
	  (bodyExpression = impliesExpressionCS | bodyExpression = letExpCS)
	  RIGHT_PAR
	  { expression = new CSTIterateExpCS(iterators, result, bodyExpression); }
		;





iteratorOperationCS returns [CSTIteratorOperationCS iteratorOperation]
{
	iteratorOperation = null;
}
: 
		(tokenExists: 	KEYW_EXISTS			{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenExists); }	
		|	
		tokenForAll:  	KEYW_FORALL			{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenForAll); }
		|	
		tokenIsUnique:	KEYW_ISUNIQUE 		{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenIsUnique); }
		|	
		tokenAny:		KEYW_ANY			{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenAny); }
		|	
		tokenOne:		KEYW_ONE			{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenOne); }
		|	
		tokenCollect:	KEYW_COLLECT		{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenCollect); }
		|	
		tokenSelect:	KEYW_SELECT			{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenSelect); }
		|
		tokenReject:	KEYW_REJECT			{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenReject); }
		|
		tokenNested:	KEYW_COLLECTNESTED 		{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenNested); }
		|	
		tokenSortedBy:	KEYW_SORTEDBY	{ iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenSortedBy); }
		)	
	;

		
		

//###########################################################################
//
//						VARIABLE DECLARATIONS
//
//###########################################################################

/*********************************************
variableDeclarationCS:
	simpleNameCS 
	(COLON typeCS)? 
	(OP_EQUAL impliesExpressionCS)?
		;
**********************************************/
variableDeclarationCS returns [CSTVariableDeclarationCS variableDeclaration] 
{
	variableDeclaration = null;
	CSTNameCS name = null;
	CSTTypeCS type = null;
	CSTOclExpressionCS	expression = null;
}
:
	name = simpleNameCS 
	(COLON type = typeCS)? 
	(OP_EQUAL 
	 (expression = impliesExpressionCS | expression = letExpCS) )?
	{ variableDeclaration = new CSTVariableDeclarationCS(name, type, expression); }
		;


/*********************************************
variableDeclarationListCS :
	variableDeclarationCS 
	(COMMA variableDeclarationCS)*
		;
**********************************************/
variableDeclarationListCS returns [List<object> variableDeclarationList] 
{
	variableDeclarationList = new List<object>();
	CSTVariableDeclarationCS declaration = null;
}
:
	declaration = variableDeclarationCS { variableDeclarationList.Add(declaration); }
	(
		COMMA 
		declaration = variableDeclarationCS { variableDeclarationList.Add(declaration); }
	)*
		;



/*********************************************
**********************************************/
initializedVariableDeclarationCS returns [CSTVariableDeclarationCS variableDeclaration] 
{
	variableDeclaration = null;
	CSTNameCS name = null;
	CSTTypeCS type = null;
	CSTOclExpressionCS	expression = null;
}
:
	name = simpleNameCS 
	COLON type = typeCS 
	OP_EQUAL 
	(expression = impliesExpressionCS |	expression = letExpCS)
	{ variableDeclaration = new CSTVariableDeclarationCS(name, type, expression); }
		;

/*********************************************
**********************************************/
defaultVariableDeclarationCS returns [CSTVariableDeclarationCS variableDeclaration] 
{
	variableDeclaration = null;
	CSTNameCS name = null;
	CSTTypeCS type = null;
	CSTOclExpressionCS	expression = null;
}
:
	name = simpleNameCS 
	(COLON type = typeCS)?
	OP_EQUAL 
	(expression = impliesExpressionCS | expression = letExpCS)
	{ variableDeclaration = new CSTVariableDeclarationCS(name, type, expression); }
		;



/*********************************************
**********************************************/
onlyTypeDefinedVariableDeclarationCS returns [CSTVariableDeclarationCS variableDeclaration] 
{
	variableDeclaration = null;
	CSTNameCS name = null;
	CSTTypeCS type = null;
}
:
	name = simpleNameCS 
	(COLON type = typeCS)?
	{ variableDeclaration = new CSTVariableDeclarationCS(name, type, null); }
		;



		
//###########################################################################
//
//					LITERAL  EXPRESSIONS
//
//###########################################################################

/*********************************************
literalExpCS :
	primitiveLiteralExpCS |
	collectionLiteralExpCS |
	tupleLiteralExpCS
		;
**********************************************/

literalExpCS returns [CSTLiteralExpCS literalExp] 
{
	literalExp = null;
}
:
	literalExp = primitiveLiteralExpCS |
	literalExp = collectionLiteralExpCS |
	literalExp = tupleLiteralExpCS
		;


/*********************************************
primitiveLiteralExpCS :
	integerLiteralExpCS |
	realLiteralExpCS |
	stringLiteralExpCS |
	booleanLiteralExpCS
		;
**********************************************/

primitiveLiteralExpCS returns [CSTLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	literalExp = integerLiteralExpCS |
	literalExp = realLiteralExpCS |
	literalExp = stringLiteralExpCS |
	literalExp = booleanLiteralExpCS |
		literalExp = nullLiteralExpCS |
	literalExp = invalidLiteralExpCS
		;

/*********************************************
integerLiteralExpCS :
	INT_NUMBER
		;
**********************************************/

integerLiteralExpCS returns [CSTIntegerLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	value : INT_NUMBER 
	{ literalExp = new CSTIntegerLiteralExpCS((OCLWorkbenchToken) value); }
		;
	
/*********************************************
realLiteralExpCS :
	REAL_NUMBER
		;
**********************************************/

realLiteralExpCS  returns [CSTRealLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	value : REAL_NUMBER 
	{ literalExp = new CSTRealLiteralExpCS((OCLWorkbenchToken) value); }
		;

/*********************************************
stringLiteralExpCS :
	STRING
		;
**********************************************/
	
stringLiteralExpCS returns [CSTStringLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	value : STRING 
	{ literalExp = new CSTStringLiteralExpCS((OCLWorkbenchToken) value); }
		;

/*********************************************
booleanLiteralExpCS :
	KEYW_TRUE | KEYW_FALSE
	;
**********************************************/
	
booleanLiteralExpCS returns [CSTBooleanLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	valueTrue : KEYW_TRUE { literalExp = new CSTBooleanLiteralExpCS((OCLWorkbenchToken) valueTrue); }
	| 
	valueFalse : KEYW_FALSE { literalExp = new CSTBooleanLiteralExpCS((OCLWorkbenchToken) valueFalse); }
		;


nullLiteralExpCS returns [CSTNullLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	valueNull : KEYW_NULL { literalExp = new CSTNullLiteralExpCS((OCLWorkbenchToken) valueNull); }
;

invalidLiteralExpCS returns [CSTInvalidLiteralExpCS literalExp]
{
	literalExp = null;
}
:
	valueInvalid : KEYW_INVALID { literalExp = new CSTInvalidLiteralExpCS((OCLWorkbenchToken) valueInvalid); }
;



/*********************************************
collectionLiteralExpCS :
	collectionLiteralTypeIdentifierCS 
	LEFT_BRACE 
	(collectionLiteralPartsCS)? 
	RIGHT_BRACE
		;
**********************************************/
collectionLiteralExpCS returns [CSTCollectionLiteralExpCS literalExp]
{
	literalExp = null;
	List<object>  literalParts = new List<object>();
	CSTCollectionTypeIdentifierCS typeId = null;
}
:
	typeId = collectionLiteralTypeIdentifierCS 
	LEFT_BRACE 
	(literalParts = collectionLiteralPartsCS)? 
	RIGHT_BRACE
	{ literalExp = new CSTCollectionLiteralExpCS(typeId, literalParts); }
		;

	
/*********************************************
collectionLiteralPartsCS :
	collectionLiteralPartCS 
	(COMMA collectionLiteralPartCS)*
		;
**********************************************/
collectionLiteralPartsCS returns [List<object> literalParts] 
{
	literalParts = new List<object>();
	CSTCollectionLiteralPartCS literalPart = null;
}
:
	literalPart = collectionLiteralPartCS { literalParts.Add(literalPart); }
	(
		COMMA 
		literalPart = collectionLiteralPartCS { literalParts.Add(literalPart); }
	)*
		;

/*********************************************
collectionLiteralPartCS :
	impliesExpressionCS
	(impliesExpressionCS	RANGE)? 
		;

**********************************************/
collectionLiteralPartCS returns [CSTCollectionLiteralPartCS literalPart]
{
	literalPart = null;
	CSTOclExpressionCS expression1 = null;
	CSTOclExpressionCS expression2 = null;
}
:
	(expression1 = impliesExpressionCS | expression1 = letExpCS)
	(
		RANGE 
		(expression2 = impliesExpressionCS | expression2 = letExpCS)
	)? 

	{ 
		if (expression2 == null)
			literalPart = new CSTCollectionLiteralSinglePartCS(expression1); 
		else
			literalPart = new CSTCollectionLiteralRangeCS(expression1, expression2);
	}	
		;
		

/*********************************************
tupleLiteralExpCS :
	KEYW_TUPLE 
	LEFT_BRACE 
	variableDeclarationListCS 
	RIGHT_BRACE
		;
**********************************************/

tupleLiteralExpCS returns [CSTTupleLiteralExpCS tupleLiteralExp] 
{
	tupleLiteralExp = null;
	List<object> variableDeclarationList = new List<object>();
}
:
	token : KEYW_TUPLE 
	LEFT_BRACE 
	variableDeclarationList = tupleLiteralPartsCS
	RIGHT_BRACE
	{ tupleLiteralExp = new CSTTupleLiteralExpCS((OCLWorkbenchToken) token, variableDeclarationList); }
		;


/*********************************************
tupleLiteralPartsCS:
	defaultVariableDeclarationCS  
	(COMMA defaultVariableDeclarationCS)*
		;
**********************************************/
tupleLiteralPartsCS returns [List<object> variableDeclarationList] 
{
	variableDeclarationList = new List<object>();
	CSTVariableDeclarationCS declaration = null;
}
:
	declaration = defaultVariableDeclarationCS { variableDeclarationList.Add(declaration); }
	(
		COMMA 
		declaration = defaultVariableDeclarationCS { variableDeclarationList.Add(declaration); }
	)*
		;




		
		
//###########################################################################
//
//								TYPES
//
//###########################################################################
		
		
		
/*********************************************
typeCS 
:
	nameCS 	|
	collectionTypeCS |
	tupleTypeCS
		;
**********************************************/

typeCS returns [CSTTypeCS type] 
{	
	type = null;
	CSTNameCS name = null;
}
:
	name = nameCS { type = new CSTSimpleTypeCS(name); }
	|
	type = collectionTypeCS 
	|
	type = tupleTypeCS
		;

/*********************************************
collectionTypeCS :
	collectionTypeIdentifierCS 
	LEFT_PAR 
	typeCS 
	RIGHT_PAR
		;
**********************************************/
	
collectionTypeCS returns [CSTCollectionTypeCS collectionType] 
{
	collectionType = null;
	CSTCollectionTypeIdentifierCS typeId = null;
	CSTTypeCS type = null;
}
:
	typeId = collectionTypeIdentifierCS 
	LEFT_PAR 
	type = typeCS 
	RIGHT_PAR
	{ collectionType = new CSTCollectionTypeCS(typeId, type); }
		;



collectionLiteralTypeIdentifierCS returns [CSTCollectionTypeIdentifierCS typeIdentifier]
{
	typeIdentifier = null;
}
:
	(valueSet : KEYW_SET { typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueSet); } ) 
	| 
	(valueBag : KEYW_BAG { typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueBag); } ) 
	| 
	(valueSequence : KEYW_SEQUENCE { typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueSequence); } ) 
	|  
	(valueOrderedSet : KEYW_ORDEREDSET { typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueOrderedSet); } ) 
	;

	
/*********************************************
collectionTypeIdentifierCS :
	KEYW_SET | 
	KEYW_BAG | 
	KEYW_SEQUENCE | 
	KEYW_ORDEREDSET | 
	KEYW_COLLECTION;
**********************************************/

collectionTypeIdentifierCS returns [CSTCollectionTypeIdentifierCS typeIdentifier]
{
	typeIdentifier = null;
}
:
	(valueCollection : KEYW_COLLECTION { typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueCollection); } ) 
	|
	typeIdentifier = collectionLiteralTypeIdentifierCS
	;
	

/*********************************************
tupleTypeCS:
	KEYW_TUPLE 
	LEFT_PAR 
	(variableDeclarationListCS)? 
	RIGHT_PAR
		;
**********************************************/
	
tupleTypeCS returns [CSTTupleTypeCS tupleType]
{
	tupleType = null;
	List<object> tuplePartsList = new List<object>();
}
:
tupleKeyw : KEYW_TUPLE 
	LEFT_PAR 
	tuplePartsList = tuplePartsListCS 
	RIGHT_PAR
	{ tupleType = new CSTTupleTypeCS((OCLWorkbenchToken) tupleKeyw, tuplePartsList); }
		;
		
		
/*********************************************
tuplePartsCS :
	tuplePartCS
	(COMMA tuplePartCS)*
		;
**********************************************/
tuplePartsListCS returns [List<object> tuplePartsList] 
{
	tuplePartsList = new List<object>();
	CSTVariableDeclarationCS declaration = null;
}
:
	declaration = tuplePartCS { tuplePartsList.Add(declaration); }
	(
		COMMA 
		declaration = tuplePartCS { tuplePartsList.Add(declaration); }
	)*
		;

/*********************************************
**********************************************/
tuplePartCS returns [CSTVariableDeclarationCS variableDeclaration] 
{
	variableDeclaration = null;
	CSTNameCS name = null;
	CSTTypeCS type = null;
}
:
	name = simpleNameCS 
	COLON 
	type = typeCS
	{ variableDeclaration = new CSTVariableDeclarationCS(name, type, null); }
		;


		
		
		
		
		
		
		
		

//###########################################################################
//
//								NAMES
//
//###########################################################################



/*********************************************
nameCS  :
	(IDENT OP_DOUBLE_COLON IDENT) => pathNameCS 
	|
	simpleNameCS 
		;

**********************************************/
nameCS returns [CSTNameCS name] 
{
	name = null;
}
:
	(IDENT OP_DOUBLE_COLON IDENT) => name = pathNameCS 
	|
	name = simpleNameCS 
		;


/*********************************************
simpleNameCS : 
	IDENT
		;
**********************************************/
simpleNameCS returns [CSTSimpleNameCS simpleName]  
{
	simpleName = null;
}
:
	name : IDENT { simpleName = new CSTSimpleNameCS((OCLWorkbenchToken) name); }
		;


/*********************************************
pathNameCS : 
	IDENT 
	OP_DOUBLE_COLON 
	IDENT 
	(OP_DOUBLE_COLON 	IDENT)*
		;
**********************************************/
pathNameCS returns [CSTPathNameCS pathName] 
{
	pathName = null;
}
: 
	name1 : IDENT 
	OP_DOUBLE_COLON 
	name2 : IDENT 
		{ 	pathName = new CSTPathNameCS();
			pathName.addName((OCLWorkbenchToken) name1); 
			pathName.addName((OCLWorkbenchToken) name2); 
		}
	(
		OP_DOUBLE_COLON 
	    name : IDENT 	{ pathName.addName((OCLWorkbenchToken) name); } 
	)*
	;

class OCLScriptLexer extends Lexer;
options {
    exportVocab = OCLScript;
    defaultErrorHandler = true;	
    testLiterals = true;
	charVocabulary = '\3'..'\377';

    k = 4;  // lookahead number of chars
}

tokens {

// Keywords
	KEYW_AND 		= "and";
	KEYW_OR 		= "or";
	KEYW_NOT 		= "not";
	KEYW_XOR 		= "xor";
	KEYW_IMPLIES 	= "implies";

	KEYW_IF 		= "if";
	KEYW_THEN 		= "then";
	KEYW_ELSE 		= "else";
	KEYW_ENDIF 		= "endif";

	KEYW_PACKAGE	= "package";
	KEYW_ENDPACKAGE = "endpackage";

	KEYW_CONTEXT 	= "context";
	KEYW_ATTR 		= "attr";
	KEYW_OPER 		= "oper";
	KEYW_DEF 		= "def";
	KEYW_LET 		= "let";
	KEYW_IN 		= "in";
	KEYW_INV 		= "inv";
	KEYW_PRE 		= "pre";
	KEYW_POST 		= "post";
	KEYW_BODY 		= "body";
	KEYW_INIT 		= "init";
	KEYW_DERIVE 	= "derive";
	
// bool literals
	KEYW_TRUE		= "true";
	KEYW_FALSE 		= "false";
	
	KEYW_SET		= "Set";
	KEYW_BAG		= "Bag";
	KEYW_SEQUENCE	= "Sequence";
	KEYW_ORDEREDSET = "OrderedSet";
	KEYW_COLLECTION = "Collection";
	KEYW_TUPLE		= "Tuple";
	
	KEYW_ITERATE	= "iterate";
	
// iterator expressions
	KEYW_EXISTS		= "exists";
	KEYW_FORALL		= "forAll";
	KEYW_ISUNIQUE	= "isUnique";
	KEYW_ANY		= "any";
	KEYW_ONE		= "one";
	KEYW_COLLECT	= "collect";
	KEYW_SELECT		= "select";
	KEYW_REJECT		= "reject";
	KEYW_COLLECTNESTED = "collectNested";
	KEYW_SORTEDBY	= "sortedBy";
		
		KEYW_NULL = "null";
	KEYW_INVALID = "invalid";
	
		
	KEYW_ACTIONBODY = "actionBody";
	KEYW_UNDEFINED  = "undefined";
	KEYW_GOTO		= "goto";
	KEYW_RETURN		= "return";
	KEYW_DELETE		= "delete";
	KEYW_BEGIN		= "begin";
	KEYW_END		= "end";
	KEYW_DO			= "do";
	KEYW_DOIF		= "doif";
	KEYW_REPEAT		= "repeat";
	KEYW_WHILE		= "while";
	KEYW_UNTIL		= "until";
	KEYW_TO			= "to";
	KEYW_DOWNTO		= "downto";
		KEYW_FOR 	= "for";
	KEYW_FOREACH	= "foreach";
	KEYW_RAISE		= "raise";
	KEYW_CREATE		= "create";
	KEYW_VAR		= "var";
	KEYW_CONST		= "const";
	KEYW_BREAK		= "break";
	KEYW_CONTINUE   = "continue";
	KEYW_STEP = "step";
	
	KEYW_MODIFIABLE = "modifiable";
	KEYW_LINKS		= "links";
	
	}

// UML/OCL special operators
OP_DOT			options { paraphrase = "'.'";}			: ".";
OP_ARROW		options { paraphrase = "'->'";}			: "->";
OP_DOUBLE_COLON	options { paraphrase = "'::'";}			: "::";
OP_DBL_MESSAGE  options { paraphrase = "'^^'";}			: "^^";
OP_MESSAGE		options { paraphrase = "'^'";}			: "^";
OP_QUOTATION	options { paraphrase = "'?'";}			: "?";

// Arithmetic operators
OP_MULTIPLY		options { paraphrase = "'*'";}			: "*";
OP_DIVIDE 		options { paraphrase = "'/'";}			: "/";
OP_PLUS			options { paraphrase = "'+'";}			: "+";
OP_MINUS		options { paraphrase = "'-'";}			: "-";

// Logical operators
OP_EQUAL		options { paraphrase = "'='";}			: "=";
OP_NOTEQUAL     options { paraphrase = "'<>'";}			: "<>";
OP_LESS_THAN 	options { paraphrase = "'<'";}			: "<";
OP_GREATER_THAN options { paraphrase = "'>'";}			: ">";
OP_LESS_OR_EQ	options { paraphrase = "'<='";}			: "<=";
OP_GREATER_OR_EQ	options { paraphrase = "'>='";}		: ">=";

// begin-end characters
LEFT_PAR		options { paraphrase = "'('";}			: "(";
RIGHT_PAR		options { paraphrase = "')'";}			: ")";
LEFT_BRACKET	options { paraphrase = "'['";}			: "[";
RIGHT_BRACKET	options { paraphrase = "']'";}			: "]";
LEFT_BRACE		options { paraphrase = "'{'";}			: "{";
RIGHT_BRACE		options { paraphrase = "'}'";}			: "}";

// punctuation
COLON			options { paraphrase = "':'";}			: ":";
COMMA			options { paraphrase = "','";}			: ",";
CHANNEL			options { paraphrase = "'#'";}			: '#';
AT				options { paraphrase = "'@'";}			: "@";
VERT_BAR		options { paraphrase = "'|'";}			: "|";
RANGE			options { paraphrase = "'..'";}			: "..";
APOSTROPH		options { paraphrase = "'''";}			: "'";
SEMI_COLON		options { paraphrase = "';'";}			: ";";


// OCL Script
OP_ASSIGNMENT	options { paraphrase = "':='";}			: ":=";

// discarded chars
WHITE_SPACE : (NEW_LINE | NEW_PAGE | BLANK)
			{ $setType(Token.SKIP); }
			;
			
// single line 			
SINGLE_LINE_COMMENT : COMMENT_INIT REST_OF_LINE
			{ $setType(Token.SKIP); }
			;

// multiple-line comments
MULTI_LINE_COMMENT:
    "/*"
    ( options { generateAmbigWarnings = false; } : { LA(2)!='/' }? '*'
    | '\r' '\n' { newline(); }
    | '\r'	{ newline(); }
    | '\n'	{ newline(); }
    | ~('*'|'\n'|'\r')
    )*
    "*/"
    { $setType(Token.SKIP); }
    ;
    

// number literal
RANGE_OR_INT:
		(INT_NUMBER "..")			=> INT_NUMBER	{ $setType(INT_NUMBER); }
	| 	(INT_NUMBER '.' INT_NUMBER) => REAL_NUMBER	{ $setType(REAL_NUMBER); }
	|   (INT_NUMBER ('e'|'E')) 		=> REAL_NUMBER	{ $setType(REAL_NUMBER); }
	| 	INT_NUMBER									{ $setType(INT_NUMBER); }
	;

// string literal
STRING : ('"' (ESCAPE_SEQUENCE | ~('\\' | '"'))* '"')
			|	
		('\'' (ESCAPE_SEQUENCE | ~('\\' | '\''))* '\'')
		 ;

/*
SIMPLE_NAME_OR_PATH_NAME:
	(IDENT OP_DOUBLE_COLON IDENT) => PATH_NAME { $setType(PATH_NAME); }
	|
	IDENT					  				 { $setType(SIMPLE_NAME); }
	;

protected SIMPLE_NAME:
	IDENT;

protected PATH_NAME:
	IDENT OP_DOUBLE_COLON IDENT (OP_DOUBLE_COLON IDENT)*;
*/

// identifier
IDENT 
   options { 
   	testLiterals = true;
   	paraphrase = "an identifier";
   } :
   (LETTER | UNDERSCORE) (LETTER | DIGIT | UNDERSCORE)*;
   


// special characters
protected	LF : '\n';
protected	CR : '\r';
protected	CRLF : '\r' '\n';
protected	NEW_LINE : (LF | CR | CRLF) { newline(); };
protected	TAB : '\t';
protected	BLANK : (' ' | TAB);
protected	NEW_PAGE : '\f';
protected	UNDERSCORE : '_';

// digits and numbers
protected	DIGIT : ('0'..'9');
protected	NUMBER: (DIGIT)+;
protected	INT_NUMBER : NUMBER;
protected	REAL_NUMBER : INT_NUMBER ('.' INT_NUMBER)?
			  (('e'|'E') ('+'|'-')? INT_NUMBER)?
			   ;
			   
// octal and hexadecimal digits and numbers			   
protected	BETWEEN_ZERO_AND_THREE : ('0'..'3');
protected	BETWEEN_FOUR_AND_SEVEN : ('4'..'7');
protected	OCTAL_DIGIT : ('0'..'7');
protected	HEXA_DIGIT : (DIGIT | ('a'..'f') | ('A'..'F'));

protected	TWO_DIGIT_OCTAL_NUMBER : BETWEEN_ZERO_AND_THREE OCTAL_DIGIT OCTAL_DIGIT;
protected	THREE_DIGIT_OCTAL_NUMBER : BETWEEN_FOUR_AND_SEVEN OCTAL_DIGIT;
protected	HEXA_NUMBER : HEXA_DIGIT HEXA_DIGIT HEXA_DIGIT HEXA_DIGIT;

// escape sequences
protected	OCTAL_ESCAPE : '\\' (TWO_DIGIT_OCTAL_NUMBER | THREE_DIGIT_OCTAL_NUMBER);
protected	HEXA_ESCAPE : '\\' 'x' HEXA_NUMBER;			   
protected	SIMPLE_ESCAPE : '\\' ('a' | 'b' | 't' | 'r' | 'n' | 'f' | 'v' | '"' | '\'' | '\\');			  
protected	ESCAPE_SEQUENCE : SIMPLE_ESCAPE | OCTAL_ESCAPE | HEXA_ESCAPE;			   


// letters and characters
protected	UPPER_CHAR : ('A'..'Z');
protected	LOWER_CHAR : ('a'..'z');
protected	LETTER     : (UPPER_CHAR | LOWER_CHAR);
protected	ANY_CHAR : (~('\r' | '\n'));

protected	ANY_ELEMENT : (~('*'));


protected	COMMENT_INIT : ("//" | "--");
protected	REST_OF_LINE : (ANY_CHAR)* ;
//protected	REST_OF_LINE : (ANY_CHAR)* (NEW_LINE);

