using cepdiWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cepdiWebAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FormasFarmaceuticasController : ControllerBase
    {

        private readonly ILogger<FormasFarmaceuticasController> objLogger;
        private readonly FormaFarmaceutica servFormaFarmaceutica;

        public FormasFarmaceuticasController(ILogger<FormasFarmaceuticasController> logger, Services.FormaFarmaceutica servFormaFarmaceutica)
        {
            this.objLogger = logger;
            this.servFormaFarmaceutica = servFormaFarmaceutica;
        }

        // GET: api/v1/<FormasFarmaceuticasController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                IList<Models.FormaFarmaceutica> listaRespuesta = await servFormaFarmaceutica.ObtenerTodos();

                if (listaRespuesta == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return Ok(listaRespuesta);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /*
        // GET api/<FormasFarmaceuticasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FormasFarmaceuticasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FormasFarmaceuticasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FormasFarmaceuticasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
