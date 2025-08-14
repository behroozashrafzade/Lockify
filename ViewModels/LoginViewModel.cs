using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lockify.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin, () => !IsBusy);
        }

        private async void OnLogin()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                // TODO: اتصال به سرویس احراز هویت
                await Task.Delay(1500); // شبیه‌سازی درخواست سرور
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
