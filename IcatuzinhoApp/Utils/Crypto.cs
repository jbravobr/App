using System;
using System.Text;

namespace IcatuzinhoApp
{
    public static class Crypto
    {
        static byte[] keyMaterial;
        static byte[] data;
        static string Salt = "CDbYtCHcZhPU2Rnu9XrJ";

        public static string EncryptStringAES(string info)
        {
            //keyMaterial = Encoding.UTF8.GetBytes(Constants.SharedSecret);
            //data = Encoding.UTF8.GetBytes(info);

            //var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            //var key = provider.CreateSymmetricKey(keyMaterial);

            //byte[] iv = Encoding.UTF8.GetBytes(Salt);
            //byte[] cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, data, iv);

            //return Encoding.UTF8.GetString(cipherText, 0, cipherText.Length);

            return string.Empty;
        }

        public static string DecryptStringAES(string info, string sharedSecret)
        {
            //keyMaterial = Encoding.UTF8.GetBytes(Constants.SharedSecret);
            //data = Encoding.UTF8.GetBytes(info);

            //var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            //var key = provider.CreateSymmetricKey(keyMaterial);

            //byte[] iv = Encoding.UTF8.GetBytes(Salt);
            //byte[] cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, data, iv);
            //byte[] plainText = WinRTCrypto.CryptographicEngine.Decrypt(key, cipherText, iv);

            //return Encoding.UTF8.GetString(plainText, 0, plainText.Length);

            return string.Empty;
        }
    }
}
