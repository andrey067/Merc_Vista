using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Commands
{
    public sealed record class UploadZipFileCommand(IFormCollection RequestStream) : ICommand;
}
