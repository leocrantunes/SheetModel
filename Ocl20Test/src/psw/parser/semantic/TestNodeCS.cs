using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.modelreader;
using Ocl20.parser.cst;
using Ocl20.parser.semantics.types;
using Ocl20.parser.typeChecker;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20Test.psw.parser.semantic
{
    abstract public class TestNodeCS {
        protected static ModelReader reader;
        protected static Environment	environment;
        protected static CoreModel 	model;
        protected String source;
        protected PSWOclCompiler oclCompiler;
        protected ConstraintSourceTracker tracker = new ConstraintSourceTrackerImpl();
	
        protected TestNodeCS()
        {
            setUp();
        }

        abstract protected Type getSpecificNodeClass();

        public virtual ModelReader getReader()
        {
            return
                new XmiReader(
                    @"C:\Users\Leo\Documents\visual studio 2010\Projects\SheetModel_20121206\SheetModel\Ocl20Test\resource\myExampleRose.xml");
        }

        public void setUp()
        {
            if (model == null)
            {
                reader = getReader();
                Assert.IsNotNull(model = reader.getModel());
                OclTypesFactory.setEnvironment(model);
            }
		
            environment = model.getEnvironmentWithoutParents();
        }
	
        protected void tearDown() {
            oclCompiler.deleteAllConstraintsForSource(source);
            tracker.clearAll();
        }

        protected CSTNode parseOK(String expression, String testName) {
            try {
                source = testName;
                oclCompiler = new PSWOclCompiler(environment, tracker);
                List<object> nodes = oclCompiler.compileOclStream(expression, testName, new StreamWriter(Console.OpenStandardOutput()), getSpecificNodeClass());
                Assert.IsNotNull(nodes);
                Assert.IsTrue(nodes.Count >= 1);
                return	(CSTNode) nodes[0];
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new AssertFailedException();
            }
        }
        
        protected void parseWithError(String expression, String testName) {
            try {
                source = testName;
                oclCompiler = new PSWOclCompiler(environment, tracker);
                List<object> nodes = oclCompiler.compileOclStream(expression, testName, new StreamWriter(Console.OpenStandardOutput()), getSpecificNodeClass());
                Assert.IsNull(nodes);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        protected String getCurrentMethodName() {
            return new StackTrace().GetFrame(1).GetMethod().Name;
        }
    }
}
