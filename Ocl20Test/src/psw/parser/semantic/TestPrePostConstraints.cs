using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestPrePostConstraints : TestPropertyCallExp {

        [TestMethod]
        public void testPrePost_01() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: result = tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() < 10",     
                                getCurrentMethodName());
				
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(1, new List<object>(operation.getSpecifications()).Count);
            OclPrePostConstraint	constraint = (OclPrePostConstraint) new List<object>(operation.getSpecifications())[0];
            Assert.AreEqual(2, constraint.getPreConditions().Count);
            Assert.AreEqual("myPre", ((OclPreConstraint) new List<object>(constraint.getPreConditions())[0]).getName());
            Assert.AreEqual("anotherPre", ((OclPreConstraint) new List<object>(constraint.getPreConditions())[1]).getName());
            Assert.AreEqual(1, new List<object>(constraint.getPostConditions()).Count);
            Assert.AreEqual("myPost", ((OclPostConstraint) new List<object>(constraint.getPostConditions())[0]).getName());
        }

        [TestMethod]
        public void testPrePost_02() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) pre: tapes->size() > 0 pre : tapes->select(t | t.number = 1)->size() > 0 post: result = tapes->select(rentalFee > 10) post: tapes->select(rentalFee = 0) = 0",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_03() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre0: tapes->size() > 0   pre anotherPre0: tapes->size() < 10   post myPost0: result = tapes->select(rentalFee > 10)   post anotherPost0: result = tapes->select(rentalFee > 10)  " +
                                "context Film::getTapes() : Set(Tape) pre myPre1: tapes->size() > 0 pre anotherPre1: tapes->size() < 10 post myPost1: result = tapes->select(rentalFee > 10)  post anotherPost1: result = tapes->select(rentalFee > 10)  ",     
                                getCurrentMethodName());
				
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(2, new List<object>(operation.getSpecifications()).Count);
            for (int i = 0; i < 2; i++) {
                OclPrePostConstraint	constraint = (OclPrePostConstraint) new List<object>(new List<object>(operation.getSpecifications()))[i];
                Assert.AreEqual(2, constraint.getPreConditions().Count);
                Assert.AreEqual("myPre"+i, ((OclPreConstraint) new List<object>(constraint.getPreConditions())[0]).getName());
                Assert.AreEqual("anotherPre"+i, ((OclPreConstraint) new List<object>(constraint.getPreConditions())[1]).getName());
                Assert.AreEqual(2, new List<object>(constraint.getPostConditions()).Count);
                Assert.AreEqual("myPost"+i, ((OclPostConstraint) new List<object>(constraint.getPostConditions())[0]).getName());
                Assert.AreEqual("anotherPost"+i, ((OclPostConstraint) new List<object>(constraint.getPostConditions())[1]).getName());
            }
        }

        [TestMethod]
        public void testPre_01() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 ",     
                                getCurrentMethodName());
				
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(1, new List<object>(operation.getSpecifications()).Count);
            OclPrePostConstraint	constraint = (OclPrePostConstraint) new List<object>(operation.getSpecifications())[0];
            Assert.AreEqual(1, new List<object>(constraint.getPreConditions()).Count);
            Assert.AreEqual("myPre", ((OclPreConstraint) new List<object>(constraint.getPreConditions())[0]).getName());
        }

        [TestMethod]
        public void testPost_01() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) post myPost: result = tapes->select(rentalFee > 10)  ",     
                                getCurrentMethodName());
				
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(1, new List<object>(operation.getSpecifications()).Count);
            OclPrePostConstraint	constraint = (OclPrePostConstraint) new List<object>(operation.getSpecifications())[0];
            Assert.AreEqual(1, new List<object>(constraint.getPostConditions()).Count);
            Assert.AreEqual("myPost", ((OclPostConstraint) new List<object>(constraint.getPostConditions())[0]).getName());
        }

        [TestMethod]
        public void testBody_01() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) body : self.tapes",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_02() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: result = tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() < 10 body: self.tapes",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_03() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 body: self.tapes post myPost: result = tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() < 10 ",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_04() {
            doTestManyContextOK("context Film::getRentalFee(dayOfWeek : Integer) : Real  pre: dayOfWeek > 10 body: self.tapes->size() * 10.3 + dayOfWeek post myPost: result = tapes->select(rentalFee > dayOfWeek * 10)  pre anotherPre: tapes->size() < dayOfWeek * 10 ",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_PreNotBoolean01() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() ",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_PreNotBoolean02() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: result = tapes->select(t | t.number > 10)  pre anotherPre: tapes->size()",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_PostNotBoolean() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() > 0",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_TwoBodies() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body: self.tapes  body: self.tapes->select(rentalFee > 10)->size() > 10",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_BodyAlreadyDefined() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body: self.tapes  context Film::getTapes() : Set(Tape) body: self.tapes->select(rentalFee > 10)->size() > 10",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_IncompatibleBodyType() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body: self.name",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testBody_UndefinedOperation() {
            doTestManyContextNotOK("context Film::getTapes() : String  body: self.name",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testPostOfOperationWithoutReturnType() {
            doTestManyContextOK("context Film::setDaysForReturn(days : Integer)  post myPost: rentalFee + days  > 20  ",     
                                getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidBody_01() {
            doTestManyContextNotOK("context Film::setDaysForReturn(days : Integer)  body: rentalFee + days  > 20  ",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidBody_02() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body : 10 ",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testInvalidBody_03() {
            doTestManyContextNotOK("context Tape::tapesQty() : Integer body : number + 10 ",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_DefLocalVar_01() {
            doTestManyContextOK("context Film::getTapes() : Set(Tape) \r\n" +
                                "def: theSize : Integer = tapes->size() \r\n" +
                                "def: abc : Integer = theSize + 10 \r\n" +
                                "pre myPre: theSize > 0 \r\n" +
                                "post myPost: result = tapes->select(rentalFee > 10) \r\n" +
                                "pre anotherPre: theSize < 10 and abc > 20",     
                                getCurrentMethodName());
				
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            CoreOperation operation = film.lookupOperation("getTapes", null);
            Assert.AreEqual(1, new List<object>(operation.getSpecifications()).Count);
            OclPrePostConstraint	constraint = (OclPrePostConstraint) new List<object>(operation.getSpecifications())[0];
            Assert.AreEqual(2, constraint.getPreConditions().Count);
            Assert.AreEqual("myPre", ((OclPreConstraint) new List<object>(constraint.getPreConditions())[0]).getName());
            Assert.AreEqual("anotherPre", ((OclPreConstraint) new List<object>(constraint.getPreConditions())[1]).getName());
            Assert.AreEqual(1, new List<object>(constraint.getPostConditions()).Count);
            Assert.AreEqual("myPost", ((OclPostConstraint) new List<object>(constraint.getPostConditions())[0]).getName());
            Assert.IsNotNull(operation.lookupLocalVariable("theSize"));
        }

        [TestMethod]
        public void testPrePost_DefLocalVar_02() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) \r\n" +
                                   "def: aSize : Integer = tapes->size() \r\n" +
                                   "def: bef : Integer = aSize + 10 \r\n" +
                                   "def: aSize : Integer = 30 \r\n" +
                                   "pre myPre: aSize > 0 \r\n" +
                                   "post myPost: result = tapes->select(rentalFee > 10) \r\n" +
                                   "pre anotherPre: aSize < 10 and bef > 20",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_DefLocalVar_03() {
            doTestManyContextNotOK("context Film::getTapes() : Set(Tape) \r\n" +
                                   "def: bSize : Integer = tapes->size() \r\n" +
                                   "def: efg : Integer = bSize + 10 \r\n" +
                                   "def: bSize : Integer = 30 \r\n" +
                                   "pre myPre: bSize > 0 \r\n" +
                                   "post myPost: result = tapes->select(rentalFee > 10) \r\n" +
                                   "pre anotherPre: bSize < 10 and efg > 20",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testPrePost_DefLocalVar_04() {
            doTestManyContextNotOK("context Film::setSpecialFee(fee : Integer) \r\n" +
                                   "def: bSize : Integer = tapes->size() \r\n" +
                                   "def: fee : Integer = bSize + 10 \r\n" +
                                   "pre myPre: bSize > 0 \r\n" +
                                   "post myPost: tapes->select(rentalFee > 10)->size() > 5 \r\n" +
                                   "pre anotherPre: fee > 2",     
                                   getCurrentMethodName());
        }

    }
}
