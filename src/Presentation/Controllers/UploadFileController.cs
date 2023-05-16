using Application.Commands;
using Domain.Enums;
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
        public async Task<ActionResult> UploadCSVDiretory
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
        public async Task<ActionResult> UploadCSVFileStream(IFormFile file)
        {
            var result = await Sender.Send(new UploadFileCommand(file));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("upload-zip-file-stream")]
        [SwaggerOperation(Summary = "Faz upload de um arquivo zip")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result>> UploadCSVFileStreamZip()
        {
            if (!Request.HasFormContentType)
                return BadRequest(Result.Failure(new Error(EnumErro.ApplicationError.ToString(), "A requisição não contém o formulário de dados.")));

            var form = await Request.ReadFormAsync();

            if (form.Files == null || form.Files.Count == 0)
                return BadRequest(Result.Failure(new Error(EnumErro.ApplicationError.ToString(), "Nenhum arquivo foi enviado.")));

            var result = await Sender.Send(new UploadZipFileCommand(form));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }
    }
}
