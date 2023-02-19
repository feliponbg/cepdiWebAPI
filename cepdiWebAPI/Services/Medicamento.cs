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
        private readonly TxtCsv servBD;
        private readonly IMapper objMapper;

        public Medicamento(ILogger<Medicamento> logger, IConfiguration configuration, Services.Utilerias.TxtCsv servBD, IMapper mapper)
        {
            this.objLogger = logger;
            this.objConfiguracion = configuration;
            this.servBD = servBD;
            this.objMapper = mapper;
        }

        public async Task<(IList<Models.Medicamento>, int)> ObtenerParametrizado(int pageSize, int pageNumber, string? nombre = null, string? presentacion = null, string? concentracion = null)
        {

            IList<Models.Medicamento> listaRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            try
            {
                var dt = servBD.LeerMedicamentos();
                var dt2 = servBD.LeerFormasFarmaceuticas();

                //filtros like
                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(presentacion) && !string.IsNullOrEmpty(concentracion))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                  on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.Trim().ToUpper())
                                                                  || m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.Trim().ToUpper())
                                                                  || m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                } 
                else if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(presentacion))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.Trim().ToUpper())
                                                                    || m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }
                else if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(concentracion))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.Trim().ToUpper())
                                                                    || m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }
                else if (!string.IsNullOrEmpty(presentacion) && !string.IsNullOrEmpty(concentracion))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.Trim().ToUpper())
                                                                    || m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }
                else if (!string.IsNullOrEmpty(nombre))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("NOMBRE").ToUpper().Contains(nombre.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }
                else if (!string.IsNullOrEmpty(presentacion))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("PRESENTACION").ToUpper().Contains(presentacion.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }
                else if (!string.IsNullOrEmpty(concentracion))
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                where m.Field<string>("CONCENTRACION").ToUpper().Contains(concentracion.Trim().ToUpper())
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }
                else
                {
                    listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                                join ff in dt2.AsEnumerable()
                                                                   on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                                select new Models.Medicamento
                                                                {
                                                                    IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                    NOMBRE = m.Field<string>("NOMBRE"),
                                                                    CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                    IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                    PRECIO = m.Field<float>("PRECIO"),
                                                                    STOCK = m.Field<int>("STOCK"),
                                                                    PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                    BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                    NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                                }).AsEnumerable().ToList();
                }

                //filtros por igualdad
                /*listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                            join ff in dt2.AsEnumerable()
                                                              on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                            where m.Field<string>("NOMBRE").ToUpper() == (string.IsNullOrEmpty(nombre) ? m.Field<string>("NOMBRE").ToUpper() : nombre.ToUpper())
                                                              || m.Field<string>("PRESENTACION").ToUpper() == (string.IsNullOrEmpty(presentacion) ? m.Field<string>("PRESENTACION").ToUpper() : presentacion.ToUpper())
                                                              || m.Field<string>("CONCENTRACION").ToUpper() == (string.IsNullOrEmpty(concentracion) ? m.Field<string>("CONCENTRACION").ToUpper() : concentracion.ToUpper())
                                                            select new Models.Medicamento
                                                            {
                                                                IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                NOMBRE = m.Field<string>("NOMBRE"),
                                                                CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                PRECIO = m.Field<float>("PRECIO"),
                                                                STOCK = m.Field<int>("STOCK"),
                                                                PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                            }).AsEnumerable().ToList();*/

                //filtros por like
                /*listaRespuesta = (List<Models.Medicamento>)(from m in dt.AsEnumerable()
                                                            join ff in dt2.AsEnumerable()
                                                              on m.Field<long>("IIDFORMAFARMACEUTICA") equals ff.Field<long>("IIDFORMAFARMACEUTICA")
                                                            where m.Field<string>("NOMBRE").ToUpper().Contains((!string.IsNullOrEmpty(nombre.Trim()) ? nombre.Trim().ToUpper() : m.Field<string>("NOMBRE").ToUpper()))
                                                              || m.Field<string>("PRESENTACION").ToUpper().Contains((!string.IsNullOrEmpty(presentacion.Trim()) ? presentacion.Trim().ToUpper() : m.Field<string>("PRESENTACION").ToUpper()))
                                                              || m.Field<string>("CONCENTRACION").ToUpper().Contains((!string.IsNullOrEmpty(concentracion.Trim()) ? concentracion.Trim().ToUpper() : m.Field<string>("CONCENTRACION").ToUpper()))
                                                            select new Models.Medicamento
                                                            {
                                                                IIDMEDICAMENTO = m.Field<long>("IIDMEDICAMENTO"),
                                                                NOMBRE = m.Field<string>("NOMBRE"),
                                                                CONCENTRACION = m.Field<string>("CONCENTRACION"),
                                                                IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                PRECIO = m.Field<float>("PRECIO"),
                                                                STOCK = m.Field<int>("STOCK"),
                                                                PRESENTACION = m.Field<string>("PRESENTACION"),
                                                                BHABILITADO = m.Field<bool>("BHABILITADO"),
                                                                NOMBREFORMAFARMACEUTICA = ff.Field<string>("NOMBRE")
                                                            }).AsEnumerable().ToList();*/

                if (!listaRespuesta.Any())
                    return (listaRespuesta, -1);
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return (listaRespuesta, -1);
            }

            listaRespuesta = listaRespuesta.OrderBy(l => l.IIDMEDICAMENTO).ToList();

            return (listaRespuesta.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), listaRespuesta.Count);
        }

        public async Task<Models.Medicamento> ObtenerPorId(int id)
        {

            Models.Medicamento objRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            EnumerableRowCollection<DataRow> resultado = null;
            try
            {
                var dt = servBD.LeerMedicamentos();

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

            if (objBusqueda != null)
                objJson.IIDMEDICAMENTO = objBusqueda.IIDMEDICAMENTO + 1;
            else
                objJson.IIDMEDICAMENTO = 1;

            string registro = $"{objJson.IIDMEDICAMENTO}|{objJson.NOMBRE}|{objJson.CONCENTRACION}|{objJson.IIDFORMAFARMACEUTICA}|{objJson.PRECIO.ToString("0.00")}|{objJson.STOCK}|{objJson.PRESENTACION}|{(objJson.BHABILITADO ? 1 : 0).ToString()}";

            bool respuesta = await servBD.EscribirMedicamentos(new string[] { registro });

            if (!respuesta)
                return null;

            var objResultado = objMapper.Map<Models.ViewModels.Medicamento, Models.Medicamento>(objJson);
            return objResultado;
        }

        public async Task<Models.Medicamento> Modificar(Models.ViewModels.Medicamento objJson)
        {
            Models.Medicamento objBusqueda = await ObtenerPorId((int)objJson.IIDMEDICAMENTO);

            if (objBusqueda == null)
                return objBusqueda;

            string registroAnterior = $"{objBusqueda.IIDMEDICAMENTO}|{objBusqueda.NOMBRE}|{objBusqueda.CONCENTRACION}|{objBusqueda.IIDFORMAFARMACEUTICA}|{objBusqueda.PRECIO.ToString("0.00")}|{objBusqueda.STOCK}|{objBusqueda.PRESENTACION}|{(objBusqueda.BHABILITADO ? 1 : 0).ToString()}";
            string registroNuevo = $"{objJson.IIDMEDICAMENTO}|{objJson.NOMBRE}|{objJson.CONCENTRACION}|{objJson.IIDFORMAFARMACEUTICA}|{objJson.PRECIO.ToString("0.00")}|{objJson.STOCK}|{objJson.PRESENTACION}|{(objJson.BHABILITADO ? 1 : 0).ToString()}";

            bool respuesta = await servBD.ActualizarMedicamentos(registroAnterior, registroNuevo);

            if (!respuesta)
                return null;

            var objResultado = objMapper.Map<Models.ViewModels.Medicamento, Models.Medicamento>(objJson);
            return objResultado;
        }

        public async Task<Models.Medicamento> ObtenerPorIdMaxMin(bool? desc = null)
        {

            List<Models.Medicamento> listaRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            EnumerableRowCollection<DataRow> resultado = null;
            try
            {
                var dt = servBD.LeerMedicamentos();

                resultado = from m in dt.AsEnumerable()
                            select m;

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

        public async Task<Models.Medicamento> Eliminar(int id)
        {
            Models.Medicamento objBusqueda = await ObtenerPorId(id);

            if (objBusqueda == null)
                return objBusqueda;

            string registro = $"{objBusqueda.IIDMEDICAMENTO}|{objBusqueda.NOMBRE}|{objBusqueda.CONCENTRACION}|{objBusqueda.IIDFORMAFARMACEUTICA}|{objBusqueda.PRECIO.ToString("0.00")}|{objBusqueda.STOCK}|{objBusqueda.PRESENTACION}|{(objBusqueda.BHABILITADO ? 1 : 0).ToString()}";

            bool respuesta = await servBD.BorrarMedicamentos(registro);

            if (!respuesta)
                return null;

            return objBusqueda;
        }

    }
}
