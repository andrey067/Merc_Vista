using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtivosController: BaseController
    {
        public AtivosController(ISender sender) : base(sender) { }

        [HttpGet("calular-forca-relativa")]
        public async Task<IActionResult> GetAtivosSelecionados([FromQuery] string dataInicial,
                                                               [FromQuery] string dataFinal,
                                                               [FromQuery] string ativosSelecionados,
                                                               [FromQuery] int totalItensAmostrar)
        {
            var t1 = DateTime.Parse(dataInicial);
            var t2 = DateTime.Parse(dataFinal);

            var ativos = ativosSelecionados.Split(',').ToList();
            var result = await Sender.Send(new GetRelativeStrengthQuery(ativos,
                                                                        t1,
                                                                        t2,
                                                                        totalItensAmostrar != 0 ? totalItensAmostrar : default));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("obterNomeAtivos")]
        public async Task<IActionResult> ObterNomeDosAtivos([FromQuery] int rangeDate)
        {
            var result = await Sender.Send(new GetTitilesQuery(rangeDate));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }
    }
}
