using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Commands
{
    public sealed record class UploadFileCommand(IFormFile File) : ICommand;
}
