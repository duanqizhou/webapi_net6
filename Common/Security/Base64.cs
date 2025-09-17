namespace SmartSoft.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Base64编码类。
    /// </summary>
    /// <remarks>为提供Base64字符串处理支持，数据加密、CA认证的基础。</remarks>
    public sealed class Base64
    {
        /// <summary>
        /// 将指定的Base64字节数组转换为其原始字节数组。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <returns>返回转换后的字节数组。</returns>
        public static byte[] Decrypt(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            if (buffer.Length == 0)
            {
                return new byte[0];
            }
            FromBase64Transform transform = new FromBase64Transform();
            int length = buffer.Length;
            int inputOffset = 0;
            byte[] outputBuffer = new byte[3];
            MemoryStream stream = new MemoryStream();
            try
            {
                while ((length - inputOffset) > 4)
                {
                    transform.TransformBlock(buffer, inputOffset, 4, outputBuffer, 0);
                    inputOffset += 4;
                    stream.Write(outputBuffer, 0, 3);
                }
                outputBuffer = transform.TransformFinalBlock(buffer, inputOffset, length - inputOffset);
                stream.Write(outputBuffer, 0, outputBuffer.Length);
            }
            finally
            {
                stream.Close();
            }
            return stream.ToArray();
        }

        /// <summary>
        /// 将指定的 base64 字符串转换为其原始字符串。
        /// </summary>
        /// <param name="source">要解密的 base64 字符串。</param>
        /// <returns>返回解密后的字符串。</returns>
        public static string Decrypt(string source)
        {
            return Encoding.Unicode.GetString(Decrypt(Bytes.FromBase64String(source)));
        }

        /// <summary>
        /// 将指定的Base64字节流转换为原始流，并保存在指定的输出流中。
        /// </summary>
        /// <param name="inStream">要转换的字节流。</param>
        /// <param name="outStream">接收输出的字节流。</param>
        /// <exception cref="T:System.ArgumentNullException">inStream 或者 outStream 为空引用。</exception>
        public static void Decrypt(Stream inStream, Stream outStream)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("inStream", "无效的需要加密的流，要加密的流不能是空引用。");
            }
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream", "无效的解密流，接收数据的流不能是空引用。");
            }
            if (inStream.Length == 0L)
            {
                outStream.SetLength(0L);
            }
            else
            {
                inStream.Seek(0L, SeekOrigin.Begin);
                byte[] buffer = new byte[inStream.Length];
                inStream.Read(buffer, 0, (int) inStream.Length);
                byte[] buffer2 = Decrypt(buffer);
                outStream.Seek(0L, SeekOrigin.Begin);
                outStream.Write(buffer2, 0, buffer2.Length);
                outStream.Seek(0L, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// 将指定的字节数组转换为Base64编码。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <returns>返回转换后的字节数组。</returns>
        public static byte[] Encrypt(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            if (buffer.Length == 0)
            {
                return new byte[0];
            }
            ToBase64Transform transform = new ToBase64Transform();
            int length = buffer.Length;
            int inputOffset = 0;
            byte[] outputBuffer = new byte[4];
            MemoryStream stream = new MemoryStream();
            try
            {
                while ((length - inputOffset) > 3)
                {
                    transform.TransformBlock(buffer, inputOffset, 3, outputBuffer, 0);
                    inputOffset += 3;
                    stream.Write(outputBuffer, 0, 4);
                }
                outputBuffer = transform.TransformFinalBlock(buffer, inputOffset, length - inputOffset);
                stream.Write(outputBuffer, 0, outputBuffer.Length);
            }
            finally
            {
                stream.Close();
            }
            return stream.ToArray();
        }

        /// <summary>
        /// 将指定的字符串转换为相应的 Base64 字符串。
        /// </summary>
        /// <param name="source">要加密的字符串。</param>
        /// <returns>返回已经加密的 base64 字符串。</returns>
        public static string Encrypt(string source)
        {
            return Bytes.ToBase64String(Encrypt(Encoding.Unicode.GetBytes(source)));
        }

        /// <summary>
        /// 将指定的字节流转换为Base64流，并保存在指定的输出流中。
        /// </summary>
        /// <param name="inStream">要转换的字节流。</param>
        /// <param name="outStream">接收输出的字节流。</param>
        /// <exception cref="T:System.ArgumentNullException">inStream 或者 outStream 为空引用。</exception>
        public static void Encrypt(Stream inStream, Stream outStream)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("inStream", "无效的需要加密的流，要加密的流不能是空引用。");
            }
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream", "无效的解密流，接收数据的流不能是空引用。");
            }
            if (inStream.Length == 0L)
            {
                outStream.SetLength(0L);
            }
            else
            {
                inStream.Seek(0L, SeekOrigin.Begin);
                byte[] buffer = new byte[inStream.Length];
                inStream.Read(buffer, 0, (int) inStream.Length);
                byte[] buffer2 = Encrypt(buffer);
                outStream.Seek(0L, SeekOrigin.Begin);
                outStream.Write(buffer2, 0, buffer2.Length);
                outStream.Seek(0L, SeekOrigin.Begin);
            }
        }
    }
}

