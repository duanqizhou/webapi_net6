namespace SmartSoft.Security
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Hex对象。
    /// </summary>
    /// <remarks>用于中的Hex字符串转换处理，数据加密、CA认证的基础，提供字符串到十六进制字符串的转换方法。该类用于将特殊字符转换为只包含0到9以及A到F共16个字符的字符串。</remarks>
    public sealed class Hex
    {
        /// <summary>
        /// 将十六制字符串字节数组转换成普通字符串。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <returns>返回转换后的字节数组。如果buffer长度为0，则返回空字符串</returns>
        public static string Decrypt(byte[] buffer)
        {
            return Decrypt(Encoding.Unicode.GetString(buffer));
        }

        /// <summary>
        /// 将指定的十六进制字符串转换为原始字符串。
        /// </summary>
        /// <param name="s">要转换的十六进制字符串。该字符串的长度必须是4的整数倍或者为0。</param>
        /// <returns>返回转换后的原始字符串。</returns>
        /// <exception cref="T:System.ArgumentNullException">s 为空引用。</exception>
        /// <exception cref="T:System.ArgumentException">s 的长度不是 4 的整数倍。</exception>
        public static string Decrypt(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s", "要转换的字符串不能为空引用。");
            }
            if (s == "")
            {
                return "";
            }
            if ((s.Length % 4) != 0)
            {
                throw new ArgumentException("无效的字符串，该字符串的长度必须为4的整数倍。", "s");
            }
            int length = s.Length;
            int startIndex = 0;
            int index = 0;
            char[] chArray = new char[length >> 2];
            while (startIndex < length)
            {
                chArray[index] = Convert.ToChar(Convert.ToUInt16(s.Substring(startIndex, 4), 0x10));
                startIndex += 4;
                index++;
            }
            return new string(chArray);
        }

        /// <summary>
        /// 将指定的Hex字节流转换为原始流，并保存在指定的输出流中。
        /// </summary>
        /// <param name="inStream">要转换的字节流。</param>
        /// <param name="outStream">接收输出的字节流。</param>
        /// <exception cref="T:System.ArgumentNullException">inStream 或者 outStream 为空引用。</exception>
        public static void Decrypt(Stream inStream, Stream outStream)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("inStream", "无效的需要转换的流，要轮换的流不能是空引用。");
            }
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream", "无效的目标流，接收数据的流不能是空引用。");
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
                byte[] buffer2 = DecryptToByte(Encoding.Unicode.GetString(buffer));
                outStream.Seek(0L, SeekOrigin.Begin);
                outStream.Write(buffer2, 0, buffer2.Length);
                outStream.Seek(0L, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// 将形如“0xXXXX...”的字符串转变为字节数组。
        /// </summary>
        /// <param name="s">要转换的字符串。</param>
        /// <returns>返回转换后的字节数组。如果s为空引用，则返回空引用，如果s为空字符串，则返回长度为0的数组。</returns>
        public static byte[] DecryptToByte(string s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.Trim() == "")
            {
                return new byte[0];
            }
            s = s.Trim();
            if ((s.Substring(0, 2) != "0x") || ((s.Length % 2) != 0))
            {
                throw new ArgumentException("要转换的字符串不是一个形如“0xXXXX...”的字符串，或者长度不是2的整数倍。", "s");
            }
            s = s.Substring(2);
            if (s == "")
            {
                return new byte[0];
            }
            int num = s.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[i] = byte.Parse(s.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);
            }
            return buffer;
        }

        /// <summary>
        /// 将指定的字符串转换为十六进制字符串。
        /// </summary>
        /// <param name="source">要转换的任意字符串。</param>
        /// <returns>返回十六进制字符串，该字符串的长度总是原始字符串的4倍，也就是说返回的字符串是4的整数倍。该字符串为原始字符串中每一个字符的Unicode代码的十六进制（形如：0xXXXX）的连接字符串。</returns>
        /// <exception cref="T:System.ArgumentNullException">s 为空引用。</exception>
        /// <remarks>
        /// 使用该方法得到的字符串只包含0到9以及A到F共16个字符（并且不包含任何空白字符），因此它可以被用在URL中。
        /// </remarks>
        public static string Encrypt(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "要转换的字符串不能为空引用。");
            }
            if (source == "")
            {
                return "";
            }
            int length = source.Length;
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str = str + Convert.ToUInt16(source[i]).ToString("X4");
            }
            return str;
        }

        /// <summary>
        /// 将指定的字节数组转换为“0xXXXX...”的字符串形式。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <returns>如果 buffer 为空引用，则返回空引用，如果 buffer 长度为0，则返回空字符串。</returns>
        public static string Encrypt(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            if (buffer.Length == 0)
            {
                return "";
            }
            string str = "";
            foreach (byte num in buffer)
            {
                str = str + num.ToString("X2");
            }
            return ("0x" + str);
        }

        /// <summary>
        /// 将指定的字节流转换为Hex流，并保存在指定的输出流中。
        /// </summary>
        /// <param name="inStream">要转换的字节流。</param>
        /// <param name="outStream">接收输出的字节流。</param>
        /// <exception cref="T:System.ArgumentNullException">inStream 或者 outStream 为空引用。</exception>
        public static void Encrypt(Stream inStream, Stream outStream)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("inStream", "无效的需要转换的流，要转换的流不能是空引用。");
            }
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream", "无效的目标流，接收数据的流不能是空引用。");
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
                byte[] buffer2 = EncryptToByte(Encoding.Unicode.GetString(buffer));
                outStream.Seek(0L, SeekOrigin.Begin);
                outStream.Write(buffer2, 0, buffer2.Length);
                outStream.Seek(0L, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// 将指定的字符串转换为十六进制字符串数组
        /// </summary>
        /// <param name="source">要转换的任意字符串。</param>
        /// <returns>返回十六进制字符串数组。</returns>
        /// <exception cref="T:System.ArgumentNullException">source 为空引用。</exception>
        /// <remarks>
        /// 把转换成的十六进制字符串转换成字节数组，数据长度是source的8倍
        /// </remarks>
        public static byte[] EncryptToByte(string source)
        {
            string s = Encrypt(source);
            return Encoding.Unicode.GetBytes(s);
        }
    }
}

