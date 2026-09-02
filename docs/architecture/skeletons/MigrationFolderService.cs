// ============================================================
// COPY into your MAUI app: Services/Migration/MigrationFolderService.cs
// ============================================================
namespace YourApp.Services.Migration;

public class MigrationFolderService
{
    public string GetSprintPackageRoot(string migrationRoot, string releaseVersion, int sprintNumber)
    {
        // \\share\Migration\GSS\{version}\Sprint {N}\
        throw new NotImplementedException();
    }

    public void EnsureMigrationFolders(string sprintRoot)
    {
        // server_application / server_database / server_web
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
