namespace SightsView.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using SightsView.Services.Contracts;

    public class FilePathsService : IFilePathsService
    {
        public string CreateFilePath(string username, string creationId, string creationTitle, IFormFile file)
        {
            // TODO: Move hard coded string to config
            var path = @"C:\SightsViewCreations";

            var directoryPath = Path.Combine(path, username);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var extension = Path.GetExtension(file.FileName);
            var filename = creationId + "_" + creationTitle + extension;
            var fullPath = Path.Combine(directoryPath, filename);

            return fullPath;
        }

        public async Task<string> GetFileSystemUrlAsync(string filePath, IFormFile file)
        {
            byte[] data = null;

            // TODO: Remove hardcoded buffersize
            using (var stream = file.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    var buffer = new byte[4 * 1024];
                    var bytesRead = 0;
                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await memoryStream.WriteAsync(buffer, 0, bytesRead);
                    }

                    data = memoryStream.ToArray();
                }
            }

            await File.WriteAllBytesAsync(filePath, data);

            string url = Convert.ToBase64String(data);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", url);

            return imageDataURL;
        }
    }
}
