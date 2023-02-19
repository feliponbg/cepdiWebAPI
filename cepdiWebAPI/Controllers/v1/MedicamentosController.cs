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


        
        // GET: api/v1/<MedicamentosController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] int? pageSize = 10, int? pageNumber = 1, string? nombre = null, string? presentacion = null, string? concentracion = null)
        {
            try
            {
                (IList<Models.Medicamento> listaRespuesta, int totalRegistros) resultado = await servMedicamento.ObtenerParametrizado(pageSize: (int)pageSize, pageNumber: (int)pageNumber, nombre: nombre, presentacion: presentacion, concentracion: concentracion);

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

        
        // GET api/v1/<MedicamentosController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                Models.Medicamento objRespuesta = await servMedicamento.ObtenerPorId(id);

                if (objRespuesta == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return Ok(objRespuesta);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        // POST api/v1/<MedicamentosController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Models.ViewModels.Medicamento objJson)
        {

            try
            {
                Models.Medicamento objRespuesta = await servMedicamento.Crear(objJson);

                if (objRespuesta == null)
                    return StatusCode(StatusCodes.Status400BadRequest);
                else
                    return StatusCode(StatusCodes.Status201Created, objRespuesta);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        // PUT api/v1/<MedicamentosController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(Models.ViewModels.Medicamento objJson)
        {
            try
            {
                Models.Medicamento objRespuesta = await servMedicamento.Modificar(objJson);

                if (objRespuesta == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return Ok(objRespuesta);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // DELETE api/v1/<MedicamentosController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Models.Medicamento objRespuesta = await servMedicamento.Eliminar(id: id);

                if (objRespuesta == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return Ok(objRespuesta);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}
