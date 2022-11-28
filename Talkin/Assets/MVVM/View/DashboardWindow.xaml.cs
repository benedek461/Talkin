using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Talkin.Assets.Helpers;
using Talkin.Assets.MVVM.Models;
//using Talkin.Assets.MVVM.Models;

namespace Talkin.Assets.MVVM.View
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
            labelCurrentUserUsername.Content = CurrentUser.currentUser.userName;
            labelCurrentUserID.Content = "ID Number: " + CurrentUser.currentUser.Id;
            if (CurrentUser.currentUser.Sex == "Male")
            {
                imageProfileImage.Source = 
                    new BitmapImage(new Uri(@"/Assets/Themes/Icons/man.png", UriKind.Relative));
            }
            else
            {
                imageProfileImage.Source =
                    new BitmapImage(new Uri(@"/Assets/Themes/Icons/beauty.png", UriKind.Relative));
            }
        }

        private void buttonOpenSettings_Click(object sender, RoutedEventArgs e)
        {
            MainSettingsWindow msw = new MainSettingsWindow();
            msw.Show();
        }

        private void buttonToggleFullscreen_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            buttonLogout_Click(sender, e);
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonAddFriend_Click(object sender, RoutedEventArgs e)
        {
            AddFriendWindow afw = new AddFriendWindow();
            afw.Show();
        }

        private async void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            var endpoint = new Uri("https://localhost:7031/api/Account/logout");

            HttpClient client = APIHelper.client;

            var result = await client.DeleteAsync(endpoint);

            if (result.IsSuccessStatusCode)
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                
                Token.CurrentUserJWTToken = string.Empty;

                User user = new User();
                CurrentUser.currentUser = user;
                
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private async Task<List<User>> AllConversationConnection()
        {
            List<User> allConversationConnection = new List<User>();

            var endpoint = new Uri("https://localhost:7031/api/User/Me");
            var client = APIHelper.client;

            var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;

            var me = JsonConvert.DeserializeObject<User>(result);


            Console.WriteLine(" ");
            

            foreach (int conversationId in me.ConversationIds)
            {
                var endpoint2 = new Uri($"https://localhost:7031/api/Conversation/{conversationId}");
                var client2 = APIHelper.client;
                var result2 = client2.GetAsync(endpoint2).Result.Content.ReadAsStringAsync().Result;
                var conversationJson = JsonConvert.DeserializeObject<Message>(result2);
                var friendId = conversationJson.partitioners[0];
                if (friendId == me.Id)
                {
                    friendId = conversationJson.partitioners[1];
                }

                var endpoint3 = new Uri($"https://localhost:7031/api/User/SpecificUserById/{friendId}");
                var client3 = APIHelper.client;

                var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                var friendUser = JsonConvert.DeserializeObject<User>(result3);

                allConversationConnection.Add(friendUser);
            }

            return allConversationConnection;
        }

    }
}
