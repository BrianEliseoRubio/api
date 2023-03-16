using Microsoft.AspNetCore.Mvc;
using Webapi.Datos;
using Webapi.Modelo;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class rolesController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<Mempleado>>> Get()
        {
            var function = new Drol();
            var lista = await function.Mostrarempleados();
            return Ok(lista);
        }
    }
}
