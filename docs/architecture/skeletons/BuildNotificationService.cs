// ============================================================
// COPY into: Services/Email/BuildNotificationService.cs
// ============================================================
namespace GssDevOpsAutomationTool.Services.Email;

public class BuildNotificationService
{
    public Task SendDbScriptsEmailAsync(
        string gssConnectionStringPlaintext,
        string destDbScriptsDir,
        Action<string> log)
    {
        // PASTE email try/catch — NO Encryption.Crypto / CryptoPassphrase
        throw new NotImplementedException();
    }
}
