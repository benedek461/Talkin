using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
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
            //string username = "";
            var endpoint = new Uri("https://localhost:7031/api/Account/login");

            var client = APIHelper.client;
            
            var user = new User()
            {
                userName = textBoxUsername.Text,
                Password = passwordBoxUsername.Password
            };

            if (textBoxUsername.Text != "" && passwordBoxUsername.Password != "")
            {
                var newPostJson = JsonConvert.SerializeObject(user);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                if (result != null)
                {
                    if (result == "Wrong username or password!")
                    {
                        LoginError le = new LoginError();
                        le.Show();
                    }
                    else
                    {
                        Token.CurrentUserJWTToken = result;

                        GetAndSetCurrentUser(Token.CurrentUserJWTToken);

                        MessageBox.Show($"Id: {CurrentUser.currentUser.Id}\n" +
                                        $"Email: {CurrentUser.currentUser.Email}\n" +
                                        $"Username: {CurrentUser.currentUser.userName}\n" +
                                        $"Password: {CurrentUser.currentUser.Password}\n" +
                                        $"FirstName: {CurrentUser.currentUser.firstName}\n" +
                                        $"LastName: {CurrentUser.currentUser.lastName}\n" +
                                        $"Birthday: {CurrentUser.currentUser.Birthday}");

                        DashboardWindow dw = new DashboardWindow();
                        dw.Show();
                    }
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
        
        private void GetAndSetCurrentUser(string token)
        {
            var endpoint = new Uri("https://localhost:7031/api/User/Me");
            var client = APIHelper.client;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + " " + token);

            var result = client.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;

            dynamic jsonData = JObject.Parse(json);

            User user = new User()
            {
                Id = Convert.ToInt32(jsonData["id"]),
                Email = jsonData["email"],
                userName = jsonData["userName"],
                Password = jsonData["password"],
                firstName = jsonData["firstName"],
                lastName = jsonData["lastName"],
                Birthday = jsonData["birthday"],
                Sex = jsonData["sex"]
            };

            CurrentUser.currentUser = user;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
