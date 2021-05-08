namespace MyRecipes.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public int RecipesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PagesCount => (int)Math.Ceiling((double)this.RecipesCount / 2);

        public bool HasNextPage => this.PageNumber < this.RecipesCount;

        public int NextPageNumber => this.PageNumber + 1;
    }
}
