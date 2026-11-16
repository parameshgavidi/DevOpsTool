# Install Jenkins Pipeline + Git plugins when Available plugins is empty.
# Use this if Jenkins shows: SocketException: Connection reset
# or "This Jenkins instance appears to be offline."
#
# Run in Administrator PowerShell on the Jenkins laptop.
# Chrome can often reach updates.jenkins.io while the Jenkins service cannot.
# This script uses Windows networking (same as Chrome), then copies .hpi files
# into the Jenkins plugins folder and restarts the Jenkins service.

$ErrorActionPreference = "Stop"
$ucUrl = "https://updates.jenkins.io/current/update-center.json"
$dest  = "C:\ProgramData\Jenkins\.jenkins\plugins"

if (-not (Test-Path $dest)) {
    New-Item -ItemType Directory -Path $dest -Force | Out-Null
}

Write-Host "Downloading plugin list..."
$raw = (Invoke-WebRequest $ucUrl -UseBasicParsing).Content
if ($raw.StartsWith("updateCenter.post(")) {
    $raw = $raw.Substring("updateCenter.post(".Length)
    $trim = $raw.Trim()
    if ($trim.EndsWith(");")) {
        $raw = $trim.Substring(0, $trim.Length - 2)
    }
}
$uc = $raw | ConvertFrom-Json

$queue = New-Object System.Collections.Queue
$seen  = @{}
"workflow-aggregator", "git" | ForEach-Object { $queue.Enqueue($_) }

Write-Host "Stopping Jenkins..."
Stop-Service Jenkins

while ($queue.Count -gt 0) {
    $name = $queue.Dequeue()
    if ($seen.ContainsKey($name)) { continue }
    $seen[$name] = $true
    $p = $uc.plugins.$name
    if (-not $p) {
        Write-Warning "Missing in update center: $name"
        continue
    }
    foreach ($d in @($p.dependencies)) {
        if (-not $d.optional) { $queue.Enqueue([string]$d.name) }
    }
    $out = Join-Path $dest "$name.hpi"
    Write-Host "Downloading $name"
    Invoke-WebRequest $p.url -OutFile $out -UseBasicParsing
}

Write-Host "Starting Jenkins..."
Start-Service Jenkins
Write-Host "DONE. Plugins saved to $dest"
Get-Service Jenkins
