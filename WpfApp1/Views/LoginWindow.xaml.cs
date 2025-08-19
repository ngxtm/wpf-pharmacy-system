using System.Windows;
using WpfApp1.ViewModels;
using System.Windows.Controls;
using DAL.Entities;
using BLL.Services;
using DAL.Repositories;

namespace WpfApp1.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            var dbContext = new DAL.Entities.PharmacyDbContext();
            var accountRepository = new DAL.Repositories.AccountRepository(dbContext);
            var authService = new BLL.Services.AuthService(accountRepository);

            DataContext = new LoginViewModel(authService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is WpfApp1.ViewModels.LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        


    }
} 