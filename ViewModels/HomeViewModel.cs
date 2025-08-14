using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lockify.ViewModels
{
    public class HomeViewModel :BaseViewModel
    {

        public ObservableCollection<string> QuickActions { get; } = new();

        private string _welcomeText;
        public string WelcomeText
        {
            get => _welcomeText;
            set => SetProperty(ref _welcomeText, value);
        }

        public ICommand NavigateToPasswordsCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand NavigateToAddPasswordCommand { get; }

        public HomeViewModel()
        {
            WelcomeText = "خوش آمدید به Password Manager";

            QuickActions.Add("📄 لیست پسوردها");
            QuickActions.Add("➕ اضافه کردن پسورد");
            QuickActions.Add("⚙️ تنظیمات");

            NavigateToPasswordsCommand = new Command(OnNavigateToPasswords);
            NavigateToSettingsCommand = new Command(OnNavigateToSettings);
            NavigateToAddPasswordCommand = new Command(OnNavigateToAddPassword);
        }

        private void OnNavigateToPasswords()
        {
            // TODO: هدایت به صفحه لیست پسوردها
        }

        private void OnNavigateToSettings()
        {
            // TODO: هدایت به صفحه تنظیمات
        }

        private void OnNavigateToAddPassword()
        {
            // TODO: هدایت به صفحه اضافه کردن پسورد
        }

    }
}
