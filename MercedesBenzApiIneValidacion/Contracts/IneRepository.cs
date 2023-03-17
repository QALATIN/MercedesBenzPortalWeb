using Dapper;
using MercedesBenzDBContext;
using MercedesBenzLibrary;
using MercedesBenzModel;
using Newtonsoft.Json;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiIneValidacion.Contracts
{
    public class IneRepository : IIneRepository
    {
        private readonly ApplicationDBContext _context;
        private string _mensaje = "";
        private string _qry = "";
        private DynamicParameters _parameters;

        public IneRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPostAsync(IneValidacionRequest Request)
        {
            _mensaje = "Error en Ine-AddPostAsync";
            bool resultado = false;
            try
            {
                Request.FechaRegistro = DateTime.Now;
                Request.IneEstatusId = Validaciones.CalcularIneEstatus(Request.Respuesta);

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();

                    _parameters = SqlHelper.IneParametros(Request);
                    _qry = SqlHelper.QryDesactivarGuidAnteriorIneValidacion(Request.Guid);
                    _qry += SqlHelper.IneAddQry();
                    int id = await connection.QuerySingleAsync<int>(_qry, _parameters);
                    if (id > 0)
                    {
                        Request.IneValidacionId = id;
                        resultado = true;
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(_mensaje, ex);
            }
        }
        public async Task<bool> AddPostAsync2(IneValidacionRequest Request)
        {
            _mensaje = "Error en Ine-AddPostAsync";
            //IneValidacion data = null;
            bool resultado = false;
            try
            {
                Request.FechaRegistro = DateTime.Now;
                Request.IneEstatusId = 1;   // Calcular 
                Request.IneEstatusId = Validaciones.CalcularIneEstatus(Request.Respuesta);

                IneValidacion entity = new()
                //{
                //    inevalidacionid = Request.IneValidacionId
                //    , guid = Request.Guid
                //    , fecharegistro = Request.FechaRegistro
                //    , activo = Request.Activo
                //    , ineestatusid = Request.IneEstatusId
                //    , modelo = Request.Modelo
                //    , ineclavedeelector = Request.IneClaveDeElector
                //    , inenumerodeemision = Request.IneNumeroDeEmision
                //    , ineocr = Request.IneOcr
                //    , inecic = Request.IneCic
                //    , ineidentificadorciudadano = Request.IneIdentificadorCiudadano
                //    , anioderegistro = Request.AnioDeRegistro
                //    , aniodeemision = Request.AnioDeEmision
                //    , fechadeconsulta = Request.FechaDeConsulta
                //    , fechadeactualizaciondeinformacion = Request.FechaDeActualizacionDeInformacion
                //    , fechadevigencia = Request.FechaDeVigencia
                //    , respuesta = Request.Respuesta
                //}
                ;
                string ine = JsonConvert.SerializeObject(entity);
                //var entity2 = new List<IneValidacion> { new()
                //{
                //    inevalidacionid = Request.IneValidacionId
                //    , guid = Request.Guid
                //    , fecharegistro = Request.FechaRegistro
                //    , activo = Request.Activo
                //    , ineestatusid = Request.IneEstatusId
                //    , modelo = Request.Modelo
                //    , ineclavedeelector = Request.IneClaveDeElector
                //    , inenumerodeemision = Request.IneNumeroDeEmision
                //    , ineocr = Request.IneOcr
                //    , inecic = Request.IneCic
                //    , ineidentificadorciudadano = Request.IneIdentificadorCiudadano
                //    , anioderegistro = Request.AnioDeRegistro
                //    , aniodeemision = Request.AnioDeEmision
                //    , fechadeconsulta = Request.FechaDeConsulta
                //    , fechadeactualizaciondeinformacion = Request.FechaDeActualizacionDeInformacion
                //    , fechadevigencia = Request.FechaDeVigencia
                //    , respuesta = Request.Respuesta
                //}
                //};


                using (var connection = _context.CreateConnection())
                {
                    connection.Open();

                    _parameters = new DynamicParameters();
                    //_parameters.Add("@ine", Request);
                    _parameters.Add("@ine", ine, DbType.String);

                    //_qry = "public.SP_IneValidacionAdd";
                    //_qry = "Call SP_IneValidacionAdd(@Ine) ";
                    //_qry = $"SELECT * FROM public.SP_IneValidacionAdd(ine:='{ine}') ";
                    _qry = "SELECT * FROM public.sp_inevalidacionadd({@ine}::T_IneValidacion); ";
                    int id = await connection.QuerySingleAsync<int>(_qry, new { ine });
//                    int id = await connection.QuerySingleAsync<int>(_qry, _parameters);
                    int id2 = await connection.QuerySingleAsync<int>(_qry, new { ine }, commandType: System.Data.CommandType.StoredProcedure);
                    if (id > 0)
                    {
                        Request.IneValidacionId = id;
                        resultado = true;
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(_mensaje, ex);
            }
        }
    }
}
