// ============================================================
// COPY next to BuildTab.razor as BuildTab.razor.cs
// ============================================================
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using GssDevOpsAutomationTool.Services;
using GssDevOpsAutomationTool.Services.Build;
using GssDevOpsAutomationTool.Services.DbRollup;
using GssDevOpsAutomationTool.Services.Email;
using GssDevOpsAutomationTool.Services.Git;
using GssDevOpsAutomationTool.Services.Logging;
using GssDevOpsAutomationTool.Services.Migration;
using System.IO;

namespace GssDevOpsAutomationTool.Components.Pages;

public partial class BuildTab : ComponentBase
{
    [Inject] private IConfiguration Config { get; set; } = default!;
    [Inject] private IBuildService BuildSvc { get; set; } = default!;
    [Inject] private GitService Git { get; set; } = default!;
    [Inject] private BuildOrchestrator Build { get; set; } = default!;
    [Inject] private DbRollupService DbRollup { get; set; } = default!;
    [Inject] private DayWiseLogService Logs { get; set; } = default!;
    [Inject] private BuildNotificationService Notify { get; set; } = default!;
    [Inject] private OutputPathService OutputPaths { get; set; } = default!;
    [Inject] private MigrationFolderService Migration { get; set; } = default!;

    private string LogOutput = "";
    private bool _isBuilding;
    private bool _isGettingLatest;
    private bool _isBuildingFilePath;
    private bool _showApps;
    private string RepoRoot = "";
    private string OutputRoot = "";
    private string RepoFromBranch = "";
    private string ResolvedBuildFilePath = "";
    private string FromBranch = "";
    private string ToBranch = "";
    private List<string> DbBranchOptions = new();
    private CancellationTokenSource? _buildCts;

    private void AppendToUiLog(string text)
    {
        LogOutput += text;
        Logs.Append(RepoRoot, text);
    }

    private async Task GetLatestClicked()
    {
        _isGettingLatest = true;
        await InvokeAsync(StateHasChanged);
        try
        {
            // await Git.GetLatestFromRepoAsync(...);
            _showApps = true;
        }
        finally
        {
            _isGettingLatest = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// Combine Output Root + selected Get Latest From Repo Branch.
    /// Button sits next to Get latest. Full paste: docs/fixes/buildtab-build-file-path.txt
    /// </summary>
    private string GetBuildFilePath()
        => OutputPaths.GetBuildFilePath(OutputRoot, RepoFromBranch);

    private async Task BuildFilePathClicked()
    {
        if (_isBuildingFilePath)
            return;

        _isBuildingFilePath = true;
        await InvokeAsync(StateHasChanged);
        try
        {
            var path = GetBuildFilePath();
            if (string.IsNullOrWhiteSpace(path))
            {
                AppendToUiLog($"[{DateTime.Now:HH:mm:ss}] [ERROR] Set Output Root and select Get Latest From Repo Branch first.\r\n");
                return;
            }

            Directory.CreateDirectory(path);
            ResolvedBuildFilePath = path;
            AppendToUiLog($"[{DateTime.Now:HH:mm:ss}] [INFO] Build file path: {path}\r\n");

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"\"{path}\"",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                AppendToUiLog($"[{DateTime.Now:HH:mm:ss}] [WARN] Path created but Explorer did not open: {ex.Message}\r\n");
            }
        }
        finally
        {
            _isBuildingFilePath = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task BuildAllAsync()
    {
        if (_isBuilding) return;
        _isBuilding = true;
        await InvokeAsync(StateHasChanged);
        try
        {
            // await Build.BuildAllSequentialAsync(...);
        }
        finally
        {
            _isBuilding = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task BundleClicked()
    {
        await Task.CompletedTask;
    }

    private async Task LoadDbBranchesAsync()
    {
        await InvokeAsync(StateHasChanged);
    }
}
