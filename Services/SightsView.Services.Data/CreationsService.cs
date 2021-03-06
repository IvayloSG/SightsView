﻿namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using SightsView.Common;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Tags;

    public class CreationsService : ICreationsService
    {
        private readonly IDeletableEntityRepository<Creation> creationsRepository;
        private readonly Cloudinary cloudinary;

        public CreationsService(
            IDeletableEntityRepository<Creation> creationsRepository,
            Cloudinary cloudinary)
        {
            this.creationsRepository = creationsRepository;
            this.cloudinary = cloudinary;
        }

        public async Task<string> AddCreationAsync(
            string title,
            string description,
            bool isPrivate,
            int? countryId,
            int categoryId,
            ApplicationUser user,
            IFormFile inputCreation,
            IEnumerable<TagsViewModel> tags,
            bool isTest = false)
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
            creationName = creationName.Replace("&", "And");

            if (!isTest)
            {
                var cloudinaryUploadResponse = await CloudinaryService.UploadToCloudAsync(this.cloudinary, inputCreation, creationName, username);
                creation.StorageAddress = cloudinaryUploadResponse.CreationDataUrl;
                creation.CreationDataUrl = cloudinaryUploadResponse.CreationDataUrl;
                creation.CloudPublicId = cloudinaryUploadResponse.PublicId;
            }

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
            await this.creationsRepository.SaveChangesAsync();

            return creation.Id;
        }

        public async Task AddDetailsToCreationAsync(string creationId, int detailsId)
        {
            var creation = await this.creationsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == creationId);
            if (creation == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CommentNotFound, creationId));
            }

            creation.DetailsId = detailsId;

            this.creationsRepository.Update(creation);
            await this.creationsRepository.SaveChangesAsync();
        }

        public async Task AddEquipmentToCreationAsync(string creationId, int equipmentId)
        {
            var creation = await this.creationsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == creationId);

            creation.EquipmentId = equipmentId;

            this.creationsRepository.Update(creation);
            await this.creationsRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCreationAsync(string creationId, string userId)
        {
            var creation = await this.creationsRepository.All()
                 .FirstOrDefaultAsync(x => x.Id == creationId);

            if (creation == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CommentNotFound, creationId));
            }

            if (userId != creation.CreatorId)
            {
                return false;
            }

            await CloudinaryService.DeleteFileAsync(this.cloudinary, creation.CloudPublicId);

            this.creationsRepository.Delete(creation);
            await this.creationsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditCreationByIdAsync(string creationId, string title, string description, bool isPrivate, int? categoryId, int? countryId, string userId)
        {
            var creation = await this.creationsRepository.All()
                 .FirstOrDefaultAsync(x => x.Id == creationId);
            if (creation == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CreationNotFound, creationId));
            }

            if (creation.CreatorId != userId)
            {
                return false;
            }

            creation.Title = title;
            creation.Description = description;
            creation.IsPrivate = isPrivate;

            if (categoryId != null)
            {
                creation.CategoryId = (int)categoryId;
            }

            if (countryId != null)
            {
                creation.CountryId = (int)countryId;
            }

            this.creationsRepository.Update(creation);
            await this.creationsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetCreationByCountryAsync<T>(int countryId, int pageNumber, int creationsCount)
            => await this.creationsRepository.AllAsNoTracking()
               .Where(x => x.IsPrivate == false && x.CountryId == countryId)
               .OrderByDescending(x => x.CreatedOn)
               .Skip((pageNumber - 1) * creationsCount)
               .Take(creationsCount)
               .To<T>()
               .ToListAsync();

        public async Task<T> GetCreationModelByIdAsync<T>(string creationId)
          => await this.creationsRepository.AllAsNoTracking()
                .Where(x => x.Id == creationId)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetCreationsByCreatorIdAsync<T>(string creatorId, int pageNumber, int creationsCount)
             => await this.creationsRepository.AllAsNoTracking()
               .Where(x => x.IsPrivate == false && x.CreatorId == creatorId)
               .OrderByDescending(x => x.CreatedOn)
               .Skip((pageNumber - 1) * creationsCount)
               .Take(creationsCount)
               .To<T>()
               .ToListAsync();

        public async Task<IEnumerable<T>> GetCreationsIncudingPrivateByCreatorIdAsync<T>(string creatorId, int pageNumber, int creationsCount)
             => await this.creationsRepository.AllAsNoTracking()
               .Where(x => x.CreatorId == creatorId)
               .OrderByDescending(x => x.CreatedOn)
               .Skip((pageNumber - 1) * creationsCount)
               .Take(creationsCount)
               .To<T>()
               .ToListAsync();

        public async Task<IEnumerable<T>> GetCreationsByNameOrTagAsync<T>(string keyWord, int creationsCount)
           => await this.creationsRepository.AllAsNoTracking()
                .Where(x => x.IsPrivate == false && (x.Title.ToLower().Contains(keyWord.ToLower()) || x.Tags.Any(x => x.Tag.Name.ToLower().Contains(keyWord.ToLower()))))
                .Distinct()
                .Take(creationsCount)
                .To<T>()
                .ToListAsync();

        public async Task<string> GetCreatorIdByCreationIdAsync(string creationId)
        {
            var creation = await this.creationsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == creationId);
            if (creation == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CommentNotFound, creationId));
            }

            var creatorId = creation.CreatorId;
            return creatorId;
        }

        public async Task<IEnumerable<T>> GetNewestCreationsByCategoryAsync<T>(int? categoryId, int pageNumber, int creationsCount)
        {
            if (categoryId == null)
            {
                return await this.creationsRepository.AllAsNoTracking()
                    .Where(x => x.IsPrivate == false)
                     .OrderByDescending(x => x.CreatedOn)
                     .Skip((pageNumber - 1) * creationsCount)
                     .Take(creationsCount)
                     .To<T>()
                     .ToListAsync();
            }

            return await this.creationsRepository.AllAsNoTracking()
                .Where(x => x.CategoryId == categoryId && x.IsPrivate == false)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * creationsCount)
                .Take(creationsCount)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<CreationsViewModel>> GetNumberRandomCreationsAsync(int countOfCreations)
        {
            return await this.creationsRepository.AllAsNoTracking()
                 .Where(x => x.IsPrivate == false)
                 .OrderBy(r => Guid.NewGuid()).Take(countOfCreations)
                 .Select(x => new CreationsViewModel()
                 {
                     Id = x.Id,
                     CreationDataUrl = x.CreationDataUrl,
                     CreatorUserName = x.Creator.UserName,
                 })
                 .ToListAsync();
        }

        public async Task IncreseCreationViewsAsync(string creationId)
        {
            var creation = await this.creationsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == creationId);
            if (creation == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CreationNotFound, creationId));
            }

            creation.Views++;

            this.creationsRepository.Update(creation);
            await this.creationsRepository.SaveChangesAsync();
        }
    }
}
