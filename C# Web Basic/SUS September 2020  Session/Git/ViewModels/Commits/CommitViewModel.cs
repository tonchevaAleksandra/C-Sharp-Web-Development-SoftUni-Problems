using System;

namespace Git.ViewModels.Commits
{
    public class CommitViewModel
    {

        public string RepositoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
