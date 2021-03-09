using Git.Data;
using Git.Models;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateRepository(string name, string repositoryType, string userId)
        {
            var repository = new Repository()
            {
                Name = name,
                IsPublic = (repositoryType == "Public" ? true : false),
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public ICollection<RepositoryViewModel> GetAllPublicRepositories()
        {
            return this.db.Repositories.Where(x => x.IsPublic).Select(x => new RepositoryViewModel()
            {
                Id = x.Id,
                CommitsCount = x.Commits.Count,
                CreatedOn = x.CreatedOn,
                Name = x.Name,
                Owner = x.Owner.Username
            }).ToList();
        }

        public string GetRepositoryName(string id)
        {
            return this.db.Repositories.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
        }
    }
}
