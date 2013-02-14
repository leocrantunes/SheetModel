using System;
using System.Collections.Generic;
using System.IO;
using Ocl20.library.iface.common;
using Ocl20.library.impl.constraints;
using Ocl20.parser.controller;
using Ocl20.parser.cst;
using Ocl20.parser.cst.context;
using Ocl20.parser.cst.expression;
using Ocl20.parser.cst.literalExp;
using Ocl20.parser.cst.type;
using Ocl20.parser.exception;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.parser.typeChecker
{
    public class PSWOclCompiler {
        protected	Environment environment;
        protected	OCLSemanticAnalyzer oclSemanticAnalyzer;
        protected	int syntaticErrorsCount = 0;
        protected	int semanticErrorsCount = 0;
        protected	HashSet<Exception> errors = new HashSet<Exception>();
        protected	ConstraintSourceTracker sourceTracker;
        protected	List<object> cstNodes;
        protected	static Dictionary<object,object> parsedTypes = new Dictionary<object,object>();
    
        /**
     * Creates a new PSWOclCompiler object.
     *
     * @param environment DOCUMENT ME!
     */
        public PSWOclCompiler(Environment environment, ConstraintSourceTracker sourceTracker) {
            this.environment = environment;
            this.sourceTracker = sourceTracker;
        }

        public int getErrorsCount() {
            return syntaticErrorsCount + semanticErrorsCount;
        }

        /**
     * @return
     */
        public int getSemanticErrorsCount() {
            return semanticErrorsCount;
        }

        /**
     * @return
     */
        public int getSyntaticErrorsCount() {
            return syntaticErrorsCount;
        }

        public HashSet<Exception> getErrors() {
            return errors;
        }

        public CSTExpressionInOclCS compileOclExpression(string expression, string sourceName, StreamWriter errWriter) {
            StringReader inR = new StringReader(expression);
            string inName = sourceName;

            cstNodes = this.compile(inR, inName, errWriter, typeof(CSTExpressionInOclCS), errors);

            if ((cstNodes != null) && (getErrorsCount() == 0)) {
                CSTExpressionInOclCS rootNode = (CSTExpressionInOclCS) cstNodes[0];
                return	rootNode;
            } else {
                return null;
            }
        }

        public List<object> compileOclStream(string expression, string sourceName, StreamWriter errWriter) {
            return compileOclStream(expression, sourceName, errWriter, null);
        }

        public List<object> compileOclStream(string expression, string sourceName, StreamWriter errWriter, Type nodeClass) {
            StringReader inStream = new StringReader(expression);

            return compileOclStream(inStream, sourceName, errWriter, nodeClass);
        }

        public List<object> compileOclStream(
            TextReader oclExpressionStream,
            string sourceName,
            StreamWriter errWriter,
            Type nodeClass) {
            deleteAllConstraintsForSource(sourceName);

            cstNodes = this.compile(oclExpressionStream, sourceName, errWriter, nodeClass, errors);

            if ((cstNodes != null) && (getErrorsCount() == 0)) {
                return cstNodes;
            } else {
                return null;
            }
            }
    
        public Dictionary<string, List<object>> compileListOclStreams(Dictionary<string, TextReader> source, StreamWriter errWriter, Type nodeClass) {
            resetCounters();
            errors.Clear();

            Dictionary<string, List<object>> result = new Dictionary<string, List<object>>();
        
//        Monitor mon1 = MonitorFactory.start("compile list ocl streams 1");

            foreach (KeyValuePair<string, TextReader> s in source)
            {
                deleteAllConstraintsForSource(s.Key);
            }
// mon1.stop();

            foreach (KeyValuePair<string, TextReader> s in source)
            {
                string sourceName = s.Key;
                TextReader oclExpressionStream = s.Value;
//            Monitor mon2 = MonitorFactory.start("compile list ocl streams 2");    
                cstNodes = performSyntaxAnalysis(oclExpressionStream, sourceName, errWriter, nodeClass, errors);
                result.Add(sourceName, cstNodes);
//            mon2.stop();
            
//            Monitor mon3 = MonitorFactory.start("compile list ocl streams 3");
                if (cstNodes != null)
                    performSemanticAnalysisPass(sourceName, cstNodes, environment,
                                                errWriter, errors, getSemanticAnalyzer(), getPass1CompilerAction());
//            mon3.stop();
            }
        
            foreach (KeyValuePair<string, TextReader> s in source)
            {
                string sourceName = s.Key;

//            Monitor mon4 = MonitorFactory.start("compile list ocl streams 4");
                result.TryGetValue(sourceName, out cstNodes);
                if (cstNodes != null)
                    performSemanticAnalysisPass(sourceName, cstNodes, environment,
                                                errWriter, errors, getSemanticAnalyzer(), getPass2CompilerAction());
//            mon4.stop();
            }
        
            if (getErrorsCount() == 0) {
                return result;
            } else {
                return null;
            }
        }
    
        protected List<object> compile(TextReader inChannel, string sourceName, StreamWriter errChannel, Type nodeClass, HashSet<Exception> pErrors) {
            resetCounters();
            pErrors.Clear();

            cstNodes = performSyntaxAnalysis(inChannel, sourceName, errChannel, nodeClass, pErrors);

            if (cstNodes != null) {
                performSemanticAnalysis(sourceName, cstNodes, environment, errChannel, pErrors);
            }

            return cstNodes;
        }

        protected List<object> performSyntaxAnalysis(
            TextReader inR,
            string sourceName,
            StreamWriter err,
            Type nodeClass,
            HashSet<Exception> pErrors) {
            OCLWorkbenchLexer lexer = new OCLWorkbenchLexer(inR, sourceName, err, pErrors);
            OCLWorkbenchParser parser = new OCLWorkbenchParser(sourceName, lexer, err, pErrors);

            List<object> result = null;
            OCLCompilerException compilerException = null;

            try {
                result = parseNode(parser, nodeClass);
            } catch (antlr.RecognitionException e) {
                compilerException = new OCLSyntaticException(e.Message,
                                                             new SourceLocation(parser.getFilename(), e.getLine(),
                                                                                e.getColumn()));
            } catch (antlr.TokenStreamRecognitionException e) {
                compilerException = new OCLSyntaticException(e.Message,
                                                             new SourceLocation(parser.getFilename(), e.recog.getLine(),
                                                                                e.recog.getColumn()));
            } catch (antlr.TokenStreamException e) {
                compilerException = new OCLSyntaticException(e.Message,
                                                             new SourceLocation(parser.getFilename(), -1, -1));
            } catch (Exception e) {
                if (e.Message != null)
                    compilerException = new OCLCompilerException(e.Message, new SourceLocation(parser.getFilename(), -1, -1));
            } finally {
                if ((err != null) && (compilerException != null)) {
                    pErrors.Add(compilerException);
                    syntaticErrorsCount++;

                    err.WriteLine(compilerException);
                    err.Flush();
                }
            }

            syntaticErrorsCount += parser.getErrorCount();

            if ((err != null) &&
                ((syntaticErrorsCount != 0) || (compilerException != null))) {
                    err.Flush();
                }
        
            return result;
            }

        protected List<object> parseNode(OCLWorkbenchParser parser, Type nodeClass) {
            List<object> result = null;
            CSTNode node = null;
    	
            if (nodeClass == null) {
                result = parser.expressionStream();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTExpressionInOclCS))) {
                node = parser.expressionInOCLCS();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTTypeCS))) {
                node = parser.typeCS();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTContextDeclarationCS))) {
                node = parser.contextDeclarationCS();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTArgumentCS))) {
                List<object> arguments = parser.argumentsCS();
                node = (CSTNode) arguments[0];
            } else if (nodeClass.IsAssignableFrom(typeof(CSTLiteralExpCS))) {
                node = parser.literalExpCS();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTVariableDeclarationCS))) {
                node = parser.variableDeclarationCS();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTVariableDeclarationCS))) {
                node = parser.variableDeclarationCS();
            } else if (nodeClass.IsAssignableFrom(typeof(CSTClassifierAttributeCallExpCS))) {
                node = parser.classifierAttributeCallExpCS();
            }
        
            if ((result == null) && (node != null)) {
                result = new List<object>();
                result.Add(node);
            }

            return	result;
        }
    
        protected void performSemanticAnalysis(
            string sourceName,
            List<object> rootNode,
            Environment environment,
            StreamWriter err,
            HashSet<Exception> pErrors) {
    	
            OCLSemanticAnalyzer oclSemanticAnalyzer = getSemanticAnalyzer();

            performSemanticAnalysisPass(sourceName, rootNode, environment, err, pErrors, oclSemanticAnalyzer, new Pass1OclCompilerAction());
            performSemanticAnalysisPass(sourceName, rootNode, environment, err, pErrors, oclSemanticAnalyzer, new Pass2OclCompilerAction());
            }

        protected void performSemanticAnalysisPass(
            string sourceName,
            List<object> rootNode,
            Environment environment,
            StreamWriter err,
            HashSet<Exception> pErrors,
            OCLSemanticAnalyzer analyzer,
            OclCompilerAction action) {

            foreach (CSTNode node in rootNode) {
                try {
                    if (node != null) {
                        action.doAction(analyzer, environment, node);
                    }
                } catch (OCLMultipleSemanticExceptions exceptions) {
                    foreach(OCLSemanticException e in exceptions.getAllExceptions()) {
                        pErrors.Add(e);
                        if (err != null) {
                            err.WriteLine(e);
                        }
                        semanticErrorsCount++;
                    }
                }  catch (OCLSemanticException e) {
                    pErrors.Add(e);

                    if (err != null) {
                        err.WriteLine(e);
                    }

                    semanticErrorsCount++;
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);

                    if (err != null) {
                        err.WriteLine(e.Message);
                    }

                    semanticErrorsCount++;
                }
            }

            if ((err != null) && (semanticErrorsCount != 0)) {
                err.Flush();
            }
            }


        class Pass1OclCompilerAction : OclCompilerAction {
            public void doAction(OCLSemanticAnalyzer analyzer, Environment environment, CSTNode node) {
                analyzer.analyzeFeatureDefinitions(environment, node);
            }
        }

        class Pass2OclCompilerAction : OclCompilerAction {
            public void doAction(OCLSemanticAnalyzer analyzer, Environment environment, CSTNode node) {
                analyzer.analyze(environment, node);
            }
        }
    
        protected void resetCounters() {
            syntaticErrorsCount = 0;
            semanticErrorsCount = 0;
        }

        public CoreClassifier parseType(
            Environment environment,
            string name) {
            CSTNode node = null;
            List<object> nodes = null;
    	
            try {
                nodes = compileOclStream(name, "", new StreamWriter(new MemoryStream()), typeof(CSTTypeCS));
                node = (CSTNode) nodes[0];
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            if (node != null) {
                CSTTypeCS typeNode = (CSTTypeCS) node;
                string key = typeNode.getAst().getFullPathName();
                if (parsedTypes.ContainsKey(key))
                    parsedTypes[key] = nodes;
                else
                    parsedTypes.Add(key, nodes);

                return typeNode.getAst();
            } else {
                return null;
            }
            }

        public void deleteAllConstraintsForSource(String sourceName) {
            if (sourceName != null) {
                HashSet<object> constraintOwners = sourceTracker.getOwnersForSource(sourceName);

                if (constraintOwners != null) 
                    foreach (OclConstraintOwner owner in constraintOwners) 
                        owner.deleteAllConstraintsForSource(sourceName);
            }
        }
    
        protected	OCLSemanticAnalyzer	getSemanticAnalyzer() {
            return	new OCLSemanticAnalyzer(sourceTracker);
        }
    
        protected	OclCompilerAction	getPass1CompilerAction() {
            return	new Pass1OclCompilerAction();
        }
    
        protected	OclCompilerAction	getPass2CompilerAction() {
            return	new Pass2OclCompilerAction();
        }

    }
}
