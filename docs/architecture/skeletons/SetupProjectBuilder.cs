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
        // PASTE BuildSetupProjectAsync from gss-website-installer-release-anycpu-settings.txt
        // Prefer devenv Process directly; path from:
        //   var devenv = _build.ResolveVisualStudioIdePath();
        // devenv args MUST include platform from appsettings:
        //   /build "Release|Any CPU"  (setupConfiguration + setupPlatform)
        // Do not hardcode /build Release — that ignores Any CPU.
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public static string BuildDevenvSolutionConfig(
        string? setupConfiguration,
        string? setupPlatform)
    {
        var configuration = string.IsNullOrWhiteSpace(setupConfiguration)
            ? "Release"
            : setupConfiguration.Trim();
        var platform = string.IsNullOrWhiteSpace(setupPlatform)
            ? "Any CPU"
            : setupPlatform.Trim();
        return $"{configuration}|{platform}";
    }
}
