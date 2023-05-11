using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface ICsvService
    {
        Task<List<CsvDto>> ReadFormFile(IFormFile file);
        Task<List<CsvDto>> ReadFormFileAync(string[] file);
        Task<List<CsvDto>> ReadFileAsync(string streamReader);
    }
}
