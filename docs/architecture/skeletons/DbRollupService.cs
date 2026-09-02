// ============================================================
// COPY into: Services/DbRollup/DbRollupService.cs
// ============================================================
using GssDevOpsAutomationTool.Services;
using GssDevOpsAutomationTool.Services.Git;

namespace GssDevOpsAutomationTool.Services.DbRollup;

public class DbRollupService
{
    private readonly IBuildService _build;
    private readonly GitService _git;

    public DbRollupService(IBuildService build, GitService git)
    {
        _build = build;
        _git = git;
    }

    public async Task BundleAsync(
        string exePath,
        string fromBranch,
        string toBranch,
        string tempOutputDir,
        string destDbScriptsDir,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // 1) run DBRollupScriptBuilder.exe -f -t via _build.RunCommandAsync
        // 2) ClearFolderContents(destDbScriptsDir)
        // 3) copy files from tempOutputDir → destDbScriptsDir
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public int ClearFolderContents(string folder, Action<string> log)
    {
        // PASTE file-by-file clear from bundle-db-scripts-clear-not-deleting.txt
        throw new NotImplementedException();
    }

    public async Task EnsureFromAndToBranchFoldersAsync(
        string branchRoot,
        string fromBranch,
        string toBranch,
        string repoRoot,
        Action<string> log)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
