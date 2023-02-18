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

        public Medicamento(ILogger<Medicamento> logger, IConfiguration configuration, Services.Utilerias.Excel servExcel)
        {
            this.objLogger = logger;
            this.objConfiguracion = configuration;
            this.servExcel = servExcel;
        }

        public async Task<IList<Models.Medicamento>> GetParametrizado(string? nombre = null, string? presentacion = null, string? concentracion = null)
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
                    return listaRespuesta;
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return listaRespuesta;
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

            return listaRespuesta;
        }
    }
}
