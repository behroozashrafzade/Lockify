using Core.Models;
using Infrastructure.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.GoogleDrive
{
    public class SyncService
    {
        private readonly GoogleDriveService _driveService;
        private readonly AesEncryptionService _crypto;

        public SyncService(GoogleDriveService driveService, AesEncryptionService crypto)
        {
            _driveService = driveService;
            _crypto = crypto;
        }

        public async Task UploadPasswordsAsync(List<PasswordEntry> passwords)
        {
            // 1. سریالایز به JSON
            string jsonData = JsonConvert.SerializeObject(passwords);

            // 2. رمزنگاری
            string encrypted = _crypto.Encrypt(jsonData);

            // 3. ذخیره به یک فایل موقت
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, encrypted);

            // 4. آپلود به گوگل درایو
            await _driveService.UploadFileAsync(tempFile, "passwords.enc", "application/octet-stream");
        }

        public async Task<List<PasswordEntry>> DownloadPasswordsAsync()
        {
            // 1. دانلود فایل رمزنگاری‌شده
            string tempFile = Path.GetTempFileName();
            await _driveService.DownloadFileAsync("passwords.enc", tempFile);

            // 2. خواندن و رمزگشایی
            string encrypted = File.ReadAllText(tempFile);
            string jsonData = _crypto.Decrypt(encrypted);

            // 3. برگرداندن لیست
            return JsonConvert.DeserializeObject<List<PasswordEntry>>(jsonData);
        }

    
    }
}
