using Dapper;
using MercedesBenzModel;
using System;

namespace MercedesBenzApiIneValidacion
{
    public static class SqlHelper
    {
        static string _qry = "";
        private static DynamicParameters _parameters;

        public static string QryDesactivarGuidAnteriorIneValidacion(Guid Guid) => $"UPDATE IneValidacion SET Activo = false WHERE Activo = true AND Guid = '{Guid}'; ";

        public static DynamicParameters IneParametros(IneValidacionRequest Ine)
        {
            _parameters = new DynamicParameters();
            _parameters.Add("@IneValidacionId ", Ine.IneValidacionId);
            _parameters.Add("@Guid", Ine.Guid);
            _parameters.Add("@FechaRegistro", Ine.FechaRegistro);
            _parameters.Add("@Activo", Ine.Activo);
            _parameters.Add("@IneEstatusId", Ine.IneEstatusId);

            _parameters.Add("@Modelo", Ine.Modelo);
            _parameters.Add("@IneClaveDeElector", Ine.IneClaveDeElector);
            _parameters.Add("@IneNumeroDeEmision", Ine.IneNumeroDeEmision);
            _parameters.Add("@IneOcr", Ine.IneOcr);
            _parameters.Add("@IneCic", Ine.IneCic);
            _parameters.Add("@IneIdentificadorCiudadano", Ine.IneIdentificadorCiudadano);
            _parameters.Add("@AnioDeRegistro", Ine.AnioDeRegistro);
            _parameters.Add("@AnioDeEmision", Ine.AnioDeEmision);
            _parameters.Add("@FechaDeConsulta", Ine.FechaDeConsulta);
            _parameters.Add("@FechaDeActualizacionDeInformacion", Ine.FechaDeActualizacionDeInformacion);
            _parameters.Add("@FechaDeVigencia", Ine.FechaDeVigencia);
            _parameters.Add("@Respuesta", Ine.Respuesta);
            return _parameters;
        }

        public static string IneAddQry()
        {
            _qry = @"INSERT INTO IneValidacion 
                    (Guid, Fecha_Registro, Activo, Ine_Estatus_Id
                    , Ine_Modelo, Ine_Clave_Elector, Ine_Numero_Emision, Ine_Ocr, Ine_Cic, Ine_Identificador_Ciudadano, Ine_Anio_Registro, Ine_Anio_Emision, Ine_Fecha_Consulta, Ine_Fecha_Actualizacion_Informacion, Ine_Fecha_Vigencia, Ine_Respuesta) 
                    VALUES (@Guid, @FechaRegistro, @Activo, @IneEstatusId
                    , @Modelo, @IneClaveDeElector, @IneNumeroDeEmision, @IneOcr, @IneCic, @IneIdentificadorCiudadano, @AnioDeRegistro, @AnioDeEmision, @FechaDeConsulta, @FechaDeActualizacionDeInformacion, @FechaDeVigencia, @Respuesta); 
                    SELECT currval('IneValidacion_Ine_Validacion_Id_seq'); 
            ";
            return _qry;
        }

    }
}
