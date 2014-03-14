using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VerifyCodeBase
{
    public class SimpleEncrypt
    {
        /// <summary>
        /// 默认的64位字符集合, 外加"=="字符做为补位, 组成全部输出集合. 可手动更换字符对应. 如原A->B, B->C, C->A, 则破解时间大幅增加.
        /// </summary>
        private static char[] base64_alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
                          'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                          'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a',
                          'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                          'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                          't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1',
                          '2', '3', '4', '5', '6', '7', '8', '9', '+',
                          '/'};

        /// <summary>
        /// Base64基础字符对应的变换字符
        /// </summary>
        private static char[] changed_alphabet = {'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
                          'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                          'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a',
                          'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                          'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                          't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1',
                          '2', '3', '4', '5', '6', '7', '8', '9', '+',
                          'A', '/'};

        /// <summary>
        /// 变换集合
        /// </summary>
        private static Dictionary<char, char>[] transfer;

        /// <summary>
        /// 变换集合
        /// </summary>
        private static Dictionary<char, char>[] Transfer
        {
            get
            {
                if (transfer == null)
                {
                    Dictionary<char, char>[] result = new Dictionary<char, char>[2];
                    result[0] = new Dictionary<char, char>();
                    result[1] = new Dictionary<char, char>();
                    if (base64_alphabet.Length == changed_alphabet.Length)
                    {
                        for (int i = 0; i < base64_alphabet.Length; i++)
                        {
                            result[0].Add(base64_alphabet[i], changed_alphabet[i]);
                            result[1].Add(changed_alphabet[i], base64_alphabet[i]);
                        }
                    }
                    transfer = result;
                    return result;
                }
                return transfer;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string DecryptString(string strText, string key)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] buffer = md5.ComputeHash(Encoding.Default.GetBytes(key));
                using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
                {
                    provider.Key = buffer;
                    provider.Mode = CipherMode.ECB;
                    strText = DecryptAlphabet(strText);
                    byte[] inputBuffer = Convert.FromBase64String(strText);
                    string str = Encoding.Default.GetString(provider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
                    return str;
                }
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string EncryptString(string strText, string key)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] buffer = md5.ComputeHash(Encoding.Default.GetBytes(key));
                using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
                {
                    provider.Key = buffer;
                    provider.Mode = CipherMode.ECB;
                    byte[] bytes = Encoding.Default.GetBytes(strText);
                    string str = Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
                    str = EncryptAlphabet(str);
                    return str;
                }
            }
        }

        /// <summary>
        /// 加密字符转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string EncryptAlphabet(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(Transfer[0].ContainsKey(input[i]) ? Transfer[0][input[i]] : input[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 解密字符转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string DecryptAlphabet(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(Transfer[1].ContainsKey(input[i]) ? Transfer[1][input[i]] : input[i]);
            }
            return sb.ToString();
        }

        /// <summary> 
        /// MD5 16位加密 
        /// </summary> 
        /// <param name="ConvertString"></param> 
        /// <returns></returns> 
        public static string Md5(string key, string gps)
        {
            string input = string.Format("{0}&{1}&Salary", key, gps);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string result = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(input)), 4, 8);
                result = result.Replace("-", "");
                return result;
            }
        }
    }
}
