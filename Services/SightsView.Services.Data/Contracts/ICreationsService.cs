namespace SightsView.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICreationsService
    {
        string AddCreationInDbAsync(string title, string description, bool isPublic, int countryId, int categoryId, string userId, List<string> tags);
    }
}
