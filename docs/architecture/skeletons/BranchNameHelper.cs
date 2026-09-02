// ============================================================
// COPY into your MAUI app: Services/Git/BranchNameHelper.cs
// Move ALL private static branch/folder name helpers here.
// ============================================================
namespace YourApp.Services.Git;

public static class BranchNameHelper
{
    public static string NormalizeBranchName(string raw)
    {
        // PASTE from BuildTab (strip origin/, refs/heads/, trim)
        throw new NotImplementedException();
    }

    public static IEnumerable<string> SplitBranchTokens(string? line)
    {
        // PASTE SplitBranchTokens — handles multi-line git stdout chunks
        throw new NotImplementedException();
    }

    public static string? ParseRemoteBranch(string line)
    {
        throw new NotImplementedException();
    }

    public static int CompareGssBranchNames(string a, string b)
    {
        throw new NotImplementedException();
    }

    public static string? TrimGssVersionFolder(string? branchName)
    {
        throw new NotImplementedException();
    }

    public static string? GetPreviousBranch(List<string> options, string toBranch)
    {
        throw new NotImplementedException();
    }
}
