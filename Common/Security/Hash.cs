namespace SmartSoft.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Hash对象。
    /// </summary>
    /// <remarks> 为提供散列算法支持，CA认证的基础。为散列提供方法支持。散列算法可以用于进行数字签名。</remarks>
    public sealed class Hash
    {
        /// <summary>
        /// 计算指定字节数组的哈希值。返回长度为32的字节数组。
        /// </summary>
        /// <param name="bytes">要计算其哈希代码的输入。</param>
        /// <returns>计算所得的哈希代码。该代码长度为32个字节。</returns>
        public static byte[] Compute(byte[] bytes)
        {
            return SHA256.Create().ComputeHash(bytes);
        }

        /// <summary>
        /// 计算Stream的哈希值。返回长度为32的字节数组。
        /// </summary>
        /// <param name="stream">要计算其哈希代码的输入。</param>
        /// <returns>计算所得的哈希代码。该代码长度为32个字节。</returns>
        public static byte[] Compute(Stream stream)
        {
            return SHA256.Create().ComputeHash(stream);
        }

        /// <summary>
        /// 计算指定字节数组的哈希值。返回指定长度（20、32、48、64）的字节数组。
        /// </summary>
        /// <param name="bytes">要计算其哈希代码的输入。</param>
        /// <param name="length">指定希望的哈希代码的长度，单位为字节，目前支持的长度为20、32、48、64。</param>
        /// <returns>计算所得的哈希代码。</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">length 不是20、32、48或者64。</exception>
        public static byte[] Compute(byte[] bytes, int length)
        {
            switch (length)
            {
                case 20:
                    return SHA1.Create().ComputeHash(bytes);

                case 0x20:
                    return SHA256.Create().ComputeHash(bytes);

                case 0x30:
                    return SHA384.Create().ComputeHash(bytes);

                case 0x40:
                    return SHA512.Create().ComputeHash(bytes);
            }
            throw new ArgumentOutOfRangeException("length", "希望的代码长度不是合法的长度，当前支持的长度为20、32、48、64。");
        }

        /// <summary>
        /// 计算Stream的哈希值。返回指定长度（20、32、48、64）的字节数组。
        /// </summary>
        /// <param name="stream">要计算其哈希代码的输入。</param>
        /// <param name="length">指定希望的哈希代码的长度，单位为字节，目前支持的长度为20、32、48、64。</param>
        /// <returns>计算所得的哈希代码。</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">length 不是20、32、48或者64。</exception>
        public static byte[] Compute(Stream stream, int length)
        {
            switch (length)
            {
                case 20:
                    return SHA1.Create().ComputeHash(stream);

                case 0x20:
                    return SHA256.Create().ComputeHash(stream);

                case 0x30:
                    return SHA384.Create().ComputeHash(stream);

                case 0x40:
                    return SHA512.Create().ComputeHash(stream);
            }
            throw new ArgumentOutOfRangeException("length", "希望的代码长度不是合法的长度，当前支持的长度为20、32、48、64。");
        }

        /// <summary>
        /// 计算指定字节数组的指定区域的哈希值。返回长度为32的字节数组。
        /// </summary>
        /// <param name="bytes">要计算其哈希代码的输入。</param>
        /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="count">数组中用作数据的字节数。</param>
        /// <returns>计算所得的哈希代码。该代码长度为32个字节。</returns>
        public static byte[] Compute(byte[] bytes, int offset, int count)
        {
            return SHA256.Create().ComputeHash(bytes, offset, count);
        }

        /// <summary>
        /// 计算指定字节数组的指定区域的哈希值。返回指定长度（20、32、48、64）的字节数组。
        /// </summary>
        /// <param name="bytes">要计算其哈希代码的输入。</param>
        /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="count">数组中用作数据的字节数。</param>
        /// <param name="length">指定希望的哈希代码的长度，单位为字节，目前支持的长度为20、32、48、64。</param>
        /// <returns>计算所得的哈希代码。</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">length 不是20、32、48或者64。</exception>
        public static byte[] Compute(byte[] bytes, int offset, int count, int length)
        {
            switch (length)
            {
                case 20:
                    return SHA1.Create().ComputeHash(bytes, offset, count);

                case 0x20:
                    return SHA256.Create().ComputeHash(bytes, offset, count);

                case 0x30:
                    return SHA384.Create().ComputeHash(bytes, offset, count);

                case 0x40:
                    return SHA512.Create().ComputeHash(bytes, offset, count);
            }
            throw new ArgumentOutOfRangeException("length", "希望的代码长度不是合法的长度，当前支持的长度为20、32、48、64。");
        }
    }
}

