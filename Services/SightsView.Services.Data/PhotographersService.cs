﻿namespace SightsView.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SightsView.Common;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Photographers;

    public class PhotographersService : IPhotographersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> photographersRepository;

        public PhotographersService(IDeletableEntityRepository<ApplicationUser> photographersRepository)
        {
            this.photographersRepository = photographersRepository;
        }

        public async Task<IEnumerable<PhotographersViewModel>> GetAllPhotographersAsync(string currentUserId)
            => await this.photographersRepository.AllAsNoTracking()
            .Where(x => x.Id != currentUserId && !x.UserName.Contains(GlobalConstants.AdministratorUserName))
            .Select(x => new PhotographersViewModel()
            {
                Id = x.Id,
                Username = x.UserName,
                Country = x.Country.Name,
                CreationsCount = x.Creations.Count,
                LikedCreations = x.Creations
                    .SelectMany(y => y.Likes)
                    .Count(),
                Followers = x.Followers.Count,
                BestCreationId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().Id,
                BestCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().CreationDataUrl,
                RunnerupCreationsId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().Id,
                RunnerupCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().CreationDataUrl,
            })
            .Take(15)
            .ToListAsync();

        public async Task<T> GetPhotographerByIdAsync<T>(string photographerId)
            => await this.photographersRepository.All()
            .Where(x => x.Id == photographerId)
            .To<T>()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<PhotographersViewModel>> GetPhotographersWithMostCreationsAsync(string currentUserId)
            => await this.photographersRepository.AllAsNoTracking()
            .Where(x => x.Id != currentUserId && !x.UserName.Contains(GlobalConstants.AdministratorUserName))
            .OrderByDescending(x => x.Creations.Count)
            .Select(x => new PhotographersViewModel()
            {
                Id = x.Id,
                Username = x.UserName,
                Country = x.Country.Name,
                CreationsCount = x.Creations.Count,
                LikedCreations = x.Creations
                    .SelectMany(y => y.Likes)
                    .Count(),
                Followers = x.Followers.Count,
                BestCreationId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().Id,
                BestCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().CreationDataUrl,
                RunnerupCreationsId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().Id,
                RunnerupCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().CreationDataUrl,
            })
            .Take(15)
            .ToListAsync();

        public async Task<IEnumerable<PhotographersViewModel>> GetPhotographersWithMostFollowersAsync(string currentUserId)
             => await this.photographersRepository.AllAsNoTracking()
            .Where(x => x.Id != currentUserId && !x.UserName.Contains(GlobalConstants.AdministratorUserName))
            .OrderByDescending(x => x.Followers.Count)
            .Select(x => new PhotographersViewModel()
            {
                Id = x.Id,
                Username = x.UserName,
                Country = x.Country.Name,
                CreationsCount = x.Creations.Count,
                LikedCreations = x.Creations
                    .SelectMany(y => y.Likes)
                    .Count(),
                Followers = x.Followers.Count,
                BestCreationId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().Id,
                BestCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().CreationDataUrl,
                RunnerupCreationsId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().Id,
                RunnerupCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().CreationDataUrl,
            })
            .Take(15)
            .ToListAsync();

        public async Task<IEnumerable<PhotographersViewModel>> GetPhotographersWithMostLikesAsync(string currentUserId)
              => await this.photographersRepository.AllAsNoTracking()
            .Where(x => x.Id != currentUserId && !x.UserName.Contains(GlobalConstants.AdministratorUserName))
            .OrderByDescending(x => x.Creations.SelectMany(y => y.Likes).Count())
            .Select(x => new PhotographersViewModel()
            {
                Id = x.Id,
                Username = x.UserName,
                Country = x.Country.Name,
                CreationsCount = x.Creations.Count,
                LikedCreations = x.Creations
                    .SelectMany(y => y.Likes)
                    .Count(),
                Followers = x.Followers.Count,
                BestCreationId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().Id,
                BestCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().CreationDataUrl,
                RunnerupCreationsId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().Id,
                RunnerupCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().CreationDataUrl,
            })
            .Take(15)
            .ToListAsync();

        public async Task<IEnumerable<PhotographersViewModel>> GetPhotographersMostNewestAsync(string currentUserId)
            => await this.photographersRepository.AllAsNoTracking()
            .Where(x => x.Id != currentUserId && !x.UserName.Contains(GlobalConstants.AdministratorUserName))
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new PhotographersViewModel()
            {
                Id = x.Id,
                Username = x.UserName,
                Country = x.Country.Name,
                CreationsCount = x.Creations.Count,
                LikedCreations = x.Creations
                    .SelectMany(y => y.Likes)
                    .Count(),
                Followers = x.Followers.Count,
                BestCreationId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().Id,
                BestCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views).
                    FirstOrDefault().CreationDataUrl,
                RunnerupCreationsId = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().Id,
                RunnerupCreationsUrl = x.Creations
                    .OrderByDescending(x => x.Likes.Count)
                    .ThenByDescending(x => x.Views)
                    .Skip(1)
                    .FirstOrDefault().CreationDataUrl,
            })
            .Take(15)
            .ToListAsync();
    }
}
