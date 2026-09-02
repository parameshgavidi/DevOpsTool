// ============================================================
// NOTE: Do NOT add this file to GssDevOpsAutomationTool.
// You already have Services/IBuildService.cs + BuildService.cs
// with RunCommandAsync / ResolveMsBuildPath / ResolveVisualStudioIdePath.
//
// New services should inject IBuildService, for example:
//
 //   private readonly IBuildService _build;
 //   public GitService(IBuildService build) => _build = build;
 //   await _build.RunCommandAsync(workingDir, command, log, ct);
//
 // See docs/architecture/EXISTING-BUILDSERVICE-ALIGN.txt
// ============================================================
namespace GssDevOpsAutomationTool.Services;

// Placeholder only — real interface already lives in your app as IBuildService.
public interface IBuildService_Reference
{
    Task<int> RunCommandAsync(
        string workingDirectory,
        string command,
        Action<string> onOutput,
        CancellationToken cancellationToken);

    string? ResolveMsBuildPath(bool netFramework);

    string? ResolveVisualStudioIdePath();
}
