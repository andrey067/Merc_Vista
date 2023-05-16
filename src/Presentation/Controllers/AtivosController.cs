﻿using Application.Query;
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
        public async Task<IActionResult> GetAtivosSelecionados([FromQuery] DateTime dataInicial,
                                                               [FromQuery] DateTime dataFinal,
                                                               [FromQuery] string ativosSelecionados,
                                                               [FromQuery] int totalItensAmostrar)
        {
            var ativos = ativosSelecionados.Split(',').ToList();
            var result = await Sender.Send(new GetRelativeStrengthQuery(ativos,
                                                                        dataInicial,
                                                                        dataFinal,
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
