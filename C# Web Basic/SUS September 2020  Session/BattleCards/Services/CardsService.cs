using System.Linq;
using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels.Cards;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext _db;

        public CardsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public int AddCard(string name, string image, string keyword, int attack, int health, string description)
        {
            var card = new Card()
            {
                Name = name,
                ImageUrl = image,
                Keyword = keyword,
                Attack = attack,
                Health = health,
                Description = description
            };

            this._db.Cards.Add(card);
            this._db.SaveChanges();
            return card.Id;
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            if (this._db.UserCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }

            this._db.UserCards.Add(new UserCard
            {
                CardId = cardId,
                UserId = userId,
            });
            this._db.SaveChanges();
        }

        public AllCardsViewModel GetAll()
        {
            var cards = this._db.Cards.Select(x => new CardViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                Attack = x.Attack,
                Health = x.Health,
                Image = x.ImageUrl,
                Keyword = x.Keyword,
                Name = x.Name
            })
                .ToList();
            var model = new AllCardsViewModel()
            {
                Cards = cards
            };

            return model;
        }

        public AllCardsViewModel GetAllCardsToUser(string userId)
        {
            var cards = this._db.UserCards.Where(u=>u.UserId==userId).Select(x => new CardViewModel()
            {
                Id = x.CardId,
                Description = x.Card.Description,
                Attack = x.Card.Attack,
                Health = x.Card.Health,
                Image = x.Card.ImageUrl,
                Keyword = x.Card.Keyword,
                Name = x.Card.Name
            })
                .ToList();

            return new AllCardsViewModel()
            {
                Cards = cards
            };
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var userCard = this._db.UserCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);
            if (userCard == null)
            {
                return;
            }

            this._db.UserCards.Remove(userCard);
            this._db.SaveChanges();
        }
    }
}
