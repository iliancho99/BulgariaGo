using System;
using System.Security.Cryptography;
using System.Text;

namespace Assets.Scripts
{
    public static class Utilities
    {
        public static string Md5Sum(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);

            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(bytes);

            var hashString = "";

            for (var i = 0; i < hashBytes.Length; i++)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }
    }
}