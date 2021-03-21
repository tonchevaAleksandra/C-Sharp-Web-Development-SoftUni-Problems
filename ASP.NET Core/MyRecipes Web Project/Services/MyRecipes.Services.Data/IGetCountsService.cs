using MyRecipes.Services.Data.Models;

namespace MyRecipes.Services.Data
{
    using MyRecipes.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        CountsDto GetCounts();
    }
}
