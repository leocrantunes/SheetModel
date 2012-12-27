using System.IO;
using System;
using System.Collections.Generic;
using OclParser.cst;
using OclParser.exception;
using antlr;
using oclparser.antlrcs;

namespace OclParser.controller
{
    public class OCLWorkbenchParser : OCLScriptParser {
        private int nest = 0;
        private int errorCount = 0;
        private StreamWriter err;
        private HashSet<Exception> errors;

        public OCLWorkbenchParser(
            String inputName,
            OCLWorkbenchLexer lexer,
            StreamWriter err,
            HashSet<Exception> errors) : base(lexer) {
            setFilename(inputName);
            this.err = err;
            this.errors = errors;
            lexer.setParser(this);
            }

        public int getErrorCount() {
            return this.errorCount;
        }

        public void incErrorCount() {
            this.errorCount++;
        }

        /* Overridden methods. */
        public override void reportError(RecognitionException ex) {
            OCLSyntaticException error = new OCLSyntaticException(ex.Message,
                                                                  new SourceLocation(getFilename(), ex.getLine(), ex.getColumn()));

            errors.Add(error);

            if (this.err != null) {
                this.err.WriteLine(error);
            }

            incErrorCount();
        }

        public override void traceIn(String rname) {
            for (int i = 0; i < this.nest; i++)
                Console.Write(" ");

            base.traceIn(rname);
            this.nest++;
        }

        public override void traceOut(String rname) {
            this.nest--;

            for (int i = 0; i < this.nest; i++)
                Console.Write(" ");

            base.traceOut(rname);
        }
    }
}
