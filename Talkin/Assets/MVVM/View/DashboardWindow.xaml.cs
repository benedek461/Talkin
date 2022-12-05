using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        private int currentConversation;
        HubConnection connection;

        public DashboardWindow()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7031/chatHub",
                (HttpConnectionOptions options) => options.Headers.Add("Authorization", "Bearer " + Token.CurrentUserJWTToken))
                .WithAutomaticReconnect()
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<int, string, string>("ReceiveMessage", (convId, userIdentifier, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (convId == currentConversation)
                    {
                        RowDefinition rd = new RowDefinition();
                        rd.Height = GridLength.Auto;
                        gridChatMessages.RowDefinitions.Add(rd);

                        Border b = new Border();
                        b.Style = Application.Current.TryFindResource("WindowDesign") as Style;
                        b.Height = double.NaN;
                        b.Background = new SolidColorBrush(Colors.White);
                        b.HorizontalAlignment = HorizontalAlignment.Left;
                        b.Margin = new Thickness(0, 5, 0, 5);

                        StackPanel sp = new StackPanel();
                        b.Child = sp;

                        TextBlock tb = new TextBlock();
                        tb.Text = message;
                        tb.VerticalAlignment = VerticalAlignment.Center;
                        tb.FontFamily = new FontFamily("laCartoonerie");
                        tb.FontSize = 20;
                        tb.Padding = new Thickness(20, 10, 20, 10);
                        tb.TextWrapping = TextWrapping.Wrap;
                        sp.Children.Add(tb);

                        Grid.SetColumn(b, 0);
                        Grid.SetRow(b, (gridChatMessages.RowDefinitions.Count - 1));
                        gridChatMessages.Children.Add(b);
                    }
                });
            });

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
            borderChat.Visibility = Visibility.Hidden;
            imageCurrentChatPartnerPicture.Visibility = Visibility.Hidden;
            labelCurrentChatPartnerUserName.Visibility = Visibility.Hidden;
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

                SplashScreenLogout ssl = new SplashScreenLogout();
                ssl.Show();

                this.Close();
            }
        }

        private List<User> AllConversationConnection()
        {
            List<User> allConversationConnection = new List<User>();

            var endpoint = new Uri("https://localhost:7031/api/User/Me");
            var client = APIHelper.client;

            var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;

            var me = JsonConvert.DeserializeObject<User>(result);


            Console.WriteLine(" ");


            foreach (int conversationId in me.ConversationIds)
            {
                var endpoint2 = new Uri($"https://localhost:7031/api/Conversation/GetConversationByIdAsync/{conversationId}");
                //var client2 = APIHelper.client;
                var result2 = client.GetAsync(endpoint2).Result.Content.ReadAsStringAsync().Result;
                var conversationJson = JsonConvert.DeserializeObject<Conversation>(result2);
                int friendId = conversationJson.partitioners[0];
                if (friendId == me.Id)
                {
                    friendId = conversationJson.partitioners[1];
                }

                var endpoint3 = new Uri($"https://localhost:7031/api/User/GetUserById/{friendId}");
                //var client3 = APIHelper.client;

                var result3 = client.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                var friendUser = JsonConvert.DeserializeObject<User>(result3);

                allConversationConnection.Add(friendUser);
            }

            return allConversationConnection;
        }

        private void AddFriendCard(StackPanel stackPanel, int id, string username, string sex)
        {
            Button btn = new Button();
            btn.Style = Application.Current.TryFindResource("FriendChatButtonDesign") as Style;
            btn.Background = new SolidColorBrush(Colors.White);
            btn.HorizontalContentAlignment = HorizontalAlignment.Left;
            btn.Margin = new Thickness(0, 0, 0, 5);
            btn.Cursor = Cursors.Hand;
            btn.Click += (sender2, e2) => buttonFriendCard_Click(sender2, e2, id, username, sex);

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Margin = new Thickness(5, 5, 5, 5);

            Image profileImage = new Image();
            if (sex == "Male")
            {
                profileImage.Source = new BitmapImage(new Uri(@"/Assets/Themes/Icons/man.png", UriKind.Relative));
            }
            else if (sex == "Female")
            {
                profileImage.Source = new BitmapImage(new Uri(@"/Assets/Themes/Icons/beauty.png", UriKind.Relative));
            }
            profileImage.Width = 50;
            profileImage.Height = 50;
            sp.Children.Add(profileImage);

            StackPanel sp2 = new StackPanel();
            sp2.VerticalAlignment = VerticalAlignment.Center;
            sp.Children.Add(sp2);

            Label labelUsername = new Label();
            labelUsername.Content = username;
            labelUsername.FontSize = 20;
            labelUsername.Margin = new Thickness(5, 0, 0, 0);
            sp2.Children.Add(labelUsername);

            btn.Content = sp;

            stackPanel.Children.Add(btn);
        }

        private void buttonFriendCard_Click(object sender, RoutedEventArgs e, int id, string username, string sex)
        {
            

            try
            {
                gridChatMessages.Children.Clear();

                borderChat.Visibility = Visibility.Visible;

                if (sex == "Male")
                {
                    imageCurrentChatPartnerPicture.Source =
                        new BitmapImage(new Uri(@"/Assets/Themes/Icons/man.png", UriKind.Relative));
                }
                else if (sex == "Female")
                {
                    imageCurrentChatPartnerPicture.Source =
                        new BitmapImage(new Uri(@"/Assets/Themes/Icons/beauty.png", UriKind.Relative));
                }
                imageCurrentChatPartnerPicture.Visibility = Visibility.Visible;

                labelCurrentChatPartnerUserName.Content = username;
                labelCurrentChatPartnerUserName.Visibility = Visibility.Visible;

                var endpoint = new Uri($"https://localhost:7031/api/Conversation/Ids/{id}");

                HttpClient client = APIHelper.client;

                var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
                var conversationIdArray = JsonConvert.DeserializeObject<int[]>(result);
                int conversationId = conversationIdArray[0];
                currentConversation = conversationId;

                var endpoint2 = new Uri($"https://localhost:7031/api/Conversation/GetConversationByIdAsync/{conversationId}");

                var result2 = client.GetAsync(endpoint2).Result.Content.ReadAsStringAsync().Result;
                var conversationJson = JsonConvert.DeserializeObject<Conversation>(result2);
                
                gridChatMessages.RowDefinitions.Clear();
                gridChatMessages.ColumnDefinitions.Clear();

                ColumnDefinition cd0 = new ColumnDefinition();
                ColumnDefinition cd1 = new ColumnDefinition();
                cd0.Width = new GridLength(1, GridUnitType.Star);
                cd1.Width = new GridLength(1, GridUnitType.Star);
                gridChatMessages.ColumnDefinitions.Add(cd0);
                gridChatMessages.ColumnDefinitions.Add(cd1);

                for (int i = 0; i < conversationJson.messages.Count; i++)
                {
                    RowDefinition rd = new RowDefinition();
                    rd.Height = GridLength.Auto;
                    gridChatMessages.RowDefinitions.Add(rd);
                }

                for (int i = 0; i < conversationJson.messages.Count; i++)
                {
                    if (conversationJson.messages[i].SenderId == id)
                    {
                        /*
                        <Border Style="{StaticResource WindowDesign}"
                                        Grid.Column="0" Grid.Row="0"
                                        Height="auto"
                                        Background="White"
                                        HorizontalAlignment="Left"
                                        Margin="0 0 0 5">
                                    <TextBlock Text="Na csá!" TextWrapping="Wrap" VerticalAlignment="Center" FontFamily="laCartoonerie" FontSize="20" Padding="20 10 20 10"/>
                        */
                        Border b = new Border();
                        b.Style = Application.Current.TryFindResource("WindowDesign") as Style;
                        b.Height = double.NaN;
                        b.Background = new SolidColorBrush(Colors.White);
                        b.HorizontalAlignment = HorizontalAlignment.Left;
                        b.Margin = new Thickness(0, 5, 0, 5);

                        StackPanel sp = new StackPanel();
                        b.Child = sp;

                        TextBlock tb = new TextBlock();
                        tb.Text = conversationJson.messages[i].Text;
                        tb.VerticalAlignment = VerticalAlignment.Center;
                        tb.FontFamily = new FontFamily("laCartoonerie");
                        tb.FontSize = 20;
                        tb.Padding = new Thickness(20, 10, 20, 10);
                        tb.TextWrapping = TextWrapping.Wrap;
                        sp.Children.Add(tb);

                        Grid.SetColumn(b, 0);
                        Grid.SetRow(b, i);
                        gridChatMessages.Children.Add(b);
                    }
                    else
                    {
                        Border b = new Border();
                        b.Style = Application.Current.TryFindResource("WindowDesign") as Style;
                        b.Height = double.NaN;
                        b.Background = new SolidColorBrush(Color.FromRgb(249, 178, 51));
                        b.HorizontalAlignment = HorizontalAlignment.Right;
                        b.Margin = new Thickness(0, 5, 0, 5);

                        StackPanel sp = new StackPanel();
                        b.Child = sp;

                        TextBlock tb = new TextBlock();
                        tb.Text = conversationJson.messages[i].Text;
                        tb.VerticalAlignment = VerticalAlignment.Center;
                        tb.FontFamily = new FontFamily("laCartoonerie");
                        tb.FontSize = 20;
                        tb.Padding = new Thickness(20, 10, 20, 10);
                        tb.TextWrapping = TextWrapping.Wrap;
                        sp.Children.Add(tb);

                        Grid.SetColumn(b, 1);
                        Grid.SetRow(b, i);
                        gridChatMessages.Children.Add(b);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                CurrentUser.friends = AllConversationConnection();
                stackPanelFriends.Children.Clear();
                foreach (var i in CurrentUser.friends)
                {
                    AddFriendCard(stackPanelFriends, i.Id, i.userName, i.Sex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.friends = AllConversationConnection();
            stackPanelFriends.Children.Clear();
            foreach (var i in CurrentUser.friends)
            {
                AddFriendCard(stackPanelFriends, i.Id, i.userName, i.Sex);
            }
        }

        private async void buttonSendMessage_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", currentConversation, textBoxChatMessageToSend.Text);
                RowDefinition rd = new RowDefinition();
                rd.Height = GridLength.Auto;
                gridChatMessages.RowDefinitions.Add(rd);

                Border b = new Border();
                b.Style = Application.Current.TryFindResource("WindowDesign") as Style;
                b.Height = double.NaN;
                b.Background = new SolidColorBrush(Color.FromRgb(249, 178, 51));
                b.HorizontalAlignment = HorizontalAlignment.Right;
                b.Margin = new Thickness(0, 5, 0, 5);

                StackPanel sp = new StackPanel();
                b.Child = sp;

                TextBlock tb = new TextBlock();
                tb.Text = textBoxChatMessageToSend.Text;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.FontFamily = new FontFamily("laCartoonerie");
                tb.FontSize = 20;
                tb.Padding = new Thickness(20, 10, 20, 10);
                tb.TextWrapping = TextWrapping.Wrap;
                sp.Children.Add(tb);

                Grid.SetColumn(b, 1);
                Grid.SetRow(b, (gridChatMessages.RowDefinitions.Count - 1));
                gridChatMessages.Children.Add(b);

                textBoxChatMessageToSend.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSearchFriend_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxSearchFriend.Text != "")
            {
                bool foundIt = false;
                CurrentUser.friends = AllConversationConnection();
                stackPanelFriends.Children.Clear();
                foreach (var i in CurrentUser.friends)
                {
                    if (i.userName == textBoxSearchFriend.Text)
                    {
                        AddFriendCard(stackPanelFriends, i.Id, i.userName, i.Sex);
                        foundIt = true;
                    }
                }

                if (!foundIt)
                {
                    LoginError le = new LoginError();
                    le.labelMessage.Content = "User not found!";
                    le.Show();
                }
            }
        }

        private void buttonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.friends = AllConversationConnection();
            stackPanelFriends.Children.Clear();
            foreach (var i in CurrentUser.friends)
            {
                AddFriendCard(stackPanelFriends, i.Id, i.userName, i.Sex);
            }
        }

        private void textBoxChatMessageToSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonSendMessage_Click_1(sender, e);
            }
        }

        private void textBoxSearchFriend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonSearchFriend_Click(sender, e);
            }
        }
    }
}
