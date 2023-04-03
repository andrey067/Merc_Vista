using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CarregarArquivosController : BaseController
    {
        public CarregarArquivosController(ISender sender) : base(sender) { }

        [HttpPost]
        [Route("upload")]
        [SwaggerOperation(Summary = "Faz upload de arquivos CSV de um diretório")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadCSV
        (
          [FromQuery(Name = "folderPath")]
          [SwaggerParameter("O caminho do diretório que contém os arquivos CSV a serem carregados.")]
        string folderPath, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new UploadFilesCommand(folderPath), cancellationToken);


            return result.Succeeded ? Ok(result.Data) : BadRequest(result.Errors);
        }
    }
}