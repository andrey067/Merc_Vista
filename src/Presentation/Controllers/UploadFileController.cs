using Application.Commands;
using Domain;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController: BaseController
    {
        public UploadFileController(ISender sender) : base(sender) { }

        [HttpPost]
        [Route("upload-csv-diretory")]
        [SwaggerOperation(Summary = "Faz upload de arquivos CSV de um diretório")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadCSVDiretory
        (
          [FromQuery(Name = "folderPath")]
          [SwaggerParameter("O caminho do diretório que contém os arquivos CSV a serem carregados.")]
          string folderPath)
        {
            var result = await Sender.Send(new UploadFilesDiretoryCommand(folderPath));

            return result.IsSuccess ? Ok(result.IsSuccess) : BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("upload-csv-file-stream")]
        [SwaggerOperation(Summary = "Faz upload de um arquivo CSV")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UploadCSVFileStream
        (
          IFormFile file)
        {
            var result = await Sender.Send(new UploadFileCommand(file));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("upload-zip-file-stream")]
        [SwaggerOperation(Summary = "Faz upload de um arquivo CSV")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result<Acao>>> UploadCSVFileStreamZip
        (
          IFormFile file)
        {
            var result = await Sender.Send(new UploadFileCommand(file));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }
    }
}