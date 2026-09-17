// ============================================================
// COPY into: Services/Build/OutputPathService.cs
// ============================================================
using System.IO;

namespace GssDevOpsAutomationTool.Services.Build;

public class OutputPathService
{
    public string GetAppOutputFolder(
        /* Application app, */
        string outputRoot,
        string? currentBranchOrVersion)
    {
        // PASTE GetAppOutputFolder / ResolveAppOutputFolder
        throw new NotImplementedException();
    }

    /// <summary>
    /// Combine Output Root + selected Get Latest From Repo Branch name.
    /// F:\build-output + GSSv9.3S1 → F:\build-output\GSSv9.3S1
    /// Paste-ready body: docs/fixes/buildtab-build-file-path.txt (CHANGE 5)
    /// </summary>
    public string GetBuildFilePath(string? outputRoot, string? selectedBranch)
    {
        var root = (outputRoot ?? string.Empty).Trim().TrimEnd('\\', '/');
        var branch = (selectedBranch ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(root) || string.IsNullOrWhiteSpace(branch))
            return string.Empty;

        var slash = branch.LastIndexOf('/');
        if (slash >= 0 && slash < branch.Length - 1)
            branch = branch[(slash + 1)..];

        foreach (var c in Path.GetInvalidFileNameChars())
            branch = branch.Replace(c, '_');

        if (string.IsNullOrWhiteSpace(branch))
            return string.Empty;

        return Path.Combine(root, branch);
    }
}
