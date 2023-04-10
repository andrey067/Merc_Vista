using Application.Interfaces;
using Domain;

namespace Application.Commands
{
    public sealed record class UploadFilesDiretoryCommand(string path) : ICommand;
}
