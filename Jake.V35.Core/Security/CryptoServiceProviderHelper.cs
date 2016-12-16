using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

/////////////////////////////////////////////////////
////2015.12.7 jake 修改                          ////
////    一、RSA                                  ////
////        1. 生成RSA密钥                       ////
////        2. 保存公钥私钥文件                   ////
////        3. RSA加密、解密、签名、验签          ////
////    二、DES                                  ////
////        1. 生成DES key                       ////
////        2. DES加密解密                       ////
/////////////////////////////////////////////////////
namespace Jake.V35.Core.Security
{
    public struct RSAKey
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public void SetPrivateKey(string privateKeyPath)
        {
            PrivateKey = ReadFromFile(privateKeyPath);
        }

        public void SetPublicKey(string publicKeyPath)
        {
            PublicKey = ReadFromFile(publicKeyPath);
        }

        private static string ReadFromFile(string path)
        {
            //加密解密用到的公钥与私钥
            using (StreamReader streamReader = new StreamReader(path))
            {
                string key = streamReader.ReadToEnd();
                return key;
            }
        }
    }

    public class CryptoServiceProviderHelper
    {
        public static RSAKey GenerateRSAKey()
        {
            //加密解密用到的公钥与私钥
            RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider();
            string privatekey = oRSA.ToXmlString(true); //私钥
            string publickey = oRSA.ToXmlString(false); //公钥
            RSAKey rsaKey = new RSAKey();
            rsaKey.PrivateKey = privatekey;
            rsaKey.PublicKey = publickey;
            return rsaKey;
        }

        public static RSAKey GenerateRSAKey(int keyLenght)
        {
            //加密解密用到的公钥与私钥
            RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider(keyLenght);
            string privatekey = oRSA.ToXmlString(true); //私钥
            string publickey = oRSA.ToXmlString(false); //公钥
            RSAKey rsaKey = new RSAKey();
            rsaKey.PrivateKey = privatekey;
            rsaKey.PublicKey = publickey;
            return rsaKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsaKey"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveRSAPrivateKey(RSAKey rsaKey, string path)
        {
            //加密解密用到的公钥与私钥
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write(rsaKey.PrivateKey);
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsaKey"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveRSAPubliceKey(RSAKey rsaKey, string path)
        {
            //加密解密用到的公钥与私钥
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write(rsaKey.PublicKey);
                return true;
            }
        }

    #region 密钥

        /// <summary>
        /// RSA 公钥加密
        /// </summary>
        /// <param name="orignalContent">待加密byte[]</param>
        /// <param name="publickey">xmlstring公钥</param>
        /// <returns></returns>
        public static byte[] RSAEncrpty(byte[] orignalContent, string publickey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(publickey); //加密要用到公钥所以导入公钥
            byte[] encrptiedBytes = rSACryptoServiceProvider.Encrypt(orignalContent, false);
            //encrptiedBytes 加密以后的数据 
            return encrptiedBytes;
        }
        /// <summary>
        /// RSA 私钥解密
        /// </summary>
        /// <param name="encrptyContent">加密后的byte[]</param>
        /// <param name="privatekey">xmlstring私钥</param>
        /// <returns></returns>
        public static byte[] RSADecrpty(byte[] encrptyContent, string privatekey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(privatekey);
            byte[] decrptiedBytes = rSACryptoServiceProvider.Decrypt(encrptyContent, false);
            return decrptiedBytes;
        }
        /// <summary>
        /// RSA-SHA1 私钥签名
        /// </summary>
        /// <param name="orignalContent">待签名byte[]</param>
        /// <param name="privatekey">xmlstring私钥</param>
        /// <returns></returns>
        public static byte[] RSASignData(byte[] orignalContent, string privatekey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(privatekey);
            byte[] signature = rSACryptoServiceProvider.SignData(orignalContent, "SHA1");
            return signature;
        }

        /// <summary>
        /// RSA-SHA1 公钥验签
        /// </summary>
        /// <param name="orignalContent">待验签byte[]</param>
        /// <param name="signature">签名后的byte[]</param>
        /// <param name="publickey">xmlstring公钥</param>
        /// <returns></returns>
        public static bool RSAVerifyData(byte[] orignalContent, byte[] signature, string publickey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(publickey);
            bool isVerify = rSACryptoServiceProvider.VerifyData(orignalContent, "SHA1", signature);
            return isVerify;
        }
        #endregion
        #region 证书
        /// <summary>
        /// RSA-SHA1 通过证书 私钥签名
        /// </summary>
        /// <param name="orignalConent">待签名byte[]</param>
        /// <param name="privateKeyPath">私钥文件路径</param>
        /// <param name="password">私钥密码</param>
        /// <returns></returns>
        public static byte[] RSASignData(byte[] orignalConent, string privateKeyPath, string password)
        {
            X509Certificate2 x509 = new X509Certificate2(privateKeyPath, password);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] hashbytes = sha1.ComputeHash(orignalConent); //对要签名的数据进行哈希
            RSAPKCS1SignatureFormatter rsaPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter();
            rsaPKCS1SignatureFormatter.SetKey(x509.PrivateKey); //设置签名用到的私钥
            rsaPKCS1SignatureFormatter.SetHashAlgorithm("SHA1"); //设置签名算法
            byte[] result = rsaPKCS1SignatureFormatter.CreateSignature(hashbytes);
            return result;
        }
        /// <summary>
        /// 通过证书 公钥验签
        /// </summary>
        /// <param name="orignalConent">待验签byte[]</param>
        /// <param name="signature">签名后byte[]</param>
        /// <param name="publicKeyPath">公钥文件路径</param>
        /// <returns></returns>
        public static bool RSAVerifyDataByCertificate(byte[] orignalConent, byte[] signature, string publicKeyPath)
        {
            X509Certificate2 x509 = new X509Certificate2(publicKeyPath);
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.FromXmlString(x509.PublicKey.Key.ToXmlString(false));
            bool bVerify = rsaCryptoServiceProvider.VerifyData(orignalConent, "SHA1", signature);
            return bVerify;
        }

        /// <summary>
        /// 证书公钥加密
        /// </summary>
        /// <param name="orignalContent">待加密byte[]</param>
        /// <param name="certPath">证书路径</param>
        /// <param name="password">私钥密码</param>
        /// <returns></returns>
        public static byte[] RSAEncrpty(byte[] orignalContent, string certPath, string password)
        {
            X509Certificate2 x509 = new X509Certificate2(certPath, password);
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.FromXmlString(x509.PublicKey.Key.ToXmlString(false));
            byte[] encrptiedBytes = rsaCryptoServiceProvider.Encrypt(orignalContent, false);
            return encrptiedBytes;
        }

        /// <summary>
        /// 证书公钥加密
        /// </summary>
        /// <param name="encrptyContent">加密后byte[]</param>
        /// <param name="certPath">证书路径</param>
        /// <param name="password">私钥密码</param>
        /// <returns></returns>
        public static byte[] RSADecrpty(byte[] encrptyContent, string certPath, string password)
        {
            X509Certificate2 x509 = new X509Certificate2(certPath, password);
            RSACryptoServiceProvider rsa2 = (RSACryptoServiceProvider)x509.PrivateKey;
            byte[] orignalContent = rsa2.Decrypt(encrptyContent, false);
            return orignalContent;
        }
        /// <summary>
        /// 自动生成DES key
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateDESKey()
        {
            DESCryptoServiceProvider Des = new DESCryptoServiceProvider();
            return Des.Key;
        }

        /// <summary>
        /// DES对称加密
        /// </summary>
        /// <param name="orignalContent">待加密byte[]</param>
        /// <param name="keys">对称Key</param>
        /// <returns></returns>
        public static byte[] DESEncrpty(byte[] orignalContent, byte[] keys)
        {
            //加密
            DESCryptoServiceProvider tdesProvider = new DESCryptoServiceProvider();
            tdesProvider.Key = keys;
            tdesProvider.Mode = CipherMode.ECB;
            byte[] encrypted = tdesProvider.CreateEncryptor().TransformFinalBlock(orignalContent, 0, orignalContent.Length);
            return encrypted;
        }
        /// <summary>
        /// DES对称解密
        /// </summary>
        /// <param name="orignalContent">待加密byte[]</param>
        /// <param name="keys">对称Key</param>
        /// <returns></returns>
        public static byte[] DESDecrpty(byte[] encryptedContent, byte[] keys)
        {
            //解密
            DESCryptoServiceProvider tdesProvider = new DESCryptoServiceProvider
            {
                Key = keys,
                Mode = CipherMode.ECB
            };
            byte[] orignalContent = tdesProvider.CreateDecryptor().TransformFinalBlock(encryptedContent, 0, encryptedContent.Length);
            return orignalContent;
        }
        #endregion
    }
}
