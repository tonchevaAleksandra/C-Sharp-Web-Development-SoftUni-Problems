namespace MyRecipes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRecipes.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category() {Name = "Закуски"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Супи"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Свинско месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Агнешко месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Заешко месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Говеждо месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Пилешко месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Гъше месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Риба"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Дивечово месо"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Десерти"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Основни ястия"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Предястия"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Сладки и бисквити"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Сосове"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Туршии и зимнина"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Хляб и пити"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Веган ястия"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Безглутенови ястия"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Диетични ястия"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Вегетариански ястия"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Пици"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Бургери"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Паста"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Бебешка кухня"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Алкохолни Коктейли"});
            await dbContext.Categories.AddAsync(new Category() {Name = "Безалкохолни Коктейли"});



            await dbContext.SaveChangesAsync();
        }
    }
}
