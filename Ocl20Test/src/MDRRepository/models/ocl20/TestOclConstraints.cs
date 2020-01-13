using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.util;
using Ocl20.library.impl.util;
using Ocl20.modelreader;

namespace Ocl20Test.MDRRepository.models.ocl20
{
    [TestClass]
    public class TestOclConstraints {

        protected static XmiReader reader;
        protected	static CoreModel	  umlModel = null;
        protected static String extentName = "RoseExample";

        [ClassInitialize]
        public static void setUp(TestContext testContext)
        {
            reader = new XmiReader(@"C:\Repos\SheetModel\Ocl20Test\resource\myExampleRose.xml");
            setUmlModelsRepository();
        }
	
        public static void setUmlModelsRepository() {
            Assert.IsNotNull(umlModel = reader.getModel());
        }

        protected CoreClassifier getClassifier(String name) {
            return	(CoreClassifier) umlModel.getEnvironmentWithoutParents().lookup(name);
        }

        [TestMethod]
        public void testAttributeInit() {
            AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
            AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
            IntegerLiteralExp exp1 = factory2.createIntegerLiteralExp(100, getClassifier("Integer"));
            ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
            OclAttributeInitConstraint constraint = (OclAttributeInitConstraint) factory1.createAttributeInitConstraint("test.ocl", getClassifier("Film"), getClassifier("Film").lookupAttribute("rentalFee"), 
                                                                                                                        expInOcl);

            Assert.AreEqual("init: 100", constraint.ToString());
		
            OclConstraint c = getClassifier("Film").getInitConstraint("rentalFee");
            Assert.AreEqual(constraint, c);
		
            CoreAttribute attrib = getClassifier("Film").lookupAttribute("rentalFee");
            ExpressionInOcl exp = attrib.getInitialValueExpression();
            Assert.AreEqual(exp, expInOcl);
		
            getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
            Assert.IsNull(getClassifier("Film").getInitConstraint("rentalFee"));
        }

        [TestMethod]
        public void testAttributeDerive() {
            AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
            AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
            IntegerLiteralExp exp1 = factory2.createIntegerLiteralExp(100, getClassifier("Integer"));
            ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
            OclAttributeDeriveConstraint constraint = (OclAttributeDeriveConstraint) factory1.createAttributeDeriveConstraint("test.ocl", getClassifier("Film"), getClassifier("Film").lookupAttribute("rentalFee"), 
                                                                                                                              expInOcl);

            Assert.AreEqual("derive: 100", constraint.ToString());
		
            OclConstraint c = getClassifier("Film").getDeriveConstraint("rentalFee");
            Assert.AreEqual(constraint, c);
		
            CoreAttribute attrib = getClassifier("Film").lookupAttribute("rentalFee");
            Assert.IsNotNull(attrib);
            Assert.AreEqual(expInOcl, attrib.getDerivedValueExpression());
            Assert.IsTrue(attrib.isDerived());
		
            getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
            Assert.IsNull(getClassifier("Film").getDeriveConstraint("rentalFee"));
        }

        [TestMethod]
        public void testInvariant() {
            AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
            AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
            BooleanLiteralExp exp1 = factory2.createBooleanLiteralExp(true, getClassifier("Boolean"));
            ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
            OclInvariantConstraint constraint = (OclInvariantConstraint) factory1.createInvariantConstraint("test.ocl", "myInvariant", getClassifier("Film"), expInOcl);

            Assert.AreEqual("inv myInvariant: True", constraint.ToString());
		
            Assert.IsTrue(getClassifier("Film").getAllInvariants().Contains(constraint));
		
            getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
            Assert.IsFalse(getClassifier("Film").getAllInvariants().Contains(constraint));
        }

        [TestMethod]
        public void testBody() {
            AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
            AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
            RealLiteralExp exp1 = factory2.createRealLiteralExp("200.50", getClassifier("Real"));
            ExpressionInOcl expInOcl = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);
		
            List<object> parms = new List<object>();
            parms.Add(getClassifier("Integer"));
            CoreOperation oper = getClassifier("Film").lookupOperation("getRentalFee", parms);
		
            OclBodyConstraint constraint = (OclBodyConstraint) factory1.createBodyConstraint("test.ocl", oper, expInOcl, null);

            Assert.AreEqual("body: 200.50", constraint.ToString());
		
            Assert.AreEqual(constraint, oper.getBodyDefinition());
            Assert.AreEqual(expInOcl, oper.getBodyDefinition().getExpression());
		
            oper.deleteAllConstraintsForSource("test.ocl");
            Assert.IsNull(oper.getBodyDefinition());
        }

        [TestMethod]
        public void testPrePost() {
            AstOclConstraintFactory factory1 = AstOclConstraintFactoryManager.getInstance(umlModel.getOclPackage());
            AstOclModelElementFactory factory2 = AstOclModelElementFactoryManager.getInstance(umlModel.getOclPackage());
		
            BooleanLiteralExp exp1 = factory2.createBooleanLiteralExp(true, getClassifier("Boolean"));
            ExpressionInOcl expInOcl1 = factory1.createExpressionInOcl("name", getClassifier("Film"), exp1);

            BooleanLiteralExp exp2 = factory2.createBooleanLiteralExp(false, getClassifier("Boolean"));
            ExpressionInOcl expInOcl2 = factory1.createExpressionInOcl("name", getClassifier("Film"), exp2);

            List<object> parms = new List<object>();
            parms.Add(getClassifier("Integer"));
            CoreOperation oper = getClassifier("Film").lookupOperation("getRentalFee", parms);
		
            OclPrePostConstraint constraint = (OclPrePostConstraint) factory1.createPrePostConstraint("test.ocl", oper);
            OclConstraint preConstraint = factory1.createPreConstraint(constraint, "test.ocl", "myPre", oper, expInOcl1);
            OclConstraint postConstraint = factory1.createPostConstraint(constraint, "test.ocl", "myPost", oper, expInOcl2);
		
            Assert.AreEqual("pre myPre: True", preConstraint.ToString());
            Assert.AreEqual("post myPost: False", postConstraint.ToString());
		
            Assert.IsTrue(oper.getSpecifications().Contains(constraint));
            OclPrePostConstraint example = (OclPrePostConstraint) oper.getSpecifications()[0];
            Assert.IsTrue(example.getPreConditions().Contains((OclPreConstraint) preConstraint));
            Assert.IsTrue(example.getPostConditions().Contains((OclPostConstraint) postConstraint));
		
            oper.deleteAllConstraintsForSource("test.ocl");
            Assert.AreEqual(0, oper.getSpecifications().Count);
        }

        [TestMethod]
        public void testDefAttribute()
        {
            getClassifier("Film").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
            
            try
            {
                getClassifier("Film").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
                throw new AssertFailedException();
            }
            catch (Exception e)
            {}

            try
            {
                getClassifier("Product").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
                throw new AssertFailedException();
            }
            catch (Exception e)
            {}

            try
            {
                getClassifier("Film").addDefinedElement("test.ocl", "name", getClassifier("Integer"));
                throw new AssertFailedException();
            }
            catch (Exception e)
            {}

            CoreAttribute attr = getClassifier("Film").lookupAttribute("myNewAttr");
            Assert.AreEqual("Integer", attr.getFeatureType().getName());
            Assert.IsTrue(attr.isOclDefined());

            CoreAttribute nameAttr = getClassifier("Film").lookupAttribute("name");
            Assert.AreEqual("String", nameAttr.getFeatureType().getName());
            Assert.IsFalse(nameAttr.isOclDefined());

            getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
            Assert.IsNull(getClassifier("Film").lookupAttribute("myNewAttr"));
        }

        [TestMethod]
        public void testDefOperation()
        {
            List<object> paramNames = new List<object>();
            paramNames.Add("param1");

            List<object> paramTypes = new List<object>();
            paramTypes.Add(getClassifier("Real"));

            getClassifier("Film").addDefinedOperation("test.ocl", "myNewOper", paramNames, paramTypes, getClassifier("Integer"));
            
            CoreOperation oper = getClassifier("Film").lookupOperation("myNewOper", paramTypes);
            Assert.AreEqual("Integer", oper.getReturnType().getName());
            Assert.IsTrue(oper.isOclDefined());
            List<object> args = new List<object>();
            args.Add(getClassifier("Integer"));
            Assert.IsTrue(oper.hasMatchingSignature(args));

            getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
            Assert.IsNull(getClassifier("Film").lookupOperation("myNewOper", paramTypes));
        }

	
        //[TestMethod]
        //public void testDefAttribute() {
        //    getClassifier("Film").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
        //    getClassifier("Film").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
        //    getClassifier("Product").addDefinedElement("test.ocl", "myNewAttr", getClassifier("Integer"));
        //    getClassifier("Film").addDefinedElement("test.ocl", "name", getClassifier("Integer"));

        //    CoreAttribute attr = getClassifier("Film").lookupAttribute("myNewAttr");
        //    Assert.AreEqual("Integer", attr.getFeatureType().getName());
        //    Assert.IsTrue(attr.isOclDefined());
		
        //    CoreAttribute nameAttr = getClassifier("Film").lookupAttribute("name");
        //    Assert.AreEqual("String", nameAttr.getFeatureType().getName());
        //    Assert.IsFalse(nameAttr.isOclDefined());
		
        //    getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
        //    Assert.IsNull(getClassifier("Film").lookupAttribute("myNewAttr"));
        //}

        //[TestMethod]
        //public void testDefOperation() {
        //    List<object> paramNames = new List<object>();
        //    paramNames.Add("param1");
		
        //    List<object> paramTypes = new List<object>();
        //    paramTypes.Add(getClassifier("Real"));
		
        //    getClassifier("Film").addDefinedOperation("test.ocl", "myNewOper", paramNames, paramTypes, getClassifier("Integer"));

        //    CoreOperation oper = getClassifier("Film").lookupOperation("myNewOper", paramTypes);
        //    Assert.AreEqual("Integer", oper.getReturnType().getName());
        //    Assert.IsTrue(oper.isOclDefined());
        //    List<object> args = new List<object>();
        //    args.Add(getClassifier("Integer"));
        //    Assert.IsTrue(oper.hasMatchingSignature(args));
		
        //    getClassifier("Film").deleteAllConstraintsForSource("test.ocl");
        //    Assert.IsNull(getClassifier("Film").lookupOperation("myNewOper", paramTypes));
        //}
    }
}
