// ============================================================
// COPY into: Services/Git/GitService.cs
// Inject EXISTING IBuildService (do not use ProcessRunner).
// ============================================================
using GssDevOpsAutomationTool.Services;

namespace GssDevOpsAutomationTool.Services.Git;

public class GitService
{
    private readonly IBuildService _build;

    public GitService(IBuildService build) => _build = build;

    public async Task<bool> GetLatestFromRepoAsync(
        /* Application app, */
        string workingDir,
        string gitLatestCommand,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // PASTE GetLatestFromRepoAsync body; use:
        //   await _build.RunCommandAsync(workingDir, gitLatestCommand, log, cancellation);
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
        // Run git via: await _build.RunCommandAsync(repoRoot, gitBranchesCommand, log, cancellation);
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
