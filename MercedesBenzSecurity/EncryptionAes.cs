using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace MercedesBenzSecurity
{
    public static partial class EncryptionAes
    {
        static readonly string _key = "zc$eb8p@pCmSe5!378pUK@G@3wXVfz2F";
        static readonly string _initializationVector = ".TNuO0uT&-sh}@Vw";
        private static string _mensaje = "";
        public static string Messaje { get => _mensaje; }

        private static byte[] KeyByte() => UTF8Encoding.UTF8.GetBytes(_key);
        private static byte[] InitializationVectorByte() => UTF8Encoding.UTF8.GetBytes(_initializationVector);

        public static string EncryptObject(Object objetoGenerico)
        {
            _mensaje = "";
            try
            {
                Type t = objetoGenerico.GetType();
                if (!t.IsClass)
                    return null;
                string objetoSerialize = JsonConvert.SerializeObject(objetoGenerico);
                return EncryptString(objetoSerialize);
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

        public static string EncryptString(string value)
        {
            _mensaje = "";
            try
            {
                string encryptedString = EncryptAES(value);
                return encryptedString;
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }

        }

        public static int EncryptInt(int value)
        {
            _mensaje = "";
            return value;
        }

        public static float EncryptFloat(float value)
        {
            _mensaje = "";
            return value;
        }

        public static DateTime EncryptDateTime(DateTime value)
        {
            _mensaje = "";
            return value;
        }

        public static byte[] EncryptBytes(byte[] value)
        {
            _mensaje = "";
            return value;
        }

        private static string EncryptAES(string text)
        {
            _mensaje = "";
            string encryptText;
            try
            {
                if (string.IsNullOrEmpty(text))
                    throw new ArgumentException("Texto vacío");

                byte[] encryptedBytes;
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = KeyByte();
                    aesAlg.IV = InitializationVectorByte();

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using MemoryStream msEncrypt = new();
                    using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                    using (StreamWriter swEncrypt = new(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
                encryptText = Convert.ToBase64String(encryptedBytes, 0, encryptedBytes.Length, Base64FormattingOptions.None);

                return encryptText;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
