using cepdiWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cepdiWebAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SesionesController : ControllerBase
    {
        private readonly ILogger<SesionesController> objLogger;
        private readonly Sesion servSesion;

        public SesionesController(ILogger<SesionesController> logger, Services.Sesion servSesion) 
        {
            this.objLogger = logger;
            this.servSesion = servSesion;
        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] Models.ViewModels.Sesion objJson)
        {
            try
            {
                Models.Sesion objRespuesta = await servSesion.InciarSesion(objJson);

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

        [Authorize]
        [HttpGet("ValidarSesion")]
        //public bool ValidarToken()
        public IActionResult ValidarSesion()
        {
            return Ok();
        }

        /*
        // GET: api/<SesionesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SesionesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SesionesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SesionesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SesionesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
