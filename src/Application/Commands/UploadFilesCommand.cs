using Application.Interfaces;
using Domain;

namespace Application.Commands
{
    public sealed record class UploadFilesCommand(string path) : ICommand<List<Acao>>;
}
