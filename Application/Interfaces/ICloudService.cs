using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICloudService
    {

        /// User authentication in cloud service

        Task<bool> AuthenticateAsync();


        ///Upload password file to cloud service

        Task UploadFileAsync(string localFilePath, string remoteFileName);


        /// Download password file from cloud service

        Task DownloadFileAsync(string remoteFileName, string localFilePath);


        /// Checking if a file exists on a cloud serviceه
       
        Task<bool> FileExistsAsync(string remoteFileName);
    
    }
}
