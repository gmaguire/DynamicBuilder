using NUnit.Framework;

namespace DynamicConfigurationBuilder.Tests
{
    [TestFixture]
    public class DynamicConfigurationTests
    {
        [Test]
        public void DynamicConfiguration_Indexer_AssignedStringValue_CreatesAProperty()
        {
            dynamic config = new DynamicConfiguration();
            config["MyPropName"] = "MyPropValue";

            Assert.IsNotNull(config.MyPropName);
        }

        [Test]
        public void DynamicConfiguration_Indexer_AssignedIntValue_CreatesAPropertyOfTypeInt()
        {
            dynamic config = new DynamicConfiguration();
            config["MyPropName"] = 99;

            Assert.IsTrue(config.MyPropName.GetType().Name == "Int32");
        }

        [Test]
        public void DynamicConfiguration_Indexer_AssignedIntValue_CreatesAPropertyWhichReturnsCorrectValue()
        {
            dynamic config = new DynamicConfiguration();
            config["MyPropName"] = 99;

            Assert.IsTrue(config.MyPropName == 99);
        }

        [Test]
        public void DynamicConfiguration_Indexer_AssignedStringValue_CreatesAPropertyOfTypeString()
        {
            dynamic config = new DynamicConfiguration();
            config["MyPropName"] = "MyPropValue";

            Assert.IsTrue(config.MyPropName.GetType().Name == "String");
        }

        [Test]
        public void DynamicConfiguration_Indexer_AssignedStringValue_CreatesAPropertyWhichReturnsCorrectValue()
        {
            dynamic config = new DynamicConfiguration();
            config["MyPropName"] = "MyPropValue";

            Assert.IsTrue(config.MyPropName == "MyPropValue");
        }

        [Test]
        public void DynamicConfiguration_Indexer_AssignedNullValue_CreatesAPropertyWhichReturnsNull()
        {
            dynamic config = new DynamicConfiguration();
            config["MyPropName"] = null;

            Assert.IsNull(config.MyPropName);
        }

        [Test]
        public void DynamicConfiguration_DynamicProperty_WhenNotPreviouslyAssigned_ReturnsNull()
        {
            dynamic config = new DynamicConfiguration();

            Assert.IsNull(config.MyPropName);
        }
    }
}
