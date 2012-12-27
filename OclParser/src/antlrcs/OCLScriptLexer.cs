// $ANTLR 2.7.7 (20060930): "OCLSCRIPT.G" -> "OCLScriptLexer.cs"$

	using System.Collections.Generic;
	using OclParser.controller;
	using OclParser.cst.context;
	using OclParser.cst.expression;
	using OclParser.cst.literalExp;
	using OclParser.cst.name;
	using OclParser.cst.type;

namespace oclparser.antlrcs
{
	// Generate header specific to lexer CSharp file
	using System;
	using Stream                          = System.IO.Stream;
	using TextReader                      = System.IO.TextReader;
	using Hashtable                       = System.Collections.Hashtable;
	using Comparer                        = System.Collections.Comparer;
	
	using TokenStreamException            = antlr.TokenStreamException;
	using TokenStreamIOException          = antlr.TokenStreamIOException;
	using TokenStreamRecognitionException = antlr.TokenStreamRecognitionException;
	using CharStreamException             = antlr.CharStreamException;
	using CharStreamIOException           = antlr.CharStreamIOException;
	using ANTLRException                  = antlr.ANTLRException;
	using CharScanner                     = antlr.CharScanner;
	using InputBuffer                     = antlr.InputBuffer;
	using ByteBuffer                      = antlr.ByteBuffer;
	using CharBuffer                      = antlr.CharBuffer;
	using Token                           = antlr.Token;
	using IToken                          = antlr.IToken;
	using CommonToken                     = antlr.CommonToken;
	using SemanticException               = antlr.SemanticException;
	using RecognitionException            = antlr.RecognitionException;
	using NoViableAltForCharException     = antlr.NoViableAltForCharException;
	using MismatchedCharException         = antlr.MismatchedCharException;
	using TokenStream                     = antlr.TokenStream;
	using LexerSharedInputState           = antlr.LexerSharedInputState;
	using BitSet                          = antlr.collections.impl.BitSet;
	
	public 	class OCLScriptLexer : antlr.CharScanner	, TokenStream
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
		
		public OCLScriptLexer(Stream ins) : this(new ByteBuffer(ins))
		{
		}
		
		public OCLScriptLexer(TextReader r) : this(new CharBuffer(r))
		{
		}
		
		public OCLScriptLexer(InputBuffer ib)		 : this(new LexerSharedInputState(ib))
		{
		}
		
		public OCLScriptLexer(LexerSharedInputState state) : base(state)
		{
			initialize();
		}
		private void initialize()
		{
			caseSensitiveLiterals = true;
			setCaseSensitive(true);
			literals = new Hashtable(100, (float) 0.4, null, Comparer.Default);
			literals.Add("invalid", 63);
			literals.Add("oper", 76);
			literals.Add("Sequence", 70);
			literals.Add("init", 8);
			literals.Add("break", 97);
			literals.Add("while", 87);
			literals.Add("repeat", 86);
			literals.Add("delete", 81);
			literals.Add("endif", 40);
			literals.Add("end", 83);
			literals.Add("Collection", 72);
			literals.Add("attr", 75);
			literals.Add("one", 51);
			literals.Add("then", 38);
			literals.Add("raise", 93);
			literals.Add("select", 53);
			literals.Add("until", 88);
			literals.Add("post", 14);
			literals.Add("to", 89);
			literals.Add("and", 24);
			literals.Add("const", 96);
			literals.Add("derive", 9);
			literals.Add("OrderedSet", 71);
			literals.Add("not", 36);
			literals.Add("package", 4);
			literals.Add("return", 80);
			literals.Add("foreach", 92);
			literals.Add("context", 6);
			literals.Add("var", 95);
			literals.Add("sortedBy", 56);
			literals.Add("null", 62);
			literals.Add("def", 11);
			literals.Add("pre", 13);
			literals.Add("do", 84);
			literals.Add("body", 15);
			literals.Add("links", 101);
			literals.Add("modifiable", 100);
			literals.Add("Set", 68);
			literals.Add("implies", 23);
			literals.Add("inv", 10);
			literals.Add("or", 25);
			literals.Add("any", 50);
			literals.Add("create", 94);
			literals.Add("if", 37);
			literals.Add("collectNested", 55);
			literals.Add("xor", 26);
			literals.Add("forAll", 48);
			literals.Add("isUnique", 49);
			literals.Add("goto", 79);
			literals.Add("iterate", 22);
			literals.Add("for", 91);
			literals.Add("collect", 52);
			literals.Add("actionBody", 77);
			literals.Add("downto", 90);
			literals.Add("false", 61);
			literals.Add("reject", 54);
			literals.Add("exists", 47);
			literals.Add("undefined", 78);
			literals.Add("doif", 85);
			literals.Add("continue", 98);
			literals.Add("begin", 82);
			literals.Add("Tuple", 67);
			literals.Add("else", 39);
			literals.Add("Bag", 69);
			literals.Add("in", 20);
			literals.Add("let", 19);
			literals.Add("step", 99);
			literals.Add("true", 60);
			literals.Add("endpackage", 5);
		}
		
		override public IToken nextToken()			//throws TokenStreamException
		{
			IToken theRetToken = null;
tryAgain:
			for (;;)
			{
				IToken _token = null;
				int _ttype = Token.INVALID_TYPE;
				resetText();
				try     // for char stream error handling
				{
					try     // for lexical error handling
					{
						switch ( cached_LA1 )
						{
						case '?':
						{
							mOP_QUOTATION(true);
							theRetToken = returnToken_;
							break;
						}
						case '*':
						{
							mOP_MULTIPLY(true);
							theRetToken = returnToken_;
							break;
						}
						case '+':
						{
							mOP_PLUS(true);
							theRetToken = returnToken_;
							break;
						}
						case '=':
						{
							mOP_EQUAL(true);
							theRetToken = returnToken_;
							break;
						}
						case '(':
						{
							mLEFT_PAR(true);
							theRetToken = returnToken_;
							break;
						}
						case ')':
						{
							mRIGHT_PAR(true);
							theRetToken = returnToken_;
							break;
						}
						case '[':
						{
							mLEFT_BRACKET(true);
							theRetToken = returnToken_;
							break;
						}
						case ']':
						{
							mRIGHT_BRACKET(true);
							theRetToken = returnToken_;
							break;
						}
						case '{':
						{
							mLEFT_BRACE(true);
							theRetToken = returnToken_;
							break;
						}
						case '}':
						{
							mRIGHT_BRACE(true);
							theRetToken = returnToken_;
							break;
						}
						case ',':
						{
							mCOMMA(true);
							theRetToken = returnToken_;
							break;
						}
						case '#':
						{
							mCHANNEL(true);
							theRetToken = returnToken_;
							break;
						}
						case '@':
						{
							mAT(true);
							theRetToken = returnToken_;
							break;
						}
						case '|':
						{
							mVERT_BAR(true);
							theRetToken = returnToken_;
							break;
						}
						case ';':
						{
							mSEMI_COLON(true);
							theRetToken = returnToken_;
							break;
						}
						case '\t':  case '\n':  case '\u000c':  case '\r':
						case ' ':
						{
							mWHITE_SPACE(true);
							theRetToken = returnToken_;
							break;
						}
						case '0':  case '1':  case '2':  case '3':
						case '4':  case '5':  case '6':  case '7':
						case '8':  case '9':
						{
							mRANGE_OR_INT(true);
							theRetToken = returnToken_;
							break;
						}
						case 'A':  case 'B':  case 'C':  case 'D':
						case 'E':  case 'F':  case 'G':  case 'H':
						case 'I':  case 'J':  case 'K':  case 'L':
						case 'M':  case 'N':  case 'O':  case 'P':
						case 'Q':  case 'R':  case 'S':  case 'T':
						case 'U':  case 'V':  case 'W':  case 'X':
						case 'Y':  case 'Z':  case '_':  case 'a':
						case 'b':  case 'c':  case 'd':  case 'e':
						case 'f':  case 'g':  case 'h':  case 'i':
						case 'j':  case 'k':  case 'l':  case 'm':
						case 'n':  case 'o':  case 'p':  case 'q':
						case 'r':  case 's':  case 't':  case 'u':
						case 'v':  case 'w':  case 'x':  case 'y':
						case 'z':
						{
							mIDENT(true);
							theRetToken = returnToken_;
							break;
						}
						default:
							if ((cached_LA1=='-') && (cached_LA2=='>'))
							{
								mOP_ARROW(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1==':') && (cached_LA2==':')) {
								mOP_DOUBLE_COLON(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='^') && (cached_LA2=='^')) {
								mOP_DBL_MESSAGE(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='<') && (cached_LA2=='>')) {
								mOP_NOTEQUAL(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='<') && (cached_LA2=='=')) {
								mOP_LESS_OR_EQ(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='>') && (cached_LA2=='=')) {
								mOP_GREATER_OR_EQ(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='.') && (cached_LA2=='.')) {
								mRANGE(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1==':') && (cached_LA2=='=')) {
								mOP_ASSIGNMENT(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='-'||cached_LA1=='/') && (cached_LA2=='-'||cached_LA2=='/')) {
								mSINGLE_LINE_COMMENT(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='/') && (cached_LA2=='*')) {
								mMULTI_LINE_COMMENT(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='"'||cached_LA1=='\'') && ((cached_LA2 >= '\u0003' && cached_LA2 <= '\u00ff'))) {
								mSTRING(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='.') && (true)) {
								mOP_DOT(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='^') && (true)) {
								mOP_MESSAGE(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='/') && (true)) {
								mOP_DIVIDE(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='-') && (true)) {
								mOP_MINUS(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='<') && (true)) {
								mOP_LESS_THAN(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='>') && (true)) {
								mOP_GREATER_THAN(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1==':') && (true)) {
								mCOLON(true);
								theRetToken = returnToken_;
							}
							else if ((cached_LA1=='\'') && (true)) {
								mAPOSTROPH(true);
								theRetToken = returnToken_;
							}
						else
						{
							if (cached_LA1==EOF_CHAR) { uponEOF(); returnToken_ = makeToken(Token.EOF_TYPE); }
				else {throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());}
						}
						break; }
						if ( null==returnToken_ ) goto tryAgain; // found SKIP token
						_ttype = returnToken_.Type;
						_ttype = testLiteralsTable(_ttype);
						returnToken_.Type = _ttype;
						return returnToken_;
					}
					catch (RecognitionException e) {
						{
							reportError(e);
							consume();
						}
					}
				}
				catch (CharStreamException cse) {
					if ( cse is CharStreamIOException ) {
						throw new TokenStreamIOException(((CharStreamIOException)cse).io);
					}
					else {
						throw new TokenStreamException(cse.Message);
					}
				}
			}
		}
		
	public void mOP_DOT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_DOT;
		
		try {      // for error handling
			match(".");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_ARROW(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_ARROW;
		
		try {      // for error handling
			match("->");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_DOUBLE_COLON(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_DOUBLE_COLON;
		
		try {      // for error handling
			match("::");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_DBL_MESSAGE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_DBL_MESSAGE;
		
		try {      // for error handling
			match("^^");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_MESSAGE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_MESSAGE;
		
		try {      // for error handling
			match("^");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_QUOTATION(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_QUOTATION;
		
		try {      // for error handling
			match("?");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_MULTIPLY(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_MULTIPLY;
		
		try {      // for error handling
			match("*");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_DIVIDE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_DIVIDE;
		
		try {      // for error handling
			match("/");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_PLUS(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_PLUS;
		
		try {      // for error handling
			match("+");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_MINUS(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_MINUS;
		
		try {      // for error handling
			match("-");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_EQUAL(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_EQUAL;
		
		try {      // for error handling
			match("=");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_NOTEQUAL(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_NOTEQUAL;
		
		try {      // for error handling
			match("<>");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_LESS_THAN(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_LESS_THAN;
		
		try {      // for error handling
			match("<");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_GREATER_THAN(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_GREATER_THAN;
		
		try {      // for error handling
			match(">");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_LESS_OR_EQ(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_LESS_OR_EQ;
		
		try {      // for error handling
			match("<=");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_GREATER_OR_EQ(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_GREATER_OR_EQ;
		
		try {      // for error handling
			match(">=");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mLEFT_PAR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LEFT_PAR;
		
		try {      // for error handling
			match("(");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mRIGHT_PAR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = RIGHT_PAR;
		
		try {      // for error handling
			match(")");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mLEFT_BRACKET(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LEFT_BRACKET;
		
		try {      // for error handling
			match("[");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mRIGHT_BRACKET(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = RIGHT_BRACKET;
		
		try {      // for error handling
			match("]");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mLEFT_BRACE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LEFT_BRACE;
		
		try {      // for error handling
			match("{");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mRIGHT_BRACE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = RIGHT_BRACE;
		
		try {      // for error handling
			match("}");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mCOLON(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = COLON;
		
		try {      // for error handling
			match(":");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mCOMMA(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = COMMA;
		
		try {      // for error handling
			match(",");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mCHANNEL(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = CHANNEL;
		
		try {      // for error handling
			match('#');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mAT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = AT;
		
		try {      // for error handling
			match("@");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mVERT_BAR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = VERT_BAR;
		
		try {      // for error handling
			match("|");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mRANGE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = RANGE;
		
		try {      // for error handling
			match("..");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mAPOSTROPH(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = APOSTROPH;
		
		try {      // for error handling
			match("'");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mSEMI_COLON(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = SEMI_COLON;
		
		try {      // for error handling
			match(";");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mOP_ASSIGNMENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OP_ASSIGNMENT;
		
		try {      // for error handling
			match(":=");
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mWHITE_SPACE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = WHITE_SPACE;
		
		try {      // for error handling
			{
				switch ( cached_LA1 )
				{
				case '\n':  case '\r':
				{
					mNEW_LINE(false);
					break;
				}
				case '\u000c':
				{
					mNEW_PAGE(false);
					break;
				}
				case '\t':  case ' ':
				{
					mBLANK(false);
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
			}
			if (0==inputState.guessing)
			{
				_ttype = Token.SKIP;
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mNEW_LINE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NEW_LINE;
		
		try {      // for error handling
			{
				if ((cached_LA1=='\r') && (cached_LA2=='\n'))
				{
					mCRLF(false);
				}
				else if ((cached_LA1=='\n')) {
					mLF(false);
				}
				else if ((cached_LA1=='\r') && (true)) {
					mCR(false);
				}
				else
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				
			}
			if (0==inputState.guessing)
			{
				newline();
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mNEW_PAGE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NEW_PAGE;
		
		try {      // for error handling
			match('\f');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mBLANK(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = BLANK;
		
		try {      // for error handling
			{
				switch ( cached_LA1 )
				{
				case ' ':
				{
					match(' ');
					break;
				}
				case '\t':
				{
					mTAB(false);
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mSINGLE_LINE_COMMENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = SINGLE_LINE_COMMENT;
		
		try {      // for error handling
			mCOMMENT_INIT(false);
			mREST_OF_LINE(false);
			if (0==inputState.guessing)
			{
				_ttype = Token.SKIP;
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mCOMMENT_INIT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = COMMENT_INIT;
		
		try {      // for error handling
			{
				switch ( cached_LA1 )
				{
				case '/':
				{
					match("//");
					break;
				}
				case '-':
				{
					match("--");
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mREST_OF_LINE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = REST_OF_LINE;
		
		try {      // for error handling
			{    // ( ... )*
				for (;;)
				{
					if ((tokenSet_1_.member(cached_LA1)))
					{
						mANY_CHAR(false);
					}
					else
					{
						goto _loop353_breakloop;
					}
					
				}
_loop353_breakloop:				;
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mMULTI_LINE_COMMENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = MULTI_LINE_COMMENT;
		
		try {      // for error handling
			match("/*");
			{    // ( ... )*
				for (;;)
				{
					if ((cached_LA1=='\r') && (cached_LA2=='\n') && ((LA(3) >= '\u0003' && LA(3) <= '\u00ff')) && ((LA(4) >= '\u0003' && LA(4) <= '\u00ff')))
					{
						match('\r');
						match('\n');
						if (0==inputState.guessing)
						{
							newline();
						}
					}
					else if (((cached_LA1=='*') && ((cached_LA2 >= '\u0003' && cached_LA2 <= '\u00ff')) && ((LA(3) >= '\u0003' && LA(3) <= '\u00ff')))&&( LA(2)!='/' )) {
						match('*');
					}
					else if ((cached_LA1=='\r') && ((cached_LA2 >= '\u0003' && cached_LA2 <= '\u00ff')) && ((LA(3) >= '\u0003' && LA(3) <= '\u00ff')) && (true)) {
						match('\r');
						if (0==inputState.guessing)
						{
							newline();
						}
					}
					else if ((cached_LA1=='\n')) {
						match('\n');
						if (0==inputState.guessing)
						{
							newline();
						}
					}
					else if ((tokenSet_2_.member(cached_LA1))) {
						{
							match(tokenSet_2_);
						}
					}
					else
					{
						goto _loop275_breakloop;
					}
					
				}
_loop275_breakloop:				;
			}    // ( ... )*
			match("*/");
			if (0==inputState.guessing)
			{
				_ttype = Token.SKIP;
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mRANGE_OR_INT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = RANGE_OR_INT;
		
		try {      // for error handling
			bool synPredMatched278 = false;
			if ((((cached_LA1 >= '0' && cached_LA1 <= '9')) && (true) && (true) && (true)))
			{
				int _m278 = mark();
				synPredMatched278 = true;
				inputState.guessing++;
				try {
					{
						mINT_NUMBER(false);
						match("..");
					}
				}
				catch (RecognitionException)
				{
					synPredMatched278 = false;
				}
				rewind(_m278);
				inputState.guessing--;
			}
			if ( synPredMatched278 )
			{
				mINT_NUMBER(false);
				if (0==inputState.guessing)
				{
					_ttype = INT_NUMBER;
				}
			}
			else {
				bool synPredMatched280 = false;
				if ((((cached_LA1 >= '0' && cached_LA1 <= '9')) && (true) && (true) && (true)))
				{
					int _m280 = mark();
					synPredMatched280 = true;
					inputState.guessing++;
					try {
						{
							mINT_NUMBER(false);
							match('.');
							mINT_NUMBER(false);
						}
					}
					catch (RecognitionException)
					{
						synPredMatched280 = false;
					}
					rewind(_m280);
					inputState.guessing--;
				}
				if ( synPredMatched280 )
				{
					mREAL_NUMBER(false);
					if (0==inputState.guessing)
					{
						_ttype = REAL_NUMBER;
					}
				}
				else {
					bool synPredMatched283 = false;
					if ((((cached_LA1 >= '0' && cached_LA1 <= '9')) && (true) && (true) && (true)))
					{
						int _m283 = mark();
						synPredMatched283 = true;
						inputState.guessing++;
						try {
							{
								mINT_NUMBER(false);
								{
									switch ( cached_LA1 )
									{
									case 'e':
									{
										match('e');
										break;
									}
									case 'E':
									{
										match('E');
										break;
									}
									default:
									{
										throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
									}
									 }
								}
							}
						}
						catch (RecognitionException)
						{
							synPredMatched283 = false;
						}
						rewind(_m283);
						inputState.guessing--;
					}
					if ( synPredMatched283 )
					{
						mREAL_NUMBER(false);
						if (0==inputState.guessing)
						{
							_ttype = REAL_NUMBER;
						}
					}
					else if (((cached_LA1 >= '0' && cached_LA1 <= '9')) && (true) && (true) && (true)) {
						mINT_NUMBER(false);
						if (0==inputState.guessing)
						{
							_ttype = INT_NUMBER;
						}
					}
					else
					{
						throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
					}
					}}
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
				if (_createToken && (null == _token) && (_ttype != Token.SKIP))
				{
					_token = makeToken(_ttype);
					_token.setText(text.ToString(_begin, text.Length-_begin));
				}
				returnToken_ = _token;
			}
			
	protected void mINT_NUMBER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = INT_NUMBER;
		
		try {      // for error handling
			mNUMBER(false);
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mREAL_NUMBER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = REAL_NUMBER;
		
		try {      // for error handling
			mINT_NUMBER(false);
			{
				if ((cached_LA1=='.'))
				{
					match('.');
					mINT_NUMBER(false);
				}
				else {
				}
				
			}
			{
				if ((cached_LA1=='E'||cached_LA1=='e'))
				{
					{
						switch ( cached_LA1 )
						{
						case 'e':
						{
							match('e');
							break;
						}
						case 'E':
						{
							match('E');
							break;
						}
						default:
						{
							throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
						}
						 }
					}
					{
						switch ( cached_LA1 )
						{
						case '+':
						{
							match('+');
							break;
						}
						case '-':
						{
							match('-');
							break;
						}
						case '0':  case '1':  case '2':  case '3':
						case '4':  case '5':  case '6':  case '7':
						case '8':  case '9':
						{
							break;
						}
						default:
						{
							throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
						}
						 }
					}
					mINT_NUMBER(false);
				}
				else {
				}
				
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mSTRING(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = STRING;
		
		try {      // for error handling
			switch ( cached_LA1 )
			{
			case '"':
			{
				{
					match('"');
					{    // ( ... )*
						for (;;)
						{
							if ((cached_LA1=='\\'))
							{
								mESCAPE_SEQUENCE(false);
							}
							else if ((tokenSet_4_.member(cached_LA1))) {
								{
									match(tokenSet_4_);
								}
							}
							else
							{
								goto _loop288_breakloop;
							}
							
						}
_loop288_breakloop:						;
					}    // ( ... )*
					match('"');
				}
				break;
			}
			case '\'':
			{
				{
					match('\'');
					{    // ( ... )*
						for (;;)
						{
							if ((cached_LA1=='\\'))
							{
								mESCAPE_SEQUENCE(false);
							}
							else if ((tokenSet_5_.member(cached_LA1))) {
								{
									match(tokenSet_5_);
								}
							}
							else
							{
								goto _loop292_breakloop;
							}
							
						}
_loop292_breakloop:						;
					}    // ( ... )*
					match('\'');
				}
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mESCAPE_SEQUENCE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = ESCAPE_SEQUENCE;
		
		try {      // for error handling
			if ((cached_LA1=='\\') && (tokenSet_6_.member(cached_LA2)))
			{
				mSIMPLE_ESCAPE(false);
			}
			else if ((cached_LA1=='\\') && ((cached_LA2 >= '0' && cached_LA2 <= '7'))) {
				mOCTAL_ESCAPE(false);
			}
			else if ((cached_LA1=='\\') && (cached_LA2=='x')) {
				mHEXA_ESCAPE(false);
			}
			else
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mIDENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = IDENT;
		
		try {      // for error handling
			{
				switch ( cached_LA1 )
				{
				case 'A':  case 'B':  case 'C':  case 'D':
				case 'E':  case 'F':  case 'G':  case 'H':
				case 'I':  case 'J':  case 'K':  case 'L':
				case 'M':  case 'N':  case 'O':  case 'P':
				case 'Q':  case 'R':  case 'S':  case 'T':
				case 'U':  case 'V':  case 'W':  case 'X':
				case 'Y':  case 'Z':  case 'a':  case 'b':
				case 'c':  case 'd':  case 'e':  case 'f':
				case 'g':  case 'h':  case 'i':  case 'j':
				case 'k':  case 'l':  case 'm':  case 'n':
				case 'o':  case 'p':  case 'q':  case 'r':
				case 's':  case 't':  case 'u':  case 'v':
				case 'w':  case 'x':  case 'y':  case 'z':
				{
					mLETTER(false);
					break;
				}
				case '_':
				{
					mUNDERSCORE(false);
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
			}
			{    // ( ... )*
				for (;;)
				{
					switch ( cached_LA1 )
					{
					case 'A':  case 'B':  case 'C':  case 'D':
					case 'E':  case 'F':  case 'G':  case 'H':
					case 'I':  case 'J':  case 'K':  case 'L':
					case 'M':  case 'N':  case 'O':  case 'P':
					case 'Q':  case 'R':  case 'S':  case 'T':
					case 'U':  case 'V':  case 'W':  case 'X':
					case 'Y':  case 'Z':  case 'a':  case 'b':
					case 'c':  case 'd':  case 'e':  case 'f':
					case 'g':  case 'h':  case 'i':  case 'j':
					case 'k':  case 'l':  case 'm':  case 'n':
					case 'o':  case 'p':  case 'q':  case 'r':
					case 's':  case 't':  case 'u':  case 'v':
					case 'w':  case 'x':  case 'y':  case 'z':
					{
						mLETTER(false);
						break;
					}
					case '0':  case '1':  case '2':  case '3':
					case '4':  case '5':  case '6':  case '7':
					case '8':  case '9':
					{
						mDIGIT(false);
						break;
					}
					case '_':
					{
						mUNDERSCORE(false);
						break;
					}
					default:
					{
						goto _loop296_breakloop;
					}
					 }
				}
_loop296_breakloop:				;
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
		_ttype = testLiteralsTable(_ttype);
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLETTER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LETTER;
		
		try {      // for error handling
			{
				switch ( cached_LA1 )
				{
				case 'A':  case 'B':  case 'C':  case 'D':
				case 'E':  case 'F':  case 'G':  case 'H':
				case 'I':  case 'J':  case 'K':  case 'L':
				case 'M':  case 'N':  case 'O':  case 'P':
				case 'Q':  case 'R':  case 'S':  case 'T':
				case 'U':  case 'V':  case 'W':  case 'X':
				case 'Y':  case 'Z':
				{
					mUPPER_CHAR(false);
					break;
				}
				case 'a':  case 'b':  case 'c':  case 'd':
				case 'e':  case 'f':  case 'g':  case 'h':
				case 'i':  case 'j':  case 'k':  case 'l':
				case 'm':  case 'n':  case 'o':  case 'p':
				case 'q':  case 'r':  case 's':  case 't':
				case 'u':  case 'v':  case 'w':  case 'x':
				case 'y':  case 'z':
				{
					mLOWER_CHAR(false);
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNDERSCORE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNDERSCORE;
		
		try {      // for error handling
			match('_');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mDIGIT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = DIGIT;
		
		try {      // for error handling
			{
				matchRange('0','9');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLF(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LF;
		
		try {      // for error handling
			match('\n');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mCR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = CR;
		
		try {      // for error handling
			match('\r');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mCRLF(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = CRLF;
		
		try {      // for error handling
			match('\r');
			match('\n');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mTAB(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = TAB;
		
		try {      // for error handling
			match('\t');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mNUMBER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NUMBER;
		
		try {      // for error handling
			{ // ( ... )+
				int _cnt311=0;
				for (;;)
				{
					if (((cached_LA1 >= '0' && cached_LA1 <= '9')))
					{
						mDIGIT(false);
					}
					else
					{
						if (_cnt311 >= 1) { goto _loop311_breakloop; } else { throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());; }
					}
					
					_cnt311++;
				}
_loop311_breakloop:				;
			}    // ( ... )+
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mBETWEEN_ZERO_AND_THREE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = BETWEEN_ZERO_AND_THREE;
		
		try {      // for error handling
			{
				matchRange('0','3');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mBETWEEN_FOUR_AND_SEVEN(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = BETWEEN_FOUR_AND_SEVEN;
		
		try {      // for error handling
			{
				matchRange('4','7');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mOCTAL_DIGIT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OCTAL_DIGIT;
		
		try {      // for error handling
			{
				matchRange('0','7');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mHEXA_DIGIT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = HEXA_DIGIT;
		
		try {      // for error handling
			{
				switch ( cached_LA1 )
				{
				case '0':  case '1':  case '2':  case '3':
				case '4':  case '5':  case '6':  case '7':
				case '8':  case '9':
				{
					mDIGIT(false);
					break;
				}
				case 'a':  case 'b':  case 'c':  case 'd':
				case 'e':  case 'f':
				{
					{
						matchRange('a','f');
					}
					break;
				}
				case 'A':  case 'B':  case 'C':  case 'D':
				case 'E':  case 'F':
				{
					{
						matchRange('A','F');
					}
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mTWO_DIGIT_OCTAL_NUMBER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = TWO_DIGIT_OCTAL_NUMBER;
		
		try {      // for error handling
			mBETWEEN_ZERO_AND_THREE(false);
			mOCTAL_DIGIT(false);
			mOCTAL_DIGIT(false);
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mTHREE_DIGIT_OCTAL_NUMBER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = THREE_DIGIT_OCTAL_NUMBER;
		
		try {      // for error handling
			mBETWEEN_FOUR_AND_SEVEN(false);
			mOCTAL_DIGIT(false);
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mHEXA_NUMBER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = HEXA_NUMBER;
		
		try {      // for error handling
			mHEXA_DIGIT(false);
			mHEXA_DIGIT(false);
			mHEXA_DIGIT(false);
			mHEXA_DIGIT(false);
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mOCTAL_ESCAPE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OCTAL_ESCAPE;
		
		try {      // for error handling
			match('\\');
			{
				switch ( cached_LA1 )
				{
				case '0':  case '1':  case '2':  case '3':
				{
					mTWO_DIGIT_OCTAL_NUMBER(false);
					break;
				}
				case '4':  case '5':  case '6':  case '7':
				{
					mTHREE_DIGIT_OCTAL_NUMBER(false);
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mHEXA_ESCAPE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = HEXA_ESCAPE;
		
		try {      // for error handling
			match('\\');
			match('x');
			mHEXA_NUMBER(false);
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mSIMPLE_ESCAPE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = SIMPLE_ESCAPE;
		
		try {      // for error handling
			match('\\');
			{
				switch ( cached_LA1 )
				{
				case 'a':
				{
					match('a');
					break;
				}
				case 'b':
				{
					match('b');
					break;
				}
				case 't':
				{
					match('t');
					break;
				}
				case 'r':
				{
					match('r');
					break;
				}
				case 'n':
				{
					match('n');
					break;
				}
				case 'f':
				{
					match('f');
					break;
				}
				case 'v':
				{
					match('v');
					break;
				}
				case '"':
				{
					match('"');
					break;
				}
				case '\'':
				{
					match('\'');
					break;
				}
				case '\\':
				{
					match('\\');
					break;
				}
				default:
				{
					throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
				}
				 }
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUPPER_CHAR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UPPER_CHAR;
		
		try {      // for error handling
			{
				matchRange('A','Z');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLOWER_CHAR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LOWER_CHAR;
		
		try {      // for error handling
			{
				matchRange('a','z');
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mANY_CHAR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = ANY_CHAR;
		
		try {      // for error handling
			{
				{
					match(tokenSet_1_);
				}
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mANY_ELEMENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = ANY_ELEMENT;
		
		try {      // for error handling
			{
				{
					match(tokenSet_10_);
				}
			}
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
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 0L, 0L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = new long[8];
		data[0]=-9224L;
		for (int i = 1; i<=3; i++) { data[i]=-1L; }
		for (int i = 4; i<=7; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = new long[8];
		data[0]=-4398046520328L;
		for (int i = 1; i<=3; i++) { data[i]=-1L; }
		for (int i = 4; i<=7; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	private static long[] mk_tokenSet_3_()
	{
		long[] data = { 70368744177664L, 137438953504L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_3_ = new BitSet(mk_tokenSet_3_());
	private static long[] mk_tokenSet_4_()
	{
		long[] data = new long[8];
		data[0]=-17179869192L;
		data[1]=-268435457L;
		for (int i = 2; i<=3; i++) { data[i]=-1L; }
		for (int i = 4; i<=7; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_4_ = new BitSet(mk_tokenSet_4_());
	private static long[] mk_tokenSet_5_()
	{
		long[] data = new long[8];
		data[0]=-549755813896L;
		data[1]=-268435457L;
		for (int i = 2; i<=3; i++) { data[i]=-1L; }
		for (int i = 4; i<=7; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_5_ = new BitSet(mk_tokenSet_5_());
	private static long[] mk_tokenSet_6_()
	{
		long[] data = { 566935683072L, 23714567704018944L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_6_ = new BitSet(mk_tokenSet_6_());
	private static long[] mk_tokenSet_7_()
	{
		long[] data = new long[8];
		data[0]=-8L;
		for (int i = 1; i<=3; i++) { data[i]=-1L; }
		for (int i = 4; i<=7; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_7_ = new BitSet(mk_tokenSet_7_());
	private static long[] mk_tokenSet_8_()
	{
		long[] data = { 287948901175001088L, 576460745995190270L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_8_ = new BitSet(mk_tokenSet_8_());
	private static long[] mk_tokenSet_9_()
	{
		long[] data = { 71776119061217280L, 0L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_9_ = new BitSet(mk_tokenSet_9_());
	private static long[] mk_tokenSet_10_()
	{
		long[] data = new long[8];
		data[0]=-4398046511112L;
		for (int i = 1; i<=3; i++) { data[i]=-1L; }
		for (int i = 4; i<=7; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_10_ = new BitSet(mk_tokenSet_10_());
	
}
}
