using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;

namespace MercedesBenzSecurity
{
    public static partial class EncryptionAes
    {
        public static dynamic DecryptObject(string objetoCifrado, dynamic objetoModelo)
        {
            _mensaje = "";
            try
            {
                string objetoDecrypt = DecryptString(objetoCifrado);
                if (objetoDecrypt == null)
                    return null;
                Type t = objetoDecrypt.GetType();
                if (!t.IsClass)
                    return null;
                var objetoDeserialize = JsonConvert.DeserializeAnonymousType(objetoDecrypt, objetoModelo);
                return objetoDeserialize;
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

        public static string DecryptString(string value)
        {
            _mensaje = "";
            try
            {
                string decryptString = DecryptAES(value);
                return decryptString;
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

        static string DecryptAES(string value)
        {
            _mensaje = "";
            string dencryptText = null;
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Texto vacío");
                
                byte[] bytes = Convert.FromBase64String(value);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = KeyByte();
                    aesAlg.IV = InitializationVectorByte();

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using MemoryStream msDecrypt = new(bytes);
                    using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                    using StreamReader srDecrypt = new(csDecrypt);
                    dencryptText = srDecrypt.ReadToEnd();
                }
                return dencryptText;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
