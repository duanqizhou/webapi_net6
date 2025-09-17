namespace SmartSoft.Security
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Bytes对象。
    /// </summary>
    /// <remarks>为提供字节处理支持，数据加密、CA认证的基础。</remarks>
    public sealed class Bytes
    {
        /// <summary>
        /// 判断给定的两个字节数组是否相等。
        /// </summary>
        /// <param name="bytes1">要比较的第一个字节数组。</param>
        /// <param name="bytes2">要比较的第二个字节数组。</param>
        /// <returns>如果两个字节数组的长度相同，并且相应下标的元素相等，或者两个字节数组都为空引用，则返回true；否则返回false。</returns>
        public static bool Equals(byte[] bytes1, byte[] bytes2)
        {
            if ((bytes1 != null) || (bytes2 != null))
            {
                if ((bytes1 == null) || (bytes2 == null))
                {
                    return false;
                }
                if (bytes1.Length != bytes2.Length)
                {
                    return false;
                }
                int length = bytes1.Length;
                for (int i = 0; i < length; i++)
                {
                    if (bytes1[i] != bytes2[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 将指定的  base64 字符串转换为 base64 字节数组。
        /// </summary>
        /// <param name="s">要转换的 base64 字符串。</param>
        /// <returns>返回 base64 字节数组。</returns>
        /// <remarks>使用 Convert.FromBase64String。</remarks>
        public static byte[] FromBase64String(string s)
        {
            return Convert.FromBase64String(s);
        }

        /// <summary>
        /// 将格式为“xx xx ...”的字符串（其中“xx”为每一个字节的十六进制表示）转换为相应的字节数组。
        /// </summary>
        /// <param name="s">要转换的字符串。</param>
        /// <returns>返回转换后的字节数组。</returns>
        /// <exception cref="T:System.ArgumentNullException">s 为空引用。</exception>
        /// <exception cref="T:System.ArgumentException">字符串 s 格式无效。</exception>
        /// <remarks>如果 s 为空字符串，则返回长度为 0 的字节数组。</remarks>
        public static byte[] FromString(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s", "要转换的字符串不能是空引用。");
            }
            if (s.Trim() == "")
            {
                return new byte[0];
            }
            string[] strArray = s.Split(new char[] { ' ' });
            byte[] buffer = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                try
                {
                    buffer[i] = byte.Parse(strArray[i], NumberStyles.AllowHexSpecifier);
                }
                catch (FormatException exception)
                {
                    throw new ArgumentException("无效的字节字符串格式。", "s", exception);
                }
            }
            return buffer;
        }

        /// <summary>
        /// 合并指定的两个字节数组。请注意字节数组的顺序。
        /// </summary>
        /// <param name="bytes1">要合并的字节数组1。</param>
        /// <param name="bytes2">要合并的字节数组2。</param>
        /// <returns>返回合并后的字节数组。</returns>
        /// <exception cref="T:System.ArgumentNullException">bytes1 或者 bytes2 为空引用。</exception>
        public static byte[] Merge(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == null)
            {
                throw new ArgumentNullException("bytes1", "要合并的字节数组不能是空引用。");
            }
            if (bytes2 == null)
            {
                throw new ArgumentNullException("bytes2", "要合并的字节数组不能是空引用。");
            }
            byte[] destinationArray = new byte[bytes1.Length + bytes2.Length];
            Array.Copy(bytes1, 0, destinationArray, 0, bytes1.Length);
            Array.Copy(bytes2, 0, destinationArray, bytes1.Length, bytes2.Length);
            return destinationArray;
        }

        /// <summary>
        /// 将指定的 base64 字节数组转换为 base64 字符串。
        /// </summary>
        /// <param name="buffer">要转换的 base64 字节数组。</param>
        /// <returns>返回  base64 字符串</returns>
        /// <remarks>使用 Convert.ToBase64String。</remarks>
        public static string ToBase64String(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// 将指定的字节数组转换为字符串。该字符串的格式为“xx xx ...”，其中“xx”为每一个字节的十六进制表示。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <returns>返回已经转换的字节数组字符串。</returns>
        public static string ToString(byte[] buffer)
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
            for (int i = 0; i < buffer.Length; i++)
            {
                str = str + buffer[i].ToString("X2") + " ";
            }
            return str.Substring(0, str.Length - 1);
        }
    }
}

