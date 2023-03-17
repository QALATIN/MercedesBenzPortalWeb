using Newtonsoft.Json;
using System;
using System.Text;

namespace MercedesBenzLibrary
{
    public static class CodificacionBase64
    {
        public static string Messaje { get => _mensaje; }

        private static string _mensaje = "";

        public static string CodificarTexto(string texto)
        {
            _mensaje = "";
            try
            {
                byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
                return Convert.ToBase64String(textoBytes, 0, textoBytes.Length);
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

        public static string DecodificarTexto(string texto)
        {
            _mensaje = "";
            try
            {
                byte[] textoBytes = Convert.FromBase64String(texto);
                return Encoding.UTF8.GetString(textoBytes);
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

        public static string CodificarObjeto(Object objetoGenerico)
        {
            _mensaje = "";
            try
            {
                Type t = objetoGenerico.GetType();
                if (!t.IsClass)
                    return null;
                string objectSerialize = JsonConvert.SerializeObject(objetoGenerico);
                return CodificarTexto(objectSerialize);
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

        public static dynamic DecodificarObjeto(string objetoCodificado, dynamic objetoModelo)
        {
            _mensaje = "";
            try
            {
                string objetoSerialize = DecodificarTexto(objetoCodificado);
                if (objetoSerialize == null)
                    return null;
                Type t = objetoSerialize.GetType();
                if (!t.IsClass)
                    return null;
                var objetoDeserialize = JsonConvert.DeserializeAnonymousType(objetoSerialize, objetoModelo);
                return objetoDeserialize;
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                return null;
            }
        }

    }
}
