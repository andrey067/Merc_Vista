using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace Infrastructure.Extensions
{
    public class CompressGzipExtension
    {
        public static async Task<string> Decompress(IFormFile file)
        {
            using (var compressedStream = file.OpenReadStream())
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var reader = new StreamReader(gzipStream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
