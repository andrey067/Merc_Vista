using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Commands
{
    public sealed record class UploadFileCommand(IFormFile File) : ICommand<List<Acao>>;
}
