using Microsoft.AspNetCore.Mvc;
using Webapi.Datos;
using Webapi.Modelo;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("api/empleados")]
    public class empleadosController : Controller
    {

        [HttpGet]
        public async Task<ActionResult<List<Mempleado>>> Get()
        {
            var function = new Dempleados();
            var lista = await function.Mostrarempleados();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Mempleado parametros)
        {
            var funcion = new Dempleados();
            await funcion.insertarEmpleados(parametros);
            return Ok(funcion);
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Mempleado parametros)
        {
            var funcion = new Dempleados();
            await funcion.insertarEmpleados(parametros);
            return Ok(funcion);
        }

        [HttpPost]
        [Route("api/nomina")]
        public async Task<ActionResult> PostNomina([FromBody] Mempleado parametros)
        {
            var funcion = new Dempleados();
            await funcion.obtenerNomida(parametros);
            return Ok(funcion);
        }
    }
}
