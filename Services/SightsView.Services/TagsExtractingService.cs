namespace SightsView.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using SightsView.Common;
    using SightsView.Services.Contracts;

    public class TagsExtractingService : ITagsExtractingService
    {
        // TODO: Regex Match #[^\s#]+
        public IList<string> GetTagsFromTagsArea(string tagsInput)
        {
            var pattern = GlobalConstants.TagExtractingRegexPattern;

            var tagNames = Regex.Matches(tagsInput, pattern).Select(x => x.Value.ToString()).ToList();

            return tagNames;
        }
    }
}
