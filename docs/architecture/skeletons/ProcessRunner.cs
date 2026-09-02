// ============================================================
// COPY into your MAUI app: Services/Process/ProcessRunner.cs
// Move RunCommandAsync / RunProcessCaptureAsync bodies here.
// ============================================================
namespace YourApp.Services.Process;

public class ProcessRunner
{
    public async Task<int> RunAsync(
        string fileName,
        string arguments,
        string workingDirectory,
        Action<string>? onStdout = null,
        Action<string>? onStderr = null,
        CancellationToken cancellation = default)
    {
        // PASTE your existing RunProcessCaptureAsync body here.
        // Replace LogOutput += ... with onStdout?.Invoke(...) / onStderr?.Invoke(...)
        await Task.CompletedTask;
        throw new NotImplementedException("Paste RunProcessCaptureAsync body from BuildTab.");
    }

    public Task<int> RunCommandAsync(
        string command,
        string workingDirectory,
        Action<string>? onStdout = null,
        Action<string>? onStderr = null,
        CancellationToken cancellation = default)
    {
        // If you used cmd.exe /c for buildCommand strings, keep that here once.
        return RunAsync(
            "cmd.exe",
            "/c " + command,
            workingDirectory,
            onStdout,
            onStderr,
            cancellation);
    }
}
