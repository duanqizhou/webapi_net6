namespace SmartSoft.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// 消息摘要对象。
    /// </summary>
    /// <remarks>用于消息摘要生成，CA认证的基础。为生成消息验证代码提供方法。</remarks>
    public sealed class MAC
    {
        /// <summary>
        /// 计算指定字节数组的哈希值。返回长度为20的字节数组。
        /// </summary>
        /// <param name="bytes">要计算其哈希代码的输入。</param>
        /// <param name="key">用于哈希算法的密钥。密钥长度为任意。</param>
        /// <returns>计算所得的哈希代码。代码长度为20个字节。</returns>
        public static byte[] Compute(byte[] bytes, byte[] key)
        {
            HMACSHA1 hmacsha = new HMACSHA1(key);
            return hmacsha.ComputeHash(bytes);
        }

        /// <summary>
        /// 计算指定 Stream 的哈希值。返回长度为20的字节数组。
        /// </summary>
        /// <param name="stream">要计算其哈希代码的输入。</param>
        /// <param name="key">用于哈希算法的密钥。密钥长度为任意。</param>
        /// <returns>计算所得的哈希代码。代码长度为20个字节。</returns>
        public static byte[] Compute(Stream stream, byte[] key)
        {
            HMACSHA1 hmacsha = new HMACSHA1(key);
            return hmacsha.ComputeHash(stream);
        }

        /// <summary>
        /// 计算指定字节数组的指定区域的哈希值。返回长度为20的字节数组。
        /// </summary>
        /// <param name="bytes">要计算其哈希代码的输入。</param>
        /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="count">数组中用作数据的字节数。</param>
        /// <param name="key">用于哈希算法的密钥。密钥长度为任意。</param>
        /// <returns>计算所得的哈希代码。代码长度为20个字节。</returns>
        public static byte[] Compute(byte[] bytes, int offset, int count, byte[] key)
        {
            HMACSHA1 hmacsha = new HMACSHA1(key);
            return hmacsha.ComputeHash(bytes, offset, count);
        }
    }
}

