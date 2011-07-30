using System;
using DynamicConfigurationBuilder.MethodStrategies;
using NUnit.Framework;

namespace DynamicConfigurationBuilder.Tests.MethodStrategies
{
    [TestFixture]
    public class ListItemSetterTests
    {
        [Test]
        public void ListItemSetter_IsMatch_NullPropertyName_ReturnsFalse()
        {
            var settter = new ListItemSetter();
            Assert.IsFalse(settter.IsMatch("HasListItem"));
        }

        [Test]
        public void ListItemSetter_IsMatch_RandomMethodName_ReturnsFalse()
        {
            var settter = new ListItemSetter();
            Assert.IsFalse(settter.IsMatch("SomeRandomName"));
        }

        [Test]
        public void ListItemSetter_IsMatch_ValidMethodName_ReturnsTrue()
        {
            var settter = new ListItemSetter();
            Assert.IsTrue(settter.IsMatch("HasSomePropertyListItem"));
        }

        [Test]
        public void ListItemSetter_IsMatch_ValidPrefixInvalidSuffix_ReturnsFalse()
        {
            var settter = new ListItemSetter();
            Assert.IsFalse(settter.IsMatch("HasSomeProperty"));
        }

        [Test]
        public void ListItemSetter_IsMatch_ValidSuffixInvalidPrefix_ReturnsFalse()
        {
            var settter = new ListItemSetter();
            Assert.IsFalse(settter.IsMatch("SomePropertyListItem"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ListItemSetter_Execute_NullParameter_ThrowsException()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("SomeMethodCall", config, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ListItemSetter_Execute_MultipleParameters_ThrowsException()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("SomeMethodCall", config, 1, 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ListItemSetter_Execute_EmptyArray_ThrowsException()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("SomeMethodCall", config);
        }

        [Test]
        public void ListItemSetter_Execute_ValidMethodName_CreatesConfigProperty()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("HasNamesListItem", config, "SomeValue");

            Assert.IsNotNull(config.Names);
        }

        [Test]
        public void ListItemSetter_Execute_ValidMethodName_CreatesListConfigProperty()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("HasNamesListItem", config, "SomeValue");

            Assert.IsTrue(config.Names.GetType().Name.StartsWith("List"));
        }

        [Test]
        public void ListItemSetter_Execute_SingleValidMethodName_CreatesListConfigPropertyWithCorrect()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("HasNamesListItem", config, "SomeValue");

            Assert.IsTrue(config.Names.Count == 1);
            Assert.AreEqual(config.Names[0], "SomeValue");
        }

        [Test]
        public void ListItemSetter_Execute_MultipleValidMethodName_CreatesListConfigPropertyWithMultipleItems()
        {
            var listItemSetter = new ListItemSetter();
            dynamic config = new DynamicConfiguration();

            listItemSetter.Execute("HasNamesListItem", config, "SomeValue1");
            listItemSetter.Execute("HasNamesListItem", config, "SomeValue2");
            listItemSetter.Execute("HasNamesListItem", config, "SomeValue3");

            Assert.IsTrue(config.Names.Count == 3);

            Assert.AreEqual(config.Names[0], "SomeValue1");

            Assert.AreEqual(config.Names[1], "SomeValue2");

            Assert.AreEqual(config.Names[2], "SomeValue3");
        }
    }
}
