// ============================================================
// COPY into your MAUI app: Services/Git/GitService.cs
// ============================================================
using YourApp.Services.Process;

namespace YourApp.Services.Git;

public class GitService
{
    private readonly ProcessRunner _process;

    public GitService(ProcessRunner process) => _process = process;

    public async Task<bool> GetLatestFromRepoAsync(
        /* Application app, */
        string workingDir,
        string gitLatestCommand,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // PASTE GetLatestFromRepoAsync body; use _process.RunCommandAsync
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<List<string>> LoadRemoteBranchesAsync(
        string repoRoot,
        string gitBranchesCommand,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // PASTE LoadDbBranchesAsync / LoadRepoBranchesAsync parse logic
        // Use BranchNameHelper.SplitBranchTokens + NormalizeBranchName
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public Task<string?> TryGetCurrentGitBranchAsync(string repoRoot, Action<string>? log = null)
    {
        // PASTE TryGetCurrentGitBranch (git rev-parse --abbrev-ref HEAD)
        throw new NotImplementedException();
    }

    public async Task PullBranchIntoFolderAsync(
        string branch,
        string folder,
        bool justCreated,
        string repoRoot,
        Action<string> log)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
