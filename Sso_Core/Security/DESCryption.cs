using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Sso_Core.Security
{
    public class DESCryption
    {
        private DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        private Encoding encoding = new UnicodeEncoding();

        public DESCryption()
        {

        }

        public void GenerateDesCryProvider(string key, out string iv)
        {
            iv = "";
            des.GenerateKey();
            des.GenerateIV();
            iv = encoding.GetString(des.IV);
        }

        public void GenerateKeyLoop()
        {
            des.GenerateKey();
        }

        public string GenerateDesCryProvider(ref string key, ref string iv)
        {
            des.GenerateKey(); // 每次调用都是随机生成     //des.GenerateIV();            
            //byte[] iv = UTF8Encoding.UTF8.GetBytes(iv1);  <=> UnicodeEncoding.GetEncoding("UTF-8").GetBytes(iv1)


            //des.IV = UTF8Encoding.UTF8.GetBytes(iv);
                           //iv = Convert.ToBase64String(des.IV);  //GetString()方法：将一个字节序列解码为一个字符串 
            //iv = UTF8Encoding.UTF8.GetString(des.IV);     // 结果就是   "********"

            des.IV = Encoding.Default.GetBytes(iv);
            iv = Encoding.Default.GetString(des.IV);
            string key0 = Encoding.Default.GetString(des.Key);
            byte[] key1 = Encoding.Default.GetBytes(key0); //排除为weekKey的情形
            if (key1.Length != 8)
            {   //GenerateKeyLoop();
                key0 = "";
                GenerateDesCryProvider(ref key0, ref iv);
            }
            else
            {   //key = UTF8Encoding.UTF8.GetString(des.Key);


                key = Encoding.Default.GetString(des.Key);
                byte[] key2 = Encoding.Default.GetBytes(key);
                int len = key2.Length;
                return key;   // 这里做不做返回效果都是一样。因为最终变化后的des.Key都是8位byte型的数组
            }
            key = Encoding.Default.GetString(des.Key);
            return Encoding.Default.GetString(des.Key);
        }

        /// <summary>
        ///  密钥的存储分配问题 。DES加密
        /// </summary>
        /// <returns></returns>
        public string Encrypt(string text)
        {
            byte[] t = encoding.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(t, 0, t.Length);
            cs.FlushFinalBlock();
            cs.Close();
            byte[] buffer = ms.ToArray();  //将加密过的数据转换成字节型数组
            ms.Close();
            return Convert.ToBase64String(buffer);  //将加密数据输出为字符串
        }

        public string Encrypt(string text, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(text);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
            //byte[] t = UTF8Encoding.UTF8.GetBytes(text);
            //byte[] k = UTF8Encoding.UTF8.GetBytes(key);
            //byte[] i = UTF8Encoding.UTF8.GetBytes(iv);
            //des.IV = i;
            //des.Key = k;
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(des.Key,des.IV), CryptoStreamMode.Write);
            //cs.Write(t, 0, t.Length);
            //cs.FlushFinalBlock();
            //cs.Close();
            //byte[] buffer = ms.ToArray();  //将加密过的数据转换成字节型数组
            //ms.Close();
            //return Convert.ToBase64String(buffer);  //将加密数据输出为字符串
        }

        public string Decrypt(string text, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(text);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
            //byte[] keyBuffer = UTF8Encoding.UTF8.GetBytes(key);
            //byte[] ivBuffer = UTF8Encoding.UTF8.GetBytes(iv);
            //byte[] t = Convert.FromBase64String(text);  // 将Base64编码部分解码为字节数组
            //des.Key = keyBuffer;
            //des.IV = ivBuffer;
            //MemoryStream ms = new MemoryStream(t);
            //CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            ////StreamReader sr = new StreamReader(cs);

            //int readPos = 0;
            //cs.Write(t, readPos, t.Length - readPos);
            //cs.FlushFinalBlock();
            //string val = UTF8Encoding.UTF8.GetString(ms.ToArray());

            //cs.Close();
            //ms.Close();
            //des.Clear();
            //return val;
        }
    }
}
