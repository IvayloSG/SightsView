namespace SightsView.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;

    public class EquipmentsService : IEquipmentsService
    {
        private readonly IDeletableEntityRepository<Equipment> equipmentRepository;

        public EquipmentsService(IDeletableEntityRepository<Equipment> equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        public async Task<int> AddEquipmentAsync(string brand, string model, string accessoaries, string notes)
        {
            var equipment = new Equipment()
            {
                Brand = brand,
                Model = model,
                Accessoaries = accessoaries,
                Notes = notes,
            };

            await this.equipmentRepository.AddAsync(equipment);
            await this.equipmentRepository.SaveChangesAsync();

            return equipment.Id;
        }

        public async Task<bool> UpdateEquipmentAsync(int? id, string brand, string model, string accessoaries, string notes)
        {
            try
            {
                var equipment = await this.equipmentRepository.All()
              .FirstOrDefaultAsync(x => x.Id == id);

                if (equipment == null)
                {
                    throw new NullReferenceException();
                }

                if (brand != null)
                {
                    equipment.Brand = brand;
                }

                if (model != null)
                {
                    equipment.Model = model;
                }

                if (accessoaries != null)
                {
                    equipment.Accessoaries = accessoaries;
                }

                if (notes != null)
                {
                    equipment.Notes = notes;
                }

                this.equipmentRepository.Update(equipment);
                await this.equipmentRepository.SaveChangesAsync();

                return true;
            }
            catch (System.Exception e)
            {
                var message = e.Message;
                throw;
            }
        }
    }
}
