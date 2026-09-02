// ============================================================
// COPY into your MAUI app: Services/DbRollup/DbRollupService.cs
// ============================================================
using YourApp.Services.Git;
using YourApp.Services.Process;

namespace YourApp.Services.DbRollup;

public class DbRollupService
{
    private readonly ProcessRunner _process;
    private readonly GitService _git;

    public DbRollupService(ProcessRunner process, GitService git)
    {
        _process = process;
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
        // 1) run DBRollupScriptBuilder.exe -f -t
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
