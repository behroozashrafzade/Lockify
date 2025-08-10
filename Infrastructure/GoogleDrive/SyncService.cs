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
        private readonly string _encryptionKey = "your-secure-key"; // کلید رمزنگاری ثابت یا از تنظیمات بخون

        public SyncService(GoogleDriveService driveService, AesEncryptionService crypto)
        {
            _driveService = driveService;
            _crypto = crypto;
        }

        public async Task UploadPasswordsAsync(List<PasswordEntry> passwords)
        {
            // 1. سریالایز به JSON
            string jsonData = JsonConvert.SerializeObject(passwords);

            // 2. رمزنگاری (ورودی کلید اضافه شد)
            string encrypted = _crypto.Encrypt(jsonData, _encryptionKey);

            // 3. ذخیره به یک فایل موقت
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, encrypted);

            // 4. آپلود به گوگل درایو
            await _driveService.UploadFileAsync(tempFile, "passwords.enc");
        }

        public async Task<List<PasswordEntry>> DownloadPasswordsAsync()
        {
            // 1. دانلود فایل رمزنگاری‌شده
            string tempFile = Path.GetTempFileName();
            await _driveService.DownloadFileAsync("passwords.enc", tempFile);

            // 2. خواندن و رمزگشایی (ورودی کلید اضافه شد)
            string encrypted = File.ReadAllText(tempFile);
            string jsonData = _crypto.Decrypt(encrypted, _encryptionKey);

            // 3. برگرداندن لیست
            return JsonConvert.DeserializeObject<List<PasswordEntry>>(jsonData);
        }

    }
}
