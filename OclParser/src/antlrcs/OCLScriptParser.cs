// $ANTLR 2.7.7 (20060930): "OCLSCRIPT.G" -> "OCLScriptParser.cs"$

	using System.Collections.Generic;
	using OclParser.controller;
	using OclParser.cst.context;
	using OclParser.cst.expression;
	using OclParser.cst.literalExp;
	using OclParser.cst.name;
	using OclParser.cst.type;

namespace oclparser.antlrcs
{
	// Generate the header common to all output files.
	using System;
	
	using TokenBuffer              = antlr.TokenBuffer;
	using TokenStreamException     = antlr.TokenStreamException;
	using TokenStreamIOException   = antlr.TokenStreamIOException;
	using ANTLRException           = antlr.ANTLRException;
	using LLkParser = antlr.LLkParser;
	using Token                    = antlr.Token;
	using IToken                   = antlr.IToken;
	using TokenStream              = antlr.TokenStream;
	using RecognitionException     = antlr.RecognitionException;
	using NoViableAltException     = antlr.NoViableAltException;
	using MismatchedTokenException = antlr.MismatchedTokenException;
	using SemanticException        = antlr.SemanticException;
	using ParserSharedInputState   = antlr.ParserSharedInputState;
	using BitSet                   = antlr.collections.impl.BitSet;
	
	public 	class OCLScriptParser : antlr.LLkParser
	{
		public const int EOF = 1;
		public const int NULL_TREE_LOOKAHEAD = 3;
		public const int KEYW_PACKAGE = 4;
		public const int KEYW_ENDPACKAGE = 5;
		public const int KEYW_CONTEXT = 6;
		public const int COLON = 7;
		public const int KEYW_INIT = 8;
		public const int KEYW_DERIVE = 9;
		public const int KEYW_INV = 10;
		public const int KEYW_DEF = 11;
		public const int OP_EQUAL = 12;
		public const int KEYW_PRE = 13;
		public const int KEYW_POST = 14;
		public const int KEYW_BODY = 15;
		public const int LEFT_PAR = 16;
		public const int RIGHT_PAR = 17;
		public const int COMMA = 18;
		public const int KEYW_LET = 19;
		public const int KEYW_IN = 20;
		public const int OP_ARROW = 21;
		public const int KEYW_ITERATE = 22;
		public const int KEYW_IMPLIES = 23;
		public const int KEYW_AND = 24;
		public const int KEYW_OR = 25;
		public const int KEYW_XOR = 26;
		public const int OP_LESS_THAN = 27;
		public const int OP_GREATER_THAN = 28;
		public const int OP_LESS_OR_EQ = 29;
		public const int OP_GREATER_OR_EQ = 30;
		public const int OP_NOTEQUAL = 31;
		public const int OP_PLUS = 32;
		public const int OP_MINUS = 33;
		public const int OP_MULTIPLY = 34;
		public const int OP_DIVIDE = 35;
		public const int KEYW_NOT = 36;
		public const int KEYW_IF = 37;
		public const int KEYW_THEN = 38;
		public const int KEYW_ELSE = 39;
		public const int KEYW_ENDIF = 40;
		public const int OP_DOT = 41;
		public const int LEFT_BRACKET = 42;
		public const int RIGHT_BRACKET = 43;
		public const int AT = 44;
		public const int VERT_BAR = 45;
		public const int SEMI_COLON = 46;
		public const int KEYW_EXISTS = 47;
		public const int KEYW_FORALL = 48;
		public const int KEYW_ISUNIQUE = 49;
		public const int KEYW_ANY = 50;
		public const int KEYW_ONE = 51;
		public const int KEYW_COLLECT = 52;
		public const int KEYW_SELECT = 53;
		public const int KEYW_REJECT = 54;
		public const int KEYW_COLLECTNESTED = 55;
		public const int KEYW_SORTEDBY = 56;
		public const int INT_NUMBER = 57;
		public const int REAL_NUMBER = 58;
		public const int STRING = 59;
		public const int KEYW_TRUE = 60;
		public const int KEYW_FALSE = 61;
		public const int KEYW_NULL = 62;
		public const int KEYW_INVALID = 63;
		public const int LEFT_BRACE = 64;
		public const int RIGHT_BRACE = 65;
		public const int RANGE = 66;
		public const int KEYW_TUPLE = 67;
		public const int KEYW_SET = 68;
		public const int KEYW_BAG = 69;
		public const int KEYW_SEQUENCE = 70;
		public const int KEYW_ORDEREDSET = 71;
		public const int KEYW_COLLECTION = 72;
		public const int IDENT = 73;
		public const int OP_DOUBLE_COLON = 74;
		public const int KEYW_ATTR = 75;
		public const int KEYW_OPER = 76;
		public const int KEYW_ACTIONBODY = 77;
		public const int KEYW_UNDEFINED = 78;
		public const int KEYW_GOTO = 79;
		public const int KEYW_RETURN = 80;
		public const int KEYW_DELETE = 81;
		public const int KEYW_BEGIN = 82;
		public const int KEYW_END = 83;
		public const int KEYW_DO = 84;
		public const int KEYW_DOIF = 85;
		public const int KEYW_REPEAT = 86;
		public const int KEYW_WHILE = 87;
		public const int KEYW_UNTIL = 88;
		public const int KEYW_TO = 89;
		public const int KEYW_DOWNTO = 90;
		public const int KEYW_FOR = 91;
		public const int KEYW_FOREACH = 92;
		public const int KEYW_RAISE = 93;
		public const int KEYW_CREATE = 94;
		public const int KEYW_VAR = 95;
		public const int KEYW_CONST = 96;
		public const int KEYW_BREAK = 97;
		public const int KEYW_CONTINUE = 98;
		public const int KEYW_STEP = 99;
		public const int KEYW_MODIFIABLE = 100;
		public const int KEYW_LINKS = 101;
		public const int OP_DBL_MESSAGE = 102;
		public const int OP_MESSAGE = 103;
		public const int OP_QUOTATION = 104;
		public const int CHANNEL = 105;
		public const int APOSTROPH = 106;
		public const int OP_ASSIGNMENT = 107;
		public const int WHITE_SPACE = 108;
		public const int SINGLE_LINE_COMMENT = 109;
		public const int MULTI_LINE_COMMENT = 110;
		public const int RANGE_OR_INT = 111;
		public const int LF = 112;
		public const int CR = 113;
		public const int CRLF = 114;
		public const int NEW_LINE = 115;
		public const int TAB = 116;
		public const int BLANK = 117;
		public const int NEW_PAGE = 118;
		public const int UNDERSCORE = 119;
		public const int DIGIT = 120;
		public const int NUMBER = 121;
		public const int BETWEEN_ZERO_AND_THREE = 122;
		public const int BETWEEN_FOUR_AND_SEVEN = 123;
		public const int OCTAL_DIGIT = 124;
		public const int HEXA_DIGIT = 125;
		public const int TWO_DIGIT_OCTAL_NUMBER = 126;
		public const int THREE_DIGIT_OCTAL_NUMBER = 127;
		public const int HEXA_NUMBER = 128;
		public const int OCTAL_ESCAPE = 129;
		public const int HEXA_ESCAPE = 130;
		public const int SIMPLE_ESCAPE = 131;
		public const int ESCAPE_SEQUENCE = 132;
		public const int UPPER_CHAR = 133;
		public const int LOWER_CHAR = 134;
		public const int LETTER = 135;
		public const int ANY_CHAR = 136;
		public const int ANY_ELEMENT = 137;
		public const int COMMENT_INIT = 138;
		public const int REST_OF_LINE = 139;
		
		
		protected void initialize()
		{
			tokenNames = tokenNames_;
		}
		
		
		protected OCLScriptParser(TokenBuffer tokenBuf, int k) : base(tokenBuf, k)
		{
			initialize();
		}
		
		public OCLScriptParser(TokenBuffer tokenBuf) : this(tokenBuf,3)
		{
		}
		
		protected OCLScriptParser(TokenStream lexer, int k) : base(lexer,k)
		{
			initialize();
		}
		
		public OCLScriptParser(TokenStream lexer) : this(lexer,3)
		{
		}
		
		public OCLScriptParser(ParserSharedInputState state) : base(state,3)
		{
			initialize();
		}
		
	public List<object>  expressionStream() //throws RecognitionException, TokenStreamException
{
		List<object> declarations;
		
		
			declarations = new List<object>();
			CSTPackageDeclarationCS packageDecl = null;
			CSTContextDeclarationCS contextDecl = null;
		
		
		try {      // for error handling
			{    // ( ... )*
				for (;;)
				{
					switch ( LA(1) )
					{
					case KEYW_PACKAGE:
					{
						packageDecl=packageDeclarationCS();
						if (0==inputState.guessing)
						{
							declarations.Add(packageDecl);
						}
						break;
					}
					case KEYW_CONTEXT:
					{
						contextDecl=contextDeclarationCS();
						if (0==inputState.guessing)
						{
							declarations.Add(contextDecl);
						}
						break;
					}
					default:
					{
						goto _loop3_breakloop;
					}
					 }
				}
_loop3_breakloop:				;
			}    // ( ... )*
			match(Token.EOF_TYPE);
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_0_);
			}
			else
			{
				throw ex;
			}
		}
		return declarations;
	}
	
/*********************************************
packageDeclarationCS :
	KEYW_PACKAGE
		nameCS
		(contextDeclarationCS)*
	KEYW_ENDPACKAGE
		;
**********************************************/
	public CSTPackageDeclarationCS  packageDeclarationCS() //throws RecognitionException, TokenStreamException
{
		CSTPackageDeclarationCS decl;
		
		
			decl = null;
			CSTNameCS name = null;
			CSTContextDeclarationCS contextDecl = null;
		
		
		try {      // for error handling
			match(KEYW_PACKAGE);
			name=nameCS();
			if (0==inputState.guessing)
			{
				decl = new CSTPackageDeclarationCS(name);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==KEYW_CONTEXT))
					{
						contextDecl=contextDeclarationCS();
						if (0==inputState.guessing)
						{
							decl.addContextDeclaration(contextDecl);
						}
					}
					else
					{
						goto _loop6_breakloop;
					}
					
				}
_loop6_breakloop:				;
			}    // ( ... )*
			match(KEYW_ENDPACKAGE);
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_1_);
			}
			else
			{
				throw ex;
			}
		}
		return decl;
	}
	
/*********************************************
contextDeclarationCS :
	classifierContextDeclCS |
	attrOrAssocContextCS |
	operationContextDeclCS
		;
**********************************************/
	public CSTContextDeclarationCS  contextDeclarationCS() //throws RecognitionException, TokenStreamException
{
		CSTContextDeclarationCS decl;
		
		
		decl = null;
		
		
		try {      // for error handling
			bool synPredMatched9 = false;
			if (((LA(1)==KEYW_CONTEXT) && (LA(2)==IDENT) && (LA(3)==OP_DOUBLE_COLON)))
			{
				int _m9 = mark();
				synPredMatched9 = true;
				inputState.guessing++;
				try {
					{
						match(KEYW_CONTEXT);
						pathNameCS();
						match(COLON);
					}
				}
				catch (RecognitionException)
				{
					synPredMatched9 = false;
				}
				rewind(_m9);
				inputState.guessing--;
			}
			if ( synPredMatched9 )
			{
				decl=attrOrAssocContextCS();
			}
			else {
				bool synPredMatched11 = false;
				if (((LA(1)==KEYW_CONTEXT) && (LA(2)==IDENT) && (LA(3)==OP_DOUBLE_COLON)))
				{
					int _m11 = mark();
					synPredMatched11 = true;
					inputState.guessing++;
					try {
						{
							match(KEYW_CONTEXT);
							pathNameCS();
							match(KEYW_INIT);
						}
					}
					catch (RecognitionException)
					{
						synPredMatched11 = false;
					}
					rewind(_m11);
					inputState.guessing--;
				}
				if ( synPredMatched11 )
				{
					decl=attrOrAssocContextCS();
				}
				else {
					bool synPredMatched13 = false;
					if (((LA(1)==KEYW_CONTEXT) && (LA(2)==IDENT) && (LA(3)==OP_DOUBLE_COLON)))
					{
						int _m13 = mark();
						synPredMatched13 = true;
						inputState.guessing++;
						try {
							{
								match(KEYW_CONTEXT);
								pathNameCS();
								match(KEYW_DERIVE);
							}
						}
						catch (RecognitionException)
						{
							synPredMatched13 = false;
						}
						rewind(_m13);
						inputState.guessing--;
					}
					if ( synPredMatched13 )
					{
						decl=attrOrAssocContextCS();
					}
					else {
						bool synPredMatched15 = false;
						if (((LA(1)==KEYW_CONTEXT) && (LA(2)==IDENT) && (LA(3)==KEYW_INV||LA(3)==KEYW_DEF||LA(3)==OP_DOUBLE_COLON)))
						{
							int _m15 = mark();
							synPredMatched15 = true;
							inputState.guessing++;
							try {
								{
									match(KEYW_CONTEXT);
									nameCS();
									match(KEYW_INV);
								}
							}
							catch (RecognitionException)
							{
								synPredMatched15 = false;
							}
							rewind(_m15);
							inputState.guessing--;
						}
						if ( synPredMatched15 )
						{
							decl=classifierContextDeclCS();
						}
						else {
							bool synPredMatched17 = false;
							if (((LA(1)==KEYW_CONTEXT) && (LA(2)==IDENT) && (LA(3)==KEYW_INV||LA(3)==KEYW_DEF||LA(3)==OP_DOUBLE_COLON)))
							{
								int _m17 = mark();
								synPredMatched17 = true;
								inputState.guessing++;
								try {
									{
										match(KEYW_CONTEXT);
										nameCS();
										match(KEYW_DEF);
									}
								}
								catch (RecognitionException)
								{
									synPredMatched17 = false;
								}
								rewind(_m17);
								inputState.guessing--;
							}
							if ( synPredMatched17 )
							{
								decl=classifierContextDeclCS();
							}
							else if ((LA(1)==KEYW_CONTEXT) && (LA(2)==IDENT) && (LA(3)==LEFT_PAR||LA(3)==OP_DOUBLE_COLON)) {
								decl=operationContextDeclCS();
							}
							else
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							}}}}
						}
						catch (RecognitionException ex)
						{
							if (0 == inputState.guessing)
							{
								reportError(ex);
								recover(ex,tokenSet_2_);
							}
							else
							{
								throw ex;
							}
						}
						return decl;
					}
					
/*********************************************
nameCS  :
	(IDENT OP_DOUBLE_COLON IDENT) => pathNameCS 
	|
	simpleNameCS 
		;

**********************************************/
	public CSTNameCS  nameCS() //throws RecognitionException, TokenStreamException
{
		CSTNameCS name;
		
		
			name = null;
		
		
		try {      // for error handling
			bool synPredMatched233 = false;
			if (((LA(1)==IDENT) && (LA(2)==OP_DOUBLE_COLON)))
			{
				int _m233 = mark();
				synPredMatched233 = true;
				inputState.guessing++;
				try {
					{
						match(IDENT);
						match(OP_DOUBLE_COLON);
						match(IDENT);
					}
				}
				catch (RecognitionException)
				{
					synPredMatched233 = false;
				}
				rewind(_m233);
				inputState.guessing--;
			}
			if ( synPredMatched233 )
			{
				name=pathNameCS();
			}
			else if ((LA(1)==IDENT) && (tokenSet_3_.member(LA(2)))) {
				name=simpleNameCS();
			}
			else
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_3_);
			}
			else
			{
				throw ex;
			}
		}
		return name;
	}
	
/*********************************************
pathNameCS : 
	IDENT 
	OP_DOUBLE_COLON 
	IDENT 
	(OP_DOUBLE_COLON 	IDENT)*
		;
**********************************************/
	public CSTPathNameCS  pathNameCS() //throws RecognitionException, TokenStreamException
{
		CSTPathNameCS pathName;
		
		IToken  name1 = null;
		IToken  name2 = null;
		IToken  name = null;
		
			pathName = null;
		
		
		try {      // for error handling
			name1 = LT(1);
			match(IDENT);
			match(OP_DOUBLE_COLON);
			name2 = LT(1);
			match(IDENT);
			if (0==inputState.guessing)
			{
					pathName = new CSTPathNameCS();
							pathName.addName((OCLWorkbenchToken) name1); 
							pathName.addName((OCLWorkbenchToken) name2); 
						
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==OP_DOUBLE_COLON))
					{
						match(OP_DOUBLE_COLON);
						name = LT(1);
						match(IDENT);
						if (0==inputState.guessing)
						{
							pathName.addName((OCLWorkbenchToken) name);
						}
					}
					else
					{
						goto _loop237_breakloop;
					}
					
				}
_loop237_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_4_);
			}
			else
			{
				throw ex;
			}
		}
		return pathName;
	}
	
/*********************************************
attrOrAssocContextCS  :
	KEYW_CONTEXT
	pathNameCS
	(COLON typeCS)?
	initOrDerivedValueCS
		;
**********************************************/
	public CSTAttrOrAssocContextCS  attrOrAssocContextCS() //throws RecognitionException, TokenStreamException
{
		CSTAttrOrAssocContextCS decl;
		
		
			decl = null;
			CSTPathNameCS pathName = null;
			CSTTypeCS type = null;
			CSTInitDerivedValueCS value = null;
		
		
		try {      // for error handling
			match(KEYW_CONTEXT);
			pathName=pathNameCS();
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					match(COLON);
					type=typeCS();
					break;
				}
				case KEYW_INIT:
				case KEYW_DERIVE:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			value=initOrDerivedValueCS();
			if (0==inputState.guessing)
			{
				decl = new CSTAttrOrAssocContextCS(pathName, type, value);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return decl;
	}
	
/*********************************************
classifierContextDeclCS 
	KEYW_CONTEXT
	nameCS
	invOrDefCS
		;
**********************************************/
	public CSTClassifierContextDeclCS  classifierContextDeclCS() //throws RecognitionException, TokenStreamException
{
		CSTClassifierContextDeclCS decl;
		
		
			decl = null;
			CSTNameCS name = null;
			List<object> constraintsList = null;
		
		
		try {      // for error handling
			match(KEYW_CONTEXT);
			name=nameCS();
			if (0==inputState.guessing)
			{
				decl = new CSTClassifierContextDeclCS(name);
			}
			constraintsList=invOrDefCS();
			if (0==inputState.guessing)
			{
				decl.addConstraints(constraintsList);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return decl;
	}
	
/*********************************************
operationContextDeclCS:
	KEYW_CONTEXT
	operationCS
	prePostOrBodyDeclCS
		;
**********************************************/
	public CSTOperationContextCS  operationContextDeclCS() //throws RecognitionException, TokenStreamException
{
		CSTOperationContextCS operationDecl;
		
		
			operationDecl = null;
			CSTOperationCS operation = null;
			List<object> decl = null;
		
		
		try {      // for error handling
			match(KEYW_CONTEXT);
			operation=operationCS();
			decl=prePostOrBodyDeclCS();
			if (0==inputState.guessing)
			{
				operationDecl = new CSTOperationContextCS(operation, decl);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return operationDecl;
	}
	
/*********************************************
typeCS 
:
	nameCS 	|
	collectionTypeCS |
	tupleTypeCS
		;
**********************************************/
	public CSTTypeCS  typeCS() //throws RecognitionException, TokenStreamException
{
		CSTTypeCS type;
		
			
			type = null;
			CSTNameCS name = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case IDENT:
			{
				name=nameCS();
				if (0==inputState.guessing)
				{
					type = new CSTSimpleTypeCS(name);
				}
				break;
			}
			case KEYW_SET:
			case KEYW_BAG:
			case KEYW_SEQUENCE:
			case KEYW_ORDEREDSET:
			case KEYW_COLLECTION:
			{
				type=collectionTypeCS();
				break;
			}
			case KEYW_TUPLE:
			{
				type=tupleTypeCS();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_5_);
			}
			else
			{
				throw ex;
			}
		}
		return type;
	}
	
/*********************************************
initOrDerivedValueCS :
	initValueCS 
	|
	derValueCS 
	;
**********************************************/
	public CSTInitDerivedValueCS  initOrDerivedValueCS() //throws RecognitionException, TokenStreamException
{
		CSTInitDerivedValueCS value;
		
		
			value = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case KEYW_INIT:
			{
				{
					value=initValueCS();
				}
				break;
			}
			case KEYW_DERIVE:
			{
				{
					value=derValueCS();
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return value;
	}
	
/*********************************************
initValueCS:
	KEYW_INIT 
	COLON
	expressionInOCLCS 
		;
**********************************************/
	public CSTInitValueCS  initValueCS() //throws RecognitionException, TokenStreamException
{
		CSTInitValueCS value;
		
		IToken  token = null;
		
			value = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_INIT);
			match(COLON);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				value = new CSTInitValueCS((OCLWorkbenchToken) token, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return value;
	}
	
/*********************************************
derValueCS:
	KEYW_DERIVE
	COLON
	expressionInOCLCS
		;
**********************************************/
	public CSTDerivedValueCS  derValueCS() //throws RecognitionException, TokenStreamException
{
		CSTDerivedValueCS value;
		
		IToken  token = null;
		
			value = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_DERIVE);
			match(COLON);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				value = new CSTDerivedValueCS((OCLWorkbenchToken) token, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return value;
	}
	
	public CSTExpressionInOclCS  expressionInOCLCS() //throws RecognitionException, TokenStreamException
{
		CSTExpressionInOclCS expression;
		
		
			expression = null;
			CSTOclExpressionCS oclExpression = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case KEYW_LET:
				{
					oclExpression=letExpCS();
					break;
				}
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					oclExpression=impliesExpressionCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				expression = new CSTExpressionInOclCS(oclExpression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_6_);
			}
			else
			{
				throw ex;
			}
		}
		return expression;
	}
	
/*********************************************
invOrDefCS :
	(invCS | defCS)+
		;
**********************************************/
	public List<object>  invOrDefCS() //throws RecognitionException, TokenStreamException
{
		List<object> constraints;
		
		
			constraints = new List<object>();
			CSTConstraintDefinitionCS constraint = null;
		
		
		try {      // for error handling
			{ // ( ... )+
				int _cnt28=0;
				for (;;)
				{
					switch ( LA(1) )
					{
					case KEYW_INV:
					{
						constraint=invCS();
						if (0==inputState.guessing)
						{
							constraints.Add(constraint);
						}
						break;
					}
					case KEYW_DEF:
					{
						constraint=defCS();
						if (0==inputState.guessing)
						{
							constraints.Add(constraint);
						}
						break;
					}
					default:
					{
						if (_cnt28 >= 1) { goto _loop28_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					break; }
					_cnt28++;
				}
_loop28_breakloop:				;
			}    // ( ... )+
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return constraints;
	}
	
/*********************************************
invCS  :
	KEYW_INV 
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
	public CSTInvariantCS  invCS() //throws RecognitionException, TokenStreamException
{
		CSTInvariantCS invariant;
		
		IToken  token = null;
		
			invariant = null;
			CSTSimpleNameCS	invariantName = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_INV);
			{
				switch ( LA(1) )
				{
				case IDENT:
				{
					invariantName=simpleNameCS();
					break;
				}
				case COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(COLON);
			if (0==inputState.guessing)
			{
				invariant = new CSTInvariantCS((OCLWorkbenchToken) token, invariantName);
			}
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				invariant.setExpressionNodeCS(expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_7_);
			}
			else
			{
				throw ex;
			}
		}
		return invariant;
	}
	
/*********************************************
defCS:
	KEYW_DEF
	(simpleNameCS)?
	COLON
	defExpressionCS
		;
**********************************************/
	public CSTDefCS  defCS() //throws RecognitionException, TokenStreamException
{
		CSTDefCS defDeclaration;
		
		IToken  token = null;
		
			defDeclaration = null;
			CSTDefExpressionCS expression = null;
			CSTNameCS name = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_DEF);
			{
				switch ( LA(1) )
				{
				case IDENT:
				{
					name=simpleNameCS();
					break;
				}
				case COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(COLON);
			expression=defExpressionCS();
			if (0==inputState.guessing)
			{
				defDeclaration = new CSTDefCS((OCLWorkbenchToken) token, name, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_7_);
			}
			else
			{
				throw ex;
			}
		}
		return defDeclaration;
	}
	
/*********************************************
simpleNameCS : 
	IDENT
		;
**********************************************/
	public CSTSimpleNameCS  simpleNameCS() //throws RecognitionException, TokenStreamException
{
		CSTSimpleNameCS simpleName;
		
		IToken  name = null;
		
			simpleName = null;
		
		
		try {      // for error handling
			name = LT(1);
			match(IDENT);
			if (0==inputState.guessing)
			{
				simpleName = new CSTSimpleNameCS((OCLWorkbenchToken) name);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_8_);
			}
			else
			{
				throw ex;
			}
		}
		return simpleName;
	}
	
/*********************************************
defExpressionCS :
	defVarExpressionCS |
	defOperationExpressionCS
		;	
**********************************************/
	public CSTDefExpressionCS  defExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTDefExpressionCS defExpression;
		
		
			defExpression = null;
		
		
		try {      // for error handling
			if ((LA(1)==IDENT) && (LA(2)==COLON))
			{
				defExpression=defVarExpressionCS();
			}
			else if ((LA(1)==IDENT) && (LA(2)==LEFT_PAR||LA(2)==OP_DOUBLE_COLON)) {
				defExpression=defOperationExpressionCS();
			}
			else
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_7_);
			}
			else
			{
				throw ex;
			}
		}
		return defExpression;
	}
	
/*********************************************
defVarExpressionCS:
	simpleNameCS 
	(COLON typeCS)? 
	OP_EQUAL
	expressionInOCLCS
		;
**********************************************/
	public CSTDefVarExpressionCS  defVarExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTDefVarExpressionCS defVarExpression;
		
		
			defVarExpression = null;
			CSTNameCS name = null;
			CSTTypeCS type = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			match(COLON);
			type=typeCS();
			match(OP_EQUAL);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				defVarExpression = new CSTDefVarExpressionCS(name, type, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_6_);
			}
			else
			{
				throw ex;
			}
		}
		return defVarExpression;
	}
	
/*********************************************
defOperationExpressionCS :
	operationCS
	OP_EQUAL
	expressionInOCLCS 
		;
**********************************************/
	public CSTDefOperationExpressionCS  defOperationExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTDefOperationExpressionCS defOperationExpression;
		
		
			defOperationExpression = null;
			CSTOperationCS operation = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			operation=operationCS();
			match(OP_EQUAL);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				defOperationExpression = new CSTDefOperationExpressionCS(operation, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_7_);
			}
			else
			{
				throw ex;
			}
		}
		return defOperationExpression;
	}
	
/*********************************************
operationCS:
	nameCS
	LEFT_PAR
	(parametersCS)?
	RIGHT_PAR
	(COLON typeCS)?
		;
**********************************************/
	public CSTOperationCS  operationCS() //throws RecognitionException, TokenStreamException
{
		CSTOperationCS operation;
		
		
			operation = null;
			CSTNameCS name = null;
			List<object> parameters = new List<object>();
			CSTTypeCS type = null;
		
		
		try {      // for error handling
			name=nameCS();
			match(LEFT_PAR);
			{
				switch ( LA(1) )
				{
				case IDENT:
				{
					parameters=parametersCS();
					break;
				}
				case RIGHT_PAR:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(RIGHT_PAR);
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					match(COLON);
					type=typeCS();
					break;
				}
				case KEYW_DEF:
				case OP_EQUAL:
				case KEYW_PRE:
				case KEYW_POST:
				case KEYW_BODY:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				operation = new CSTOperationCS(name, parameters, type);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_9_);
			}
			else
			{
				throw ex;
			}
		}
		return operation;
	}
	
/*********************************************
prePostOrBodyDeclCS:
	(preDeclCS | postDeclCS | bodyDeclCS)+
		;
**********************************************/
	public List<object>  prePostOrBodyDeclCS() //throws RecognitionException, TokenStreamException
{
		List<object> declarations;
		
		IToken  token = null;
		
			declarations = new List<object>();
			CSTDefVarExpressionCS defVar = null;
			CSTOperationConstraintCS decl = null;
		
		
		try {      // for error handling
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==KEYW_DEF))
					{
						token = LT(1);
						match(KEYW_DEF);
						match(COLON);
						defVar=defVarExpressionCS();
						if (0==inputState.guessing)
						{
							declarations.Add(defVar);
						}
					}
					else
					{
						goto _loop39_breakloop;
					}
					
				}
_loop39_breakloop:				;
			}    // ( ... )*
			{ // ( ... )+
				int _cnt41=0;
				for (;;)
				{
					switch ( LA(1) )
					{
					case KEYW_PRE:
					{
						decl=preDeclCS();
						if (0==inputState.guessing)
						{
							declarations.Add(decl);
						}
						break;
					}
					case KEYW_POST:
					{
						decl=postDeclCS();
						if (0==inputState.guessing)
						{
							declarations.Add(decl);
						}
						break;
					}
					case KEYW_BODY:
					{
						decl=bodyDeclCS();
						if (0==inputState.guessing)
						{
							declarations.Add(decl);
						}
						break;
					}
					default:
					{
						if (_cnt41 >= 1) { goto _loop41_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					break; }
					_cnt41++;
				}
_loop41_breakloop:				;
			}    // ( ... )+
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_2_);
			}
			else
			{
				throw ex;
			}
		}
		return declarations;
	}
	
/*********************************************
preDeclCS:
	KEYW_PRE
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
	public CSTPreDeclCS  preDeclCS() //throws RecognitionException, TokenStreamException
{
		CSTPreDeclCS decl;
		
		IToken  token = null;
		
			decl = null;
			CSTNameCS name = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_PRE);
			{
				switch ( LA(1) )
				{
				case IDENT:
				{
					name=simpleNameCS();
					break;
				}
				case COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(COLON);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				decl = new CSTPreDeclCS((OCLWorkbenchToken) token, name, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_10_);
			}
			else
			{
				throw ex;
			}
		}
		return decl;
	}
	
/*********************************************
postDeclCS:
	KEYW_POST
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
	public CSTPostDeclCS  postDeclCS() //throws RecognitionException, TokenStreamException
{
		CSTPostDeclCS decl;
		
		IToken  token = null;
		
			decl = null;
			CSTNameCS name = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_POST);
			{
				switch ( LA(1) )
				{
				case IDENT:
				{
					name=simpleNameCS();
					break;
				}
				case COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(COLON);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				decl = new CSTPostDeclCS((OCLWorkbenchToken) token, name, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_10_);
			}
			else
			{
				throw ex;
			}
		}
		return decl;
	}
	
/*********************************************
bodyDeclCS:
	KEYW_BODY
	(simpleNameCS)?
	COLON
	expressionInOCLCS
		;
**********************************************/
	public CSTBodyDeclCS  bodyDeclCS() //throws RecognitionException, TokenStreamException
{
		CSTBodyDeclCS decl;
		
		IToken  token = null;
		
			decl = null;
			CSTNameCS name = null;
			CSTExpressionInOclCS expression = null;
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_BODY);
			{
				switch ( LA(1) )
				{
				case IDENT:
				{
					name=simpleNameCS();
					break;
				}
				case COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(COLON);
			expression=expressionInOCLCS();
			if (0==inputState.guessing)
			{
				decl = new CSTBodyDeclCS((OCLWorkbenchToken) token, name, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_10_);
			}
			else
			{
				throw ex;
			}
		}
		return decl;
	}
	
/*********************************************
parametersCS:
	onlyTypeDefinedVariableDeclarationCS 
	(COMMA onlyTypeDefinedVariableDeclarationCS)*
		;
**********************************************/
	public List<object>  parametersCS() //throws RecognitionException, TokenStreamException
{
		List<object> parameters;
		
		
			parameters = new List<object>();
			CSTVariableDeclarationCS variableDeclaration = null;
		
		
		try {      // for error handling
			variableDeclaration=onlyTypeDefinedVariableDeclarationCS();
			if (0==inputState.guessing)
			{
				parameters.Add(variableDeclaration);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						variableDeclaration=onlyTypeDefinedVariableDeclarationCS();
						if (0==inputState.guessing)
						{
							parameters.Add(variableDeclaration);
						}
					}
					else
					{
						goto _loop53_breakloop;
					}
					
				}
_loop53_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_11_);
			}
			else
			{
				throw ex;
			}
		}
		return parameters;
	}
	
/*********************************************
**********************************************/
	public CSTVariableDeclarationCS  onlyTypeDefinedVariableDeclarationCS() //throws RecognitionException, TokenStreamException
{
		CSTVariableDeclarationCS variableDeclaration;
		
		
			variableDeclaration = null;
			CSTNameCS name = null;
			CSTTypeCS type = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					match(COLON);
					type=typeCS();
					break;
				}
				case RIGHT_PAR:
				case COMMA:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				variableDeclaration = new CSTVariableDeclarationCS(name, type, null);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_12_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclaration;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  letExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTVariableDeclarationCS varDeclaration = null;
			CSTOclExpressionCS	oclExpression = null;
			List<object>	varDeclarations = new List<object>();
		
		
		try {      // for error handling
			match(KEYW_LET);
			varDeclaration=initializedVariableDeclarationCS();
			if (0==inputState.guessing)
			{
				varDeclarations.Add(varDeclaration);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						varDeclaration=initializedVariableDeclarationCS();
						if (0==inputState.guessing)
						{
							varDeclarations.Add(varDeclaration);
						}
					}
					else
					{
						goto _loop58_breakloop;
					}
					
				}
_loop58_breakloop:				;
			}    // ( ... )*
			match(KEYW_IN);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					oclExpression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					oclExpression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				resultExpression = new CSTLetExpCS(varDeclarations, oclExpression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_13_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  impliesExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS operat = null;
			CSTOclExpressionCS	rightExpression = null;
		
		
		try {      // for error handling
			resultExpression=logicalExpressionCS();
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==KEYW_IMPLIES))
					{
						operat=impliesOperatorCS();
						rightExpression=logicalExpressionCS();
						if (0==inputState.guessing)
						{
							resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression);
						}
					}
					else
					{
						goto _loop62_breakloop;
					}
					
				}
_loop62_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_13_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTVariableDeclarationCS  initializedVariableDeclarationCS() //throws RecognitionException, TokenStreamException
{
		CSTVariableDeclarationCS variableDeclaration;
		
		
			variableDeclaration = null;
			CSTNameCS name = null;
			CSTTypeCS type = null;
			CSTOclExpressionCS	expression = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			match(COLON);
			type=typeCS();
			match(OP_EQUAL);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					expression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					expression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				variableDeclaration = new CSTVariableDeclarationCS(name, type, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_14_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclaration;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  logicalExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS operat = null;
			CSTOclExpressionCS	rightExpression = null;
		
		
		try {      // for error handling
			resultExpression=equalityExpressionCS();
			{    // ( ... )*
				for (;;)
				{
					if (((LA(1) >= KEYW_AND && LA(1) <= KEYW_XOR)))
					{
						operat=logicalOperatorCS();
						rightExpression=equalityExpressionCS();
						if (0==inputState.guessing)
						{
							resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression);
						}
					}
					else
					{
						goto _loop65_breakloop;
					}
					
				}
_loop65_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_15_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTOperatorCS  impliesOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opImplies = null;
		
			operat = null;
		
		
		try {      // for error handling
			opImplies = LT(1);
			match(KEYW_IMPLIES);
			if (0==inputState.guessing)
			{
				operat = new CSTOperatorCS((OCLWorkbenchToken) opImplies);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_16_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  equalityExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS operat = null;
			CSTOclExpressionCS	rightExpression = null;
		
		
		try {      // for error handling
			resultExpression=relationalExpressionCS();
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==OP_EQUAL||LA(1)==OP_NOTEQUAL))
					{
						operat=equalityOperatorCS();
						rightExpression=relationalExpressionCS();
						if (0==inputState.guessing)
						{
							resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression);
						}
					}
					else
					{
						goto _loop68_breakloop;
					}
					
				}
_loop68_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_17_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTOperatorCS  logicalOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opAnd = null;
		IToken  opOr = null;
		IToken  opXor = null;
		
			operat = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case KEYW_AND:
				{
					opAnd = LT(1);
					match(KEYW_AND);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opAnd);
					}
					break;
				}
				case KEYW_OR:
				{
					opOr = LT(1);
					match(KEYW_OR);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opOr);
					}
					break;
				}
				case KEYW_XOR:
				{
					opXor = LT(1);
					match(KEYW_XOR);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opXor);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_16_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  relationalExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS operat = null;
			CSTOclExpressionCS	rightExpression = null;
		
		
		try {      // for error handling
			resultExpression=additiveExpressionCS();
			{    // ( ... )*
				for (;;)
				{
					if (((LA(1) >= OP_LESS_THAN && LA(1) <= OP_GREATER_OR_EQ)))
					{
						operat=relationalOperatorCS();
						rightExpression=additiveExpressionCS();
						if (0==inputState.guessing)
						{
							resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression);
						}
					}
					else
					{
						goto _loop71_breakloop;
					}
					
				}
_loop71_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_18_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
	public CSTOperatorCS  equalityOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opEqual = null;
		IToken  opNotEqual = null;
		
			operat = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case OP_EQUAL:
				{
					opEqual = LT(1);
					match(OP_EQUAL);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opEqual);
					}
					break;
				}
				case OP_NOTEQUAL:
				{
					opNotEqual = LT(1);
					match(OP_NOTEQUAL);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opNotEqual);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_16_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  additiveExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS operat = null;
			CSTOclExpressionCS	rightExpression = null;
		
		
		try {      // for error handling
			resultExpression=multiplicativeExpressionCS();
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==OP_PLUS||LA(1)==OP_MINUS))
					{
						operat=addOperatorCS();
						rightExpression=multiplicativeExpressionCS();
						if (0==inputState.guessing)
						{
							resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression);
						}
					}
					else
					{
						goto _loop74_breakloop;
					}
					
				}
_loop74_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_19_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTOperatorCS  relationalOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opLessThan = null;
		IToken  opGreaterThan = null;
		IToken  opLessOrEqual = null;
		IToken  opGreaterOrEqual = null;
		
			operat = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case OP_LESS_THAN:
				{
					opLessThan = LT(1);
					match(OP_LESS_THAN);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opLessThan);
					}
					break;
				}
				case OP_GREATER_THAN:
				{
					opGreaterThan = LT(1);
					match(OP_GREATER_THAN);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opGreaterThan);
					}
					break;
				}
				case OP_LESS_OR_EQ:
				{
					opLessOrEqual = LT(1);
					match(OP_LESS_OR_EQ);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opLessOrEqual);
					}
					break;
				}
				case OP_GREATER_OR_EQ:
				{
					opGreaterOrEqual = LT(1);
					match(OP_GREATER_OR_EQ);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opGreaterOrEqual);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_16_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  multiplicativeExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS operat = null;
			CSTOclExpressionCS	rightExpression = null;
		
		
		try {      // for error handling
			resultExpression=unaryExpressionCS();
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==OP_MULTIPLY||LA(1)==OP_DIVIDE))
					{
						operat=multiplyOperatorCS();
						rightExpression=unaryExpressionCS();
						if (0==inputState.guessing)
						{
							resultExpression = new CSTBinaryExpressionCS(resultExpression, operat, rightExpression);
						}
					}
					else
					{
						goto _loop77_breakloop;
					}
					
				}
_loop77_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_20_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTOperatorCS  addOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opPlus = null;
		IToken  opMinus = null;
		
			operat = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case OP_PLUS:
				{
					opPlus = LT(1);
					match(OP_PLUS);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opPlus);
					}
					break;
				}
				case OP_MINUS:
				{
					opMinus = LT(1);
					match(OP_MINUS);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opMinus);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_16_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
**********************************************/
	public CSTOclExpressionCS  unaryExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		
			resultExpression = null;
			CSTOperatorCS unaryOperator = null;
			CSTOclExpressionCS expression = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case OP_MINUS:
			case KEYW_NOT:
			{
				{
					unaryOperator=unaryOperatorCS();
					{
						switch ( LA(1) )
						{
						case KEYW_IF:
						case INT_NUMBER:
						case REAL_NUMBER:
						case STRING:
						case KEYW_TRUE:
						case KEYW_FALSE:
						case KEYW_NULL:
						case KEYW_INVALID:
						case KEYW_TUPLE:
						case KEYW_SET:
						case KEYW_BAG:
						case KEYW_SEQUENCE:
						case KEYW_ORDEREDSET:
						case IDENT:
						{
							expression=oclExpressionCS();
							break;
						}
						case LEFT_PAR:
						{
							expression=parenthesisExpressionCS();
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
				}
				if (0==inputState.guessing)
				{
					resultExpression = new CSTUnaryExpressionCS(unaryOperator, expression);
				}
				break;
			}
			case KEYW_IF:
			case INT_NUMBER:
			case REAL_NUMBER:
			case STRING:
			case KEYW_TRUE:
			case KEYW_FALSE:
			case KEYW_NULL:
			case KEYW_INVALID:
			case KEYW_TUPLE:
			case KEYW_SET:
			case KEYW_BAG:
			case KEYW_SEQUENCE:
			case KEYW_ORDEREDSET:
			case IDENT:
			{
				{
					resultExpression=oclExpressionCS();
				}
				break;
			}
			case LEFT_PAR:
			{
				{
					resultExpression=parenthesisExpressionCS();
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_21_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
/*********************************************
**********************************************/
	public CSTOperatorCS  multiplyOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opMultiply = null;
		IToken  opDivide = null;
		
			operat = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case OP_MULTIPLY:
				{
					opMultiply = LT(1);
					match(OP_MULTIPLY);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opMultiply);
					}
					break;
				}
				case OP_DIVIDE:
				{
					opDivide = LT(1);
					match(OP_DIVIDE);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opDivide);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_16_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
**********************************************/
	public CSTOperatorCS  unaryOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTOperatorCS operat;
		
		IToken  opMinus = null;
		IToken  opNot = null;
		
			operat = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case OP_MINUS:
				{
					opMinus = LT(1);
					match(OP_MINUS);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opMinus);
					}
					break;
				}
				case KEYW_NOT:
				{
					opNot = LT(1);
					match(KEYW_NOT);
					if (0==inputState.guessing)
					{
						operat = new CSTOperatorCS((OCLWorkbenchToken) opNot);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_22_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
	public CSTOclExpressionCS  oclExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS expression;
		
		
			expression = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case KEYW_IF:
			{
				expression=ifExpCS();
				break;
			}
			case INT_NUMBER:
			case REAL_NUMBER:
			case STRING:
			case KEYW_TRUE:
			case KEYW_FALSE:
			case KEYW_NULL:
			case KEYW_INVALID:
			case KEYW_TUPLE:
			case KEYW_SET:
			case KEYW_BAG:
			case KEYW_SEQUENCE:
			case KEYW_ORDEREDSET:
			case IDENT:
			{
				expression=navigationExpCS();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_21_);
			}
			else
			{
				throw ex;
			}
		}
		return expression;
	}
	
	public CSTOclExpressionCS  parenthesisExpressionCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		IToken  opArrow1 = null;
		IToken  opArrow2 = null;
		
			resultExpression = null;
			CSTOperatorCS unaryOperator = null;
			CSTOclExpressionCS expression = null;
			
			CSTOclExpressionCS callExp = null;
			CSTOclExpressionCS innerNavigation = null;
			CSTNavigationExpressionCS navigationExpression = null;
			CSTNavigationOperatorCS operat = null;
			
		
		
		try {      // for error handling
			match(LEFT_PAR);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					resultExpression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					resultExpression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(RIGHT_PAR);
			{    // ( ... )*
				for (;;)
				{
					bool synPredMatched87 = false;
					if (((LA(1)==OP_ARROW) && (LA(2)==KEYW_ITERATE)))
					{
						int _m87 = mark();
						synPredMatched87 = true;
						inputState.guessing++;
						try {
							{
								match(OP_ARROW);
								match(KEYW_ITERATE);
								match(LEFT_PAR);
							}
						}
						catch (RecognitionException)
						{
							synPredMatched87 = false;
						}
						rewind(_m87);
						inputState.guessing--;
					}
					if ( synPredMatched87 )
					{
						opArrow1 = LT(1);
						match(OP_ARROW);
						innerNavigation=iterateExpCS();
						if (0==inputState.guessing)
						{
								if (navigationExpression == null) {
									    		navigationExpression = new CSTNavigationExpressionCS(resultExpression);
									    		resultExpression = navigationExpression;
								    	}
								    	navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow1), innerNavigation); 
									
						}
					}
					else {
						bool synPredMatched89 = false;
						if (((LA(1)==OP_ARROW) && ((LA(2) >= KEYW_EXISTS && LA(2) <= KEYW_SORTEDBY))))
						{
							int _m89 = mark();
							synPredMatched89 = true;
							inputState.guessing++;
							try {
								{
									match(OP_ARROW);
									iteratorOperationCS();
								}
							}
							catch (RecognitionException)
							{
								synPredMatched89 = false;
							}
							rewind(_m89);
							inputState.guessing--;
						}
						if ( synPredMatched89 )
						{
							opArrow2 = LT(1);
							match(OP_ARROW);
							innerNavigation=iteratorExpCS();
							if (0==inputState.guessing)
							{
									if (navigationExpression == null) {
										    		navigationExpression = new CSTNavigationExpressionCS(resultExpression);
										    		resultExpression = navigationExpression;
										    	}
										    	navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow2), innerNavigation); 
										
							}
						}
						else if ((LA(1)==OP_ARROW||LA(1)==OP_DOT) && (LA(2)==IDENT)) {
							{
								operat=navigationOperatorCS();
								innerNavigation=partTwoExpCS();
								if (0==inputState.guessing)
								{
										if (navigationExpression == null) {
											    		navigationExpression = new CSTNavigationExpressionCS(resultExpression);
											    		resultExpression = navigationExpression;
											    	}
											  		navigationExpression.addInnerNavigation(operat, innerNavigation); 
											  	
								}
							}
						}
						else
						{
							goto _loop91_breakloop;
						}
						}
					}
_loop91_breakloop:					;
				}    // ( ... )*
			}
			catch (RecognitionException ex)
			{
				if (0 == inputState.guessing)
				{
					reportError(ex);
					recover(ex,tokenSet_21_);
				}
				else
				{
					throw ex;
				}
			}
			return resultExpression;
		}
		
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
	public CSTIterateExpCS  iterateExpCS() //throws RecognitionException, TokenStreamException
{
		CSTIterateExpCS expression;
		
		
			expression = null;
			List<object> iterators = null;
			CSTVariableDeclarationCS result = null;
			CSTOclExpressionCS bodyExpression = null;
		
		
		try {      // for error handling
			match(KEYW_ITERATE);
			match(LEFT_PAR);
			{
				bool synPredMatched167 = false;
				if (((LA(1)==IDENT) && (LA(2)==COLON||LA(2)==COMMA||LA(2)==SEMI_COLON) && ((LA(3) >= KEYW_TUPLE && LA(3) <= IDENT))))
				{
					int _m167 = mark();
					synPredMatched167 = true;
					inputState.guessing++;
					try {
						{
							simpleNameCS();
							match(COLON);
							typeCS();
							match(SEMI_COLON);
						}
					}
					catch (RecognitionException)
					{
						synPredMatched167 = false;
					}
					rewind(_m167);
					inputState.guessing--;
				}
				if ( synPredMatched167 )
				{
					{
						iterators=iteratorsCS();
						match(SEMI_COLON);
						result=initializedVariableDeclarationCS();
					}
				}
				else {
					bool synPredMatched170 = false;
					if (((LA(1)==IDENT) && (LA(2)==COLON||LA(2)==COMMA||LA(2)==SEMI_COLON) && ((LA(3) >= KEYW_TUPLE && LA(3) <= IDENT))))
					{
						int _m170 = mark();
						synPredMatched170 = true;
						inputState.guessing++;
						try {
							{
								simpleNameCS();
								match(COLON);
								typeCS();
								match(COMMA);
							}
						}
						catch (RecognitionException)
						{
							synPredMatched170 = false;
						}
						rewind(_m170);
						inputState.guessing--;
					}
					if ( synPredMatched170 )
					{
						{
							iterators=iteratorsCS();
							match(SEMI_COLON);
							result=initializedVariableDeclarationCS();
						}
					}
					else {
						bool synPredMatched173 = false;
						if (((LA(1)==IDENT) && (LA(2)==COLON||LA(2)==COMMA||LA(2)==SEMI_COLON) && ((LA(3) >= KEYW_TUPLE && LA(3) <= IDENT))))
						{
							int _m173 = mark();
							synPredMatched173 = true;
							inputState.guessing++;
							try {
								{
									simpleNameCS();
									match(SEMI_COLON);
								}
							}
							catch (RecognitionException)
							{
								synPredMatched173 = false;
							}
							rewind(_m173);
							inputState.guessing--;
						}
						if ( synPredMatched173 )
						{
							{
								iterators=iteratorsCS();
								match(SEMI_COLON);
								result=initializedVariableDeclarationCS();
							}
						}
						else {
							bool synPredMatched176 = false;
							if (((LA(1)==IDENT) && (LA(2)==COLON||LA(2)==COMMA||LA(2)==SEMI_COLON) && ((LA(3) >= KEYW_TUPLE && LA(3) <= IDENT))))
							{
								int _m176 = mark();
								synPredMatched176 = true;
								inputState.guessing++;
								try {
									{
										simpleNameCS();
										match(COMMA);
									}
								}
								catch (RecognitionException)
								{
									synPredMatched176 = false;
								}
								rewind(_m176);
								inputState.guessing--;
							}
							if ( synPredMatched176 )
							{
								{
									iterators=iteratorsCS();
									match(SEMI_COLON);
									result=initializedVariableDeclarationCS();
								}
							}
							else if ((LA(1)==IDENT) && (LA(2)==COLON) && ((LA(3) >= KEYW_TUPLE && LA(3) <= IDENT))) {
								{
									result=initializedVariableDeclarationCS();
								}
							}
							else
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							}}}
						}
						match(VERT_BAR);
						{
							switch ( LA(1) )
							{
							case LEFT_PAR:
							case OP_MINUS:
							case KEYW_NOT:
							case KEYW_IF:
							case INT_NUMBER:
							case REAL_NUMBER:
							case STRING:
							case KEYW_TRUE:
							case KEYW_FALSE:
							case KEYW_NULL:
							case KEYW_INVALID:
							case KEYW_TUPLE:
							case KEYW_SET:
							case KEYW_BAG:
							case KEYW_SEQUENCE:
							case KEYW_ORDEREDSET:
							case IDENT:
							{
								bodyExpression=impliesExpressionCS();
								break;
							}
							case KEYW_LET:
							{
								bodyExpression=letExpCS();
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						match(RIGHT_PAR);
						if (0==inputState.guessing)
						{
							expression = new CSTIterateExpCS(iterators, result, bodyExpression);
						}
					}
					catch (RecognitionException ex)
					{
						if (0 == inputState.guessing)
						{
							reportError(ex);
							recover(ex,tokenSet_23_);
						}
						else
						{
							throw ex;
						}
					}
					return expression;
				}
				
	public CSTIteratorOperationCS  iteratorOperationCS() //throws RecognitionException, TokenStreamException
{
		CSTIteratorOperationCS iteratorOperation;
		
		IToken  tokenExists = null;
		IToken  tokenForAll = null;
		IToken  tokenIsUnique = null;
		IToken  tokenAny = null;
		IToken  tokenOne = null;
		IToken  tokenCollect = null;
		IToken  tokenSelect = null;
		IToken  tokenReject = null;
		IToken  tokenNested = null;
		IToken  tokenSortedBy = null;
		
			iteratorOperation = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case KEYW_EXISTS:
				{
					tokenExists = LT(1);
					match(KEYW_EXISTS);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenExists);
					}
					break;
				}
				case KEYW_FORALL:
				{
					tokenForAll = LT(1);
					match(KEYW_FORALL);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenForAll);
					}
					break;
				}
				case KEYW_ISUNIQUE:
				{
					tokenIsUnique = LT(1);
					match(KEYW_ISUNIQUE);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenIsUnique);
					}
					break;
				}
				case KEYW_ANY:
				{
					tokenAny = LT(1);
					match(KEYW_ANY);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenAny);
					}
					break;
				}
				case KEYW_ONE:
				{
					tokenOne = LT(1);
					match(KEYW_ONE);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenOne);
					}
					break;
				}
				case KEYW_COLLECT:
				{
					tokenCollect = LT(1);
					match(KEYW_COLLECT);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenCollect);
					}
					break;
				}
				case KEYW_SELECT:
				{
					tokenSelect = LT(1);
					match(KEYW_SELECT);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenSelect);
					}
					break;
				}
				case KEYW_REJECT:
				{
					tokenReject = LT(1);
					match(KEYW_REJECT);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenReject);
					}
					break;
				}
				case KEYW_COLLECTNESTED:
				{
					tokenNested = LT(1);
					match(KEYW_COLLECTNESTED);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenNested);
					}
					break;
				}
				case KEYW_SORTEDBY:
				{
					tokenSortedBy = LT(1);
					match(KEYW_SORTEDBY);
					if (0==inputState.guessing)
					{
						iteratorOperation = new CSTIteratorOperationCS((OCLWorkbenchToken) tokenSortedBy);
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_24_);
			}
			else
			{
				throw ex;
			}
		}
		return iteratorOperation;
	}
	
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
	public CSTIteratorExpCS  iteratorExpCS() //throws RecognitionException, TokenStreamException
{
		CSTIteratorExpCS expression;
		
		
			expression = null;
			CSTVariableDeclarationCS varDecl;
			List<object> iterators = new List<object>();
			CSTOclExpressionCS bodyExpression = null;
			CSTIteratorOperationCS iteratorOperation = null;
		
		
		try {      // for error handling
			iteratorOperation=iteratorOperationCS();
			match(LEFT_PAR);
			{
				bool synPredMatched156 = false;
				if (((LA(1)==IDENT) && (LA(2)==COLON||LA(2)==COMMA||LA(2)==VERT_BAR)))
				{
					int _m156 = mark();
					synPredMatched156 = true;
					inputState.guessing++;
					try {
						{
							if ((LA(1)==IDENT) && (LA(2)==COLON))
							{
								{
									simpleNameCS();
									match(COLON);
								}
							}
							else if ((LA(1)==IDENT) && (LA(2)==VERT_BAR)) {
								{
									simpleNameCS();
									match(VERT_BAR);
								}
							}
							else if ((LA(1)==IDENT) && (LA(2)==COMMA)) {
								{
									simpleNameCS();
									match(COMMA);
								}
							}
							else
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							
						}
					}
					catch (RecognitionException)
					{
						synPredMatched156 = false;
					}
					rewind(_m156);
					inputState.guessing--;
				}
				if ( synPredMatched156 )
				{
					{
						iterators=iteratorsCS();
						match(VERT_BAR);
						{
							switch ( LA(1) )
							{
							case LEFT_PAR:
							case OP_MINUS:
							case KEYW_NOT:
							case KEYW_IF:
							case INT_NUMBER:
							case REAL_NUMBER:
							case STRING:
							case KEYW_TRUE:
							case KEYW_FALSE:
							case KEYW_NULL:
							case KEYW_INVALID:
							case KEYW_TUPLE:
							case KEYW_SET:
							case KEYW_BAG:
							case KEYW_SEQUENCE:
							case KEYW_ORDEREDSET:
							case IDENT:
							{
								bodyExpression=impliesExpressionCS();
								break;
							}
							case KEYW_LET:
							{
								bodyExpression=letExpCS();
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
					}
				}
				else if ((tokenSet_25_.member(LA(1))) && (tokenSet_26_.member(LA(2)))) {
					{
						switch ( LA(1) )
						{
						case LEFT_PAR:
						case OP_MINUS:
						case KEYW_NOT:
						case KEYW_IF:
						case INT_NUMBER:
						case REAL_NUMBER:
						case STRING:
						case KEYW_TRUE:
						case KEYW_FALSE:
						case KEYW_NULL:
						case KEYW_INVALID:
						case KEYW_TUPLE:
						case KEYW_SET:
						case KEYW_BAG:
						case KEYW_SEQUENCE:
						case KEYW_ORDEREDSET:
						case IDENT:
						{
							bodyExpression=impliesExpressionCS();
							break;
						}
						case KEYW_LET:
						{
							bodyExpression=letExpCS();
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
				}
				else
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				
			}
			match(RIGHT_PAR);
			if (0==inputState.guessing)
			{
				expression = new CSTIteratorExpCS(iteratorOperation, iterators, bodyExpression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return expression;
	}
	
	public CSTNavigationOperatorCS  navigationOperatorCS() //throws RecognitionException, TokenStreamException
{
		CSTNavigationOperatorCS operat;
		
		IToken  opDot = null;
		IToken  opArrow = null;
		
			operat = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case OP_DOT:
			{
				opDot = LT(1);
				match(OP_DOT);
				if (0==inputState.guessing)
				{
					operat = new CSTNavigationOperatorCS((OCLWorkbenchToken) opDot);
				}
				break;
			}
			case OP_ARROW:
			{
				opArrow = LT(1);
				match(OP_ARROW);
				if (0==inputState.guessing)
				{
					operat = new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow);
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_27_);
			}
			else
			{
				throw ex;
			}
		}
		return operat;
	}
	
/*********************************************
partTwoExpCS :
	(simpleNameCS LEFT_PAR) => instanceOperationCallExpCS |
	(simpleNameCS isMarkedPreCS LEFT_PAR) => instanceOperationCallExpCS |
	simpleNameExpCS 
		;
**********************************************/
	public CSTOclExpressionCS  partTwoExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS expression;
		
		
			expression = null;
		
		
		try {      // for error handling
			bool synPredMatched130 = false;
			if (((LA(1)==IDENT) && (LA(2)==LEFT_PAR||LA(2)==AT) && (tokenSet_28_.member(LA(3)))))
			{
				int _m130 = mark();
				synPredMatched130 = true;
				inputState.guessing++;
				try {
					{
						simpleNameCS();
						match(LEFT_PAR);
					}
				}
				catch (RecognitionException)
				{
					synPredMatched130 = false;
				}
				rewind(_m130);
				inputState.guessing--;
			}
			if ( synPredMatched130 )
			{
				expression=instanceOperationCallExpCS();
			}
			else {
				bool synPredMatched132 = false;
				if (((LA(1)==IDENT) && (LA(2)==LEFT_PAR||LA(2)==AT) && (tokenSet_28_.member(LA(3)))))
				{
					int _m132 = mark();
					synPredMatched132 = true;
					inputState.guessing++;
					try {
						{
							simpleNameCS();
							isMarkedPreCS();
							match(LEFT_PAR);
						}
					}
					catch (RecognitionException)
					{
						synPredMatched132 = false;
					}
					rewind(_m132);
					inputState.guessing--;
				}
				if ( synPredMatched132 )
				{
					expression=instanceOperationCallExpCS();
				}
				else if ((LA(1)==IDENT) && (tokenSet_29_.member(LA(2))) && (tokenSet_30_.member(LA(3)))) {
					expression=simpleNameExpCS();
				}
				else
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
			}
			catch (RecognitionException ex)
			{
				if (0 == inputState.guessing)
				{
					reportError(ex);
					recover(ex,tokenSet_23_);
				}
				else
				{
					throw ex;
				}
			}
			return expression;
		}
		
/*********************************************
**********************************************/
	public CSTOclExpressionCS  ifExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS resultExpression;
		
		IToken  tokenIf = null;
		
			resultExpression = null;
			CSTOclExpressionCS conditionExpression = null;
			CSTOclExpressionCS thenExpression = null;
			CSTOclExpressionCS elseExpression = null;
		
		
		try {      // for error handling
			tokenIf = LT(1);
			match(KEYW_IF);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					conditionExpression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					conditionExpression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(KEYW_THEN);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					thenExpression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					thenExpression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(KEYW_ELSE);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					elseExpression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					elseExpression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(KEYW_ENDIF);
			if (0==inputState.guessing)
			{
				resultExpression = new CSTIfExpCS((OCLWorkbenchToken) tokenIf, conditionExpression, thenExpression, elseExpression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_21_);
			}
			else
			{
				throw ex;
			}
		}
		return resultExpression;
	}
	
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
	public CSTOclExpressionCS  navigationExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS expression;
		
		IToken  opArrow1 = null;
		IToken  opArrow2 = null;
		
			expression = null;
			CSTOclExpressionCS callExp = null;
			CSTOclExpressionCS innerNavigation = null;
			CSTNavigationExpressionCS navigationExpression = null;
			CSTNavigationOperatorCS operat = null;
		
		
		try {      // for error handling
			callExp=partOneExpCS();
			if (0==inputState.guessing)
			{
				navigationExpression = new CSTNavigationExpressionCS(callExp);
					  	  expression = navigationExpression;
					  	
			}
			{    // ( ... )*
				for (;;)
				{
					bool synPredMatched113 = false;
					if (((LA(1)==OP_ARROW) && (LA(2)==KEYW_ITERATE)))
					{
						int _m113 = mark();
						synPredMatched113 = true;
						inputState.guessing++;
						try {
							{
								match(OP_ARROW);
								match(KEYW_ITERATE);
								match(LEFT_PAR);
							}
						}
						catch (RecognitionException)
						{
							synPredMatched113 = false;
						}
						rewind(_m113);
						inputState.guessing--;
					}
					if ( synPredMatched113 )
					{
						opArrow1 = LT(1);
						match(OP_ARROW);
						innerNavigation=iterateExpCS();
						if (0==inputState.guessing)
						{
							navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow1), innerNavigation);
						}
					}
					else {
						bool synPredMatched115 = false;
						if (((LA(1)==OP_ARROW) && ((LA(2) >= KEYW_EXISTS && LA(2) <= KEYW_SORTEDBY))))
						{
							int _m115 = mark();
							synPredMatched115 = true;
							inputState.guessing++;
							try {
								{
									match(OP_ARROW);
									iteratorOperationCS();
								}
							}
							catch (RecognitionException)
							{
								synPredMatched115 = false;
							}
							rewind(_m115);
							inputState.guessing--;
						}
						if ( synPredMatched115 )
						{
							opArrow2 = LT(1);
							match(OP_ARROW);
							innerNavigation=iteratorExpCS();
							if (0==inputState.guessing)
							{
								navigationExpression.addInnerNavigation(new CSTNavigationOperatorCS((OCLWorkbenchToken) opArrow2), innerNavigation);
							}
						}
						else if ((LA(1)==OP_ARROW||LA(1)==OP_DOT) && (LA(2)==IDENT)) {
							{
								operat=navigationOperatorCS();
								innerNavigation=partTwoExpCS();
								if (0==inputState.guessing)
								{
									navigationExpression.addInnerNavigation(operat, innerNavigation);
								}
							}
						}
						else
						{
							goto _loop117_breakloop;
						}
						}
					}
_loop117_breakloop:					;
				}    // ( ... )*
				if (0==inputState.guessing)
				{
					
						  if (innerNavigation == null) 
							expression = callExp;
						
				}
			}
			catch (RecognitionException ex)
			{
				if (0 == inputState.guessing)
				{
					reportError(ex);
					recover(ex,tokenSet_21_);
				}
				else
				{
					throw ex;
				}
			}
			return expression;
		}
		
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
	public CSTOclExpressionCS  partOneExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS expression;
		
		
			expression = null;
		
		
		try {      // for error handling
			bool synPredMatched121 = false;
			if (((LA(1)==IDENT) && (LA(2)==LEFT_PAR||LA(2)==AT) && (tokenSet_28_.member(LA(3)))))
			{
				int _m121 = mark();
				synPredMatched121 = true;
				inputState.guessing++;
				try {
					{
						simpleNameCS();
						match(LEFT_PAR);
					}
				}
				catch (RecognitionException)
				{
					synPredMatched121 = false;
				}
				rewind(_m121);
				inputState.guessing--;
			}
			if ( synPredMatched121 )
			{
				expression=instanceOperationCallExpCS();
			}
			else {
				bool synPredMatched123 = false;
				if (((LA(1)==IDENT) && (LA(2)==OP_DOUBLE_COLON) && (LA(3)==IDENT)))
				{
					int _m123 = mark();
					synPredMatched123 = true;
					inputState.guessing++;
					try {
						{
							pathNameCS();
							match(LEFT_PAR);
						}
					}
					catch (RecognitionException)
					{
						synPredMatched123 = false;
					}
					rewind(_m123);
					inputState.guessing--;
				}
				if ( synPredMatched123 )
				{
					expression=classOperationCallExpCS();
				}
				else {
					bool synPredMatched125 = false;
					if (((LA(1)==IDENT) && (LA(2)==LEFT_PAR||LA(2)==AT) && (tokenSet_28_.member(LA(3)))))
					{
						int _m125 = mark();
						synPredMatched125 = true;
						inputState.guessing++;
						try {
							{
								simpleNameCS();
								isMarkedPreCS();
								match(LEFT_PAR);
							}
						}
						catch (RecognitionException)
						{
							synPredMatched125 = false;
						}
						rewind(_m125);
						inputState.guessing--;
					}
					if ( synPredMatched125 )
					{
						expression=instanceOperationCallExpCS();
					}
					else {
						bool synPredMatched127 = false;
						if (((LA(1)==IDENT) && (LA(2)==OP_DOUBLE_COLON) && (LA(3)==IDENT)))
						{
							int _m127 = mark();
							synPredMatched127 = true;
							inputState.guessing++;
							try {
								{
									pathNameCS();
									isMarkedPreCS();
									match(LEFT_PAR);
								}
							}
							catch (RecognitionException)
							{
								synPredMatched127 = false;
							}
							rewind(_m127);
							inputState.guessing--;
						}
						if ( synPredMatched127 )
						{
							expression=classOperationCallExpCS();
						}
						else if ((LA(1)==IDENT) && (tokenSet_29_.member(LA(2))) && (tokenSet_30_.member(LA(3)))) {
							expression=simpleNameExpCS();
						}
						else if ((LA(1)==IDENT) && (LA(2)==OP_DOUBLE_COLON) && (LA(3)==IDENT)) {
							expression=classifierAttributeCallExpCS();
						}
						else if ((tokenSet_31_.member(LA(1)))) {
							expression=literalExpCS();
						}
						else
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						}}}
					}
					catch (RecognitionException ex)
					{
						if (0 == inputState.guessing)
						{
							reportError(ex);
							recover(ex,tokenSet_23_);
						}
						else
						{
							throw ex;
						}
					}
					return expression;
				}
				
/*********************************************
instanceOperationCallExpCS :
	simpleNameCS
	(isMarkedPreCS)?
	LEFT_PAR
	(argumentsCS)?
	RIGHT_PAR
		;
**********************************************/
	public CSTOclExpressionCS  instanceOperationCallExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS expression;
		
		
			expression = null;
			CSTSimpleNameCS simpleName = null;
			List<object> arguments = new List<object>();
			bool isMarkedPre = false;
		
		
		try {      // for error handling
			simpleName=simpleNameCS();
			{
				switch ( LA(1) )
				{
				case AT:
				{
					isMarkedPre=isMarkedPreCS();
					break;
				}
				case LEFT_PAR:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(LEFT_PAR);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case KEYW_LET:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					arguments=argumentsCS();
					break;
				}
				case RIGHT_PAR:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(RIGHT_PAR);
			if (0==inputState.guessing)
			{
				expression = new CSTInstanceOperationCallExpCS(simpleName, arguments, isMarkedPre);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return expression;
	}
	
/*********************************************
classOperationCallExpCS :
	pathNameCS
	(isMarkedPreCS)?
	LEFT_PAR
	(argumentsCS)?
	RIGHT_PAR
		;
**********************************************/
	public CSTOclExpressionCS  classOperationCallExpCS() //throws RecognitionException, TokenStreamException
{
		CSTOclExpressionCS expression;
		
		
			expression = null;
			CSTPathNameCS pathName = null;
			bool isMarkedPre = false;
			List<object> arguments = new List<object>();
		
		
		try {      // for error handling
			pathName=pathNameCS();
			{
				switch ( LA(1) )
				{
				case AT:
				{
					isMarkedPre=isMarkedPreCS();
					break;
				}
				case LEFT_PAR:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(LEFT_PAR);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case KEYW_LET:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					arguments=argumentsCS();
					break;
				}
				case RIGHT_PAR:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(RIGHT_PAR);
			if (0==inputState.guessing)
			{
				expression = new CSTClassOperationCallExpCS(pathName, arguments, isMarkedPre);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return expression;
	}
	
/*********************************************
isMarkedPreCS :
	AT 
	KEYW_PRE
		;
**********************************************/
	public bool  isMarkedPreCS() //throws RecognitionException, TokenStreamException
{
		bool isMarkedPre;
		
		
			isMarkedPre = false;
		
		
		try {      // for error handling
			match(AT);
			match(KEYW_PRE);
			if (0==inputState.guessing)
			{
				isMarkedPre = true;
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_32_);
			}
			else
			{
				throw ex;
			}
		}
		return isMarkedPre;
	}
	
/*********************************************
simpleNameExpCS :
	simpleNameCS 
	(LEFT_BRACKET 
	 arguments = argumentsCS
	 RIGHT_BRACKET)?	 
	(isMarkedPreCS)?
		;
**********************************************/
	public CSTSimpleNameExpCS  simpleNameExpCS() //throws RecognitionException, TokenStreamException
{
		CSTSimpleNameExpCS callExpCS;
		
		
			callExpCS = null;
			CSTSimpleNameCS name = null;
			bool isMarkedPre = false;
			List<object> arguments = new List<object>();
		
		
		try {      // for error handling
			name=simpleNameCS();
			{
				switch ( LA(1) )
				{
				case LEFT_BRACKET:
				{
					match(LEFT_BRACKET);
					arguments=argumentsCS();
					match(RIGHT_BRACKET);
					break;
				}
				case EOF:
				case KEYW_PACKAGE:
				case KEYW_ENDPACKAGE:
				case KEYW_CONTEXT:
				case KEYW_INV:
				case KEYW_DEF:
				case OP_EQUAL:
				case KEYW_PRE:
				case KEYW_POST:
				case KEYW_BODY:
				case RIGHT_PAR:
				case COMMA:
				case KEYW_IN:
				case OP_ARROW:
				case KEYW_IMPLIES:
				case KEYW_AND:
				case KEYW_OR:
				case KEYW_XOR:
				case OP_LESS_THAN:
				case OP_GREATER_THAN:
				case OP_LESS_OR_EQ:
				case OP_GREATER_OR_EQ:
				case OP_NOTEQUAL:
				case OP_PLUS:
				case OP_MINUS:
				case OP_MULTIPLY:
				case OP_DIVIDE:
				case KEYW_THEN:
				case KEYW_ELSE:
				case KEYW_ENDIF:
				case OP_DOT:
				case RIGHT_BRACKET:
				case AT:
				case VERT_BAR:
				case RIGHT_BRACE:
				case RANGE:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			{
				switch ( LA(1) )
				{
				case AT:
				{
					isMarkedPre=isMarkedPreCS();
					break;
				}
				case EOF:
				case KEYW_PACKAGE:
				case KEYW_ENDPACKAGE:
				case KEYW_CONTEXT:
				case KEYW_INV:
				case KEYW_DEF:
				case OP_EQUAL:
				case KEYW_PRE:
				case KEYW_POST:
				case KEYW_BODY:
				case RIGHT_PAR:
				case COMMA:
				case KEYW_IN:
				case OP_ARROW:
				case KEYW_IMPLIES:
				case KEYW_AND:
				case KEYW_OR:
				case KEYW_XOR:
				case OP_LESS_THAN:
				case OP_GREATER_THAN:
				case OP_LESS_OR_EQ:
				case OP_GREATER_OR_EQ:
				case OP_NOTEQUAL:
				case OP_PLUS:
				case OP_MINUS:
				case OP_MULTIPLY:
				case OP_DIVIDE:
				case KEYW_THEN:
				case KEYW_ELSE:
				case KEYW_ENDIF:
				case OP_DOT:
				case RIGHT_BRACKET:
				case VERT_BAR:
				case RIGHT_BRACE:
				case RANGE:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				callExpCS = new CSTSimpleNameExpCS(name, arguments, isMarkedPre);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return callExpCS;
	}
	
/*********************************************
classifierAttributeCallExpCS :
	pathNameCS
	(isMarkedPreCS)?
		;
**********************************************/
	public CSTClassifierAttributeCallExpCS  classifierAttributeCallExpCS() //throws RecognitionException, TokenStreamException
{
		CSTClassifierAttributeCallExpCS callExpCS;
		
		
			callExpCS = null;
			CSTPathNameCS name = null;
			bool isMarkedPre = false;
		
		
		try {      // for error handling
			name=pathNameCS();
			{
				switch ( LA(1) )
				{
				case AT:
				{
					isMarkedPre=isMarkedPreCS();
					break;
				}
				case EOF:
				case KEYW_PACKAGE:
				case KEYW_ENDPACKAGE:
				case KEYW_CONTEXT:
				case KEYW_INV:
				case KEYW_DEF:
				case OP_EQUAL:
				case KEYW_PRE:
				case KEYW_POST:
				case KEYW_BODY:
				case RIGHT_PAR:
				case COMMA:
				case KEYW_IN:
				case OP_ARROW:
				case KEYW_IMPLIES:
				case KEYW_AND:
				case KEYW_OR:
				case KEYW_XOR:
				case OP_LESS_THAN:
				case OP_GREATER_THAN:
				case OP_LESS_OR_EQ:
				case OP_GREATER_OR_EQ:
				case OP_NOTEQUAL:
				case OP_PLUS:
				case OP_MINUS:
				case OP_MULTIPLY:
				case OP_DIVIDE:
				case KEYW_THEN:
				case KEYW_ELSE:
				case KEYW_ENDIF:
				case OP_DOT:
				case RIGHT_BRACKET:
				case VERT_BAR:
				case RIGHT_BRACE:
				case RANGE:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				callExpCS = new CSTClassifierAttributeCallExpCS(name, isMarkedPre);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return callExpCS;
	}
	
/*********************************************
literalExpCS :
	primitiveLiteralExpCS |
	collectionLiteralExpCS |
	tupleLiteralExpCS
		;
**********************************************/
	public CSTLiteralExpCS  literalExpCS() //throws RecognitionException, TokenStreamException
{
		CSTLiteralExpCS literalExp;
		
		
			literalExp = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case INT_NUMBER:
			case REAL_NUMBER:
			case STRING:
			case KEYW_TRUE:
			case KEYW_FALSE:
			case KEYW_NULL:
			case KEYW_INVALID:
			{
				literalExp=primitiveLiteralExpCS();
				break;
			}
			case KEYW_SET:
			case KEYW_BAG:
			case KEYW_SEQUENCE:
			case KEYW_ORDEREDSET:
			{
				literalExp=collectionLiteralExpCS();
				break;
			}
			case KEYW_TUPLE:
			{
				literalExp=tupleLiteralExpCS();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
/*********************************************
argumentsCS :
	impliesExpressionCS
	(COMMA argumentsCS)?
		;
**********************************************/
	public List<object>  argumentsCS() //throws RecognitionException, TokenStreamException
{
		List<object> arguments;
		
		
			arguments = new List<object>();
			CSTOclExpressionCS expression = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					expression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					expression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				arguments.Add(new CSTArgumentCS(expression));
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						{
							switch ( LA(1) )
							{
							case LEFT_PAR:
							case OP_MINUS:
							case KEYW_NOT:
							case KEYW_IF:
							case INT_NUMBER:
							case REAL_NUMBER:
							case STRING:
							case KEYW_TRUE:
							case KEYW_FALSE:
							case KEYW_NULL:
							case KEYW_INVALID:
							case KEYW_TUPLE:
							case KEYW_SET:
							case KEYW_BAG:
							case KEYW_SEQUENCE:
							case KEYW_ORDEREDSET:
							case IDENT:
							{
								expression=impliesExpressionCS();
								break;
							}
							case KEYW_LET:
							{
								expression=letExpCS();
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						if (0==inputState.guessing)
						{
							arguments.Add(new CSTArgumentCS(expression));
						}
					}
					else
					{
						goto _loop149_breakloop;
					}
					
				}
_loop149_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_33_);
			}
			else
			{
				throw ex;
			}
		}
		return arguments;
	}
	
	public List<object>  iteratorsCS() //throws RecognitionException, TokenStreamException
{
		List<object> iterators;
		
		
			iterators = new List<object>();
			CSTNameCS name = null;
			CSTTypeCS type1 = null;
			CSTTypeCS type2 = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					match(COLON);
					type1=typeCS();
					break;
				}
				case COMMA:
				case VERT_BAR:
				case SEMI_COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				iterators.Add(new CSTVariableDeclarationCS(name, type1, null));
			}
			{
				switch ( LA(1) )
				{
				case COMMA:
				{
					match(COMMA);
					name=simpleNameCS();
					{
						switch ( LA(1) )
						{
						case COLON:
						{
							match(COLON);
							type2=typeCS();
							break;
						}
						case VERT_BAR:
						case SEMI_COLON:
						{
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					if (0==inputState.guessing)
					{
						iterators.Add(new CSTVariableDeclarationCS(name, type2, null));
					}
					break;
				}
				case VERT_BAR:
				case SEMI_COLON:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_34_);
			}
			else
			{
				throw ex;
			}
		}
		return iterators;
	}
	
/*********************************************
variableDeclarationCS:
	simpleNameCS 
	(COLON typeCS)? 
	(OP_EQUAL impliesExpressionCS)?
		;
**********************************************/
	public CSTVariableDeclarationCS  variableDeclarationCS() //throws RecognitionException, TokenStreamException
{
		CSTVariableDeclarationCS variableDeclaration;
		
		
			variableDeclaration = null;
			CSTNameCS name = null;
			CSTTypeCS type = null;
			CSTOclExpressionCS	expression = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					match(COLON);
					type=typeCS();
					break;
				}
				case EOF:
				case OP_EQUAL:
				case COMMA:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			{
				switch ( LA(1) )
				{
				case OP_EQUAL:
				{
					match(OP_EQUAL);
					{
						switch ( LA(1) )
						{
						case LEFT_PAR:
						case OP_MINUS:
						case KEYW_NOT:
						case KEYW_IF:
						case INT_NUMBER:
						case REAL_NUMBER:
						case STRING:
						case KEYW_TRUE:
						case KEYW_FALSE:
						case KEYW_NULL:
						case KEYW_INVALID:
						case KEYW_TUPLE:
						case KEYW_SET:
						case KEYW_BAG:
						case KEYW_SEQUENCE:
						case KEYW_ORDEREDSET:
						case IDENT:
						{
							expression=impliesExpressionCS();
							break;
						}
						case KEYW_LET:
						{
							expression=letExpCS();
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					break;
				}
				case EOF:
				case COMMA:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				variableDeclaration = new CSTVariableDeclarationCS(name, type, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_35_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclaration;
	}
	
/*********************************************
variableDeclarationListCS :
	variableDeclarationCS 
	(COMMA variableDeclarationCS)*
		;
**********************************************/
	public List<object>  variableDeclarationListCS() //throws RecognitionException, TokenStreamException
{
		List<object> variableDeclarationList;
		
		
			variableDeclarationList = new List<object>();
			CSTVariableDeclarationCS declaration = null;
		
		
		try {      // for error handling
			declaration=variableDeclarationCS();
			if (0==inputState.guessing)
			{
				variableDeclarationList.Add(declaration);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						declaration=variableDeclarationCS();
						if (0==inputState.guessing)
						{
							variableDeclarationList.Add(declaration);
						}
					}
					else
					{
						goto _loop188_breakloop;
					}
					
				}
_loop188_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_0_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclarationList;
	}
	
/*********************************************
**********************************************/
	public CSTVariableDeclarationCS  defaultVariableDeclarationCS() //throws RecognitionException, TokenStreamException
{
		CSTVariableDeclarationCS variableDeclaration;
		
		
			variableDeclaration = null;
			CSTNameCS name = null;
			CSTTypeCS type = null;
			CSTOclExpressionCS	expression = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					match(COLON);
					type=typeCS();
					break;
				}
				case OP_EQUAL:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(OP_EQUAL);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					expression=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					expression=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				variableDeclaration = new CSTVariableDeclarationCS(name, type, expression);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_36_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclaration;
	}
	
/*********************************************
primitiveLiteralExpCS :
	integerLiteralExpCS |
	realLiteralExpCS |
	stringLiteralExpCS |
	booleanLiteralExpCS
		;
**********************************************/
	public CSTLiteralExpCS  primitiveLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTLiteralExpCS literalExp;
		
		
			literalExp = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case INT_NUMBER:
			{
				literalExp=integerLiteralExpCS();
				break;
			}
			case REAL_NUMBER:
			{
				literalExp=realLiteralExpCS();
				break;
			}
			case STRING:
			{
				literalExp=stringLiteralExpCS();
				break;
			}
			case KEYW_TRUE:
			case KEYW_FALSE:
			{
				literalExp=booleanLiteralExpCS();
				break;
			}
			case KEYW_NULL:
			{
				literalExp=nullLiteralExpCS();
				break;
			}
			case KEYW_INVALID:
			{
				literalExp=invalidLiteralExpCS();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
/*********************************************
collectionLiteralExpCS :
	collectionLiteralTypeIdentifierCS 
	LEFT_BRACE 
	(collectionLiteralPartsCS)? 
	RIGHT_BRACE
		;
**********************************************/
	public CSTCollectionLiteralExpCS  collectionLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTCollectionLiteralExpCS literalExp;
		
		
			literalExp = null;
			List<object>  literalParts = new List<object>();
			CSTCollectionTypeIdentifierCS typeId = null;
		
		
		try {      // for error handling
			typeId=collectionLiteralTypeIdentifierCS();
			match(LEFT_BRACE);
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case KEYW_LET:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					literalParts=collectionLiteralPartsCS();
					break;
				}
				case RIGHT_BRACE:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(RIGHT_BRACE);
			if (0==inputState.guessing)
			{
				literalExp = new CSTCollectionLiteralExpCS(typeId, literalParts);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
/*********************************************
tupleLiteralExpCS :
	KEYW_TUPLE 
	LEFT_BRACE 
	variableDeclarationListCS 
	RIGHT_BRACE
		;
**********************************************/
	public CSTTupleLiteralExpCS  tupleLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTTupleLiteralExpCS tupleLiteralExp;
		
		IToken  token = null;
		
			tupleLiteralExp = null;
			List<object> variableDeclarationList = new List<object>();
		
		
		try {      // for error handling
			token = LT(1);
			match(KEYW_TUPLE);
			match(LEFT_BRACE);
			variableDeclarationList=tupleLiteralPartsCS();
			match(RIGHT_BRACE);
			if (0==inputState.guessing)
			{
				tupleLiteralExp = new CSTTupleLiteralExpCS((OCLWorkbenchToken) token, variableDeclarationList);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return tupleLiteralExp;
	}
	
/*********************************************
integerLiteralExpCS :
	INT_NUMBER
		;
**********************************************/
	public CSTIntegerLiteralExpCS  integerLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTIntegerLiteralExpCS literalExp;
		
		IToken  value = null;
		
			literalExp = null;
		
		
		try {      // for error handling
			value = LT(1);
			match(INT_NUMBER);
			if (0==inputState.guessing)
			{
				literalExp = new CSTIntegerLiteralExpCS((OCLWorkbenchToken) value);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
/*********************************************
realLiteralExpCS :
	REAL_NUMBER
		;
**********************************************/
	public CSTRealLiteralExpCS  realLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTRealLiteralExpCS literalExp;
		
		IToken  value = null;
		
			literalExp = null;
		
		
		try {      // for error handling
			value = LT(1);
			match(REAL_NUMBER);
			if (0==inputState.guessing)
			{
				literalExp = new CSTRealLiteralExpCS((OCLWorkbenchToken) value);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
/*********************************************
stringLiteralExpCS :
	STRING
		;
**********************************************/
	public CSTStringLiteralExpCS  stringLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTStringLiteralExpCS literalExp;
		
		IToken  value = null;
		
			literalExp = null;
		
		
		try {      // for error handling
			value = LT(1);
			match(STRING);
			if (0==inputState.guessing)
			{
				literalExp = new CSTStringLiteralExpCS((OCLWorkbenchToken) value);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
/*********************************************
booleanLiteralExpCS :
	KEYW_TRUE | KEYW_FALSE
	;
**********************************************/
	public CSTBooleanLiteralExpCS  booleanLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTBooleanLiteralExpCS literalExp;
		
		IToken  valueTrue = null;
		IToken  valueFalse = null;
		
			literalExp = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case KEYW_TRUE:
			{
				valueTrue = LT(1);
				match(KEYW_TRUE);
				if (0==inputState.guessing)
				{
					literalExp = new CSTBooleanLiteralExpCS((OCLWorkbenchToken) valueTrue);
				}
				break;
			}
			case KEYW_FALSE:
			{
				valueFalse = LT(1);
				match(KEYW_FALSE);
				if (0==inputState.guessing)
				{
					literalExp = new CSTBooleanLiteralExpCS((OCLWorkbenchToken) valueFalse);
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
	public CSTNullLiteralExpCS  nullLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTNullLiteralExpCS literalExp;
		
		IToken  valueNull = null;
		
			literalExp = null;
		
		
		try {      // for error handling
			valueNull = LT(1);
			match(KEYW_NULL);
			if (0==inputState.guessing)
			{
				literalExp = new CSTNullLiteralExpCS((OCLWorkbenchToken) valueNull);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
	public CSTInvalidLiteralExpCS  invalidLiteralExpCS() //throws RecognitionException, TokenStreamException
{
		CSTInvalidLiteralExpCS literalExp;
		
		IToken  valueInvalid = null;
		
			literalExp = null;
		
		
		try {      // for error handling
			valueInvalid = LT(1);
			match(KEYW_INVALID);
			if (0==inputState.guessing)
			{
				literalExp = new CSTInvalidLiteralExpCS((OCLWorkbenchToken) valueInvalid);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_23_);
			}
			else
			{
				throw ex;
			}
		}
		return literalExp;
	}
	
	public CSTCollectionTypeIdentifierCS  collectionLiteralTypeIdentifierCS() //throws RecognitionException, TokenStreamException
{
		CSTCollectionTypeIdentifierCS typeIdentifier;
		
		IToken  valueSet = null;
		IToken  valueBag = null;
		IToken  valueSequence = null;
		IToken  valueOrderedSet = null;
		
			typeIdentifier = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case KEYW_SET:
			{
				{
					valueSet = LT(1);
					match(KEYW_SET);
					if (0==inputState.guessing)
					{
						typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueSet);
					}
				}
				break;
			}
			case KEYW_BAG:
			{
				{
					valueBag = LT(1);
					match(KEYW_BAG);
					if (0==inputState.guessing)
					{
						typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueBag);
					}
				}
				break;
			}
			case KEYW_SEQUENCE:
			{
				{
					valueSequence = LT(1);
					match(KEYW_SEQUENCE);
					if (0==inputState.guessing)
					{
						typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueSequence);
					}
				}
				break;
			}
			case KEYW_ORDEREDSET:
			{
				{
					valueOrderedSet = LT(1);
					match(KEYW_ORDEREDSET);
					if (0==inputState.guessing)
					{
						typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueOrderedSet);
					}
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_37_);
			}
			else
			{
				throw ex;
			}
		}
		return typeIdentifier;
	}
	
/*********************************************
collectionLiteralPartsCS :
	collectionLiteralPartCS 
	(COMMA collectionLiteralPartCS)*
		;
**********************************************/
	public List<object>  collectionLiteralPartsCS() //throws RecognitionException, TokenStreamException
{
		List<object> literalParts;
		
		
			literalParts = new List<object>();
			CSTCollectionLiteralPartCS literalPart = null;
		
		
		try {      // for error handling
			literalPart=collectionLiteralPartCS();
			if (0==inputState.guessing)
			{
				literalParts.Add(literalPart);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						literalPart=collectionLiteralPartCS();
						if (0==inputState.guessing)
						{
							literalParts.Add(literalPart);
						}
					}
					else
					{
						goto _loop208_breakloop;
					}
					
				}
_loop208_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_38_);
			}
			else
			{
				throw ex;
			}
		}
		return literalParts;
	}
	
/*********************************************
collectionLiteralPartCS :
	impliesExpressionCS
	(impliesExpressionCS	RANGE)? 
		;

**********************************************/
	public CSTCollectionLiteralPartCS  collectionLiteralPartCS() //throws RecognitionException, TokenStreamException
{
		CSTCollectionLiteralPartCS literalPart;
		
		
			literalPart = null;
			CSTOclExpressionCS expression1 = null;
			CSTOclExpressionCS expression2 = null;
		
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case LEFT_PAR:
				case OP_MINUS:
				case KEYW_NOT:
				case KEYW_IF:
				case INT_NUMBER:
				case REAL_NUMBER:
				case STRING:
				case KEYW_TRUE:
				case KEYW_FALSE:
				case KEYW_NULL:
				case KEYW_INVALID:
				case KEYW_TUPLE:
				case KEYW_SET:
				case KEYW_BAG:
				case KEYW_SEQUENCE:
				case KEYW_ORDEREDSET:
				case IDENT:
				{
					expression1=impliesExpressionCS();
					break;
				}
				case KEYW_LET:
				{
					expression1=letExpCS();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			{
				switch ( LA(1) )
				{
				case RANGE:
				{
					match(RANGE);
					{
						switch ( LA(1) )
						{
						case LEFT_PAR:
						case OP_MINUS:
						case KEYW_NOT:
						case KEYW_IF:
						case INT_NUMBER:
						case REAL_NUMBER:
						case STRING:
						case KEYW_TRUE:
						case KEYW_FALSE:
						case KEYW_NULL:
						case KEYW_INVALID:
						case KEYW_TUPLE:
						case KEYW_SET:
						case KEYW_BAG:
						case KEYW_SEQUENCE:
						case KEYW_ORDEREDSET:
						case IDENT:
						{
							expression2=impliesExpressionCS();
							break;
						}
						case KEYW_LET:
						{
							expression2=letExpCS();
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					break;
				}
				case COMMA:
				case RIGHT_BRACE:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				
						if (expression2 == null)
							literalPart = new CSTCollectionLiteralSinglePartCS(expression1); 
						else
							literalPart = new CSTCollectionLiteralRangeCS(expression1, expression2);
					
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_36_);
			}
			else
			{
				throw ex;
			}
		}
		return literalPart;
	}
	
/*********************************************
tupleLiteralPartsCS:
	defaultVariableDeclarationCS  
	(COMMA defaultVariableDeclarationCS)*
		;
**********************************************/
	public List<object>  tupleLiteralPartsCS() //throws RecognitionException, TokenStreamException
{
		List<object> variableDeclarationList;
		
		
			variableDeclarationList = new List<object>();
			CSTVariableDeclarationCS declaration = null;
		
		
		try {      // for error handling
			declaration=defaultVariableDeclarationCS();
			if (0==inputState.guessing)
			{
				variableDeclarationList.Add(declaration);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						declaration=defaultVariableDeclarationCS();
						if (0==inputState.guessing)
						{
							variableDeclarationList.Add(declaration);
						}
					}
					else
					{
						goto _loop216_breakloop;
					}
					
				}
_loop216_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_38_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclarationList;
	}
	
/*********************************************
collectionTypeCS :
	collectionTypeIdentifierCS 
	LEFT_PAR 
	typeCS 
	RIGHT_PAR
		;
**********************************************/
	public CSTCollectionTypeCS  collectionTypeCS() //throws RecognitionException, TokenStreamException
{
		CSTCollectionTypeCS collectionType;
		
		
			collectionType = null;
			CSTCollectionTypeIdentifierCS typeId = null;
			CSTTypeCS type = null;
		
		
		try {      // for error handling
			typeId=collectionTypeIdentifierCS();
			match(LEFT_PAR);
			type=typeCS();
			match(RIGHT_PAR);
			if (0==inputState.guessing)
			{
				collectionType = new CSTCollectionTypeCS(typeId, type);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_5_);
			}
			else
			{
				throw ex;
			}
		}
		return collectionType;
	}
	
/*********************************************
tupleTypeCS:
	KEYW_TUPLE 
	LEFT_PAR 
	(variableDeclarationListCS)? 
	RIGHT_PAR
		;
**********************************************/
	public CSTTupleTypeCS  tupleTypeCS() //throws RecognitionException, TokenStreamException
{
		CSTTupleTypeCS tupleType;
		
		IToken  tupleKeyw = null;
		
			tupleType = null;
			List<object> tuplePartsList = new List<object>();
		
		
		try {      // for error handling
			tupleKeyw = LT(1);
			match(KEYW_TUPLE);
			match(LEFT_PAR);
			tuplePartsList=tuplePartsListCS();
			match(RIGHT_PAR);
			if (0==inputState.guessing)
			{
				tupleType = new CSTTupleTypeCS((OCLWorkbenchToken) tupleKeyw, tuplePartsList);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_5_);
			}
			else
			{
				throw ex;
			}
		}
		return tupleType;
	}
	
/*********************************************
collectionTypeIdentifierCS :
	KEYW_SET | 
	KEYW_BAG | 
	KEYW_SEQUENCE | 
	KEYW_ORDEREDSET | 
	KEYW_COLLECTION;
**********************************************/
	public CSTCollectionTypeIdentifierCS  collectionTypeIdentifierCS() //throws RecognitionException, TokenStreamException
{
		CSTCollectionTypeIdentifierCS typeIdentifier;
		
		IToken  valueCollection = null;
		
			typeIdentifier = null;
		
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case KEYW_COLLECTION:
			{
				{
					valueCollection = LT(1);
					match(KEYW_COLLECTION);
					if (0==inputState.guessing)
					{
						typeIdentifier = new CSTCollectionTypeIdentifierCS((OCLWorkbenchToken) valueCollection);
					}
				}
				break;
			}
			case KEYW_SET:
			case KEYW_BAG:
			case KEYW_SEQUENCE:
			case KEYW_ORDEREDSET:
			{
				typeIdentifier=collectionLiteralTypeIdentifierCS();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_24_);
			}
			else
			{
				throw ex;
			}
		}
		return typeIdentifier;
	}
	
/*********************************************
tuplePartsCS :
	tuplePartCS
	(COMMA tuplePartCS)*
		;
**********************************************/
	public List<object>  tuplePartsListCS() //throws RecognitionException, TokenStreamException
{
		List<object> tuplePartsList;
		
		
			tuplePartsList = new List<object>();
			CSTVariableDeclarationCS declaration = null;
		
		
		try {      // for error handling
			declaration=tuplePartCS();
			if (0==inputState.guessing)
			{
				tuplePartsList.Add(declaration);
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==COMMA))
					{
						match(COMMA);
						declaration=tuplePartCS();
						if (0==inputState.guessing)
						{
							tuplePartsList.Add(declaration);
						}
					}
					else
					{
						goto _loop229_breakloop;
					}
					
				}
_loop229_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_11_);
			}
			else
			{
				throw ex;
			}
		}
		return tuplePartsList;
	}
	
/*********************************************
**********************************************/
	public CSTVariableDeclarationCS  tuplePartCS() //throws RecognitionException, TokenStreamException
{
		CSTVariableDeclarationCS variableDeclaration;
		
		
			variableDeclaration = null;
			CSTNameCS name = null;
			CSTTypeCS type = null;
		
		
		try {      // for error handling
			name=simpleNameCS();
			match(COLON);
			type=typeCS();
			if (0==inputState.guessing)
			{
				variableDeclaration = new CSTVariableDeclarationCS(name, type, null);
			}
		}
		catch (RecognitionException ex)
		{
			if (0 == inputState.guessing)
			{
				reportError(ex);
				recover(ex,tokenSet_12_);
			}
			else
			{
				throw ex;
			}
		}
		return variableDeclaration;
	}
	
	private void initializeFactory()
	{
	}
	
	public static readonly string[] tokenNames_ = new string[] {
		@"""<0>""",
		@"""EOF""",
		@"""<2>""",
		@"""NULL_TREE_LOOKAHEAD""",
		@"""package""",
		@"""endpackage""",
		@"""context""",
		@"""':'""",
		@"""init""",
		@"""derive""",
		@"""inv""",
		@"""def""",
		@"""'='""",
		@"""pre""",
		@"""post""",
		@"""body""",
		@"""'('""",
		@"""')'""",
		@"""','""",
		@"""let""",
		@"""in""",
		@"""'->'""",
		@"""iterate""",
		@"""implies""",
		@"""and""",
		@"""or""",
		@"""xor""",
		@"""'<'""",
		@"""'>'""",
		@"""'<='""",
		@"""'>='""",
		@"""'<>'""",
		@"""'+'""",
		@"""'-'""",
		@"""'*'""",
		@"""'/'""",
		@"""not""",
		@"""if""",
		@"""then""",
		@"""else""",
		@"""endif""",
		@"""'.'""",
		@"""'['""",
		@"""']'""",
		@"""'@'""",
		@"""'|'""",
		@"""';'""",
		@"""exists""",
		@"""forAll""",
		@"""isUnique""",
		@"""any""",
		@"""one""",
		@"""collect""",
		@"""select""",
		@"""reject""",
		@"""collectNested""",
		@"""sortedBy""",
		@"""INT_NUMBER""",
		@"""REAL_NUMBER""",
		@"""STRING""",
		@"""true""",
		@"""false""",
		@"""null""",
		@"""invalid""",
		@"""'{'""",
		@"""'}'""",
		@"""'..'""",
		@"""Tuple""",
		@"""Set""",
		@"""Bag""",
		@"""Sequence""",
		@"""OrderedSet""",
		@"""Collection""",
		@"""an identifier""",
		@"""'::'""",
		@"""attr""",
		@"""oper""",
		@"""actionBody""",
		@"""undefined""",
		@"""goto""",
		@"""return""",
		@"""delete""",
		@"""begin""",
		@"""end""",
		@"""do""",
		@"""doif""",
		@"""repeat""",
		@"""while""",
		@"""until""",
		@"""to""",
		@"""downto""",
		@"""for""",
		@"""foreach""",
		@"""raise""",
		@"""create""",
		@"""var""",
		@"""const""",
		@"""break""",
		@"""continue""",
		@"""step""",
		@"""modifiable""",
		@"""links""",
		@"""'^^'""",
		@"""'^'""",
		@"""'?'""",
		@"""'#'""",
		@"""'''""",
		@"""':='""",
		@"""WHITE_SPACE""",
		@"""SINGLE_LINE_COMMENT""",
		@"""MULTI_LINE_COMMENT""",
		@"""RANGE_OR_INT""",
		@"""LF""",
		@"""CR""",
		@"""CRLF""",
		@"""NEW_LINE""",
		@"""TAB""",
		@"""BLANK""",
		@"""NEW_PAGE""",
		@"""UNDERSCORE""",
		@"""DIGIT""",
		@"""NUMBER""",
		@"""BETWEEN_ZERO_AND_THREE""",
		@"""BETWEEN_FOUR_AND_SEVEN""",
		@"""OCTAL_DIGIT""",
		@"""HEXA_DIGIT""",
		@"""TWO_DIGIT_OCTAL_NUMBER""",
		@"""THREE_DIGIT_OCTAL_NUMBER""",
		@"""HEXA_NUMBER""",
		@"""OCTAL_ESCAPE""",
		@"""HEXA_ESCAPE""",
		@"""SIMPLE_ESCAPE""",
		@"""ESCAPE_SEQUENCE""",
		@"""UPPER_CHAR""",
		@"""LOWER_CHAR""",
		@"""LETTER""",
		@"""ANY_CHAR""",
		@"""ANY_ELEMENT""",
		@"""COMMENT_INIT""",
		@"""REST_OF_LINE"""
	};
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 2L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { 82L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = { 114L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	private static long[] mk_tokenSet_3_()
	{
		long[] data = { 105553116790626L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_3_ = new BitSet(mk_tokenSet_3_());
	private static long[] mk_tokenSet_4_()
	{
		long[] data = { 136133278695410L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_4_ = new BitSet(mk_tokenSet_4_());
	private static long[] mk_tokenSet_5_()
	{
		long[] data = { 105553116723970L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_5_ = new BitSet(mk_tokenSet_5_());
	private static long[] mk_tokenSet_6_()
	{
		long[] data = { 60530L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_6_ = new BitSet(mk_tokenSet_6_());
	private static long[] mk_tokenSet_7_()
	{
		long[] data = { 3186L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_7_ = new BitSet(mk_tokenSet_7_());
	private static long[] mk_tokenSet_8_()
	{
		long[] data = { 140531325206514L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_8_ = new BitSet(mk_tokenSet_8_());
	private static long[] mk_tokenSet_9_()
	{
		long[] data = { 63488L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_9_ = new BitSet(mk_tokenSet_9_());
	private static long[] mk_tokenSet_10_()
	{
		long[] data = { 57458L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_10_ = new BitSet(mk_tokenSet_10_());
	private static long[] mk_tokenSet_11_()
	{
		long[] data = { 131072L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_11_ = new BitSet(mk_tokenSet_11_());
	private static long[] mk_tokenSet_12_()
	{
		long[] data = { 393216L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_12_ = new BitSet(mk_tokenSet_12_());
	private static long[] mk_tokenSet_13_()
	{
		long[] data = { 45904611961970L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_13_ = new BitSet(mk_tokenSet_13_());
	private static long[] mk_tokenSet_14_()
	{
		long[] data = { 35184373399552L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_14_ = new BitSet(mk_tokenSet_14_());
	private static long[] mk_tokenSet_15_()
	{
		long[] data = { 45904620350578L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_15_ = new BitSet(mk_tokenSet_15_());
	private static long[] mk_tokenSet_16_()
	{
		long[] data = { -144114973327425536L, 760L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_16_ = new BitSet(mk_tokenSet_16_());
	private static long[] mk_tokenSet_17_()
	{
		long[] data = { 45904737791090L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_17_ = new BitSet(mk_tokenSet_17_());
	private static long[] mk_tokenSet_18_()
	{
		long[] data = { 45906885278834L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_18_ = new BitSet(mk_tokenSet_18_());
	private static long[] mk_tokenSet_19_()
	{
		long[] data = { 45908898544754L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_19_ = new BitSet(mk_tokenSet_19_());
	private static long[] mk_tokenSet_20_()
	{
		long[] data = { 45921783446642L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_20_ = new BitSet(mk_tokenSet_20_());
	private static long[] mk_tokenSet_21_()
	{
		long[] data = { 45973323054194L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_21_ = new BitSet(mk_tokenSet_21_());
	private static long[] mk_tokenSet_22_()
	{
		long[] data = { -144115050636836864L, 760L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_22_ = new BitSet(mk_tokenSet_22_());
	private static long[] mk_tokenSet_23_()
	{
		long[] data = { 48172348406898L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_23_ = new BitSet(mk_tokenSet_23_());
	private static long[] mk_tokenSet_24_()
	{
		long[] data = { 65536L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_24_ = new BitSet(mk_tokenSet_24_());
	private static long[] mk_tokenSet_25_()
	{
		long[] data = { -144114973326901248L, 760L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_25_ = new BitSet(mk_tokenSet_25_());
	private static long[] mk_tokenSet_26_()
	{
		long[] data = { -144090723947704320L, 1785L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_26_ = new BitSet(mk_tokenSet_26_());
	private static long[] mk_tokenSet_27_()
	{
		long[] data = { 0L, 512L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_27_ = new BitSet(mk_tokenSet_27_());
	private static long[] mk_tokenSet_28_()
	{
		long[] data = { -144114973326761984L, 760L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_28_ = new BitSet(mk_tokenSet_28_());
	private static long[] mk_tokenSet_29_()
	{
		long[] data = { 70162580962418L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_29_ = new BitSet(mk_tokenSet_29_());
	private static long[] mk_tokenSet_30_()
	{
		long[] data = { -74766790689550L, 766L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_30_ = new BitSet(mk_tokenSet_30_());
	private static long[] mk_tokenSet_31_()
	{
		long[] data = { -144115188075855872L, 248L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_31_ = new BitSet(mk_tokenSet_31_());
	private static long[] mk_tokenSet_32_()
	{
		long[] data = { 48172348472434L, 6L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_32_ = new BitSet(mk_tokenSet_32_());
	private static long[] mk_tokenSet_33_()
	{
		long[] data = { 8796093153280L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_33_ = new BitSet(mk_tokenSet_33_());
	private static long[] mk_tokenSet_34_()
	{
		long[] data = { 105553116266496L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_34_ = new BitSet(mk_tokenSet_34_());
	private static long[] mk_tokenSet_35_()
	{
		long[] data = { 262146L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_35_ = new BitSet(mk_tokenSet_35_());
	private static long[] mk_tokenSet_36_()
	{
		long[] data = { 262144L, 2L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_36_ = new BitSet(mk_tokenSet_36_());
	private static long[] mk_tokenSet_37_()
	{
		long[] data = { 65536L, 1L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_37_ = new BitSet(mk_tokenSet_37_());
	private static long[] mk_tokenSet_38_()
	{
		long[] data = { 0L, 2L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_38_ = new BitSet(mk_tokenSet_38_());
	
}
}
