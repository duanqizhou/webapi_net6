namespace webapi.Common
{
    using Dm;
    using SmartSoft.Security;
    using System;
    using System.Security.Cryptography;
    using System.Security.Policy;
    using System.Text;
    using Hash = SmartSoft.Security.Hash;

    /// <summary>
    /// 非对称加密密码算法。
    /// </summary>
    /// <remarks>为提供非对称密码算法，提供密码生成、密码验证等功能。字符串密码加密后得到的密钥长度为64个字节，相同的字符串密码在不同位置、不同的事件加密将产生不同的密钥。</remarks>
    public sealed class Password
    {
        /// <summary>
        /// 加密指定的密码。
        /// </summary>
        /// <param name="password">要加密的密码。该值不能为空引用。</param>
        /// <returns>返回加密后的密钥。该密钥的长度为64个字节。后32个字节为一个随机salt值。</returns>
        /// <exception cref="T:System.ArgumentNullException">password 为空引用。</exception>
        /// <remarks>
        /// 加密过程如下：
        /// （1）、将密码（口令）转换为字节数组；
        /// （2）、散列该字节数组；
        /// （3）、为口令添加随机salt值，并再次散列该值；
        /// （4）、将随机的salt值添加到散列数据之后，返回。
        /// </remarks>
        public static byte[] Encrypt(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password", "密码不能为空引用。");
            }
            byte[] buffer = SmartSoft.Security.Hash.Compute(Encoding.ASCII.GetBytes(password), 0x20);
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            byte[] data = new byte[0x20];
            generator.GetBytes(data);
            return Bytes.Merge(Hash.Compute(Bytes.Merge(buffer, data), 0x20), data);
        }

        /// <summary>
        /// 验证给定的密码是否和指定的密钥匹配。
        /// </summary>
        /// <param name="password">要验证的密码。</param>
        /// <param name="key">要验证的密钥。密钥的长度为64个字节。</param>
        /// <returns>如果可以通过指定的密钥生成和key相匹配的密钥，则返回true；否则返回false。</returns>
        /// <exception cref="T:System.ArgumentNullException">password 或者 key 为空引用。</exception>
        /// <exception cref="T:System.ArgumentException">key 的长度不等于 64。</exception>
        public static bool Verify(string password, byte[] key)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password", "密码不能为空引用。");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key", "密钥不能为空引用。");
            }
            if (key.Length != 0x40)
            {
                throw new ArgumentException("无效的密钥长度，密钥长度应该为64个字节。", "key");
            }
            byte[] buffer = Hash.Compute(Encoding.ASCII.GetBytes(password), 0x20);
            byte[] destinationArray = new byte[0x20];
            Array.Copy(key, 0x20, destinationArray, 0, 0x20);
            return Bytes.Equals(Bytes.Merge(Hash.Compute(Bytes.Merge(buffer, destinationArray), 0x20), destinationArray), key);
        }

        /// <summary>
        /// 代表一个空密码（String.Empty 对应的密码）。每次使用该值都会返回一个不同的结果。
        /// </summary>
        /// <remarks>每次使用该值都会返回一个不同的结果。</remarks>
        public static byte[] EmptyPassword
        {
            get
            {
                return Encrypt(string.Empty);
            }
        }
    }

}

