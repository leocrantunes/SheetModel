using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ocl20Test.MDRRepository.models.ocl20
{
    [TestClass]
    public class TestOclTypesDefinition {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize]
        public static void setUp(TestContext testContext)
        {
            OclTypesDefinition.getEnvironment();
        }
	
        [TestMethod]
        public void testPrimitiveType() {
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("Boolean"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("Integer"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("Real"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("String"));
		
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("integer"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("int"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("Int"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("Long"));
            Assert.IsTrue(OclTypesDefinition.isOclPrimitiveType("long"));
		
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Integer"), OclTypesDefinition.getOclPrimitiveType("int"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Integer"), OclTypesDefinition.getOclPrimitiveType("integer"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Integer"), OclTypesDefinition.getOclPrimitiveType("long"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Integer"), OclTypesDefinition.getOclPrimitiveType("Byte"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Real"), OclTypesDefinition.getOclPrimitiveType("Double"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Real"), OclTypesDefinition.getOclPrimitiveType("float"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("String"), OclTypesDefinition.getOclPrimitiveType("string"));
            Assert.AreEqual(OclTypesDefinition.getOclPrimitiveType("Boolean"), OclTypesDefinition.getOclPrimitiveType("bool"));
        }
    }
}
