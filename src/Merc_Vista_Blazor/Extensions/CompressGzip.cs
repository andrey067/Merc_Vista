using Microsoft.AspNetCore.Components.Forms;
using System.IO.Compression;

namespace Merc_Vista_Blazor.Extensions
{
    public class CompressGzip
    {
        public static async Task<byte[]> Compress(IBrowserFile file)
        {
            using (var msi = file.OpenReadStream())
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    await msi.CopyToAsync(gs);
                }
                return mso.ToArray();
            }
        }
    }
}
