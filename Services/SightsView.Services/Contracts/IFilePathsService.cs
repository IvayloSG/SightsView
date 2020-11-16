namespace SightsView.Services.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFilePathsService
    {
        string CreateFilePath(string username, string creationId, string title, IFormFile file);

        Task<string> GetFileSystemUrlAsync(string filePath, IFormFile file);
    }
}
