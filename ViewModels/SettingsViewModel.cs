using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lockify.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private bool _syncWithGoogleDrive;
        public bool SyncWithGoogleDrive
        {
            get => _syncWithGoogleDrive;
            set => SetProperty(ref _syncWithGoogleDrive, value);
        }

        private string _encryptionKey;
        public string EncryptionKey
        {
            get => _encryptionKey;
            set => SetProperty(ref _encryptionKey, value);
        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand ConnectGoogleDriveCommand { get; }

        public SettingsViewModel()
        {
            SaveSettingsCommand = new Command(OnSaveSettings);
            ConnectGoogleDriveCommand = new Command(OnConnectGoogleDrive);
        }

        private void OnSaveSettings()
        {
            // TODO: ذخیره تنظیمات در حافظه محلی
        }

        private void OnConnectGoogleDrive()
        {
            // TODO: فراخوانی سرویس اتصال به Google Drive
        }


    }
}
