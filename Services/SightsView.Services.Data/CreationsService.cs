namespace SightsView.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Tags;

    public class CreationsService : ICreationsService
    {
        private readonly IDeletableEntityRepository<Creation> creationsRepository;
        private readonly IRepository<TagCreation> tagCreationRepository;
        private readonly IFilePathsService filePathService;

        public CreationsService(IDeletableEntityRepository<Creation> creationsRepository, IRepository<TagCreation> tagCreationRepository, IFilePathsService filePathService)
        {
            this.creationsRepository = creationsRepository;
            this.tagCreationRepository = tagCreationRepository;
            this.filePathService = filePathService;
        }

        public async Task<string> AddCreationInDbAsync(
            string title,
            string description,
            bool isPrivate,
            int? countryId,
            int categoryId,
            ApplicationUser user,
            IFormFile inputCreation,
            IEnumerable<TagsViewModel> tags)
        {
            var creation = new Creation()
            {
                Title = title,
                Description = description,
                IsPrivate = isPrivate,
                CountryId = countryId,
                CategoryId = categoryId,
                CreatorId = user.Id,
            };

            var username = user.UserName;

            var creationPath = this.filePathService.CreateFilePath(username, creation.Id, creation.Title,  inputCreation);
            creation.StorageAddress = creationPath;

            var creationDataUrl = await this.filePathService.GetFileSystemUrlAsync(creationPath, inputCreation);
            creation.CreationDataUrl = creationDataUrl;

            foreach (var tag in tags)
            {
                await this.tagCreationRepository
                    .AddAsync(new TagCreation()
                {
                    CreationId = creation.Id,
                    TagId = tag.Id,
                });
            }

            return creation.Id;
        }
    }
}
