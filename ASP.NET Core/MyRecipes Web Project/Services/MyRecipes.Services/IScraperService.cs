namespace MyRecipes.Services
{
    using System.Threading.Tasks;

    public interface IScraperService
    {
        Task PopulateDbWithRecipesAsync();
    }
}
