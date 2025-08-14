using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lockify.ViewModels
{
    public class PasswordListViewModel :BaseViewModel
    {
        public ObservableCollection<string> Passwords { get; } = new();

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ICommand RefreshCommand { get; }
        public ICommand AddPasswordCommand { get; }

        public PasswordListViewModel()
        {
            RefreshCommand = new Command(OnRefresh);
            AddPasswordCommand = new Command(OnAddPassword);
        }

        private async void OnRefresh()
        {
            IsRefreshing = true;
            await Task.Delay(1000); // شبیه‌سازی بارگذاری
            Passwords.Clear();
            Passwords.Add("Email - mymail@gmail.com");
            Passwords.Add("GitHub - githubpass123");
            Passwords.Add("Bank - securepass!@#");
            IsRefreshing = false;
        }

        private void OnAddPassword()
        {
            // TODO: هدایت به صفحه اضافه کردن پسورد
        }

    }
}
