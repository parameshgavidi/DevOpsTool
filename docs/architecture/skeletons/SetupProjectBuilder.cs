// ============================================================
// COPY into your MAUI app: Services/Build/SetupProjectBuilder.cs
// ============================================================
using YourApp.Services.Process;

namespace YourApp.Services.Build;

public class SetupProjectBuilder
{
    private readonly ProcessRunner _process;

    public SetupProjectBuilder(ProcessRunner process) => _process = process;

    public async Task<int> BuildSetupProjectAsync(
        string devenvPath,
        string vdprojPath,
        string workingDir,
        Action<string> log,
        CancellationToken cancellation = default)
    {
        // PASTE BuildSetupProjectAsync (devenv Process, no cmd) from
        // web-to-client-bridge-setup-msi-devenv-process.txt
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
