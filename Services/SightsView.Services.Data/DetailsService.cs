namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public async Task<T> GetDetailsByCreationId<T>(string creationId)
            => await this.detailsRepository.AllAsNoTracking()
               .Where(x => x.Creations.Select(c => c.Id == creationId).FirstOrDefault())
               .To<T>()
               .FirstOrDefaultAsync();

    }
}
