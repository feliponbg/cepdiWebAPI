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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
