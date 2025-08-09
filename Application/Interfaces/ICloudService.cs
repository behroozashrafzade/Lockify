using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICloudService
    {
        /// <summary>
        /// احراز هویت کاربر در سرویس ابری
        /// </summary>
        Task<bool> AuthenticateAsync();

        /// <summary>
        /// آپلود فایل پسوردها به سرویس ابری
        /// </summary>
        Task UploadFileAsync(string localFilePath, string remoteFileName);

        /// <summary>
        /// دانلود فایل پسوردها از سرویس ابری
        /// </summary>
        Task DownloadFileAsync(string remoteFileName, string localFilePath);

        /// <summary>
        /// بررسی اینکه آیا فایل روی سرویس ابری وجود دارد یا نه
        /// </summary>
        Task<bool> FileExistsAsync(string remoteFileName);
    
    }
}
