using Domain;
using Domain.Dtos;
using Domain.Enums;
using Infrastructure.Extensions;
using Mapster;

namespace Infrastructure
{
    public class MapsterConfiguration
    {
        public void ConfigureMaspter()
        {
            TypeAdapterConfig<CsvDto, Acao>.NewConfig()
                .Map(dest => dest.CodidoAcao, src => Enum.Parse<B3Companies>(src.Ativo))
                .Map(dest => dest.EmpresaNome, src => Enum.Parse<B3Companies>(src.Ativo).GetDescription());
        }
    }
}