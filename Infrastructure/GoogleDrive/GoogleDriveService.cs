using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Oauth2.v2;

namespace Infrastructure.GoogleDrive
{
    public class GoogleDriveService : ICloudService
    {
        private DriveService _driveService;
        private readonly string[] _scopes = { DriveService.Scope.DriveFile };
        private const string ApplicationName = "MyPasswordManager";

        public async Task<bool> AuthenticateAsync()
        {
            try
            {
                using var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read);
                string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials/MyPasswordManager");

                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                );
                string userEmail = await GetUserEmailAsync(credential);
                _driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Google Drive authentication failed: {ex.Message}");
                return false;
            }
        }

        public async Task UploadFileAsync(string localFilePath, string remoteFileName)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = remoteFileName
            };

            using var stream = new FileStream(localFilePath, FileMode.Open);
            var request = _driveService.Files.Create(fileMetadata, stream, "application/octet-stream");
            request.Fields = "id";
            await request.UploadAsync();
        }

        public async Task DownloadFileAsync(string remoteFileName, string localFilePath)
        {
            var request = _driveService.Files.List();
            request.Q = $"name = '{remoteFileName}'";
            var result = await request.ExecuteAsync();

            if (result.Files.Count > 0)
            {
                var fileId = result.Files[0].Id;
                var getRequest = _driveService.Files.Get(fileId);

                using var stream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write);
                await getRequest.DownloadAsync(stream);
            }
        }

        public async Task<bool> FileExistsAsync(string remoteFileName)
        {
            var request = _driveService.Files.List();
            request.Q = $"name = '{remoteFileName}'";
            var result = await request.ExecuteAsync();
            return result.Files.Count > 0;


        }

        public async Task<string> GetUserEmailAsync(UserCredential credential)
        {
            var oauth2Service = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });

            Userinfo userInfo = await oauth2Service.Userinfo.Get().ExecuteAsync();
            return userInfo.Email;
        }

    }

}
