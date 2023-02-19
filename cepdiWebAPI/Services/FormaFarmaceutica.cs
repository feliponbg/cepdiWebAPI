using AutoMapper;
using cepdiWebAPI.Services.Utilerias;
using System.Data;
using System.Linq;

namespace cepdiWebAPI.Services
{
    public class FormaFarmaceutica
    {
        private readonly ILogger<FormaFarmaceutica> objLogger;
        private readonly IConfiguration objConfiguracion;
        private readonly TxtCsv servBD;
        private readonly IMapper objMapper;

        public FormaFarmaceutica(ILogger<FormaFarmaceutica> logger, IConfiguration configuration, Services.Utilerias.TxtCsv servBD, IMapper mapper)
        {
            this.objLogger = logger;
            this.objConfiguracion = configuration;
            this.servBD = servBD;
            this.objMapper = mapper;
        }

        public async Task<IList<Models.FormaFarmaceutica>> ObtenerTodos()
        {

            IList<Models.FormaFarmaceutica> listaRespuesta = null;

            //leer el archivo de texto haciendolo pasar por excel
            try
            {
                var dt = servBD.LeerFormasFarmaceuticas();

                listaRespuesta = (List<Models.FormaFarmaceutica>)(from m in dt.AsEnumerable()
                                                                  where m.Field<bool>("BHABILITADO") == true
                                                                  select new Models.FormaFarmaceutica
                                                                  {
                                                                      IIDFORMAFARMACEUTICA = m.Field<long>("IIDFORMAFARMACEUTICA"),
                                                                      NOMBRE = m.Field<string>("NOMBRE"),
                                                                      BHABILITADO = m.Field<bool>("BHABILITADO")
                                                                  }).AsEnumerable().ToList();

                if (!listaRespuesta.Any())
                    return listaRespuesta;
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return listaRespuesta;
            }

            listaRespuesta = listaRespuesta.OrderBy(l => l.IIDFORMAFARMACEUTICA).ToList();

            return listaRespuesta;
        }

    }
}
