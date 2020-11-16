namespace SightsView.Services.Contracts
{
    using System.Collections.Generic;

    public interface ITagsExtractingService
    {
        IList<string> GetTagsFromTagsArea(string tagsInput);
    }
}
