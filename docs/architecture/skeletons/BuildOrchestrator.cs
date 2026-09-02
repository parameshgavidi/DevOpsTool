// ============================================================
// COPY into your MAUI app: Services/Build/BuildOrchestrator.cs
// ============================================================
using YourApp.Services.Process;

namespace YourApp.Services.Build;

public class BuildOrchestrator
{
    private readonly ProcessRunner _process;
    private readonly OutputPathService _outputPaths;
    private readonly PublishCopyService _publish;
    private readonly SetupProjectBuilder _setup;

    public BuildOrchestrator(
        ProcessRunner process,
        OutputPathService outputPaths,
        PublishCopyService publish,
        SetupProjectBuilder setup)
    {
        _process = process;
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
        // 3) _process.RunCommandAsync(buildCommand, ...)
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
