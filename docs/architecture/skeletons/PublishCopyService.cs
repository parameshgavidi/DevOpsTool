// ============================================================
// COPY into your MAUI app: Services/Build/PublishCopyService.cs
// ============================================================
namespace YourApp.Services.Build;

public class PublishCopyService
{
    private readonly OutputPathService _outputPaths;

    public PublishCopyService(OutputPathService outputPaths) => _outputPaths = outputPaths;

    public async Task CopyArtifactsAsync(
        /* Application app, */
        string workingDir,
        string outputRoot,
        Action<string> log)
    {
        // PASTE the if (exit == 0) PrecompiledWeb / publish / Mailer / GSSApi
        // copy block from BuildApplication (buildtab-gss-webservice-and-gssweb-full)
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
