using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.parser.controller;
using Ocl20.parser.cst;
using Ocl20.parser.exception;
using Ocl20.parser.typeChecker;
using Ocl20.xmireader;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestOCLSemanticAnalyzer
    {
        private OCLSemanticAnalyzer oclSemanticAnalyzer = new OCLSemanticAnalyzer();
        private Environment environment;
        private CSTNode rootNode;

        public TestOCLSemanticAnalyzer()
        {
            setUp();
        }

        protected void setUp() {
            Assert.IsNotNull(environment = getEnvironment());
        }

        [TestMethod]
        public void testPackageDeclaration_01() {
            doTestPackageOK("package MyExample endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testPackageDeclaration_02() {
            doTestPackageOK("package MyExample::package_1 endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testPackageDeclaration_03() {
            doTestPackageNotOK("package MyTestPackage endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testPackageDeclaration_04() {
            doTestPackageNotOK("package Film endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_01() {
            doTestContextOK("context Film inv: expression",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_02() {
            doTestContextOK("context MyExample::package_1::package_1_1::MyBoolean inv: expression",   
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_03() {
            doTestContextNotOK("context Product inv: expression",   
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_04() {
            doTestContextNotOK("context MyExample::package_1::Product inv: expression",  
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_05() {
            try {
                rootNode = getRootNode("package MyExample::package_1::package_1_1 context  Product inv: expression context Rental inv: exp context Film inv: exp context  Product inv: expression endpackage" +
                                       " package java context Integer inv: expression endpackage", "testClassifier_05");
                oclSemanticAnalyzer.analyze(environment, rootNode);
            }
            catch (OCLSemanticException ex) {
                OCLWorkbenchToken token = ex.getNode().getToken();
                Console.WriteLine(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.Message);
                throw new AssertFailedException();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new AssertFailedException();
            }
        }

        [TestMethod]
        public void testClassifier_06() {
            doTestPackageOK("package MyExample context Film inv first: expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_07() {
            doTestPackageOK("package MyExample context Film inv first: expr inv second: expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_08() {
            doTestPackageNotOK("package MyExample context Film inv first: expr inv second: expr inv first: expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_09() {
            doTestPackageOK("package MyExample context Film inv first: expr inv second: expr " +
                            "  context Tape inv first: expr inv second : expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_10() {
            doTestPackageNotOK("package MyExample context Film inv first: expr inv second: expr " +
                               "  context SpecialFilm inv first: expr inv second : expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_11() {
            doTestPackageOK("package MyExample context Film inv first: expr inv second: expr " +
                            "  context SpecialFilm inv third: expr inv fourth: expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_12() {
            Assert.IsNotNull(environment.lookupLocal("OclString"));
            Assert.IsNotNull(environment.lookup("OclString"));
            Assert.IsTrue(environment.lookup("OclString") is CoreClassifier);
            doTestPackageOK("package MyExample context OclString inv: expr endpackage" , getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_AttributeOK() {
            doTestContextOK("context Film::rentalFee : Integer  init: value", 
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_WrongType() {
            doTestContextNotOK("context Film::rentalFee : Real init: value", 
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_WrongName() {
            doTestContextNotOK("context Film::rentalFee1 : Real init: value",
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_WrongClassifier() {
            doTestContextNotOK("context AFilm::rentalFee1 : Real init: value", 
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_RoleNameOK() {
            doTestContextOK("context Tape::theFilm : Film init: value", 
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_WrongRoleType() {
            doTestContextNotOK("context Tape::theFilm : Tape init: value", 
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_RoleNameWithoutTypeOK() {
            doTestContextOK("context Film::rentalFee init: value", 
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_SubClassOK() {
            doTestContextOK("context SpecialFilm::rentalFee : Integer init: value", 
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_SubClassRedefinitionOK() {
            doTestPackageOK("package MyExample context SpecialFilm::rentalFee : Integer init: value " +
                            "  context Film::rentalFee : Integer init: value " +
                            "  endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_WrongSubClass_01() {
            doTestPackageNotOK("package MyExample context SpecialFilm::rentalFee : Integer init: value " +
                               " context Film::rentalFee : Integer init: value " +
                               "  context SpecialFilm::rentalFee : Integer init: value "+
                               " endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testInitExpression_WrongSubClass_02() {
            doTestPackageNotOK("package MyExample context SpecialFilm::rentalFee : Integer init: value " +
                               " context Film::rentalFee : Integer init: value " +
                               "  context SpecialFilm::rentalFee : Integer derive: value "+
                               " endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testDeriveExpression_01() {
            doTestContextOK("context Film::rentalFee : Integer  derive: value", 
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testDeriveExpression_02() {
            doTestContextNotOK("context Film::rentalFee : String derive: value", 
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_01() {
            doTestPackageOK("package MyExample context Film def:  var : double = expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_02() {
            doTestPackageNotOK("package MyExample context Film def:  var : double = expr  " +
                               " context Film def: var2 : int = expr  def: var2 : double = expr1 endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_03() {
            doTestPackageNotOK("package MyExample context Film def:  var : double = expr  " +
                               " context Film def: var2 : int = expr  def: var : double = expr1 endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_04() {
            doTestPackageNotOK("package MyExample context Film def:  code : type = expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_05() {
            doTestPackageNotOK("package MyExample context Film def:  code : type = endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_06() {
            doTestPackageNotOK("package MyExample context Film def:  code : type endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_07() {
            doTestPackageOK("package MyExample context Film def:  xpto = expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_DefExpression_08() {
            doTestPackageNotOK("package MyExample context Film def:  xpto = expr " +
                               " context SpecialFilm def: xpto = expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_OperationDefExpression_01() {
            doTestPackageOK("package MyExample context Film def: operation(a : int, b : double) : int = expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_OperationDefExpression_02() {
            doTestPackageNotOK("package MyExample context Film def: operation(a : int, b : double) : int = expr " +
                               " context Film def: operation(x : int, y : double) : int = expr endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_OperationDefExpression_03() {
            doTestPackageNotOK("package MyExample context Film def: getRentalFee(dayOfWeek : int) : double = expr " +
                               " endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_OperationDefExpression_04() {
            doTestPackageOK("package MyExample context Film def: getRentalFee(dayOfWeek : double) : double = expr " +
                            " endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testClassifier_OperationDefExpression_05() {
            doTestPackageNotOK("package MyExample context Film def: Film::getRentalFee() : double = expr " +
                               " endpackage", getCurrentMethodName());
        }

        [TestMethod]
        public void testOperationContext_01() {
            doTestContextOK("context Film::getRentalFee(dayOfWeek : int) : double body : expression",
                            getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_02() {
            doTestContextNotOK("context Film::getRentalFee(dayOfWeek : double) : double body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_03() {
            doTestContextNotOK("context Film::getRentalFee(dayOfWeek : int, another : int) : double body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_04() {
            doTestContextNotOK("context Film::getRentalFee() body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_05() {
            doTestContextNotOK("context Film::getRental()  body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_06() {
            doTestContextNotOK("context Film::getRentalFee(dayOfWeek : int) : int body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_07() {
            doTestContextNotOK("context Foo::getRentalFee(dayOfWeek : int) : double body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_08() {
            doTestContextOK("context Film::setDaysForReturn(days : int) body : expression",
                            getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_09() {
            doTestContextNotOK("context getRentalFee(dayOfWeek : int) : double body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_10() {
            doTestContextOK("context SpecialFilm::doSomething() post : expression",
                            getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_11() {
            doTestContextOK("context SpecialFilm::doSomething(a : int, b : int, c :double) : Integer pre : expression",
                            getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_12() {
            doTestContextNotOK("context SpecialFilm::setDaysForReturn(days : int) body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_13() {
            doTestContextNotOK("context SpecialFilm::doSomething(a : int, b : int, c :double) : int body : expression",
                               getCurrentMethodName());			
        }

        [TestMethod]
        public void testOperationContext_14() {
            doTestContextNotOK("context SpecialFilm::doSomething(a : int, b : double, c :int) : int body : expression",
                               getCurrentMethodName());			
        }

        private void doTestPackageOK(String expression, String testName) {
            try {
                rootNode = getRootNode(expression, testName);
                oclSemanticAnalyzer.analyze(environment, rootNode);
            }
            catch (OCLSemanticException ex) {
                OCLWorkbenchToken token = ex.getNode().getToken();
                Console.WriteLine(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.Message);
                throw new AssertFailedException();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new AssertFailedException();
            }
        }

        private void doTestPackageNotOK(String expression, String testName) {
            try {	
                rootNode = getRootNode(expression, testName);
                oclSemanticAnalyzer.analyze(environment, rootNode);
                throw new AssertFailedException();
            }
            catch (OCLSemanticException ex) {
                OCLWorkbenchToken token = ex.getNode().getToken();
                Console.WriteLine(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.Message);
                throw new AssertFailedException();
            }

            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }		


        private void doTestContextOK(String expression, String testName) {
            try {
                rootNode = getClassifierRootNode(expression, testName);
                oclSemanticAnalyzer.analyze(environment, rootNode);
            }
            catch (OCLSemanticException ex) {
                OCLWorkbenchToken token = ex.getNode().getToken();
                Console.WriteLine(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.Message);
                throw new AssertFailedException();
            }

            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new AssertFailedException();
            }
        }

        private void doTestContextNotOK(String expression, String testName) {
            try {
                rootNode = getClassifierRootNode(expression, testName);
                oclSemanticAnalyzer.analyze(environment, rootNode);
                throw new AssertFailedException();
            }
            catch (OCLSemanticException ex) {
                OCLWorkbenchToken token = ex.getNode().getToken();
                Console.WriteLine(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.Message);
                throw new AssertFailedException();
            }

            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        private String getCurrentMethodName() {
            return new StackTrace().GetFrame(1).GetMethod().Name;
        }

        private Environment getEnvironment()
        {
            CoreModel model;
            XmiReader reader;
            reader = new XmiReader(@"C:\Users\Leo\Documents\visual studio 2010\Projects\SheetModel_20121206\SheetModel\Ocl20Test\resource\myExamplePoseidon.xml");
            Assert.IsNotNull(model = reader.getMetamodel());
            return model.getEnvironmentWithoutParents();
        }

        private CSTNode getRootNode(String source, String inputName) {
            OCLWorkbenchParser parser;
            StreamWriter err; 
	 
            StringReader inR = new StringReader(source);
            String inName = inputName;
            err = new StreamWriter(Console.OpenStandardOutput());
            err.Flush();
			
            HashSet<Exception> errorsList = new HashSet<Exception>();
            OCLWorkbenchLexer lexer = new OCLWorkbenchLexer(inR, inName, err, errorsList);
            parser = new OCLWorkbenchParser(inName, lexer, err, errorsList);
		
            CSTNode result = null;
		
            try {				
                result = parser.packageDeclarationCS();	
            } catch(antlr.RecognitionException e) {
                err.WriteLine(parser.getFilename() +":" + 
                              "[" + e.getLine() + ", " + e.getColumn() + "]: " + 
                              e.Message);
                throw e;
            } catch(antlr.TokenStreamRecognitionException e) {
                err.WriteLine(parser.getFilename() +":" + 
                              "[" + e.recog.getLine() + ", " + e.recog.getColumn() + "]: " + 
                              e.recog.Message);
                throw e;
            } catch(antlr.TokenStreamException ex) {
                err.WriteLine(parser.getFilename() +":" + ex.Message);
                throw ex;
            } finally {
                err.Flush(); 
            }

            if (parser.getErrorCount() != 0)
                throw new Exception("syntatic errors in compilation");
		
            return result;
        }
	
        private CSTNode getClassifierRootNode(String source, String inputName) {
            OCLWorkbenchParser parser;
            StreamWriter err; 
	 
            StringReader inR = new StringReader(source);
            String inName = inputName;
            err = new StreamWriter(Console.OpenStandardOutput());
            err.Flush();
			
            HashSet<Exception> errorsList = new HashSet<Exception>();
            OCLWorkbenchLexer lexer = new OCLWorkbenchLexer(inR, inName, err, errorsList);
            parser = new OCLWorkbenchParser(inName, lexer, err, errorsList);
		
            CSTNode result = null;
		
            try {				
                result = parser.contextDeclarationCS();	
            } catch(antlr.RecognitionException e) {
                err.WriteLine(parser.getFilename() +":" + 
                              "[" + e.getLine() + ", " + e.getColumn() + "]: " + 
                              e.Message);
                throw e;
            } catch(antlr.TokenStreamRecognitionException e) {
                err.WriteLine(parser.getFilename() +":" + 
                              "[" + e.recog.getLine() + ", " + e.recog.getColumn() + "]: " + 
                              e.recog.Message);
                throw e;
            } catch(antlr.TokenStreamException ex) {
                err.WriteLine(parser.getFilename() +":" + ex.Message);
                throw ex;
            }	finally {
                err.Flush(); 
            }
			
            if (parser.getErrorCount() != 0)
                throw new Exception("syntatic errors in compilation");
			
            return result;
        }
		
    }
}
