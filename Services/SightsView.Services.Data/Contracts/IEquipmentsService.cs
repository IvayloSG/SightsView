namespace SightsView.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IEquipmentsService
    {
        Task<int> AddEquipmentAsync(string brand, string model, string accessoaries, string notes);

        Task<bool> UpdateEquipmentAsync(int? id, string brand, string model, string accessoaries, string notes);
    }
}
