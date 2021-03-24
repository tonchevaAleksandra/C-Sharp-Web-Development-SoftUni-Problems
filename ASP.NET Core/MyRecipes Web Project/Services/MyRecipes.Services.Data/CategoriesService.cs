namespace MyRecipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyRecipes.Data.Common.Models;
    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categoriesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList()
                .OrderBy(x => x.Name)
                  .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
