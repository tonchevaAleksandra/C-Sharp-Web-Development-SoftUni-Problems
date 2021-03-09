using System.Collections.Generic;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void CreateRepository(string name, string repositoryType, string userId);

        ICollection<RepositoryViewModel> GetAllPublicRepositories();

        string GetRepositoryName(string id);
    }
}
