using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EML.Util
{
    public class SQLHelper
    {
        public static string ConnStringFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

        //public static string LogisticsConnString
        //{
        //    get
        //    {
        //        string str = string.Empty;
        //        try
        //        {
        //            ConfigSetting setting = ConfigSetting.Load(ServiceStaticInfo.EMLSettingPath);
        //            str = string.Format(ConnStringFormat, new object[] { CryptUtil.Decrypt(setting.A), CryptUtil.Decrypt(setting.B), CryptUtil.Decrypt(setting.C), CryptUtil.Decrypt(setting.D) });
        //        }
        //        catch (Exception exception)
        //        {
        //            throw new Exception("ConfigSetting:" + exception.Message);
        //        }
        //        return str;
        //    }
        //}

        //public static string SecurityConnString
        //{
        //    get
        //    {
        //        string str = string.Empty;
        //        try
        //        {
        //            ConfigSetting setting = ConfigSetting.Load(ServiceStaticInfo.SecSettingPath);
        //            str = string.Format(ConnStringFormat, new object[] { CryptUtil.Decrypt(setting.A), CryptUtil.Decrypt(setting.B), CryptUtil.Decrypt(setting.C), CryptUtil.Decrypt(setting.D) });
        //        }
        //        catch (Exception exception)
        //        {
        //            throw new Exception("ConfigSetting:" + exception.Message);
        //        }
        //        return str;
        //    }
        //}

        public static string DictConnString(string key)
        {
            string str = string.Empty;
            try
            {

                ConfigSetting setting = ConfigSetting.Load( ServiceStaticInfo.DictSettingPath[key]);
                str = string.Format(ConnStringFormat, new object[] { CryptUtil.Decrypt(setting.A), CryptUtil.Decrypt(setting.B), CryptUtil.Decrypt(setting.C), CryptUtil.Decrypt(setting.D) });
            }
            catch (Exception exception)
            {
                throw new Exception("ConfigSetting:" + exception.Message);
            }
            return str;
        }
    }
}
