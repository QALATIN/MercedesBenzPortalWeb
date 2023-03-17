using Dapper;
using MercedesBenzModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MercedesBenzDBValidaciones
{
    public static class DbValidaciones
    {
        public static async Task<SolicitudComparacionHuellas> HuellasValidar(IDbConnection Connection, int SolicitanteId, int AfisId, DateTime FechaValidacion, string NombreCliente, string PathFile)
        {
            var resultado = new Resultado { Semaforo = "verde", Mensaje = "No se encontraron huellas coincidentes", FechaValidacion = FechaValidacion };
            var data = new SolicitudComparacionHuellas
            {
                AfisId = AfisId
            };
            var parameters = new DynamicParameters();
            parameters.Add("@SolicitanteId", SolicitanteId);
            parameters.Add("@AfisId", AfisId);

            string qry = DbSql.QryGetSolicitudHuellasCoincidentes();
            List<SolicitudHuellaCoincidente> huellasCoincidentes = (List<SolicitudHuellaCoincidente>)await Connection.QueryAsync<SolicitudHuellaCoincidente>(qry, parameters);
            if (huellasCoincidentes.Count > 0)
            {
                resultado.Semaforo = "amarillo";
                resultado.Mensaje = "Coincidencia encontrada mismo nombre";
                foreach (var item in huellasCoincidentes)
                {
                    if (item.NombreCompleto != NombreCliente)
                    {
                        resultado.Semaforo = "rojo";
                        resultado.Mensaje = "Coincidencia encontrada";
                    }
                }
            }
            else
                huellasCoincidentes = null;

            data.Resultado = resultado;
            data.HuellasCoincidentes = huellasCoincidentes;
            System.IO.File.WriteAllText(PathFile, JsonConvert.SerializeObject(data));

            parameters.Add("@Semaforo", resultado.Semaforo);
            parameters.Add("@Resultado", resultado.Mensaje);
            parameters.Add("@FechaValidacion", resultado.FechaValidacion);

            qry = DbSql.QryUpdateSemaforoHuellas();
            _ = await Connection.ExecuteAsync(qry, parameters);

            return data;
        }

    }
}
