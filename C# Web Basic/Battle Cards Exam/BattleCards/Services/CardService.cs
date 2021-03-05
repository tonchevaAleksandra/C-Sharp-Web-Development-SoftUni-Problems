using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels.Cards;

namespace BattleCards.Services
{
  public  class CardService:ICardsService
    {
        private readonly ApplicationDbContext _db;

        public CardService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Create(string name, string url, string keyword, int attack, int health, string description)
        {
            var card = new Card()
            {
                Name = name,
                ImageUrl = url,
                Keyword = keyword,
                Attack = attack,
                Health = health,
                Description = description
            };

            this._db.Cards.Add(card);
            this._db.SaveChanges();

        }

        public AllCardsViewModel GetAll()
        {
            var cards = this._db.Cards.Select(x => new CardViewModel()
            {
                Attack = x.Attack,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
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
            var cards = this._db.Users.Find(userId).UserCards.Select(x=> new CardViewModel()
            {
                Attack = x.Card.Attack,
                Health = x.Card.Health,
                ImageUrl = x.Card.ImageUrl,
                Keyword = x.Card.Keyword,
                Name = x.Card.Name
            })
                .ToList();

            return new AllCardsViewModel()
            {
                Cards = cards
            };
        }
    }
}
