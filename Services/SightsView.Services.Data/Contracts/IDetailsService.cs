namespace SightsView.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IDetailsService
    {
        Task<T> GetDetailsByCreationId<T>(string creationId);
    }
}
