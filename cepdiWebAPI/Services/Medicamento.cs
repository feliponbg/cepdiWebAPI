using AutoMapper;
using cepdiWebAPI.Services.Utilerias;
using System.Data;
using System.Linq;

namespace cepdiWebAPI.Services
{
    public class Medicamento
    {

        private readonly ILogger<Medicamento> objLogger;
        private readonly IConfiguration objConfiguracion;
        private readonly Excel servExcel;
        private readonly IMapper objMapper;

        public Medicamento(ILogger<Medicamento> logger, IConfiguration configuration, Services.Utilerias.Excel servExcel, IMapper mapper)
        {
            this.objLogger = logger;
            this.objConfiguracion = configuration;
            this.servExcel = servExcel;
            this.objMapper = mapper;
        }

        public async Task<(IList<Models.Medicamento>, int)> ObtenerParametrizado(int pageSize, int pageNumber, string? nombre = null, string? presentacion = null, string? concentracion = null)
        {

            IList<Models.Medicamento> listaRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            //System.Data.DataRow[]? resultado;
            EnumerableRowCollection<DataRow> resultado = null;
            try
            {
                //Revisar si existe el usuario:
                var dt = servExcel.LeerMedicamentos();

                /*string where = string.Empty;
                where = !string.IsNullOrEmpty(nombre) ? $"NOMBRE like '%{nombre}%'" : string.Empty;
                where += !string.IsNullOrEmpty(presentacion) && !string.IsNullOrEmpty(where) ? $" or PRESENTACION like'%{presentacion}%'" : string.Empty;
                where = !string.IsNullOrEmpty(presentacion) && string.IsNullOrEmpty(where) ? $"PRESENTACION like'%{presentacion}%'" : where;
                //if (!string.IsNullOrEmpty(presentacion))
                //{
                //    if (!string.IsNullOrEmpty(where))
                //        where += $" or PRESENTACION like'%{presentacion}%'";
                //    else
                //        where = $"PRESENTACION like'%{presentacion}%'";
                //}
                where += !string.IsNullOrEmpty(concentracion) && !string.IsNullOrEmpty(where) ? $" or CONCENTRACION like'%{concentracion}%'" : string.Empty;
                where = !string.IsNullOrEmpty(concentracion) && string.IsNullOrEmpty(where) ? $"CONCENTRACION like'%{concentracion}%'" : where;
                //where = $"NOMBRE like '%{nombre}%' or PRESENTACION like'%{presentacion}%' or CONCENTRACION like '%{concentracion}%'";
                resultado = dt.Select(where);*/

                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(presentacion) && !string.IsNullOrEmpty(concentracion))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.ToUpper())
                                  || m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.ToUpper())
                                  || m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.ToUpper())
                                select m;
                } 
                else if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(presentacion))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.ToUpper())
                                  || m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.ToUpper())
                                select m;
                }
                else if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(concentracion))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.ToUpper())
                                  || m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.ToUpper())
                                select m;
                }
                else if (!string.IsNullOrEmpty(presentacion) && !string.IsNullOrEmpty(concentracion))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.ToUpper())
                                  || m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.ToUpper())
                                select m;
                }
                else if (!string.IsNullOrEmpty(nombre))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.ToUpper())
                                select m;
                }
                else if (!string.IsNullOrEmpty(presentacion))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.ToUpper())
                                select m;
                }
                else if (!string.IsNullOrEmpty(concentracion))
                {
                    resultado = from m in dt.AsEnumerable()
                                where m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.ToUpper())
                                select m;
                }

                //if (resultado.Length == 0)
                if (!resultado.Any())
                    return (listaRespuesta, -1);
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return (listaRespuesta, -1);
            }

            listaRespuesta = new List<Models.Medicamento>();
            foreach (var item in resultado)
                listaRespuesta.Add(new Models.Medicamento()
                {
                    IIDMEDICAMENTO = Convert.ToInt32(item["IIDMEDICAMENTO"].ToString()),
                    NOMBRE = item["NOMBRE"].ToString(),
                    CONCENTRACION = item["CONCENTRACION"].ToString(),
                    IIDFORMAFARMACEUTICA = Convert.ToInt32(item["IIDFORMAFARMACEUTICA"].ToString()),
                    PRECIO = Convert.ToSingle(item["PRECIO"].ToString()),
                    STOCK = Convert.ToInt32(item["STOCK"].ToString()),
                    PRESENTACION = item["PRESENTACION"].ToString(),
                    BHABILITADO = Convert.ToBoolean(item["BHABILITADO"].ToString())
                });

            listaRespuesta = listaRespuesta.OrderBy(l => l.IIDMEDICAMENTO).ToList<Models.Medicamento>();

            return (listaRespuesta.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), listaRespuesta.Count);
        }

        public async Task<Models.Medicamento> ObtenerPorId(int id)
        {

            Models.Medicamento objRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            EnumerableRowCollection<DataRow> resultado = null;
            try
            {
                //Revisar si existe el usuario:
                var dt = servExcel.LeerMedicamentos();

                resultado = from m in dt.AsEnumerable()
                            where m.Field<int>("IIDMEDICAMENTO") == id
                            select m;

                //if (resultado.Length == 0)
                if (!resultado.Any())
                    return objRespuesta;
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return objRespuesta;
            }

            foreach (var item in resultado)
            {
                objRespuesta = new Models.Medicamento()
                {
                    IIDMEDICAMENTO = Convert.ToInt32(item["IIDMEDICAMENTO"].ToString()),
                    NOMBRE = item["NOMBRE"].ToString(),
                    CONCENTRACION = item["CONCENTRACION"].ToString(),
                    IIDFORMAFARMACEUTICA = Convert.ToInt32(item["IIDFORMAFARMACEUTICA"].ToString()),
                    PRECIO = Convert.ToSingle(item["PRECIO"].ToString()),
                    STOCK = Convert.ToInt32(item["STOCK"].ToString()),
                    PRESENTACION = item["PRESENTACION"].ToString(),
                    BHABILITADO = Convert.ToBoolean(item["BHABILITADO"].ToString())
                };
            }

            return objRespuesta;
        }

        public async Task<Models.Medicamento> Crear(Models.ViewModels.Medicamento objJson)
        {
            Models.Medicamento objBusqueda = await ObtenerPorIdMaxMin(desc: true);

            objJson.IIDMEDICAMENTO = objBusqueda.IIDMEDICAMENTO + 1;



            var objResultado = objMapper.Map<Models.ViewModels.Medicamento, Models.Medicamento>(objJson);
            return objResultado;
        }

        public async Task<Models.Medicamento> Actualizar(Models.ViewModels.Medicamento objJson)
        {
            Models.Medicamento objBusqueda = await ObtenerPorId(objJson.IIDFORMAFARMACEUTICA);
            return objBusqueda;
        }

        public async Task<Models.Medicamento> ObtenerPorIdMaxMin(bool? desc = null)
        {

            List<Models.Medicamento> listaRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            EnumerableRowCollection<DataRow> resultado = null;
            try
            {
                //Revisar si existe el usuario:
                var dt = servExcel.LeerMedicamentos();

                resultado = from m in dt.AsEnumerable()
                            select m;

                //if (resultado.Length == 0)
                if (!resultado.Any())
                    return null;
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return null;
            }

            listaRespuesta = new List<Models.Medicamento>();
            foreach (var item in resultado)
                listaRespuesta.Add(new Models.Medicamento()
                {
                    IIDMEDICAMENTO = Convert.ToInt32(item["IIDMEDICAMENTO"].ToString()),
                    NOMBRE = item["NOMBRE"].ToString(),
                    CONCENTRACION = item["CONCENTRACION"].ToString(),
                    IIDFORMAFARMACEUTICA = Convert.ToInt32(item["IIDFORMAFARMACEUTICA"].ToString()),
                    PRECIO = Convert.ToSingle(item["PRECIO"].ToString()),
                    STOCK = Convert.ToInt32(item["STOCK"].ToString()),
                    PRESENTACION = item["PRESENTACION"].ToString(),
                    BHABILITADO = Convert.ToBoolean(item["BHABILITADO"].ToString())
                });

            if (listaRespuesta.Count > 1)
            {
                if (desc != null && (bool)desc)
                    listaRespuesta = listaRespuesta.OrderByDescending(l => l.IIDMEDICAMENTO).ToList<Models.Medicamento>();
                else
                    listaRespuesta = listaRespuesta.OrderBy(l => l.IIDMEDICAMENTO).ToList<Models.Medicamento>();
            }

            return listaRespuesta[0];
        }

    }
}
