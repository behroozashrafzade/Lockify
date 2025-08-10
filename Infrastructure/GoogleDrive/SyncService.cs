using Core.Models;
using Infrastructure.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Infrastructure.GoogleDrive
{
    public class SyncService
    {
        private readonly GoogleDriveService _driveService;
        private readonly AesEncryptionService _crypto;
        private readonly string _encryptionKey;  // 

        // ایمیل رو تو کانستراکتور می‌گیری
        public SyncService(GoogleDriveService driveService, AesEncryptionService crypto, string encryptionKey)
        {
            _driveService = driveService;
            _crypto = crypto;
            _encryptionKey = encryptionKey;
        }

        public async Task UploadPasswordsAsync(List<PasswordEntry> passwords)
        {
            string jsonData = JsonConvert.SerializeObject(passwords);

            // پاس دادن کلید ایمیل به متد Encrypt
            string encrypted = _crypto.Encrypt(jsonData, _encryptionKey);

            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, encrypted);

            await _driveService.UploadFileAsync(tempFile, "passwords.enc");
        }

        public async Task<List<PasswordEntry>> DownloadPasswordsAsync()
        {
            string tempFile = Path.GetTempFileName();
            await _driveService.DownloadFileAsync("passwords.enc", tempFile);

            string encrypted = File.ReadAllText(tempFile);

            // پاس دادن کلید ایمیل به متد Decrypt
            string jsonData = _crypto.Decrypt(encrypted, _encryptionKey);

            return JsonConvert.DeserializeObject<List<PasswordEntry>>(jsonData);
        }

    }
}
