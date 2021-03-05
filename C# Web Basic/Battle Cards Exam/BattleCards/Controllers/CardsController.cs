using BattleCards.Services;
using BattleCards.ViewModels.Cards;
using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService _cardsService;

        public CardsController(ICardsService cardsService)
        {
            _cardsService = cardsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }

            if (input.Name.Length < 5 || input.Name.Length > 15)
            {
                return this.Redirect("Cards/Add");
            }

            if (input.Attack < 0)
            {
                return this.Redirect("Cards/Add");
            }

            if (input.Health < 0)
            {
                return this.Redirect("Cards/Add");
            }

            if (input.Description.Length > 200 || input.Description==null)
            {
                return this.Redirect("Cards/Add");
            }

            this._cardsService.Create(input.Name, input.ImageUrl, input.Keyword, input.Attack, input.Health, input.Description);

            return this.Redirect("Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }

            var viewModel = this._cardsService.GetAll();
            return this.View(viewModel);
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }

            this._cardsService.GetAllCardsToUser(this.User);
            return this.View();
        }
    }
}
