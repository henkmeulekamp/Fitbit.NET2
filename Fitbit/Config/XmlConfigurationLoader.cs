using System;
using System.IO;
using Fitbit.Api.Config;
using Fitbit.Helpers;

namespace Fitbit.Config
{
    public class XmlConfigurationLoader : IConfigurationLoader
    {
        private readonly string _filePath;

        public XmlConfigurationLoader(string filePath)
        {
            _filePath = filePath;
        }

        public FitBitConfiguration GetConfiguration()
        {
            if (!File.Exists(_filePath))
                throw new ArgumentException(string.Format("Configuration file does not exits: {0}", _filePath));

            return XmlHelper.XmlDeserializeFromString<FitBitConfiguration>(File.ReadAllText(_filePath));
        }

        public static void SaveConfig(FitBitConfiguration config, string filePath)
        {
            File.WriteAllText(filePath, XmlHelper.XmlSerializeToString(config));
        }



    }
}