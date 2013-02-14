using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.parser.cst;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestInitConstraints : TestPropertyCallExp {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testInit_01() {
            doTestContextOK("context Film::name init : \"film name\"",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInit_15() {
            doTestContextOK("context Film::name : String init : \"film name\"",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInit_02() {
            doTestContextOK("context Tape::number derive : 10 ",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInit_03() {
            doTestManyContextOK("context Film::name init : \"film name\"  " +
                                "context SpecialFilm::name init : \"special film name\"",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testInit_04() {
            doTestContextOK(  "context SpecialFilm::name init : \"special film name\"",     
                              getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidInit() {
            doTestContextNotOK(  "context SpecialFilm::days init : rentalFee + 20",     
                                 getCurrentMethodName());
        }

        [TestMethod]
        public void testInitRedefinition() {
            doTestManyContextNotOK(
                "context Film::name init : \"film name\" \r\n" +
                "context Tape::number derive : 10 \r\n" + 
                "context Film::name init : \"special film name\"",     
                getCurrentMethodName());
        }

        [TestMethod]
        public void testInitWrongDeclaration() {
            doTestContextNotOK("context Tape::number : String init : \"joao\"",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInitWrongExpressionType() {
            doTestContextNotOK("context Tape::number init : \"joao\"",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testDeriveRedefinition() {
            doTestManyContextNotOK(
                "context Tape::number derive : 10 " +
                "context Tape::number derive : 20",     
                getCurrentMethodName());
        }

        [TestMethod]
        public void testderiveWrongExpressionType() {
            doTestContextNotOK("context Tape::number derive : true ",     
                               getCurrentMethodName());
        }

        protected override List<object> getConstraints(CSTNode rootNode) {
            return	null;
        }

//	protected List doTestContextOK(String expression, String testName) {
//		try {
//			rootNode = getNode(expression, testName);
//			oclSemanticAnalyzer.analyze(environment, rootNode);
//			CSTAttrOrAssocContextCS context = (CSTAttrOrAssocContextCS) rootNode;
//			CSTNode constraints = context.getValueExpressionNodeCS();
//			List result = new ArrayList();
//			result.add(constraints);
//			return result;
//		}
//		catch (SemanticException ex) {
//			OCLWorkbenchToken token = ex.getNode().getToken();
//			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
//			fail();
//		}
//		catch (Exception e) {
//			System.out.println(e.getMessage());
//			fail();
//		}
//		return null;
//	}

    }
}
