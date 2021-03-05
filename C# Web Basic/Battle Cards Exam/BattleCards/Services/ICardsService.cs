using BattleCards.ViewModels.Cards;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        void Create(string name, string url, string keyword, int attack, int health, string description);

        AllCardsViewModel GetAll();

        AllCardsViewModel GetAllCardsToUser(string userId);
    }
}
