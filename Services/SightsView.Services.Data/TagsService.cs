namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Tags;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;

        public TagsService(IDeletableEntityRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public async Task CreateTagsAsync(IList<string> tagNames)
        {
            foreach (var tagName in tagNames)
            {
                if (!this.tagsRepository.AllAsNoTrackingWithDeleted().Any(x => x.Name == tagName))
                {
                    var tag = new Tag()
                    {
                        Name = tagName,
                    };
                    await this.tagsRepository.AddAsync(tag);
                }
            }

            await this.tagsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagsViewModel>> GetTagsByNameAsync(IEnumerable<string> tagNames)
            => await this.tagsRepository.AllAsNoTracking()
                 .Where(x => tagNames.Contains(x.Name))
                 .Select(x => new TagsViewModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                 })
                 .ToListAsync();
    }
}
