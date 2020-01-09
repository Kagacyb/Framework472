using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EML.Util
{
    public class CryptUtil
    {
        private static string encryptionKey = "12345678";
        private static byte[] rgbIV = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
        private static byte[] rgbKey = new byte[0];

        public static string Decrypt(string textToDecrypt)
        {
            if (string.IsNullOrEmpty(textToDecrypt))
            {
                return string.Empty;
            }
            byte[] buffer = new byte[textToDecrypt.Length];
            rgbKey = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            buffer = Convert.FromBase64String(textToDecrypt);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static string Encrypt(string textToEncrypt)
        {
            if (string.IsNullOrEmpty(textToEncrypt))
            {
                return string.Empty;
            }
            rgbKey = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(textToEncrypt);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    //public class EncryptObject
    //{
    //    // Fields
    //    private const string desKey = "19791215";

    //    // Methods
    //    public static string Decrypt(string pToDecrypt)
    //    {
    //        return Decrypt(pToDecrypt, desKey);
    //    }

    //    private static string Decrypt(string pToDecrypt, string sKey)
    //    {
    //        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
    //        int num2 = (int)Math.Round((double)((((double)pToDecrypt.Length) / 2.0) - 1.0));
    //        byte[] buffer = new byte[num2 + 1];
    //        int num4 = num2;
    //        for (int i = 0; i <= num4; i++)
    //        {
    //            int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
    //            buffer[i] = (byte)num;
    //        }
    //        provider.Key = Encoding.ASCII.GetBytes(sKey);
    //        provider.IV = Encoding.ASCII.GetBytes(sKey);
    //        MemoryStream stream2 = new MemoryStream();
    //        CryptoStream stream = new CryptoStream(stream2, provider.CreateDecryptor(), CryptoStreamMode.Write);
    //        stream.Write(buffer, 0, buffer.Length);
    //        stream.FlushFinalBlock();
    //        return Encoding.Default.GetString(stream2.ToArray());
    //    }

    //    public static string Encrypt(string pToEncrypt)
    //    {
    //        return Encrypt(pToEncrypt, desKey);
    //    }

    //    private static string Encrypt(string pToEncrypt, string sKey)
    //    {
    //        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
    //        byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
    //        provider.Key = Encoding.ASCII.GetBytes(sKey);
    //        provider.IV = Encoding.ASCII.GetBytes(sKey);
    //        MemoryStream stream2 = new MemoryStream();
    //        CryptoStream stream = new CryptoStream(stream2, provider.CreateEncryptor(), CryptoStreamMode.Write);
    //        stream.Write(bytes, 0, bytes.Length);
    //        stream.FlushFinalBlock();
    //        StringBuilder builder = new StringBuilder();
    //        foreach (byte num in stream2.ToArray())
    //        {
    //            builder.AppendFormat("{0:X2}", num);
    //        }
    //        return builder.ToString();
    //    }
    //}
}
