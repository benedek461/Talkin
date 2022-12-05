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

namespace Talkin.Assets.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddFriendWindow.xaml
    /// </summary>
    public partial class AddFriendWindow : Window
    {
        public AddFriendWindow()
        {
            InitializeComponent();
        }

        private void buttonRequest_Click(object sender, RoutedEventArgs e)
        {
            var user = new User()
            {
                userName = textBoxUsername.Text
            };

            var userName = user.userName;

            var endpoint = new Uri($"https://localhost:7031/api/User/GetUserByUsername/{userName}");
            var client = APIHelper.client;

            if (textBoxUsername.Text == CurrentUser.currentUser.userName)
            {
                var afew = new AddFriendError();
                afew.labelMessage.Content = "You typed your own username!";
                afew.Show();
            }
            else if (textBoxUsername.Text != "")
            {
                var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
                var friendUser = JsonConvert.DeserializeObject<User>(result);
                if (friendUser.Id == 0)
                {
                    var afew = new AddFriendError();
                    afew.Show();
                }
                else
                {
                    var endpoint2 = new Uri("https://localhost:7031/api/Conversation");
                    var client2 = APIHelper.client;

                    var idInList = new List<int>() { friendUser.Id };
                    var newPostJson = JsonConvert.SerializeObject(idInList);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result2 = client2.PostAsync(endpoint2, payload).Result.Content.ReadAsStringAsync().Result;
                    var afsw = new AddFriendSuccessful();
                    afsw.Show();
                }
            }
            else
            {
                var afew = new AddFriendError();
                afew.labelMessage.Content = "You left the field empty!";
                afew.Show();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
