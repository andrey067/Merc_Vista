using System.Xml.Linq;

namespace Merc_Vista_Blazor.Models
{
    public record class TituloNome(string Ativo)
    {
        public override string ToString() => Ativo;
    }
}
