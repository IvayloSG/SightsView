namespace SightsView.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;

    public class DetailsService : IDetailsService
    {
        private readonly IRepository<Details> detailsRepository;

        public DetailsService(IRepository<Details> detailsRepository)
        {
            this.detailsRepository = detailsRepository;
        }

        public async Task<int> AddDetailsAsync(string apereture, string shutterSpeed, string iso, string notes)
        {
            var details = new Details()
            {
                Apereture = apereture,
                ShutterSpeed = shutterSpeed,
                ISO = iso,
                TipAndTricks = notes,
            };

            await this.detailsRepository.AddAsync(details);
            await this.detailsRepository.SaveChangesAsync();

            return details.Id;
        }

        public async Task<T> GetDetailsByCreationIdAsync<T>(string creationId)
            => await this.detailsRepository.AllAsNoTracking()
               .Where(x => x.Creations.Select(c => c.Id == creationId).FirstOrDefault())
               .To<T>()
               .FirstOrDefaultAsync();

        public async Task<bool> UpdateDetailsAsync(int? id, string apereture, string shutterSpeed, string iso, string notes)
        {
            var details = await this.detailsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (details == null)
            {
                throw new NullReferenceException();
            }

            if (apereture != null)
            {
                details.Apereture = apereture;
            }

            if (shutterSpeed != null)
            {
                details.ShutterSpeed = shutterSpeed;
            }

            if (iso != null)
            {
                details.ISO = iso;
            }

            if (notes != null)
            {
                details.TipAndTricks = notes;
            }

            this.detailsRepository.Update(details);
            await this.detailsRepository.SaveChangesAsync();

            return true;
        }
    }
}
