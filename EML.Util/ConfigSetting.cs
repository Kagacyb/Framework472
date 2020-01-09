using System;
using System.IO;
using System.Xml.Serialization;

namespace EML.Util
{
    public class ConfigSetting
    {
        public static ConfigSetting Load(string path)
        {
            ConfigSetting setting = new ConfigSetting();
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigSetting));
                    using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        return (ConfigSetting)serializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return null;
        }

        public static void Save(ConfigSetting setting, string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigSetting));
                    using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        serializer.Serialize(stream, setting);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public string Code { get; set; }

        public string D { get; set; }

        public string E { get; set; }

        public string F { get; set; }

        public string Name { get; set; }
    }
}
