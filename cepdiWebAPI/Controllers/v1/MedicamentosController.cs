using cepdiWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cepdiWebAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly ILogger<MedicamentosController> objLogger;
        private readonly Medicamento servMedicamento;

        //readonly Services.Medicamento servMedicamento;

        public MedicamentosController(ILogger<MedicamentosController> logger, Services.Medicamento servMedicamento) 
        { 
            this.objLogger = logger;
            this.servMedicamento = servMedicamento;
        }


        
        // GET: api/<MedicamentosController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] int? pageSize = 10, int? pageNumber = 1, string? nombre = null, string? presentacion = null, string? concentracion = null)
        {
            try
            {
                (IList<Models.Medicamento> listaRespuesta, int totalRegistros) resultado = await servMedicamento.GetParametrizado(pageSize: (int)pageSize, pageNumber: (int)pageNumber, nombre: nombre, presentacion: presentacion, concentracion: concentracion);

                if (resultado.listaRespuesta == null || resultado.listaRespuesta.Count == 0)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                {
                    Response.Headers.Add("PageSize", pageSize.ToString());
                    Response.Headers.Add("PageNumer", pageNumber.ToString());
                    Response.Headers.Add("TotalRecords", resultado.totalRegistros.ToString());
                    return Ok(resultado.listaRespuesta);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /*
        // GET api/<MedicamentosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MedicamentosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MedicamentosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicamentosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
