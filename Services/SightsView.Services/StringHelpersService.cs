namespace SightsView.Services
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using SightsView.Common;
    using SightsView.Services.Contracts;

    public class StringHelpersService : IStringHelpersService
    {
        public string CreateFilePath(string username, string creationId, string creationTitle, IFormFile file)
        {
            var path = GlobalConstants.FileSystemPath;

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
                    var buffer = new byte[GlobalConstants.MemoryStreamBufferSize];
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

        public string GetEmailContent(string receiverUserName, string sendrUserName, string messageContent)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<p1>Dear {receiverUserName},</p1>");
            sb.AppendLine("<br />");
            sb.AppendLine($"<p1>You have recieved a new message from {sendrUserName}:<p1>");
            sb.AppendLine("<br />");
            sb.AppendLine($"<p1>{messageContent}</p1>");

            return sb.ToString();
        }
    }
}
