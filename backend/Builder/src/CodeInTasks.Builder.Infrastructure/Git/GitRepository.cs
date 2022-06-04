using LibGit2Sharp;

namespace CodeInTasks.Builder.Infrastructure.Git
{
    internal class GitRepository : IGitRepository
    {
        public string Path { get; }

        private Repository repository;

        public GitRepository(string path)
        {
            Path = path;
        }

        public void CheckoutPaths(string commitId, IEnumerable<string> paths)
        {
            AssertRepositoryExists();

            repository.CheckoutPaths(commitId, paths);
        }

        public Task CloneAsync(string sourceUrl, GitAuthCredintials gitAuth, long sizeLimitBytes)
        {
            var cloneOptions = new CloneOptions()
            {
                CredentialsProvider = (_, _, _) => new UsernamePasswordCredentials()
                {
                    Username = gitAuth.UserName,
                    Password = gitAuth.Password,
                },

                OnTransferProgress = progress => progress.ReceivedBytes <= sizeLimitBytes
            };

            try
            {
                var clonePath = Repository.Clone(sourceUrl, Path, cloneOptions);

                repository = new Repository(clonePath);

                return Task.CompletedTask;
            }
            catch (UserCancelledException)
            {
                throw new SizeLimitExceedException();
            }
        }

        public string GetLastCommitId()
        {
            AssertRepositoryExists();

            var lastCommit = repository.Commits.Last();
            var commitIdString = lastCommit.Id.ToString();

            return commitIdString;
        }

        public Task PullAsync(string sourceUrl, GitAuthCredintials gitAuth, long sizeLimitBytes)
        {
            AssertRepositoryExists();

            Fetch(sourceUrl, gitAuth, sizeLimitBytes);
            MergeFetchedRefs();

            return Task.CompletedTask;
        }

        private void AssertRepositoryExists()
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository dont exists! Clone it before calling other methods");
            }
        }

        private void Fetch(string sourceUrl, GitAuthCredintials gitAuth, long sizeLimitBytes)
        {
            var fetchOptions = new FetchOptions()
            {
                CredentialsProvider = (_, _, _) => new UsernamePasswordCredentials()
                {
                    Username = gitAuth.UserName,
                    Password = gitAuth.Password,
                },

                OnTransferProgress = progress => progress.ReceivedBytes <= sizeLimitBytes
            };

            var refspecs = Array.Empty<string>();

            try
            {
                repository.Network.Fetch(sourceUrl, refspecs, fetchOptions);
            }
            catch (UserCancelledException)
            {
                throw new SizeLimitExceedException();
            }
        }

        private void MergeFetchedRefs()
        {
            const string name = "builder";
            const string email = "builder <builder@nowhere.com>";
            var signature = new Signature(name, email, DateTimeOffset.UtcNow);

            var mergeOptions = new MergeOptions()
            {
                FileConflictStrategy = CheckoutFileConflictStrategy.Ours,
            };

            repository.MergeFetchedRefs(signature, mergeOptions);

            //With merge strategy "ours", refs always merged, so there is no need in result checks
        }
    }
}
