namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Creations;
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

            var creationPath = this.filePathService.CreateFilePath(username, creation.Id, creation.Title, inputCreation);
            creation.StorageAddress = creationPath;

            var creationDataUrl = await this.filePathService.GetFileSystemUrlAsync(creationPath, inputCreation);
            creation.CreationDataUrl = creationDataUrl;

            foreach (var tag in tags)
            {
                var currenTag = new TagCreation()
                {
                    CreationId = creation.Id,
                    TagId = tag.Id,
                };

                creation.Tags.Add(currenTag);
            }

            await this.creationsRepository.AddAsync(creation);
            try
            {
                await this.creationsRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return creation.Id;
        }

        public async Task<IEnumerable<CreationsViewModel>> GetNumberRandomCreationsAsync(int countOfCreations)
        {
            return await this.creationsRepository.AllAsNoTracking()
                 .Where(x => x.IsPrivate == false)
                 .OrderBy(r => Guid.NewGuid()).Take(countOfCreations)
                 .Select(x => new CreationsViewModel()
                 {
                     DataUrl = x.CreationDataUrl,
                 })
                 .ToListAsync();
        }
    }
}
