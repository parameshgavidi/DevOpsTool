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
    private bool _showApps;
    private string RepoRoot = "";
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
