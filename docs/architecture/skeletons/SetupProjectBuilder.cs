// ============================================================
// COPY into: Services/Build/SetupProjectBuilder.cs
// Uses EXISTING IBuildService.ResolveVisualStudioIdePath + RunCommandAsync
// ============================================================
using GssDevOpsAutomationTool.Services;

namespace GssDevOpsAutomationTool.Services.Build;

public class SetupProjectBuilder
{
    private readonly IBuildService _build;

    public SetupProjectBuilder(IBuildService build) => _build = build;

    public async Task<int> BuildSetupProjectAsync(
        string vdprojPath,
        string workingDir,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // PASTE BuildSetupProjectAsync from web-to-client-bridge-setup-msi-devenv-process.txt
        // Prefer devenv Process directly; path from:
        //   var devenv = _build.ResolveVisualStudioIdePath();
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
