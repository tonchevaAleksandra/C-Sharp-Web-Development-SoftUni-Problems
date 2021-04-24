using System.Threading.Tasks;

namespace MyRecipes.Services
{
    public interface IScraperService
    {
        Task PopulateDbWithRecipes();
    }
}
