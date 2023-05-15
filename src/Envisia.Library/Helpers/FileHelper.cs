using Microsoft.AspNetCore.Http;

namespace Envisia.Library.Helpers
{
    public static class FileHelper
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            
            file.CopyTo(memoryStream);

            var fileBytes = memoryStream.ToArray();

            return fileBytes;
        }
    }
}
