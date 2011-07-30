using System;
using DynamicConfigurationBuilder.MethodStrategies;
using NUnit.Framework;

namespace DynamicConfigurationBuilder.Tests.MethodStrategies
{
    [TestFixture]
    public class PropertySetterTests
    {
        [Test]
        public void PropertySetter_IsMatch_NullPropertyName_ReturnsFalse()
        {
            var settter = new PropertySetter();
            Assert.IsFalse(settter.IsMatch("HasSetTo"));
        }

        [Test]
        public void PropertySetter_IsMatch_RandomMethodName_ReturnsFalse()
        {
            var settter = new PropertySetter();
            Assert.IsFalse(settter.IsMatch("SomeRandomName"));
        }

        [Test]
        public void PropertySetter_IsMatch_ValidMethodName_ReturnsTrue()
        {
            var settter = new PropertySetter();
            Assert.IsTrue(settter.IsMatch("HasSomePropertySetTo"));
        }

        [Test]
        public void PropertySetter_IsMatch_ValidPrefixInvalidSuffix_ReturnsFalse()
        {
            var settter = new PropertySetter();
            Assert.IsFalse(settter.IsMatch("HasSomeProperty"));
        }

        [Test]
        public void PropertySetter_IsMatch_ValidSuffixInvalidPrefix_ReturnsFalse()
        {
            var settter = new PropertySetter();
            Assert.IsFalse(settter.IsMatch("SomePropertySetTo"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void PropertySetter_Execute_NullParameter_ThrowsException()
        {
            var propertySetter = new PropertySetter();
            dynamic config = new DynamicConfiguration();

            propertySetter.Execute("SomeProperty", config, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void PropertySetter_Execute_MultipleParameters_ThrowsException()
        {
            var propertySetter = new PropertySetter();
            dynamic config = new DynamicConfiguration();

            propertySetter.Execute("SomeProperty", config, 1, 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void PropertySetter_Execute_EmptyArray_ThrowsException()
        {
            var propertySetter = new PropertySetter();
            dynamic config = new DynamicConfiguration();

            propertySetter.Execute("SomeProperty", config);
        }

        [Test]
        public void PropertySetter_Execute_ValidMethodNameAndStringValue_SetsConfigProperty()
        {
            var propertySetter = new PropertySetter();
            dynamic config = new DynamicConfiguration();

            propertySetter.Execute("HasSomePropertySetTo", config, "Value");
            Assert.IsTrue(config.SomeProperty == "Value");
        }

        [Test]
        public void PropertySetter_Execute_ValidMethodNameAndIntValue_SetsConfigProperty()
        {
            var propertySetter = new PropertySetter();
            dynamic config = new DynamicConfiguration();

            propertySetter.Execute("HasSomePropertySetTo", config, 1);
            Assert.IsTrue(config.SomeProperty == 1);
        }
    }
}
