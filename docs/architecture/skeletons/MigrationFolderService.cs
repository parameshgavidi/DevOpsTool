// ============================================================
// COPY into: Services/Migration/MigrationFolderService.cs
// ============================================================
namespace GssDevOpsAutomationTool.Services.Migration;

public class MigrationFolderService
{
    public string GetSprintPackageRoot(string migrationRoot, string releaseVersion, int sprintNumber)
    {
        throw new NotImplementedException();
    }

    public void EnsureMigrationFolders(string sprintRoot)
    {
        throw new NotImplementedException();
    }

    public async Task CopyBuildToMigrationShareAsync(
        /* Application app, */
        string outputFolder,
        string sprintRoot,
        Action<string> log)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
