using Microsoft.AspNetCore.Http;

namespace Merc_Vista.Tests.Fixtures
{
    public class FormFileFixture: IFormFile
    {
        public FormFileFixture(string fileName, string contentType, byte[] content)
        {
            Name = fileName;
            FileName = fileName;
            ContentType = contentType;
            Content = content;
        }

        public string ContentType { get; }
        public string ContentDisposition { get; }
        public IHeaderDictionary Headers { get; } = new HeaderDictionary();
        public long Length => Content?.LongLength ?? 0;
        public string Name { get; }
        public string FileName { get; }
        public byte[] Content { get; }
        public void CopyTo(Stream target) => throw new NotImplementedException();
        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Stream OpenReadStream() => new MemoryStream(Content);
    }
}
