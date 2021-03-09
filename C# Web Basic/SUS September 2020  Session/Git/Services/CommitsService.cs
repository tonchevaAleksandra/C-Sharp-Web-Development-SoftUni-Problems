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
        private readonly ApplicationDbContext db;

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
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                RepositoryName = x.Repository.Name
            }).ToList();
        }

        public bool CanUserDeleteThisCommit(string userId, string commitId)
        {
            var repositoryOwnerId = this.db.Commits.Where(x => x.Id == commitId).Select(x => x.Repository.OwnerId).FirstOrDefault();

            return repositoryOwnerId == userId ? true : false;

        }

        public void DeleteCommmit(string commitId)
        {
            var commmit = this.db.Commits.Find(commitId);
            this.db.Commits.Remove(commmit);
            this.db.SaveChanges();
        }
    }
}
