// ============================================================
// COPY into your MAUI app: Services/Email/BuildNotificationService.cs
// ============================================================
namespace YourApp.Services.Email;

public class BuildNotificationService
{
    public Task SendDbScriptsEmailAsync(
        string gssConnectionStringPlaintext,
        string destDbScriptsDir,
        /* email settings from appsettings, */
        Action<string> log)
    {
        // PASTE email try/catch from:
        //   buildtab-plaintext-cs-no-crypto.txt
        //   buildtab-email-script-file-path-mailbody.txt
        //   gssemail-send-attachments-db-scripts.txt
        // NO Encryption.Crypto / CryptoPassphrase here.
        throw new NotImplementedException();
    }
}
