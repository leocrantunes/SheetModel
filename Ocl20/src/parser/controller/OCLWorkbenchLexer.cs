using System.IO;
using System.Collections.Generic;
using System;
using Ocl20.parser.antlrcs;
using Ocl20.parser.cst;
using Ocl20.parser.exception;
using antlr;

namespace Ocl20.parser.controller
{
    /// <summary>
    ///  Lexer class derived from the ANTLR generated lexer.
    ///  Keeps track of token start columns.
    /// </summary>
    public class OCLWorkbenchLexer : OCLScriptLexer {
        private StreamWriter err;
        private OCLWorkbenchParser parser;
        protected int tokenColumn = 1;
        private HashSet<Exception> errors;

        public OCLWorkbenchLexer(TextReader inR, string filename, StreamWriter err, HashSet<Exception> errors)
            : base(new CharBuffer(inR))
        {
            setFilename(filename);
            this.err = err;
            this.errors = errors;
        }

        public void setParser(OCLWorkbenchParser parser) {
            this.parser = parser;
        }

        public override void consume() {
            if ((inputState.guessing == 0) && (text.Length == 0)) {
                // remember token start column
                this.tokenColumn = getColumn();
            }

            base.consume();
        }

        protected override IToken makeToken(int t) {
            OCLWorkbenchToken token = new OCLWorkbenchToken(getFilename(),
                                                            getLine(), this.tokenColumn);
            token.setType(t);

            if (t == EOF) {
                token.setText("end of file or input");
            }

            return token;
        }

        /**
     * Overrides default error-reporting function.
     */
        public override void reportError(antlr.RecognitionException ex) {
            // if this is a NoViableAltForCharException there
            // is still no column information in antlr, so we
            // use the current column
            OCLSyntaticException error = new OCLSyntaticException(ex.Message,
                                                                  new SourceLocation(getFilename(), ex.getLine(), getColumn()));

            if (this.err != null) {
                this.err.WriteLine(error);
            }

            // continue but remember that we had errors
            this.parser.incErrorCount();
        }

        /**
     * Returns true if word is a reserved keyword.
     */
        public bool isKeyword(String word) {
            bool res = literals[word] != null;

            //	Log.trace(this, "keyword " + word + ": " + res);
            Console.WriteLine("keyword " + word + ": " + res);

            return res;
        }

        /*
        public void traceIn(String rname) throws CharStreamException {
            traceIndent();
            traceDepth += 1;
            System.out.println("> lexer " + rname + ": c == '" +
                               StringUtil.escapeChar(LA(1), '\'') + "'");
        }

        public void traceOut(String rname) throws CharStreamException {
            traceDepth -= 1;
            traceIndent();
            System.out.println("< lexer " + rname + ": c == '" +
                               StringUtil.escapeChar(LA(1), '\'') + "'");
        }
    */
    }
}
