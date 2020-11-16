namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SightsView.Web.ViewModels.Tags;

    public interface ITagsService
    {
        Task CreateTagsAsync(IList<string> tagNames);

        Task<IEnumerable<TagsViewModel>> GetTagsByNameAsync(IEnumerable<string> tagNames);
    }
}
