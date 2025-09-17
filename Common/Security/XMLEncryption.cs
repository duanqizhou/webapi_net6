using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.IO;

namespace SmartSoft.Core
{
    /// <summary>
    /// XML文档加密
    /// </summary>
    public sealed class XMLEncryption
    {
        /// <summary>
        /// 返回加密后的XML文档内容
        /// </summary>
        /// <param name="content">内容</param>
        public static string Encrypt(string content)
        {
            if (string.IsNullOrEmpty(content) == true)
            {
                throw new Exception("XML文档内容为空!");
            }

            RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);

            XmlElement xmlElemt;
            xmlElemt = xmlDoc.DocumentElement;

            EncryptedXml xmlEnc = new EncryptedXml(xmlDoc);

            xmlEnc.AddKeyNameMapping("TianChengSoft121479561", oRSA);

            EncryptedData encXml = xmlEnc.Encrypt(xmlElemt, "TianChengSoft121479561");

            EncryptedXml.ReplaceElement(xmlElemt, encXml, false);

            return xmlDoc.OuterXml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string Decrypt(string content)
        {
            if (string.IsNullOrEmpty(content) == true)
            {
                throw new Exception("XML文档内容为空!");
            }

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            XmlDocument xmlEncDoc = new XmlDocument();
            xmlEncDoc.LoadXml(content);

            EncryptedXml encXml = new EncryptedXml(xmlEncDoc);
            encXml.AddKeyNameMapping("TianChengSoft121479561", rsa);

            encXml.DecryptDocument();

            return xmlEncDoc.OuterXml;
        }
    }
}
