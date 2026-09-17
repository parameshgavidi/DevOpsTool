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
    /// Combine Output Root + selected Get Latest From Repo Branch.
    /// F:\build-output + GSSv9.3S1 → F:\build-output\9.3S1  (trim GSSv)
    /// Paste-ready body: docs/fixes/buildtab-build-file-path-trim-gssv.txt
    /// </summary>
    public string GetBuildFilePath(string? outputRoot, string? selectedBranch)
    {
        var root = (outputRoot ?? string.Empty).Trim().TrimEnd('\\', '/');
        var branch = (selectedBranch ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(root) || string.IsNullOrWhiteSpace(branch))
            return string.Empty;

        var folder = TrimGssVersionFolder(branch);
        if (string.IsNullOrWhiteSpace(folder))
        {
            var slash = branch.LastIndexOf('/');
            folder = (slash >= 0 && slash < branch.Length - 1)
                ? branch[(slash + 1)..]
                : branch;
        }

        foreach (var c in Path.GetInvalidFileNameChars())
            folder = folder.Replace(c, '_');

        if (string.IsNullOrWhiteSpace(folder))
            return string.Empty;

        return Path.Combine(root, folder);
    }

    /// <summary>
    /// GSSv9.3S1 / origin/GSSv9.3S1 / GSS8.7 → 9.3S1 / 8.7
    /// Same rules as BuildTab.TrimGssVersionFolder.
    /// </summary>
    private static string? TrimGssVersionFolder(string? branchName)
    {
        if (string.IsNullOrWhiteSpace(branchName))
            return null;

        var name = branchName.Trim();

        var slash = name.LastIndexOf('/');
        if (slash >= 0 && slash < name.Length - 1)
            name = name[(slash + 1)..];

        if (name.StartsWith("GSSv", StringComparison.OrdinalIgnoreCase))
            return name[4..];

        if (name.StartsWith("GSS", StringComparison.OrdinalIgnoreCase)
            && name.Length > 3
            && char.IsDigit(name[3]))
            return name[3..];

        return null;
    }
}
