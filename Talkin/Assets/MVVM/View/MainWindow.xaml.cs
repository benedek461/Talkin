using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string connectionString = "Data Source=DESKTOP-4EFJV65\\SQLEXPRESS;Initial Catalog=TalkinDatabase;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                
                string query = "SELECT COUNT(1) FROM [User] WHERE username=@username AND password=@password";
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", textBoxUsername.Text);
                command.Parameters.AddWithValue("@password", passwordBoxUsername.Password);
                
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (textBoxUsername.Text == "" || passwordBoxUsername.Password == "")
                {
                    LoginError le = new LoginError();
                    le.labelMessage.Content = "One or more field(s) are empty!";
                    le.Show();
                }
                else if (count == 1)
                {
                    DashboardWindow dashboardWindow = new DashboardWindow();
                    dashboardWindow.Show();
                    Close();
                }
                else
                {
                    LoginError le = new LoginError();
                    le.Show();
                }

            }
            catch
            {
                NoInternetErrorMessageWindow niemw = new NoInternetErrorMessageWindow();
                niemw.Show();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
