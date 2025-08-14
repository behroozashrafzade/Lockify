using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lockify.ViewModels
{
    public class PasswordDetailViewModel :BaseViewModel
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand SaveCommand { get; }

        public PasswordDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
        }

        private void OnSave()
        {
            // TODO: ذخیره اطلاعات پسورد در دیتابیس
        }

    }
}
