using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.types;
using Ocl20.parser.cst;
using Ocl20.parser.cst.type;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestTypeCS : TestNodeCS {

        [TestMethod]
        public void test_01() {
            String stream = "MyExample::Film ";
            doParseTestOK(stream, this.getCurrentMethodName(), "Film", typeof(CoreClassifier));
        }
	
        [TestMethod]
        public void test_02() {
            String stream = "Set  ( Film ) ";
            doParseTestOK(stream, this.getCurrentMethodName(), "Set(Film)", typeof(SetType));
        }

        [TestMethod]
        public void test_03() {
            String stream = "Bag  ( Film ) ";
            doParseTestOK(stream, this.getCurrentMethodName(), "Bag(Film)", typeof(BagType));
        }

        [TestMethod]
        public void test_04() {
            String stream = "Sequence  ( Film ) ";
            doParseTestOK(stream, this.getCurrentMethodName(), "Sequence(Film)", typeof(SequenceType));
        }

        [TestMethod]
        public void test_05() {
            String stream = "Collection  ( Film ) ";
            doParseTestOK(stream, this.getCurrentMethodName(), "Collection(Film)", typeof(CollectionType));
        }

        [TestMethod]
        public void test_06() {
            String stream = "OrderedSet  ( Film ) ";
            doParseTestOK(stream, this.getCurrentMethodName(), "OrderedSet(Film)", typeof(OrderedSetType));
        }

        [TestMethod]
        public void test_SetOclAny() {
            String stream = "Set(OclAny)";
            doParseTestOK(stream, this.getCurrentMethodName(), "Set(OclAny)", typeof(SetType));
        }

        [TestMethod]
        public void test_07() {
            String stream = "Set(Bag(Sequence(Film)))";
            CoreClassifier c = doParseTestOK(stream, this.getCurrentMethodName(), stream, typeof(SetType));
            SetType s = (SetType) c;
            Assert.IsTrue(s.getElementType() is BagType);
            BagType b = (BagType) s.getElementType();
            Assert.IsTrue(b.getElementType() is SequenceType);
            SequenceType seq = (SequenceType) b.getElementType();
            Assert.IsTrue(seq.getElementType() is CoreClassifier);
            Assert.AreEqual("Film", seq.getElementType().getName());
        }

        [TestMethod]
        public void test_08() {
            String stream = "Tuple(a : Film)";
            doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(a : Film)", typeof(TupleType));
        }

        [TestMethod]
        public void test_09() {
            String stream = "Tuple(a : Film,  b : String, c : Integer)";
            CoreClassifier c = doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(a : Film, b : String, c : Integer)", typeof(TupleType));
            TupleType tuple = (TupleType) c;
            List<object> parts = tuple.getTupleParts();
            Assert.AreEqual(3, parts.Count);
        }

        [TestMethod]
        public void test_10() {
            String stream = "Set(Tuple(a : Film,  b : String, c : Integer))";
            doParseTestOK(stream, this.getCurrentMethodName(), "Set(Tuple(a : Film, b : String, c : Integer))", typeof(SetType));
        }

        [TestMethod]
        public void test_12() {
            String stream = "Set(Tuple(a : Film,  b : String, c : Integer))";
            doParseTestOK(stream, this.getCurrentMethodName(), "Set(Tuple(a : Film, b : String, c : Integer))", typeof(SetType));
        }

        [TestMethod]
        public void test_13() {
            String stream = "Tuple(s1:Set(Integer), s2:Bag(String))";
            doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(s1 : Set(Integer), s2 : Bag(String))", typeof(TupleType));
        }

        [TestMethod]
        public void test_14() {
            String stream = "Tuple(a : Film,  b : Film)";
            doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(a : Film, b : Film)", typeof(TupleType));
        }

        [TestMethod]
        public void test_15() {
            String stream = "Set(Sequence(Foo))";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_16() {
            String stream = "Tuple(a, b, c)";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_17() {
            String stream = "Tuple(a : Film, b : String, c)";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_18() {
            String stream = "Tuple( : Film,  : String, c : Integer)";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_19() {
            String stream = "Tuple(c : Film, a : String, c : Integer)";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_20() {
            String stream = "Tuple(c : Film, a : String, d : Integer = 10)";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_21() {
            String stream = "Tuple(c : Film, a : String, d : Foo)";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        [TestMethod]
        public void test_22() {
            String stream = "Set()";
            doParseTestNotOK(stream, this.getCurrentMethodName());
        }

        public CoreClassifier doParseTestOK(String stream, String inputName, String resultName, Type className) {
            try {
                CSTNode node = parseOK(stream, inputName); 
                Assert.IsTrue(node is CSTTypeCS);
                CSTTypeCS type = (CSTTypeCS) node;
			
                CoreClassifier ast = type.getAst();
                Assert.IsNotNull(ast);
                Assert.AreEqual(resultName, ast.getName());
                Assert.IsTrue(className.IsInstanceOfType(ast));
                return ast;
            } 
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                throw new AssertFailedException();
            }
		
            return null;
        }

        public void doParseTestNotOK(String stream, String inputName) {
            try {
                parseWithError(stream, inputName);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        protected override Type getSpecificNodeClass()
        {
            return typeof (CSTTypeCS);
        }
    }

}
