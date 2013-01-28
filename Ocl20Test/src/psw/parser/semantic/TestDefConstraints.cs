using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.constraints;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestDefConstraints : TestPropertyCallExp {

        [TestMethod]
        public void testNewAttribute_01() {
            doTestManyContextOK("context Film def: newAttr : Integer = 10 context SpecialFilm inv: newAttr > 10",     
                                getCurrentMethodName());
            CoreClassifier film = (CoreClassifier) environment.lookup("Film");
            OclDeriveConstraint constraint = (OclDeriveConstraint) film.getDeriveConstraint("newAttr");
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Integer", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());		
        }

        [TestMethod]
        public void testNewAttributeOK_02() {
            doTestContextNotOK("context Film def: newAttr : Integer = true",     
                               getCurrentMethodName());
            CoreClassifier film = (CoreClassifier) environment.lookup("Film");
            CoreAttribute attr = (CoreAttribute) film.lookupAttribute("newAttr");
            Assert.IsNotNull(attr);
            Assert.AreEqual("Integer", attr.getFeatureType().getName());
            Assert.IsNull(film.getDeriveConstraint("newAttr"));
        }

        [TestMethod]
        public void testNewAttributeOK_03() {
            doTestContextOK("context Film def: newAttr : Integer = 10  def: newAttr2 : Boolean = true",     
                            getCurrentMethodName());
            CoreClassifier film = (CoreClassifier) environment.lookup("Film");

            Assert.IsNotNull((CoreAttribute) film.lookupAttribute("newAttr"));
            OclDeriveConstraint constraint = (OclDeriveConstraint) film.getDeriveConstraint("newAttr");
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Integer", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
		
            Assert.IsNotNull((CoreAttribute) film.lookupAttribute("newAttr2"));
            constraint = (OclDeriveConstraint) film.getDeriveConstraint("newAttr2");
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());		
        }

        [TestMethod]
        public void testNewAttribute_02() {
            doTestContextNotOK("context Film def: name : Integer = 10",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_03() {
            doTestContextNotOK("context Film def: newAttr = 10",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_04() {
            doTestContextNotOK("context Film def: newAttr : Integer",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_05() {
            doTestManyContextNotOK("context Film def: newAttr : Integer = 20 context SpecialFilm def: newAttr : Integer = 10",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_06() {
            doTestContextNotOK("context Film def: newAttr : Integer = 10 def: newAttr : Boolean = true",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_07() {
            doTestManyContextNotOK("context SpecialFilm def: abc : Integer = 20 context Film def: abc : Integer = 10",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_08() {
            doTestContextNotOK("context Film def: name : Integer = true",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNewAttribute_09() {
            doTestManyContextNotOK("context Film def: newAttr : Integer = 10  context Film def: newAttr : Integer = 20",     
                                   getCurrentMethodName());
        }

        [TestMethod]
        public void testNewOperation_01() {
            doTestContextOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"",     
                            getCurrentMethodName());
            CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");
            List<object> paramTypes = new List<object>();
            CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
            CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
            Assert.IsNotNull(definedOperation);
            Assert.IsNotNull(definedOperation.getBodyDefinition());
            OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
        }

        [TestMethod]
        public void testNewOperation_02() {
            doTestContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = 10 + 20",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNewOperation_03() {
            doTestContextOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\" " +
                            "inv: tapes->size() > 0 implies isSpecialFilm()",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testNewOperation_NotOK01() {
            doTestContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  def: isSpecialFilm() : Boolean = name = \"Special\"",     
                               getCurrentMethodName());
				
            CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");
            List<object> paramTypes = new List<object>();
            CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
            CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
            Assert.IsNotNull(definedOperation);
            Assert.IsNotNull(definedOperation.getBodyDefinition());
            OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
        }

        [TestMethod]
        public void testNewOperation_NotOK02() {
            doTestManyContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"",     
                                   getCurrentMethodName());
				
            CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");
            List<object> paramTypes = new List<object>();
            CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
            CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
            Assert.IsNotNull(definedOperation);
            Assert.IsNotNull(definedOperation.getBodyDefinition());
            OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
        }

        [TestMethod]
        public void testNewOperation_NotOK03() {
            doTestManyContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  context SpecialFilm::isSpecialFilm() : Boolean body: true",     
                                   getCurrentMethodName());
				
        }

        [TestMethod]
        public void testNewOperation_OK04() {
            doTestManyContextOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  context SpecialFilm::isSpecialFilm() : Boolean post: true",     
                                getCurrentMethodName());
				
            CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");
            List<object> paramTypes = new List<object>();
            CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
            CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
            Assert.IsNotNull(definedOperation);
            Assert.IsNotNull(definedOperation.getBodyDefinition());
            OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
            Assert.IsNotNull(constraint);
            Assert.AreEqual("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
        }

        [TestMethod]
        public void testNewOperation_OK05() {
            doTestManyContextOK("context SpecialFilm def: xpto1000() : Integer = 1000  \r\n  context SpecialFilm def: abc : Integer = xpto1000() \r\n",     
                                getCurrentMethodName());
        }

	
    }
}
