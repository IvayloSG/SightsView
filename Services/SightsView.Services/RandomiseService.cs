namespace SightsView.Services
{
    using System;
    using System.Collections.Generic;

    using SightsView.Services.Contracts;

    public class RandomiseService : IRandomiseService
    {
        public T GetRandomElement<T>(IList<T> elements)
        {
            var random = new Random();
            var index = random.Next(elements.Count);

            return elements[index];
        }
    }
}
