using Git.Data;
using Git.Models;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string description, string repositoryId, string userId)
        {
            var commit = new Commit()
            {
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                Description = description,
                RepositoryId = repositoryId
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public ICollection<CommitViewModel> GetAllCommitsFromUser(string userId)
        {
            return this.db.Commits.Where(x => x.CreatorId == userId).Select(x => new CommitViewModel()
            {
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                RepositoryName = x.Repository.Name
            }).ToList();
        }
    }
}
