namespace SightsView.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IDetailsService
    {
        Task<T> GetDetailsByCreationIdAsync<T>(string creationId);

        Task<int> AddDetailsAsync(string apereture, string shutterSpeed, string iso, string notes);

        Task<bool> UpdateDetailsAsync(int? id, string apereture, string shutterSpeed, string iso, string notes);
    }
}
