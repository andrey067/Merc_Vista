using Domain.Shared;

namespace Merc_Vista.Tests.Fixtures
{
    public class ErrorFixture
    {
        public Error NotFoundDiretory()
          => new Error("Caminho não encontrato", "Fornecessa um diretorio valido");
    }
}
