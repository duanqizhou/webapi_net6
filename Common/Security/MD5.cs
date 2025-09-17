using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SmartSoft.Security
{
    /// <summary>
    /// MD5加密算法
    /// </summary>
    public sealed class MD5PASS
    {
        /// <summary>
        /// 给一个字符串进行MD5加密
        /// </summary>
        /// <param   name="strText">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }
    }
}
