using Microsoft.AspNetCore.Mvc;
using minicore_comiciones.Dtos;
using minicore_comiciones.Services;

namespace minicore_comiciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComisionesController : ControllerBase
    {
       private readonly IComisionService _svc;
        public ComisionesController(IComisionService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<List<ComisionDto>>> Get(
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var res = await _svc.CalcularAsync(desde, hasta);
            return Ok(res);
        }
    }

}
