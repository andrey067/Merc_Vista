using Domain;
using Domain.Dtos;
using Domain.Enums;
using Infrastructure.Extensions;
using Mapster;

namespace Infrastructure
{
    public class MapsterConfiguration: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CsvDto, Acao>()
                .Map(dest => dest.CodidoAcao, src => ConverterAtivo(src.Ativo))
                .Map(dest => dest.EmpresaNome, src => ConverterEmpresaNome(src.Ativo));
        }

        private string ConverterAtivo(string ativo)
        {
            if (Enum.TryParse<B3Companies>(ativo, out var b3Companies))
                return b3Companies.ToString();
            else
                return ativo;
        }

        private string ConverterEmpresaNome(string ativo)
        {
            if (Enum.TryParse<B3Companies>(ativo, out var b3Companies))
                return b3Companies.GetDescription();
            else
                return string.Empty;
        }
    }
}