namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;

    public class CreationsService : ICreationsService
    {
        private IDeletableEntityRepository<Creation> creationsRepository;

        public CreationsService(IDeletableEntityRepository<Creation> creationsRepository)
        {
            this.creationsRepository = creationsRepository;
        }

        public string AddCreationInDbAsync(string title, string description, bool isPublic, int countryId, int categoryId, string userId, List<string> tags)
        {
            throw new NotImplementedException();
        }
    }
}
