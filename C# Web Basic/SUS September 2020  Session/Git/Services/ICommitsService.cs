using System.Collections.Generic;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string description, string repositoryId, string userId);

        ICollection<CommitViewModel> GetAllCommitsFromUser(string userId);

        bool CanUserDeleteThisCommit(string userId, string commitId);

        void DeleteCommmit(string commitId);
    }
}
