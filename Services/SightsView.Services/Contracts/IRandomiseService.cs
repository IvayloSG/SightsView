namespace SightsView.Services.Contracts
{
    using System.Collections.Generic;

    public interface IRandomiseService
    {
        T GetRandomElement<T>(IList<T> elements);
    }
}
