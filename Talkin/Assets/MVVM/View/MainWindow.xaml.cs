using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private void buttonSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = "";
            var endpoint = new Uri("https://localhost:44394/login");
            var endpoint2 = new Uri($"https://localhost:44394/getUser/{username}");

            var client = APIHelper.client;
            
            var user = new User()
            {
                userName = textBoxUsername.Text,
                password = passwordBoxUsername.Password
            };


            if (textBoxUsername.Text != "" && passwordBoxUsername.Password != "")
            {
                var newPostJson = JsonConvert.SerializeObject(user);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                dynamic jsonData = JObject.Parse(result);


                if (result != null)
                {
                    Properties.Settings.Default.UserToken = jsonData.accessToken;
                    MessageBox.Show(Properties.Settings.Default.UserToken);

                    /*
                    username = user.userName;
                    var result2 = client.GetAsync(endpoint2).Result;
                    var json = result2.Content.ReadAsStringAsync().Result;

                    dynamic jsonData2 = JObject.Parse(json);

                    User currentUser = new User
                    {
                        userName = jsonData2.username,
                        sex = jsonData2.sex,
                        password = passwordBoxUsername.Password,
                        email = jsonData2.email,
                        firstName = jsonData2.firstName,
                        lastName = jsonData2.lastName,
                        dateOfBirth = jsonData2.dateOfBirth
                    };

                    Globals.LoggedInUser = currentUser;
                    */

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
