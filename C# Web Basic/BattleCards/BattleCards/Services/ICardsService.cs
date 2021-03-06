using BattleCards.ViewModels.Cards;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        int AddCard(string  name, string url, string keyword, int attack, int health, string description);

        void AddCardToUserCollection(string userId, int cardId);
        AllCardsViewModel GetAll();

        AllCardsViewModel GetAllCardsToUser(string userId);

        void RemoveCardFromUserCollection(string userId, int cardId);
    }
}
