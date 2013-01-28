using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestInvConstraints : TestPropertyCallExp {
        
        [TestMethod]
        public void testInv_01() {
            doTestContextOK("context Film inv : name.size() <= 10",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testInv_02() {
            doTestManyContextOK(
                "context Film inv: name.size() < 10 " +
                "context Tape inv: number > 30 " + 
                "context SpecialFilm inv: name.size() < 50",     
                getCurrentMethodName());
        }

        [TestMethod]
        public void testInv_03() {
            doTestManyContextOK(
                "context Film inv  names01: name.size() < 10 " +
                "context Tape inv: number > 30 " + 
                "context SpecialFilm inv names01: name.size() < 50",     
                getCurrentMethodName());
        }

        [TestMethod]
        public void testInv_04() {
            doTestManyContextOK(
                "context Film inv  names01: name.size() < 10 " +
                "context Tape inv: number > 30 " + 
                "context Film inv names02: name.size() < 50",     
                getCurrentMethodName());
        }

        [TestMethod]
        public void testInv_05() {
            doTestManyContextOK(
                "context Film inv  names01: name.size() < 10 " +
                "context Film inv : name.size() > 20 " + 
                "context Film inv : name.size() < 50",     
                getCurrentMethodName());
				
            CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
            Assert.AreEqual(3, film.getAllInvariants().Count);
            Assert.IsNotNull(film.getInvariant("names01"));				
        }

        [TestMethod]
        public void testInvNotOK_01() {
            doTestContextNotOK("context Film inv : name.size() ",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testInvNotOK_02() {
            doTestManyContextNotOK(
                "context Film inv  names01: name.size() < 10 " +
                "context Tape inv: number > 30 " + 
                "context Film inv names01: name.size() < 50",     
                getCurrentMethodName());
        }

    }
}

