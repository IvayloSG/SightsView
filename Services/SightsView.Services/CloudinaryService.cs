namespace SightsView.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public static class CloudinaryService
    {
        public static async Task<string> UploadToCloudAsync(Cloudinary cloudinary, IFormFile file, string fileName, string username)
        {
            // TODO: Remove hardcoded buffersize
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);

            var imageUpload = new ImageUploadParams
            {
                File = new FileDescription(fileName, memoryStream),
                Folder = username,
            };

            var uploadResult = await cloudinary.UploadAsync(imageUpload);
            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
