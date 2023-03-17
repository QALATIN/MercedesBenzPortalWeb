using MercedesBenzModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MercedesBenzLibrary
{
    public static class Validaciones
    {

        public static (float, string) CalcularScoreGeneral(SolicitudValidacion validaciones)
        {
            var semaforosResultado = new List<SemaforoResultado>();
            var semaforoIdentificacion = new SemaforoPonderacion() { Semaforo = "Identificación", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 50, Amarillo2Puntos = 0, RojoPuntos = 100, Rojo2Puntos = 0, GrisPuntos = 40 };
            var semaforoFacial = new SemaforoPonderacion() { Semaforo = "Comparación Facial", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 50, Amarillo2Puntos = 0, RojoPuntos = 75, Rojo2Puntos = 0, GrisPuntos = 40 };
            var semaforoCorreo = new SemaforoPonderacion() { Semaforo = "Correo", VerdePuntos = 0, Verde2Puntos = 5, AmarilloPuntos = 20, Amarillo2Puntos = 30, RojoPuntos = 50, Rojo2Puntos = 75, GrisPuntos = 10 };
            var semaforoTelefono = new SemaforoPonderacion() { Semaforo = "Teléfono", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 0, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoCurp = new SemaforoPonderacion() { Semaforo = "Curp", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 0, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoListasInteres = new SemaforoPonderacion() { Semaforo = "Listas de interés", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 0, Amarillo2Puntos = 0, RojoPuntos = 200, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoComprobanteDomicilio = new SemaforoPonderacion() { Semaforo = "Comprobante de domicilio", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 30, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoComprobanteBancario = new SemaforoPonderacion() { Semaforo = "Comprobante bancario", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 30, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoComprobanteIngresos = new SemaforoPonderacion() { Semaforo = "Comprobante de ingresos", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 30, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoListaNegra = new SemaforoPonderacion() { Semaforo = "Lista negra", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 0, Amarillo2Puntos = 0, RojoPuntos = 100, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoIne = new SemaforoPonderacion() { Semaforo = "Ine", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 0, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };
            var semaforoHuellas = new SemaforoPonderacion() { Semaforo = "Huellas", VerdePuntos = 0, Verde2Puntos = 0, AmarilloPuntos = 30, Amarillo2Puntos = 0, RojoPuntos = 50, Rojo2Puntos = 0, GrisPuntos = 10 };

            if (validaciones.ValidarIdentificacion)
            {
                var resultadoIdentificacion = new SemaforoResultado() { PuntosMaximos = semaforoIdentificacion.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoIdentificacion, validaciones.SemaforoIdentificacion) };
                semaforosResultado.Add(resultadoIdentificacion);
            }

            if (validaciones.ValidarFacial)
            {
                var resultadoFacial = new SemaforoResultado() { PuntosMaximos = semaforoFacial.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoFacial, validaciones.SemaforoFacial) };
                semaforosResultado.Add(resultadoFacial);
            }

            if (validaciones.ValidarCorreo)
            {
                var resultadoCorreo = new SemaforoResultado() { PuntosMaximos = semaforoCorreo.Rojo2Puntos };
                string colorSemaforo;
                if (validaciones.ScoreCorreo >= 0)
                {
                    if (validaciones.ScoreCorreo <= 100)
                        colorSemaforo = "verde";
                    else if (validaciones.ScoreCorreo <= 300)
                        colorSemaforo = "verde2";
                    else if (validaciones.ScoreCorreo <= 600)
                        colorSemaforo = "amarillo";
                    else if (validaciones.ScoreCorreo <= 799)
                        colorSemaforo = "amarillo2";
                    else if (validaciones.ScoreCorreo <= 899)
                        colorSemaforo = "rojo";
                    else
                        colorSemaforo = "rojo2";
                }
                else
                    colorSemaforo = "gris";
                resultadoCorreo.PuntosObtenidos = SemaforoPuntaje(semaforoCorreo, colorSemaforo);
                semaforosResultado.Add(resultadoCorreo);
            }

            if (validaciones.ValidarTelefono)
            {
                var resultadoTelefono = new SemaforoResultado() { PuntosMaximos = semaforoTelefono.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoTelefono, validaciones.SemaforoTelefono) };
                semaforosResultado.Add(resultadoTelefono);
            }

            if (validaciones.ValidarCurp)
            {
                var resultadoCurp = new SemaforoResultado() { PuntosMaximos = semaforoCurp.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoCurp, validaciones.SemaforoCurp) };
                semaforosResultado.Add(resultadoCurp);
            }

            if (validaciones.ValidarListaAml)
            {
                var resultadoListasInteres = new SemaforoResultado() { PuntosMaximos = semaforoListasInteres.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoListasInteres, validaciones.SemaforoListaAml) };
                semaforosResultado.Add(resultadoListasInteres);
            }

            if (validaciones.ValidarComprobanteDomicilio)
            {
                var resultadoComprobanteDomicilio = new SemaforoResultado() { PuntosMaximos = semaforoComprobanteDomicilio.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoComprobanteDomicilio, validaciones.SemaforoComprobanteDomicilio) };
                semaforosResultado.Add(resultadoComprobanteDomicilio);
            }

            if (validaciones.ValidarComprobanteBancario)
            {
                var resultadoComprobanteBancario = new SemaforoResultado() { PuntosMaximos = semaforoComprobanteBancario.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoComprobanteBancario, validaciones.SemaforoComprobanteBancario) };
                semaforosResultado.Add(resultadoComprobanteBancario);
            }

            if (validaciones.ValidarComprobanteIngresos)
            {
                var resultadoComprobanteIngresos = new SemaforoResultado() { PuntosMaximos = semaforoComprobanteIngresos.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoComprobanteIngresos, validaciones.SemaforoComprobanteIngresos) };
                semaforosResultado.Add(resultadoComprobanteIngresos);
            }

            if (validaciones.ValidarListaNegra)
            {
                var resultadoListaNegra = new SemaforoResultado() { PuntosMaximos = semaforoListaNegra.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoListaNegra, validaciones.SemaforoListaNegra) };
                semaforosResultado.Add(resultadoListaNegra);
            }

            if (validaciones.ValidarIne)
            {
                var resultadoIne = new SemaforoResultado() { PuntosMaximos = semaforoIne.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoIne, validaciones.SemaforoIne) };
                semaforosResultado.Add(resultadoIne);
            }

            if (validaciones.ValidarAfis)
            {
                var resultadoHuellas = new SemaforoResultado() { PuntosMaximos = semaforoHuellas.RojoPuntos, PuntosObtenidos = SemaforoPuntaje(semaforoHuellas, validaciones.SemaforoAfis) };
                semaforosResultado.Add(resultadoHuellas);
            }

            float puntosMaximos = 0;
            float puntosObtenidos = 0;
            foreach (var item in semaforosResultado)
            {
                puntosMaximos += item.PuntosMaximos;
                puntosObtenidos += item.PuntosObtenidos;
            }
            string semaforo;
            if (puntosObtenidos <= 49)
                semaforo = "verde";
            else if (puntosObtenidos <= 99)
                semaforo = "amarillo";
            else if (puntosObtenidos <= 199)
                semaforo = "rojo";
            else
                semaforo = "rojo2";
            float calificacion = (puntosObtenidos * 100) / puntosMaximos;
            float score = 100 - calificacion;

            return (score, semaforo);
        }

        private static float SemaforoPuntaje(SemaforoPonderacion ponderacion, string semaforo)
        {
            float puntos = semaforo.ToUpper() switch
            {
                "VERDE" => ponderacion.VerdePuntos,
                "VERDE2" => ponderacion.Verde2Puntos,
                "AMARILLO" => ponderacion.AmarilloPuntos,
                "AMARILLO2" => ponderacion.Amarillo2Puntos,
                "ROJO" => ponderacion.RojoPuntos,
                "ROJO2" => ponderacion.Rojo2Puntos,
                "GRIS" => ponderacion.GrisPuntos,
                _ => ponderacion.GrisPuntos,
            };
            return puntos;
        }

        public static string CalcularTiempoEnvio(DateTime fechaEnvio)
        {
            var minutos = (DateTime.Now - fechaEnvio).TotalMinutes;
            string tiempoRegistro;
            if (minutos >= 60 && minutos < 1440)
            {
                if (minutos >= 120)
                {
                    tiempoRegistro = "Hace " + (DateTime.Now - fechaEnvio).Hours.ToString() + " horas";
                }
                else
                {
                    tiempoRegistro = "Hace " + (DateTime.Now - fechaEnvio).Hours.ToString() + " hora";
                }
            }
            else if (minutos >= 1440)
            {
                if (minutos >= 2880)
                {
                    tiempoRegistro = "Hace " + (DateTime.Now - fechaEnvio).Days.ToString() + " días";
                }
                else
                {
                    tiempoRegistro = "Hace " + (DateTime.Now - fechaEnvio).Days.ToString() + " día";
                }
            }
            else
            {
                if (minutos >= 1)
                {
                    if (minutos >= 1 && minutos < 2)
                    {
                        tiempoRegistro = "Hace " + (DateTime.Now - fechaEnvio).Minutes.ToString() + " minuto";
                    }
                    else
                    {
                        tiempoRegistro = "Hace " + (DateTime.Now - fechaEnvio).Minutes.ToString() + " minutos";
                    }

                }
                else
                {
                    tiempoRegistro = "Hace un momento";
                }
            }
            return tiempoRegistro;
        }

        public static (string, string) CalcularSemaforoIdentificacion(string valor)
        {
            string semaforo;
            string resultado;
            switch (valor.ToUpper())
            {
                case "PASSED":
                    semaforo = "verde";
                    resultado = "RIESGO BAJO";
                    break;
                case "ATTENTION":
                    semaforo = "amarillo";
                    resultado = "RIESGO MODERADO";
                    break;
                case "FAILED":
                    semaforo = "rojo";
                    resultado = "RIESGO ALTO";
                    break;
                default:
                    semaforo = "gris";
                    resultado = "DOCUMENTO DESCONOCIDO";
                    break;
            }
            return (semaforo, resultado);
        }

        public static (string, string) CalcularSemaforoComparacionFacial(string score)
        {
            string semaforo = "gris";
            string resultado = "IMÁGENES NO PROCESADAS";

            if (float.TryParse(score, out float scoreValidado))
            {
                if (scoreValidado > 0)
                {
                    if (scoreValidado < 50)
                    {
                        semaforo = "rojo";
                        resultado = "BAJA PROBABILIDAD MISMA PERSONA";
                    }
                    else if (scoreValidado < 79)
                    {
                        semaforo = "amarillo";
                        resultado = "MEDIA PROBABILIDAD MISMA PERSONA";
                    }
                    else
                    {
                        semaforo = "verde";
                        resultado = "ALTA PROBABILIDAD MISMA PERSONA";
                    }
                }
            }
            return (semaforo, resultado);
        }

        public static string CalcularAlertaxSemaforo(string valor)
        {
            string resultado = valor.ToUpper() switch
            {
                "VERDE" => "Sin alerta",
                "AMARILLO" => "Con precaución",
                "ROJO" => "Con alerta",
                _ => "Sin consulta",
            };
            return resultado;
        }

        public static string CalcularAlertaxResultado(string valor)
        {
            string resultado = valor.ToUpper() switch
            {
                "PASSED" => "Sin alerta",
                "ATTENTION" => "Con precaución",
                "FAILED" => "Con alerta",
                _ => "Sin consulta",
            };
            return resultado;

        }

        public static int CalcularIneEstatus(string respuesta)
        {
            int estatusId = 7;
            if (!string.IsNullOrEmpty(respuesta))
            {
                string vigente = respuesta.Substring(0, 12);
                if (vigente == "Está vigente")
                    estatusId = 1;
                else if (respuesta.Contains("Tus datos se encuentran"))
                    estatusId = 1;
                else if (respuesta.Contains("No está vigente"))
                    estatusId = 2;
                else if (respuesta.Contains("No se obtuvieron datos"))
                    estatusId = 3;
                else if (respuesta.Contains("reportada"))
                    estatusId = 4;
                else if (respuesta.Contains("Por mandato judicial"))
                    estatusId = 5;
                else if (respuesta.Contains("mantenimiento"))
                    estatusId = 6;
                else
                    estatusId = 7;
            }
            return estatusId;
        }

        public static string IneGenerarUrlConsulta(IneValidacion ine, string urlListaNominal, string urlApiIneValidacion)
        {

            string param1;
            string param2;
            string param3;
            string param4;
            string param5;
            string param6;
            string param7;
            string urlConsultaListaNominal;

            switch (ine.Modelo)
            {
                case "A":
                case "B":
                case "C":
                    if (string.IsNullOrEmpty(ine.NumeroEmision))
                    {
                        ine.NumeroEmision = "00";
                    }
                    param1 = ine.ClaveElector;
                    param2 = ine.NumeroEmision;
                    param3 = ine.Ocr;
                    break;
                case "D":
                    param1 = ine.Cic;
                    param2 = ine.Ocr;
                    param3 = string.Empty;
                    break;
                case "E":
                    if(string.IsNullOrEmpty(ine.IdentificadorCiudadano) && !string.IsNullOrEmpty(ine.Mrz))
                    {
                        ine.IdentificadorCiudadano = ine.Mrz.Substring(21, 9);
                    }
                    param1 = ine.Cic;
                    param2 = ine.IdentificadorCiudadano;
                    param3 = string.Empty;
                    break;
                default:
                    return "";
            }
            param4 = ine.Modelo;
            param5 = "serv";
            param6 = ine.Guid.ToString();

            //urlApiIneValidacion = "https://idbiometrics.net/InevalidacionDesarrollo/api/IneValidacion";
            //UrlApiIneValidacion = "http://mbfs.latinid.com.mx:9582/Gateway/api/ineValidacion";

            var plainTextBytes = Encoding.UTF8.GetBytes(urlApiIneValidacion);
            param7 = Convert.ToBase64String(plainTextBytes);
            urlConsultaListaNominal = string.Format($"{urlListaNominal}?param1={param1}&param2={param2}&param3={param3}&param4={param4}&param5={param5}&param6={param6}&param7={param7}");

            return urlConsultaListaNominal;
        }

        public static (string, string, string) ComprobarTipoValidacion(TipoSemaforo tipoValidacion)
        {
            string CampoSemaforo;
            string CampoResultado;
            string CampoFecha;

            switch (tipoValidacion)
            {
                case TipoSemaforo.Ine:  // 5 OK
                    CampoSemaforo = "Semaforo_Ine";
                    CampoResultado = "Resultado_Ine";
                    CampoFecha = "Fecha_Ine";
                    break;
                case TipoSemaforo.Correo:   // 6 OK
                    CampoSemaforo = "Semaforo_Correo";
                    CampoResultado = "Resultado_Correo";
                    CampoFecha = "Fecha_Correo";
                    break;
                case TipoSemaforo.Telefono: // 7 OK
                    CampoSemaforo = "Semaforo_Telefono";
                    CampoResultado = "Resultado_Telefono";
                    CampoFecha = "Fecha_Telefono";
                    break;
                case TipoSemaforo.Curp: // 8 OK
                    CampoSemaforo = "Semaforo_Curp";
                    CampoResultado = "Resultado_Curp";
                    CampoFecha = "Fecha_Curp";
                    break;
                case TipoSemaforo.ComprobanteDomicilio: // 9 OK
                    CampoSemaforo = "Semaforo_Comprobante_Domicilio";
                    CampoResultado = "Resultado_Comprobante_Domicilio";
                    CampoFecha = "Fecha_Comprobante_Domicilio";
                    break;
                case TipoSemaforo.ComprobanteIngresos:  // 10 OK
                    CampoSemaforo = "Semaforo_Comprobante_Ingresos";
                    CampoResultado = "Resultado_Comprobante_Ingresos";
                    CampoFecha = "Fecha_Comprobante_Ingresos";
                    break;
                case TipoSemaforo.ComprobanteBancario:  // 11 OK
                    CampoSemaforo = "Semaforo_Comprobante_Bancario";
                    CampoResultado = "Resultado_Comprobante_Bancario";
                    CampoFecha = "Fecha_Comprobante_Bancario";
                    break;
                case TipoSemaforo.ListasInteres:    // 12 OK
                    CampoSemaforo = "Semaforo_Lista_Aml";
                    CampoResultado = "Resultado_Lista_Aml";
                    CampoFecha = "Fecha_Lista_Aml";
                    break;
                default:
                    throw Validaciones.ExceptionCampoIncorrecto("Tipo de Validación");
            }

            return (CampoSemaforo, CampoResultado, CampoFecha);
        }

        public static string GenerarPathFile(string rutaPrincipal, DateTime fechaValidacion)
        {
            string carpeta_year = fechaValidacion.Year.ToString().PadLeft(4, '0');
            string carpeta_month = fechaValidacion.Month.ToString().PadLeft(4, '0');
            string carpeta_day = fechaValidacion.Day.ToString().PadLeft(4, '0');

            if (!Directory.Exists(Path.Combine(rutaPrincipal, carpeta_year)))
            {
                if (!Directory.Exists(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month)))
                {
                    if (!Directory.Exists(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day)))
                    {
                        Directory.CreateDirectory(Path.Combine(rutaPrincipal, carpeta_year));
                        Directory.CreateDirectory(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month));
                        Directory.CreateDirectory(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day));
                    }
                }
            }
            else
            {
                if (!Directory.Exists(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month)))
                {
                    if (!Directory.Exists(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day)))
                    {
                        Directory.CreateDirectory(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month));
                        Directory.CreateDirectory(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day));
                    }
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day)))
                        Directory.CreateDirectory(Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day));
                }
            }
            string fileName = Path.Combine(rutaPrincipal, carpeta_year, carpeta_month, carpeta_day);

            return fileName;
        }

        public static string GenerarPathFileHuellasValidacion(string repositoryPath, int solicitanteId)
        {
            string pathFile = $"{repositoryPath}\\HuellasValidacion{solicitanteId.ToString().PadLeft(12, '0')}.json";
            return pathFile;
        }

        public static string GetPathConfigurationFile()
        {
            string directoryPath = Assembly.GetExecutingAssembly().Location;
            directoryPath = Path.GetDirectoryName(directoryPath);
            string fileName = Path.Combine(directoryPath,"LibrarySettings.json");
            string settingsFilePath;
            if (System.IO.File.Exists(fileName))
            {
                var configuration = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(fileName));
                settingsFilePath = configuration.SettingsFilePath;
                if(string.IsNullOrEmpty(settingsFilePath))
                    settingsFilePath = @$"{directoryPath}\";
            }
            else
                settingsFilePath = @$"{directoryPath}\";
            return settingsFilePath;
        }

        public static void ValidarCampoExiste(string nombreCampo, int idEncontrado, int idActual)
        {
            if (idEncontrado != idActual)
                throw Validaciones.ExceptionCampoIncorrecto(nombreCampo, "ya existe");
        }

        public static Exception ExceptionCampoIncorrecto(string nombreCampo, string mensajeError = "es incorrecto")
        {
            string error = $"El campo {nombreCampo} {mensajeError}";
            return new InvalidOperationException(error);
        }

        public static string GetMIMEType(string fileName)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

    }
}
