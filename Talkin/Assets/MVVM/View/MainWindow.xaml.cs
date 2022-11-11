using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Talkin.Assets.Helpers;
using Talkin.Assets.MVVM.Models;

namespace Talkin.Assets.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            MainSettingsWindow msw = new MainSettingsWindow();
            msw.Show();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            rw.Show();
        }

        private async void buttonSignIn_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://localhost:7063/api/User/Login";

            var client = APIHelper.client;
            
            var user = new User()
            {
                userName = textBoxUsername.Text,
                password = passwordBoxUsername.Password
            };

            if (textBoxUsername.Text != "" && passwordBoxUsername.Password != "")
            {
                var response = await client.PostAsJsonAsync(url, user);

                if (response.IsSuccessStatusCode)
                {
                    DashboardWindow dw = new DashboardWindow();
                    dw.Show();
                }
                else
                {
                    NoInternetErrorMessageWindow niemw = new NoInternetErrorMessageWindow();
                    niemw.Show();
                }
            }
            else
            {
                LoginError le = new LoginError();
                le.labelMessage.Content = "One or more field(s) are empty!";
                le.Show();
            } 
        }
    }
}
