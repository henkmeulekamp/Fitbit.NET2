using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Fitbit.Helpers
{
    class XmlHelper
    {
        /// <summary>
        /// Deserialize the XML string to an instance of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">A XML serialized string.</param>
        /// <returns></returns>
        public static T XmlDeserializeFromString<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var theStringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(theStringReader);
            }
        }

        /// <summary>
        /// Serializes the object to an XML string.
        /// </summary>
        /// <param name="anObject">An object.</param>
        /// <returns></returns>
        public static string XmlSerializeToString(object anObject)
        {
            var xmlSerializer = new XmlSerializer(anObject.GetType());
            using (var memoryStream = new MemoryStream())
            using (var textWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
            {
                textWriter.Formatting = Formatting.Indented;
                try
                {
                    xmlSerializer.Serialize(textWriter, anObject);
                    textWriter.Flush();
                    memoryStream.Position = 0;

                    using (var theStreamReader = new StreamReader(memoryStream))
                    {
                        return theStreamReader.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    //supress any errors
                }
                finally
                {
                    textWriter.Close();
                }
            }
            return string.Empty;
        }
    }
}
