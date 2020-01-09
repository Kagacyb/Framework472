using System.Collections.Generic;
using System.Web;

namespace EML.Util
{
    public class ServiceStaticInfo
    {
        public static string CompanyCode { get; set; }

        public static List<string> ConfigSettingCatalog { get; set; }

        public static string DefaultSettingCatalog { get; private set; }

        public static string EMLSettingPath { get; set; }

        public static int LocationLevel { get; set; }

        public static string SecSettingPath { get; set; }

        public static string SearchKey { get; set; }

        private static Dictionary<string, string> _dictSettingPath;
        public static Dictionary<string, string> DictSettingPath
        {

            get
            {
                if (_dictSettingPath == null) _dictSettingPath = new Dictionary<string, string>();
                return _dictSettingPath;
            }

            private set
            {
                _dictSettingPath = value;

            }
        }

        public static void InitSettingPath(string key, string path)
        {

            if (DictSettingPath.ContainsKey(key))
                throw new System.Exception(key + " is Double");

            if (string.IsNullOrEmpty(path))
                throw new System.Exception(key + "Value is Empty");

            DictSettingPath.Add(key, path);
        }



    }
}
