using System;
using System.Collections.Generic;
using DynamicConfigurationBuilder.MethodStrategies;
using Moq;
using NUnit.Framework;

namespace DynamicConfigurationBuilder.Tests
{
    [TestFixture]
    public class ConfigurationBuilderTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConfigurationBuilder_Constructor_PassingNullConfig_ThrowsException()
        {
            var strategyListMock = new Mock<List<IMethodStrategy>>();

            new ConfigurationBuilder(null, strategyListMock.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConfigurationBuilder_Constructor_PassingNullStrategyList_ThrowsException()
        {
            new ConfigurationBuilder(new DynamicConfiguration(), null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConfigurationBuilder_Constructor_PassingNullStrategyListAndNullConfig_ThrowsException()
        {
            new ConfigurationBuilder(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConfigurationBuilder_TryInvokeMember_UnknownMethodStrategy_ThrowsException()
        {
            var strategyMock = new Mock<IMethodStrategy>();
            strategyMock.Setup(s => s.IsMatch(It.IsAny<string>())).Returns(false);
            var strategyList = new List<IMethodStrategy>() { strategyMock.Object };

            dynamic configurationBuilder = new ConfigurationBuilder(new DynamicConfiguration(), strategyList);
            configurationBuilder.UnknownMethod();
        }

        [Test]
        public void ConfigurationBuilder_TryInvokeMember_KnownMethodStrategy_IsExecuted()
        {
            var strategyMock = new Mock<IMethodStrategy>();
            var config = new DynamicConfiguration();
            strategyMock.Setup(s => s.IsMatch(It.IsAny<string>())).Returns(true);

            var strategyList = new List<IMethodStrategy> { strategyMock.Object };

            dynamic configurationBuilder = new ConfigurationBuilder(config, strategyList);
            configurationBuilder.SomeExpectedMethod();
            strategyMock.Verify(s => s.Execute(It.IsAny<string>(), It.IsAny<Object>(), It.IsAny<Object[]>()));
        }

        [Test]
        public void ConfigurationBuilder_TryInvokeMember_KnownMethodStrategy_IsExecutedWithCorrectParameters()
        {
            var strategyMock = new Mock<IMethodStrategy>();
            var config = new DynamicConfiguration();
            strategyMock.Setup(s => s.IsMatch(It.IsAny<string>())).Returns(true);

            var strategyList = new List<IMethodStrategy> { strategyMock.Object };

            dynamic configurationBuilder = new ConfigurationBuilder(config, strategyList);
            configurationBuilder.SomeExpectedMethod("ParamValue");
            strategyMock.Verify(s => s.Execute("SomeExpectedMethod", config, new object[] { "ParamValue" }));
        }

        [Test]
        public void ConfigurationBuilder_TryInvokeMember_KnownMethodStrategy_IsExecutedWithCorrectMultipleParameters()
        {
            var strategyMock = new Mock<IMethodStrategy>();
            var config = new DynamicConfiguration();
            strategyMock.Setup(s => s.IsMatch(It.IsAny<string>())).Returns(true);

            var strategyList = new List<IMethodStrategy> { strategyMock.Object };

            dynamic configurationBuilder = new ConfigurationBuilder(config, strategyList);
            configurationBuilder.SomeExpectedMethod(1, 2, 3);
            strategyMock.Verify(s => s.Execute("SomeExpectedMethod", config, new object[] { 1, 2, 3 }));
        }

        [Test]
        public void ConfigurationBuilder_TryInvokeMember_KnownMethodStrategy_IsReturnsBuilder()
        {
            var strategyMock = new Mock<IMethodStrategy>();
            var config = new DynamicConfiguration();
            strategyMock.Setup(s => s.IsMatch(It.IsAny<string>())).Returns(true);

            var strategyList = new List<IMethodStrategy> { strategyMock.Object };

            dynamic configurationBuilder = new ConfigurationBuilder(config, strategyList);
            dynamic returnedValue = configurationBuilder.SomeExpectedMethod();

            Assert.AreEqual(configurationBuilder, returnedValue);
        }
    }
}
