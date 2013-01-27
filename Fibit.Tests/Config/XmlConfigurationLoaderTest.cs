using System;
using System.Diagnostics;
using System.IO;
using Fitbit.Api.Config;
using Fitbit.Config;
using NUnit.Framework;

namespace Fibit.Tests.Config
{
    [TestFixture]
    public class XmlConfigurationLoaderTest
    {
        private string _filePath;

        [TestFixtureSetUp]
        public void InitFixture()
        {
            _filePath = WriteConfig("FitBitClient.config");            
        }

        [Test]
        public void GetConfigurationForNonExistingFileThrows()
        {
            var configLoader = new XmlConfigurationLoader("C:\\projects\\test.config");

            Assert.Throws<ArgumentException>(() => configLoader.GetConfiguration());
        }

        [Test]
        public void GetConfiguration()
        {
            var configLoader = new XmlConfigurationLoader(_filePath);
            
            FitBitConfiguration config = configLoader.GetConfiguration();
            
            
            Assert.AreEqual("yourConsumerSecret", config.ConsumerSecret);
            Assert.AreEqual("yourConsumerKey", config.Consumerkey);
        }


        private string WriteConfig(string file)
        {
            var config = new FitBitConfiguration("yourConsumerSecret", "yourConsumerKey");

            string filePath = Path.Combine(Environment.CurrentDirectory, file);
            XmlConfigurationLoader.SaveConfig(config, filePath);

            Trace.WriteLine(string.Format("Written config to: {0}", filePath));
            return filePath;
        }
    }
}