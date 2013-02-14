using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.iface.util;
using Ocl20.library.impl.types;
using Ocl20.library.impl.util;
using Ocl20.modelreader;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20Test.MDRRepository.models.ocl20
{
    [TestClass]
    public class TestOclModelElements
    {
        protected static XmiReader reader;
        protected static CoreModel umlModel = null;
        protected static String extentName = "RoseExample";

        [ClassInitialize]
        public static void setUp(TestContext testContext)
        {
            reader = new XmiReader(@"C:\Users\Leo\Documents\visual studio 2010\Projects\SheetModel_20121206\SheetModel\Ocl20Test\resource\myExampleRose.xml");
            setUmlModelsRepository();
        }

        public static void setUmlModelsRepository()
        {
            Assert.IsNotNull(umlModel = reader.getModel());
        }

        protected CoreClassifier getClassifier(String name)
        {
            return (CoreClassifier) umlModel.getEnvironmentWithoutParents().lookup(name);
        }

        [TestMethod]
        public void testBooleanLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            BooleanLiteralExp exp1 = factory1.createBooleanLiteralExp(true, getClassifier("Boolean"));
            Assert.AreEqual("True", exp1.ToString());
            Assert.AreEqual("Boolean", exp1.getType().getName());

            AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            BooleanLiteralExp exp2 = factory2.createBooleanLiteralExp(true, getClassifier("Boolean"));
            Assert.AreEqual("True", exp2.ToString());

            Assert.IsFalse(exp1 == exp2);
            Assert.IsTrue(factory1 == factory2);
        }

        [TestMethod]
        public void testIntegerLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            IntegerLiteralExp exp1 = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            Assert.AreEqual("100", exp1.ToString());
            Assert.AreEqual("Integer", exp1.getType().getName());
        }

        [TestMethod]
        public void testRealLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            RealLiteralExp exp1 = factory1.createRealLiteralExp("247.49", getClassifier("Real"));
            Assert.AreEqual("247.49", exp1.ToString());
            Assert.AreEqual("Real", exp1.getType().getName());
        }

        [TestMethod]
        public void testStringLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            StringLiteralExp exp1 = factory1.createStringLiteralExp("England", getClassifier("String"));
            Assert.AreEqual("\'England\'", exp1.ToString());
            Assert.AreEqual("String", exp1.getType().getName());
        }

        [TestMethod]
        public void testEnumLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            CoreEnumeration situation = (CoreEnumeration) getClassifier("Situation");
            CoreEnumLiteral married = situation.lookupEnumLiteral("married");
            EnumLiteralExp exp = factory1.createEnumLiteralExp(married);
            Assert.AreEqual("Situation::married", exp.ToString());
        }

        [TestMethod]
        public void testCollectionLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            CollectionLiteralExp exp = factory1.createCollectionLiteralExp(createCollectionParts(),
                                                                           factory1.createSetType(
                                                                               getClassifier("Integer")));
            Assert.AreEqual("Set{ 100, 200, 300 }", exp.ToString());
            Assert.AreEqual("Set(Integer)", exp.getType().getName());

            CollectionLiteralExp exp2 = factory1.createCollectionLiteralExp(createCollectionParts(),
                                                                            factory1.createSetType(
                                                                                getClassifier("Integer")));
            Assert.AreEqual("Set{ 100, 200, 300 }", exp2.ToString());
            Assert.AreEqual("Set(Integer)", exp2.getType().getName());

            exp = factory1.createCollectionLiteralExp(createCollectionParts(),
                                                      factory1.createBagType(getClassifier("Integer")));
            Assert.AreEqual("Bag{ 100, 200, 300 }", exp.ToString());
            Assert.AreEqual("Bag(Integer)", exp.getType().getName());

            exp = factory1.createCollectionLiteralExp(createCollectionParts(),
                                                      factory1.createSequenceType(getClassifier("Integer")));
            Assert.AreEqual("Sequence{ 100, 200, 300 }", exp.ToString());
            Assert.AreEqual("Sequence(Integer)", exp.getType().getName());

            exp = factory1.createCollectionLiteralExp(createCollectionParts(),
                                                      factory1.createOrderedSetType(getClassifier("Integer")));
            Assert.AreEqual("OrderedSet{ 100, 200, 300 }", exp.ToString());
            Assert.AreEqual("OrderedSet(Integer)", exp.getType().getName());

            OclExpression elem1 = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            OclExpression elem2 = factory1.createIntegerLiteralExp(200, getClassifier("Integer"));
            CollectionRange range = factory1.createCollectionRange(elem1, elem2);
            List<object> parts = new List<object> ();
            parts.Add(range);
            exp = factory1.createCollectionLiteralExp(parts, factory1.createSetType(getClassifier("Integer")));
            Assert.AreEqual("Set{ 100 .. 200 }", exp.ToString());
            Assert.AreEqual("Set(Integer)", exp.getType().getName());
        }

        private List<object> createCollectionParts()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            OclExpression elem1 = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            OclExpression elem2 = factory1.createIntegerLiteralExp(200, getClassifier("Integer"));
            OclExpression elem3 = factory1.createIntegerLiteralExp(300, getClassifier("Integer"));
            List<object> collectionParts = new List<object> ();
            collectionParts.Add(factory1.createCollectionItem(elem1));
            collectionParts.Add(factory1.createCollectionItem(elem2));
            collectionParts.Add(factory1.createCollectionItem(elem3));

            return collectionParts;
        }

        [TestMethod]
        public void testTupleLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            OclExpression init1 = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            VariableDeclaration part1 = factory1.createVariableDeclaration("a", getClassifier("Integer"), init1);

            OclExpression init2 = factory1.createRealLiteralExp("100.38", getClassifier("Real"));
            VariableDeclaration part2 = factory1.createVariableDeclaration("b", getClassifier("Real"), init2);

            OclExpression init3 = factory1.createBooleanLiteralExp(true, getClassifier("Boolean"));
            VariableDeclaration part3 = factory1.createVariableDeclaration("c", getClassifier("Boolean"), init3);

            List<object> parts = new List<object> ();
            parts.Add(part1);
            parts.Add(part2);
            parts.Add(part3);

            TupleTypeImpl tupleType = (TupleTypeImpl) factory1.createTupleType();
            tupleType.addElement("a", getClassifier("Integer"));
            tupleType.addElement("b", getClassifier("Real"));
            tupleType.addElement("c", getClassifier("Boolean"));
            Assert.AreEqual("Tuple(a : Integer, b : Real, c : Boolean)", tupleType.getName());

            //		TuplePartType part1Type = factory1.createTuplePartType(tupleType, "a", getClassifier("Integer"));
            //		TuplePartType part2Type = factory1.createTuplePartType(tupleType, "b", getClassifier("Real"));
            //		TuplePartType part3Type = factory1.createTuplePartType(tupleType, "c", getClassifier("Boolean"));

            OclExpression exp = factory1.createTupleLiteralExp(parts, tupleType);

            Assert.AreEqual("Tuple{ a : Integer = 100, b : Real = 100.38, c : Boolean = True }", exp.ToString());

            Assert.AreEqual("Tuple(a : Integer, b : Real, c : Boolean)", exp.getType().getName());

            Assert.IsNotNull(exp.getType().lookupAttribute("a"));
            Assert.IsNull(exp.getType().lookupAttribute("d"));
        }

        [TestMethod]
        public void testVariableExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            OclExpression init1 = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Integer"), init1);

            VariableExp exp1 = factory1.createVariableExp(variable);
            VariableExp exp2 = factory1.createVariableExp(variable);

            Assert.AreEqual("abc", exp1.ToString());
            Assert.AreEqual("Integer", exp1.getType().getName());

            Assert.AreEqual("abc", exp2.ToString());
            Assert.AreEqual("Integer", exp2.getType().getName());

            Assert.AreEqual("100", variable.getInitExpression().ToString());
        }

        [TestMethod]
        public void testAttributeCallExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Film"), null);
            VariableExp source1 = factory1.createVariableExp(variable);
            VariableExp source2 = factory1.createVariableExp(variable);

            CoreAttribute attribute = getClassifier("Film").lookupAttribute("name");

            AttributeCallExp exp1 = factory1.createAttributeCallExp(source1, attribute, false);
            AttributeCallExp exp2 = factory1.createAttributeCallExp(source2, attribute, true);

            Assert.AreEqual("abc.name", exp1.ToString());
            Assert.AreEqual("String", exp1.getType().getName());

            Assert.AreEqual("abc.name@pre", exp2.ToString());
            Assert.AreEqual("String", exp2.getType().getName());
        }

        [TestMethod]
        public void testOperationCallExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Film"), null);
            VariableExp source = factory1.createVariableExp(variable);

            List<object> paramTypes = new List<object> ();
            paramTypes.Add(getClassifier("Integer"));

            CoreOperation operation = getClassifier("Film").lookupOperation("getRentalFee", paramTypes);

            List<object> arguments = new List<object> ();
            arguments.Add(factory1.createIntegerLiteralExp(100, getClassifier("Integer")));

            OperationCallExp exp = factory1.createOperationCallExp(source, operation, arguments,
                                                                   operation.getReturnType(), false);

            Assert.AreEqual("abc.getRentalFee(100)", exp.ToString());
            Assert.AreEqual("Real", exp.getType().getName());

            CollectionLiteralExp literalCollection = factory1.createCollectionLiteralExp(createCollectionParts(),
                                                                                         factory1.createSetType(
                                                                                             getClassifier("Integer")));
            CollectionTypeImpl type1 = (CollectionTypeImpl) factory1.createSetType(getClassifier("Integer"));
            CoreOperation collectionOper = type1.lookupOperation("size", new List<object>());

            OperationCallExp exp1 = factory1.createOperationCallExp(literalCollection, collectionOper, new List<object>(),
                                                                    getClassifier("Integer"), false);
            Assert.IsNotNull(exp1);
        }

        [TestMethod]
        public void testOperationCallExpWithAtPre()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Film"), null);
            VariableExp source = factory1.createVariableExp(variable);

            List<object> paramTypes = new List<object> ();
            paramTypes.Add(getClassifier("Integer"));

            CoreOperation operation = getClassifier("Film").lookupOperation("getRentalFee", paramTypes);

            List<object> arguments = new List<object> ();
            arguments.Add(factory1.createIntegerLiteralExp(100, getClassifier("Integer")));

            OperationCallExp exp = factory1.createOperationCallExp(source, operation, arguments,
                                                                   operation.getReturnType(), true);

            Assert.AreEqual("abc.getRentalFee@pre(100)", exp.ToString());
            Assert.AreEqual("Real", exp.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_01()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("SpecialFilm"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("SpecialFilm").lookupAssociationEnd("dist");

            AssociationEndCallExp exp1 = factory1.createAssociationEndCallExp(source, assocEnd, null, null, false);

            Assert.AreEqual("abc.dist", exp1.ToString());
            Assert.AreEqual("Set(Distributor)", exp1.getType().getName());
        }

        [TestMethod]
        public void fTest_01()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("SpecialFilm"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreClassifier c1 = getClassifier("SpecialFilm");

            CoreModel coreModel = c1.getModel();

            List<object> ass = coreModel.getAllAssociations();

            
            CoreClassifier c2 = getClassifier("Film");
            List<object> core = c1.getAllAssociationEnds();
            List<object> core2 = c2.getAllAssociationEnds();

            var a = c2.lookupAssociationEnd("tapes");
        }

        [TestMethod]
        public void testAssociationEndCallExp_02()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Reservation"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("Reservation").lookupAssociationEnd("Person");

            AssociationEndCallExp exp1 = factory1.createAssociationEndCallExp(source, assocEnd, null, null, false);

            Assert.AreEqual("abc.Person", exp1.ToString());
            Assert.AreEqual("Person", exp1.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_03()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Person"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("Person").lookupAssociationEnd("Reservation");

            AssociationEndCallExp exp1 = factory1.createAssociationEndCallExp(source, assocEnd, null, null, false);

            Assert.AreEqual("abc.Reservation", exp1.ToString());
            Assert.AreEqual("OrderedSet(Reservation)", exp1.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_04()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Person"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("Person").lookupAssociationEnd("Reservation");
            AssociationEndCallExp sourceExp = factory1.createAssociationEndCallExp(source, assocEnd, null, null, false);

            assocEnd = getClassifier("Reservation").lookupAssociationEnd("RentalItem");
            AssociationEndCallExp exp = factory1.createAssociationEndCallExp(sourceExp, assocEnd, null, null, false);

            Assert.AreEqual("abc.Reservation.RentalItem", exp.ToString());
            Assert.AreEqual("Sequence(RentalItem)", exp.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_05()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("RentalItem"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("RentalItem").lookupAssociationEnd("Reservation");
            AssociationEndCallExp sourceExp = factory1.createAssociationEndCallExp(source, assocEnd, null, null, false);

            assocEnd = getClassifier("Reservation").lookupAssociationEnd("Person");
            AssociationEndCallExp exp = factory1.createAssociationEndCallExp(sourceExp, assocEnd, null, null, false);

            Assert.AreEqual("abc.Reservation.Person", exp.ToString());
            Assert.AreEqual("Bag(Person)", exp.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_06()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Person"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd bosses = getClassifier("Person").lookupAssociationEnd("bosses");
            CoreAssociationEnd employees = getClassifier("Person").lookupAssociationEnd("employees");
            AssociationEndCallExp sourceExp = factory1.createAssociationEndCallExp(source, bosses, employees, null,
                                                                                   false);

            CoreAssociationEnd assocEnd = getClassifier("Person").lookupAssociationEnd("Reservation");
            AssociationEndCallExp exp = factory1.createAssociationEndCallExp(sourceExp, assocEnd, null, null, false);

            Assert.AreEqual("abc.bosses.Reservation", exp.ToString());
            Assert.AreEqual("Sequence(Reservation)", exp.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_07()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Reservation"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("Reservation").lookupAssociationEnd("Person");

            AssociationEndCallExp exp1 = factory1.createAssociationEndCallExp(source, assocEnd, null, null, true);

            Assert.AreEqual("abc.Person@pre", exp1.ToString());
            Assert.AreEqual("Person", exp1.getType().getName());
        }

        [TestMethod]
        public void testAssociationEndCallExp_08()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Allocation"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationEnd assocEnd = getClassifier("Allocation").lookupAssociationEnd("films");

            AssociationEndCallExp exp1 = factory1.createAssociationEndCallExp(source, assocEnd, null, null, false);

            Assert.AreEqual("abc.films", exp1.ToString());
            Assert.AreEqual("SpecialFilm", exp1.getType().getName());
        }

        [TestMethod]
        public void testAssociationClassExp_01()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("SpecialFilm"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationClass assocClass = getClassifier("SpecialFilm").lookupAssociationClass("Allocation");
            //		CoreAssociationEnd dist = getClassifier("SpecialFilm").lookupAssociationEnd("dist");

            AssociationClassCallExp exp1 = factory1.createAssociationClassCallExp(source, assocClass, null, null, false);

            Assert.AreEqual("abc.Allocation", exp1.ToString());
            Assert.AreEqual("Set(Allocation)", exp1.getType().getName());
        }

        [TestMethod]
        public void testAssociationClassExp_02()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("SpecialFilm"), null);
            VariableExp source = factory1.createVariableExp(variable);

            CoreAssociationClass assocClass = getClassifier("SpecialFilm").lookupAssociationClass("Allocation");
            CoreAssociationEnd dist = getClassifier("SpecialFilm").lookupAssociationEnd("dist");

            AssociationClassCallExp exp1 = factory1.createAssociationClassCallExp(source, assocClass, dist, null, false);

            Assert.AreEqual("abc.Allocation", exp1.ToString());
            Assert.AreEqual("Set(Allocation)", exp1.getType().getName());
        }

        [TestMethod]
        public void testIfExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Boolean"), null);
            VariableExp condition = factory1.createVariableExp(variable);

            IntegerLiteralExp thenExp = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            RealLiteralExp elseExp = factory1.createRealLiteralExp("247.49", getClassifier("Real"));

            IfExp exp = factory1.createIfExp(condition, thenExp, elseExp);

            Assert.AreEqual("if abc then 100 else 247.49 endif", exp.ToString());
            Assert.AreEqual("Real", exp.getType().getName());
        }

        [TestMethod]
        public void testLetExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            OclExpression initExp = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Integer"), initExp);

            OclExpression varExp = factory1.createVariableExp(variable);
            OclExpression intLiteral = factory1.createIntegerLiteralExp(200, getClassifier("Integer"));
            List<object> paramTypes = new List<object>();
            paramTypes.Add(getClassifier("Integer"));
            CoreOperation oper = getClassifier("Integer").lookupOperation("+", paramTypes);

            List<object> args = new List<object> ();
            args.Add(intLiteral);
            OclExpression inExp = factory1.createOperationCallExp(varExp, oper, args, getClassifier("Integer"), false);

            LetExp exp = factory1.createLetExp(variable, inExp);

            Assert.AreEqual("let abc : Integer = 100 in abc + 200", exp.ToString());
            Assert.AreEqual("Integer", exp.getType().getName());
        }

        [TestMethod]
        public void testIteratorExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Distributor"), null);
            VariableExp source = factory1.createVariableExp(variable);

            VariableDeclaration iter = factory1.createVariableDeclaration("iter", getClassifier("SpecialFilm"), null);
            VariableExp iterRef = factory1.createVariableExp(iter);

            CoreAttribute attr = getClassifier("SpecialFilm").lookupAttribute("name");
            AttributeCallExp body = factory1.createAttributeCallExp(iterRef, attr, false);

            CoreClassifier setSpecialFilm = factory1.createSetType(getClassifier("SpecialFilm"));

            List<object> iterators = new List<object> ();
            iterators.Add(iter);

            IteratorExp exp = factory1.createIteratorExp("select", setSpecialFilm, source, body, iterators);

            Assert.AreEqual("abc->select(iter : SpecialFilm | iter.name)", exp.ToString());
            Assert.AreEqual("Set(SpecialFilm)", exp.getType().getName());
        }

        [TestMethod]
        public void testIterateExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            VariableDeclaration variable = factory1.createVariableDeclaration("abc", getClassifier("Distributor"), null);
            VariableExp source = factory1.createVariableExp(variable);

            VariableDeclaration iter = factory1.createVariableDeclaration("iter", getClassifier("SpecialFilm"), null);
            VariableExp iterRef = factory1.createVariableExp(iter);

            CoreAttribute attr = getClassifier("SpecialFilm").lookupAttribute("lateReturnFee");
            AttributeCallExp attCall = factory1.createAttributeCallExp(iterRef, attr, false);

            IntegerLiteralExp initExp = factory1.createIntegerLiteralExp(100, getClassifier("Integer"));
            VariableDeclaration result = factory1.createVariableDeclaration("result", getClassifier("Integer"), initExp);


            List<object> paramTypes = new List<object>();
            paramTypes.Add(getClassifier("Integer"));
            CoreOperation oper = getClassifier("Integer").lookupOperation("+", paramTypes);

            VariableExp resultExp = factory1.createVariableExp(result);
            List<object> args = new List<object>();
            args.Add(attCall);
            OclExpression body = factory1.createOperationCallExp(resultExp, oper, args, getClassifier("Integer"), false);

            List<object> iterators = new List<object>();
            iterators.Add(iter);

            IterateExp exp = factory1.createIterateExp(getClassifier("Integer"), source, body, iterators, result);

            Assert.AreEqual("abc->iterate(iter : SpecialFilm ; result : Integer = 100 | result + iter.lateReturnFee)",
                         exp.ToString());
            Assert.AreEqual("Integer", exp.getType().getName());
        }

        [TestMethod]
        public void testOclModelElementLiteralExp()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            OclTypeLiteralExp exp =
                factory1.createOclTypeLiteralExp(factory1.createOclModelElementType(getClassifier("Film")));

            Assert.AreEqual("Film", exp.ToString());
            Assert.AreEqual("OclModelElementType", exp.getType().getName());
        }

        [TestMethod]
        public void testCollectionType_01()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());

            CollectionTypeImpl type1 = (CollectionTypeImpl) factory1.createSetType(getClassifier("Integer"));
            CollectionTypeImpl type2 = (CollectionTypeImpl) factory1.createSetType(getClassifier("Real"));
            CollectionTypeImpl type3 =
                (CollectionTypeImpl) factory1.createSetType(factory1.createSetType(getClassifier("Real")));
            CollectionTypeImpl type4 = (CollectionTypeImpl) factory1.createBagType(getClassifier("Integer"));
            CollectionTypeImpl type5 = (CollectionTypeImpl) factory1.createBagType(getClassifier("Real"));
            CollectionTypeImpl type6 = (CollectionTypeImpl) factory1.createBagType(getClassifier("Boolean"));
            CollectionTypeImpl type7 = (CollectionTypeImpl) factory1.createOrderedSetType(getClassifier("Integer"));
            CollectionTypeImpl type8 = (CollectionTypeImpl) factory1.createSetType(getClassifier("Film"));
            CollectionTypeImpl type9 = (CollectionTypeImpl) factory1.createSetType(getClassifier("SpecialFilm"));
            CollectionTypeImpl type10 = (CollectionTypeImpl) factory1.createCollectionType(getClassifier("OclAny"));

            Assert.AreEqual("Integer", type1.getInnerMostElementType().getName());
            Assert.AreEqual("Real", type3.getInnerMostElementType().getName());

            Assert.AreEqual("Set(Real)", type1.getMostSpecificCommonSuperType(type2).getName());
            Assert.AreEqual("Collection(Integer)", type1.getMostSpecificCommonSuperType(type4).getName());
            Assert.AreEqual("Set(OclAny)", type1.getMostSpecificCommonSuperType(type3).getName());
            Assert.AreEqual("OclAny", type1.getMostSpecificCommonSuperType(getClassifier("Integer")).getName());
            Assert.AreEqual("Collection(Real)", type1.getMostSpecificCommonSuperType(type5).getName());
            Assert.AreEqual("Collection(OclAny)", type1.getMostSpecificCommonSuperType(type6).getName());

            Assert.IsTrue(getClassifier("Integer").conformsTo(getClassifier("Real")));
            Assert.IsTrue(getClassifier("Integer").conformsTo(getClassifier("OclAny")));
            Assert.IsTrue(type1.conformsTo(type2));
            Assert.IsFalse(type2.conformsTo(type1));
            Assert.IsFalse(type1.conformsTo(type3));
            Assert.IsFalse(type1.conformsTo(type4));
            Assert.IsTrue(type1.conformsTo(type1.getMostSpecificCommonSuperType(type2)));
            Assert.IsTrue(type1.conformsTo(type1.getMostSpecificCommonSuperType(type5)));
            Assert.IsTrue(type1.conformsTo(type1.getMostSpecificCommonSuperType(type6)));
            Assert.IsFalse(type1.conformsTo(getClassifier("Integer")));
            Assert.IsTrue(type1.conformsTo(getClassifier("OclAny")));

            type1.setInnerMostElementType(getClassifier("Real"));
            Assert.AreEqual("Real", type1.getInnerMostElementType().getName());
            Assert.AreEqual("Set(Real)", type1.getName());

            Assert.IsTrue(type7.conformsTo(type1));
            Assert.IsFalse(type1.conformsTo(type7));
            Assert.IsTrue(type7.conformsTo(type7.createGenericCollectionType(type7.getInnerMostElementType())));

            Assert.IsTrue(type9.conformsTo(type8));
            Assert.IsTrue(type9.conformsTo(type10));
            Assert.IsFalse(type8.conformsTo(type9));
        }

        [TestMethod]
        public void testLookupOperation()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            CollectionTypeImpl type1 = (CollectionTypeImpl) factory1.createSetType(getClassifier("Integer"));
            Assert.IsNotNull(type1.lookupOperation("size", new List<object>()));

            CoreClassifier intType = getClassifier("Integer");
            List<object> paramTypes = new List<object>();
            paramTypes.Add(getClassifier("Integer"));
            Assert.IsNotNull(intType.lookupOperation("+", paramTypes));
            Assert.IsNull(intType.lookupOperation("*", new List<object>()));
        }

        [TestMethod]
        public void testTupleType()
        {
            AstOclModelElementFactory factory1 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
            factory1.resetTypes();

            TupleTypeImpl type1 = (TupleTypeImpl) factory1.createTupleType();
            type1.addElement("a", getClassifier(("Integer")));
            type1.addElement("b", getClassifier(("Boolean")));

            TupleTypeImpl type2 = (TupleTypeImpl) factory1.createTupleType();
            type2.addElement("a", getClassifier(("Real")));
            type2.addElement("b", getClassifier(("Boolean")));

            TupleTypeImpl type3 = (TupleTypeImpl) factory1.createTupleType();
            type3.addElement("a", getClassifier(("Integer")));
            type3.addElement("b", getClassifier(("Integer")));

            TupleTypeImpl type4 = (TupleTypeImpl) factory1.createTupleType();
            type4.addElement("a", getClassifier(("Real")));
            type4.addElement("c", getClassifier(("Boolean")));

            TupleTypeImpl type5 = (TupleTypeImpl) factory1.createTupleType();
            type5.addElement("a", getClassifier(("Integer")));
            type5.addElement("b", getClassifier(("Boolean")));
            type5.addElement("c", getClassifier(("Boolean")));

            TupleTypeImpl type6 = (TupleTypeImpl) factory1.createTupleType();
            type6.addElement("a", getClassifier(("Integer")));

            TupleTypeImpl type7 = (TupleTypeImpl) factory1.createTupleType();
            type7.addElement("a", getClassifier(("Integer")));
            type7.addElement("b", type1);

            Assert.IsTrue(type1.conformsTo(type2));
            Assert.IsFalse(type1.conformsTo(type3));
            Assert.IsFalse(type1.conformsTo(type4));
            Assert.IsFalse(type1.conformsTo(type5));
            Assert.IsFalse(type1.conformsTo(type6));

            Assert.IsNotNull(type1.lookupAttribute("a"));
            Assert.AreEqual("Integer", type1.lookupAttribute("a").getFeatureType().getName());
            Assert.AreEqual("Tuple(a : Integer, b : Boolean)", type1.getName());
            Assert.AreEqual("Tuple(a : Integer, b : Boolean, c : Boolean)", type5.getName());

            foreach (TuplePartType part in type1.getTupleParts())
            {}

            CollectionTypeImpl col = (CollectionTypeImpl) factory1.createSetType(type1);
            Assert.AreEqual("Set(Tuple(a : Integer, b : Boolean))", col.getName());

            CollectionTypeImpl col2 = (CollectionTypeImpl) factory1.createSetType(type7);
            Assert.AreEqual("Set(Tuple(a : Integer, b : Tuple(a : Integer, b : Boolean)))", col2.getName());
        }
    }
}
