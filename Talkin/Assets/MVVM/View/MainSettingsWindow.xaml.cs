using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Talkin.Assets.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainSettingsWindow.xaml
    /// </summary>
    public partial class MainSettingsWindow : Window
    {

        public MainSettingsWindow()
        {
            InitializeComponent();
            ShowAppearanceSettings();
            SolidColorBrush orange = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9b233"));
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            this.buttonSettingsAppearance.Background = orange;

        }

        public void ShowAppearanceSettings()
        {
            this.stackPanelSettings.Children.Clear();
            this.stackPanelSettings.Children.Add(this.settingWindowDarkMode);
        }

        public void ShowUserSettings()
        {
            this.stackPanelSettings.Children.Clear();
            this.stackPanelSettings.Children.Add(this.settingWindowUsername);
            this.stackPanelSettings.Children.Add(this.settingWindowPassword);
            this.stackPanelSettings.Children.Add(this.settingWindowDeleteAccount);
        }

        public void ShowUserInfo()
        {
            this.stackPanelSettings.Children.Clear();
            this.stackPanelSettings.Children.Add(this.settingWindowUserInfo);

        }
        
        public void ShowAbout()
        {
            this.stackPanelSettings.Children.Clear();
            this.stackPanelSettings.Children.Add(this.settingWindowAbout);
        }

        private void buttonSettingsAppearance_Click(object sender, RoutedEventArgs e)
        {
            ShowAppearanceSettings();
            SolidColorBrush orange = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9b233"));
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            this.buttonSettingsAppearance.Background = orange;
            this.buttonSettingsUser.Background = white;
            this.buttonSettingsAbout.Background = white;
            this.buttonSettingsUserInfo.Background = white;
        }

        private void buttonSettingsUser_Click(object sender, RoutedEventArgs e)
        {
            ShowUserSettings();
            SolidColorBrush orange = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9b233"));
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            this.buttonSettingsUser.Background = orange;
            this.buttonSettingsAppearance.Background = white;
            this.buttonSettingsAbout.Background = white;
            this.buttonSettingsUserInfo.Background = white;
        }

        private void buttonSettingsUserInfo_Click(object sender, RoutedEventArgs e)
        {
            ShowUserInfo();
            SolidColorBrush orange = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9b233"));
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            this.buttonSettingsUserInfo.Background = orange;
            this.buttonSettingsUser.Background = white;
            this.buttonSettingsAbout.Background = white;
            this.buttonSettingsAppearance.Background = white;
        }

        private void buttonSettingsAbout_Click(object sender, RoutedEventArgs e)
        {
            ShowAbout();
            SolidColorBrush orange = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9b233"));
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            this.buttonSettingsUserInfo.Background = white;
            this.buttonSettingsUser.Background = white;
            this.buttonSettingsAbout.Background = orange;
            this.buttonSettingsAppearance.Background = white;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            AreYouSureWindow aysWindow = new AreYouSureWindow();
            aysWindow.Show();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
