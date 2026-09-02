// ============================================================
// COPY into: Services/Build/BuildOrchestrator.cs
// Inject EXISTING IBuildService — not ProcessRunner.
// ============================================================
using GssDevOpsAutomationTool.Services;

namespace GssDevOpsAutomationTool.Services.Build;

public class BuildOrchestrator
{
    private readonly IBuildService _build;
    private readonly OutputPathService _outputPaths;
    private readonly PublishCopyService _publish;
    private readonly SetupProjectBuilder _setup;

    public BuildOrchestrator(
        IBuildService build,
        OutputPathService outputPaths,
        PublishCopyService publish,
        SetupProjectBuilder setup)
    {
        _build = build;
        _outputPaths = outputPaths;
        _publish = publish;
        _setup = setup;
    }

    public async Task BuildApplicationAsync(
        /* Application app, */
        string workingDir,
        string? projectFile,
        string buildCommand,
        bool isLegacy,
        string outputRoot,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // ORDER (do not change — matches working BuildTab flow):
        // 1) RestorePackagesAsync (skip AspNetWebForms / AsmxWebService / Mailer)
        // 2) CleanPrecompiledOutputAsync when needed
        // 3) var exit = await _build.RunCommandAsync(workingDir, buildCommand, log, cancellation);
        //    (optional) _build.ResolveMsBuildPath(netFramework: isLegacy)
        // 4) if exit==0 → _publish.CopyArtifactsAsync(...)
        // 5) if setup project → _setup.BuildSetupProjectAsync(...)
        await Task.CompletedTask;
        throw new NotImplementedException("Paste BuildApplication body in sections.");
    }

    public async Task BuildAllSequentialAsync(
        /* IEnumerable<Application> apps, */
        Func<object, Task> buildOne,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // Sequential loop — one app at a time (see buildtab-buildall-sequential)
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
