namespace SightsView.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using SightsView.Web.ViewModels.Cloudinary;

    public static class CloudinaryService
    {
        public static async Task<CloudinaryUploadResponseModel> UploadToCloudAsync(Cloudinary cloudinary, IFormFile file, string fileName, string username)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);

            var imageUpload = new ImageUploadParams
            {
                File = new FileDescription(fileName, memoryStream),
                Folder = username,
            };

            var uploadResult = await cloudinary.UploadAsync(imageUpload);

            var response = new CloudinaryUploadResponseModel()
            {
                CreationDataUrl = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                FullPublicId = uploadResult.FullyQualifiedPublicId,
            };
            return response;
        }

        public static async Task DeleteFileAsync(Cloudinary cloudinary, string publicId)
            => await cloudinary.DeleteResourcesAsync(publicId);
    }
}
