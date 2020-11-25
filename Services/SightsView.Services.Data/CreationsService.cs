namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
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
            IEnumerable<TagsViewModel> tags,
            Cloudinary cloudinary)
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

            var extension = Path.GetExtension(inputCreation.FileName);
            var creationName = creation.Id + "_" + creation.Title + extension;

            var creationCloudUrl = await CloudinaryService.UploadToCloudAsync(cloudinary, inputCreation, creationName, username);
            creation.StorageAddress = creationCloudUrl;
            creation.CreationDataUrl = creationCloudUrl;

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

        public async Task<CreationsViewModel> GetCreationByIdAsync(string creationId, string userId)
        {
            var creation = await this.creationsRepository.AllAsNoTracking()
                .Where(x => x.IsPrivate == false && x.Id == creationId)
                .Select(x => new CreationsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    DataUrl = x.CreationDataUrl,
                    Likes = x.Likes.Count,
                    Views = x.Views,
                    CreatorId = x.CreatorId,
                    CreatorName = x.Creator.UserName,
                })
                .FirstOrDefaultAsync();

            if (creation.CreatorId != userId)
            {
                await this.IncreseCreationViewsAsync(creationId);
                creation.Views++;
            }

            return creation;
        }

        public async Task<IEnumerable<CreationsViewModel>> GetNumberRandomCreationsAsync(int countOfCreations)
        {
            return await this.creationsRepository.AllAsNoTracking()
                 .Where(x => x.IsPrivate == false)
                 .OrderBy(r => Guid.NewGuid()).Take(countOfCreations)
                 .Select(x => new CreationsViewModel()
                 {
                     Id = x.Id,
                     DataUrl = x.CreationDataUrl,
                 })
                 .ToListAsync();
        }      

        private async Task IncreseCreationViewsAsync(string creationId)
        {
            var creation = await this.creationsRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == creationId);

            creation.Views++;

            this.creationsRepository.Update(creation);
            await this.creationsRepository.SaveChangesAsync();
        }
    }
}
