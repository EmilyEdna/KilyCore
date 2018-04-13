using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KilyCore.Extension.RSACryption
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5CryptionExtension
    {
        public static string MD5HASH(string source)
        {
            MD5 md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }
    }
}
