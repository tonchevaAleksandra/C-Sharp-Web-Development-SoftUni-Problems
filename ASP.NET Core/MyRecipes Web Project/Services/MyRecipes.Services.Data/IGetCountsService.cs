namespace MyRecipes.Services.Data
{
    using MyRecipes.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        IndexViewModel GetCounts();
    }
}
