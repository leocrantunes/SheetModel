using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.expressions;

namespace Ocl20Test.psw.parser.semantic
{
    [TestClass]
    public class TestIteratorExp : TestPropertyCallExp  {

        [TestCleanup]
        public void testCleanup()
        {
            tearDown();
        }

        [TestMethod]
        public void testIteratorExp_01() {
            List<object> constraints = doTestContextOK("context Film inv: self.tapes.theFilm = self.tapes.theFilm", getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp  = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Bag(Film)", "collect", "Tape", "iterator");

            checkAssociationEndCallExp(exp.getBody(), "theFilm", "Film");
            checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
			
            checkAssociationEndCallExp(exp.getSource(), "tapes", "Set(Tape)");
            checkImplicitSource((PropertyCallExp) exp.getSource(), "self", "Film");
        }

        [TestMethod]
        public void testIteratorExp_02() {
            List<object> constraints = doTestContextOK("context Rental inv: self.itens.Rental = self.itens.Rental",     
                                                       getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp  = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Sequence(Rental)", "collect", "RentalItem", "iterator");

            checkAssociationEndCallExp(exp.getBody(), "Rental", "Rental");
            checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "RentalItem");
			
            checkAssociationEndCallExp(exp.getSource(), "itens", "OrderedSet(RentalItem)");
            checkImplicitSource((PropertyCallExp) exp.getSource(), "self", "Rental");
        }

        [TestMethod]
        public void testIteratorExp_03() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes.theFilm.tapes = self.tapes.theFilm.tapes",     
                                    	                  getCurrentMethodName());

            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp  = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Bag(Tape)", "collect", "Film", "iterator");

            checkAssociationEndCallExp(exp.getBody(), "tapes", "Set(Tape)");
            checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Film");
			
            checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
        }

        [TestMethod]
        public void testExists_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->exists(number > 1)",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "iterator"); 
		
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
        }

        [TestMethod]
        public void testExists_02() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->exists(number = 1)",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "iterator"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), "=", "Boolean");
        }

        [TestMethod]
        public void testExists_03() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->exists(t | t.number = 1 and self.name = \"alex\")",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "t"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), "and", "Boolean");
        }

        [TestMethod]
        public void testExists_04() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->exists(t : Tape | t.number = 1 and self.name = \"alex\")",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "t"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), "and", "Boolean");
        }

        [TestMethod]
        public void testExists_05() {
            doTestContextNotOK("context Film inv: self.tapes->exists(t1 , t2 | t1.number = 1 and self.name = \"alex\")",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testForAll_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->forAll(t1 : Tape, t2 : Tape | t1 <> t2 and t1.number = 1 and self.name = \"alex\")",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            Assert.IsTrue(oclExpression is IteratorExp);
            IteratorExp exp = (IteratorExp) oclExpression;
            Assert.AreEqual("Boolean", exp.getType().getName());
            Assert.AreEqual("forAll", exp.getName());
            Assert.AreEqual(2, exp.getIterators().Count);
            VariableDeclaration v1 = (VariableDeclaration) exp.getIterators()[0];
            VariableDeclaration v2 = (VariableDeclaration) exp.getIterators()[1];
		
            Assert.AreEqual("t1", v1.getName());
            Assert.AreEqual("t2", v2.getName());
            Assert.AreEqual("Tape", v1.getType().getName());
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), "and", "Boolean");
        }

        [TestMethod]
        public void testExists_06() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->exists(t : Tape | t.theFilm.tapes->forAll(t | t.number = 1 and self.name=\"alex\"))",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "t"); 
			
            Assert.IsTrue(exp.getBody() is IteratorExp);
        }

        [TestMethod]
        public void testExists_07() {
            List<object> constraints	= doTestContextOK("context Film inv: " +
                                    	                  "Set{ 1, 2, 3}->forAll(i | i > 0)", 	getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            checkIteratorExp(oclExpression, "Boolean", "forAll", "Integer", "i"); 
        }

        [TestMethod]
        public void testExists_08() {
            doTestContextNotOK("context Film inv: self.tapes->exists(t : Film | t.name = \"alex\")",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testExists_09() {
            doTestContextNotOK("context Film inv: self.tapes[1]->exists(t : Film | t.name = \"alex\")",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testExists_10() {
            doTestContextNotOK("context Film inv: self.tapes->exists(t : Film = self.tapes[1] | t.number = 1)",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testCollect_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->collect(number)->notEmpty()",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp( ((ModelPropertyCallExp) oclExpression).getSource(), "Bag(Integer)", "collect", "Tape", "iterator"); 
			
            Assert.IsTrue(((ModelPropertyCallExp) oclExpression).getSource() is IteratorExp);
        }

        [TestMethod]
        public void testCollect_02() {
            List<object> constraints	= doTestContextOK("context Rental inv: self.itens->collect(number)->notEmpty()",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp( ((ModelPropertyCallExp) oclExpression).getSource(), "Sequence(String)", "collect", "RentalItem", "iterator"); 
            Console.WriteLine("source = " + exp.getSource().getType().getName());	
		
            Assert.IsTrue(((ModelPropertyCallExp) oclExpression).getSource() is IteratorExp);
        }

        [TestMethod]
        public void testSelect_01() { 
            List<object> constraints	= doTestContextOK("context Film inv: self.tapes->select(t2 | number = 10)->notEmpty()",     
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp( ((ModelPropertyCallExp) oclExpression).getSource(), "Set(Tape)", "select", "Tape", "t2"); 
			
            Assert.IsTrue(((ModelPropertyCallExp) oclExpression).getSource() is IteratorExp);
        }

        [TestMethod]
        public void testSelect_02() {
            doTestContextNotOK("context Film inv: self.tapes->select(t2 | t1.number = 10)->notEmpty()",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testForAll_IncorrectIterators() {
            doTestContextNotOK("context Film inv: self.tapes->forAll(t1, t2 : Tape = self.tapes[1] | t1 = t2)",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testNestedExpression_01() {
            doTestContextOK("context Client inv: Rental->forAll(r | r.returned = true and itens->forAll(Rental::maxDaysToReturn > Rental.Client.name.size() and number = 10 and name->notEmpty()))",     
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testImplicitCollect_01() {
            List<object> constraints	= doTestContextOK("context Client inv : self.Rental.itens->exists(i | i.number = 1)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "RentalItem", "i"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), "=", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Sequence(RentalItem)", "collect", "Rental", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is AssociationEndCallExp);
        }

        [TestMethod]
        public void testImplicitCollect_02() {
            List<object> constraints	= doTestContextOK("context Client inv : self.Rental.maxDaysToReturn->exists(x | x > 5)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Integer", "x"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Bag(Integer)", "collect", "Rental", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is AttributeCallExp);
		
        }

        [TestMethod]
        public void testImplicitCollect_03() {
            List<object> constraints	= doTestContextOK("context Person inv: Reservation.Film.getTapes()->exists(x | x.number > 5)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "x"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Sequence(Tape)", "collect", "Film", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is OperationCallExp);
        }

        [TestMethod]
        public void testImplicitCollect_04() {
            List<object> constraints	= doTestContextOK("context Tape inv: self.theFilm.getTapes().theFilm.getTapes()->exists( x | x.number > 5)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "x"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Bag(Tape)", "collect", "Film", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is OperationCallExp);
        }

        [TestMethod]
        public void testImplicitCollect_05() {
            List<object> constraints	= doTestContextOK("context Film inv: self.Reservation.Person->exists( x | x.age > 5)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Person", "x"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Bag(Person)", "collect", "Reservation", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is AssociationEndCallExp);
        }

        [TestMethod]
        public void testIncorrectIterator_01() { 
            doTestContextNotOK("context Film inv: self.tapes->select(t2 | number = 10)->select(t3 | t2.number = 10)->notEmpty()",     
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testImplicitCollect_06() {
            List<object> constraints	= doTestContextOK("context Film inv: self.Reservation.Person.EmployeeRanking[bosses]->exists( x | x.score > 5)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "EmployeeRanking", "x"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Bag(EmployeeRanking)", "collect", "Person", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is AssociationClassCallExp);
        }

        [TestMethod]
        public void testImplicitCollect_08() {
            doTestContextNotOK("context Film inv: self.Reservation.Person.EmployeeRanking->exists( x | x.score > 5)",      
                               getCurrentMethodName());
        }

        [TestMethod]
        public void testImplicitCollect_09() {
            doTestContextOK("context SpecialFilm inv : dist.films.dist.films.dist.Allocation->exists(x | x.abc > 10)",      
                            getCurrentMethodName());
        }

        [TestMethod]
        public void testImplicitCollect_07() {
            List<object> constraints	= doTestContextOK("context Distributor  inv: self.films.doSomething(rentalFee, 10, 20.5)->size() > 5",      
                                    	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testNestedIf_01() {
            List<object> constraints	= doTestContextOK("context Distributor  inv: self.films->forAll(f | f.rentalFee > 10 and tapes->select(number > 10 and if number <=0 then theFilm.rentalFee > 10 else theFilm.rentalFee < 5 endif)->size() > 3 )",      
                                    	                  getCurrentMethodName());
        }

        [TestMethod]
        public void testCollectTuple_01() {
            List<object> constraints	= doTestContextOK("context Film inv: self.Reservation.Person->collect(Tuple{a = age, b : Sequence(Integer) = bosses.age->asSequence()})->exists(x | x.a > 10)",      
                                    	                  getCurrentMethodName());
	
            OclExpression oclExpression = getConstraintExpression(constraints);
            IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tuple(a : Integer, b : Sequence(Integer))", "x"); 
			
            Assert.IsTrue(exp.getBody() is OperationCallExp);
            checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
            checkIteratorExp(exp.getSource(), "Bag(Tuple(a : Integer, b : Sequence(Integer)))", "collect", "Person", "iterator"); 
            Assert.IsTrue( ((IteratorExp)exp.getSource()).getBody() is TupleLiteralExp);
        }
    }
}
