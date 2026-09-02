// ============================================================
// COPY into your MAUI app next to BuildTab.razor as BuildTab.razor.cs
// After moving @code here, delete the @code block from the .razor file.
// Then replace method bodies with service calls (see BUILDTAB-CLASS-STRUCTURE.txt).
// ============================================================
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using YourApp.Services.Build;
using YourApp.Services.DbRollup;
using YourApp.Services.Email;
using YourApp.Services.Git;
using YourApp.Services.Logging;
using YourApp.Services.Migration;
using YourApp.Services.Process;

namespace YourApp.Components.Pages;

public partial class BuildTab : ComponentBase
{
    [Inject] private IConfiguration Config { get; set; } = default!;
    [Inject] private ProcessRunner Process { get; set; } = default!;
    [Inject] private GitService Git { get; set; } = default!;
    [Inject] private BuildOrchestrator Build { get; set; } = default!;
    [Inject] private DbRollupService DbRollup { get; set; } = default!;
    [Inject] private DayWiseLogService Logs { get; set; } = default!;
    [Inject] private BuildNotificationService Notify { get; set; } = default!;
    [Inject] private OutputPathService OutputPaths { get; set; } = default!;
    [Inject] private MigrationFolderService Migration { get; set; } = default!;

    // --- UI state (keep in the page) ---
    private string LogOutput = "";
    private bool _isBuilding;
    private bool _isGettingLatest;
    private bool _showApps;
    private string RepoRoot = "";
    private string FromBranch = "";
    private string ToBranch = "";
    private List<string> DbBranchOptions = new();
    private CancellationTokenSource? _buildCts;

    // PASTE remaining fields from your current @code (BuildApps, settings, …)

    private void AppendToUiLog(string text)
    {
        LogOutput += text;
        Logs.Append(RepoRoot, text);
    }

    // Example thin handlers — wire onclick to these names in the .razor markup:

    private async Task GetLatestClicked()
    {
        _isGettingLatest = true;
        await InvokeAsync(StateHasChanged);
        try
        {
            // await Git.GetLatestFromRepoAsync(...);
            // await LoadApplicationsFromConfigAsync();
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
        // await DbRollup.BundleAsync(...);
        // await Notify.SendDbScriptsEmailAsync(...);
        await Task.CompletedTask;
    }

    private async Task LoadDbBranchesAsync()
    {
        // DbBranchOptions = await Git.LoadRemoteBranchesAsync(...);
        await InvokeAsync(StateHasChanged);
    }
}
