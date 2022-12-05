using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            FillDateAttributes(this);
            SetDefaultDateAttributes(this);
        }

        private void FillDateAttributes(RegistrationWindow rw)
        {
            for (int i = DateTime.Today.Year; i >= 1900; i--)
            {
                rw.comboBoxYear.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                {
                    string month = "0" + i.ToString();
                    rw.comboBoxMonth.Items.Add(month);
                }
                else
                {
                    rw.comboBoxMonth.Items.Add(i.ToString());
                }
            }
        }

        private void SetDefaultDateAttributes(RegistrationWindow rw)
        {
            rw.comboBoxMonth.SelectedItem = rw.comboBoxMonth.Items.GetItemAt(0);
            rw.comboBoxMonth.SelectedItem = rw.comboBoxMonth.Items.GetItemAt(0);
            rw.comboBoxDay.SelectedItem = rw.comboBoxDay.Items.GetItemAt(0);
            rw.comboBoxSex.SelectedItem = rw.comboBoxSex.Items.GetItemAt(0);
        }

        private void CalculateDays(string selectedMonth)
        {
            switch (selectedMonth)
            {
                case "01":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "02":
                    comboBoxDay.Items.Clear();
                    string selectedYear = comboBoxYear.SelectedItem.ToString();
                    if (Int32.Parse(selectedYear) % 4 == 0)
                    {
                        for (int i = 1; i <= 29; i++)
                        {
                            if (i < 10)
                            {
                                string day = "0" + i.ToString();
                                comboBoxDay.Items.Add(day);
                            }
                            else
                            {
                                comboBoxDay.Items.Add(i.ToString());
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= 28; i++)
                        {
                            if (i < 10)
                            {
                                string day = "0" + i.ToString();
                                comboBoxDay.Items.Add(day);
                            }
                            else
                            {
                                comboBoxDay.Items.Add(i.ToString());
                            }
                        }
                    }
                    break;
                case "03":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "04":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 30; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "05":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "06":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 30; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "07":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "08":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "09":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 30; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "10":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "11":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 30; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxMonth.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
                case "12":
                    comboBoxDay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                        {
                            string day = "0" + i.ToString();
                            comboBoxDay.Items.Add(day);
                        }
                        else
                        {
                            comboBoxDay.Items.Add(i.ToString());
                        }
                    }
                    break;
            }
        }

        private void comboBoxMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMonth = comboBoxMonth.SelectedItem.ToString();
            CalculateDays(selectedMonth);

            
        }

        private void comboBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxMonth.SelectedIndex >= 0)
            {
                string selectedMonth = comboBoxMonth.SelectedItem.ToString();
                CalculateDays(selectedMonth);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool isRegistrationValid()
        {
            //Minden mező ki van töltve?
            ComboBoxItem cbi = (ComboBoxItem)comboBoxSex.SelectedItem;
            string selectedSex = cbi.Content.ToString();
            if (textBoxUsername.Text == "" | textBoxPassword.Password == "" | selectedSex == "" | textBoxEmail.Text == "" | textBoxFirstname.Text == "" |
                        textBoxLastname.Text == "" | comboBoxMonth.SelectedValue.ToString() == "" |
                        comboBoxDay.SelectedValue.ToString() == "")
            {
                RegistrationError re = new RegistrationError();
                re.labelErrorMessage.Content = "You left one or more field empty!";
                re.Show();
                return false;
            }
            //Egyeznek a jelszavak?
            else if (!(textBoxPassword.Password == textBoxConfirmPassword.Password))
            {
                RegistrationError re = new RegistrationError();
                re.labelErrorMessage.Content = "Passwords doesn't match!";
                re.Show();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = APIHelper.client;
            var endpoint = new Uri("https://localhost:7031/api/User/Register");

            ComboBoxItem cbi = (ComboBoxItem)comboBoxSex.SelectedItem;
            var newUser = new User()
            {
                Email = textBoxEmail.Text,
                userName = textBoxUsername.Text,
                Password = textBoxPassword.Password,
                firstName = textBoxFirstname.Text,
                lastName = textBoxLastname.Text,
                Birthday = comboBoxYear.SelectedValue.ToString() + "/" +
                              comboBoxMonth.SelectedValue.ToString() + "/" +
                              comboBoxDay.SelectedValue.ToString(),
                Sex = cbi.Content.ToString(),
            };
            
            if (isRegistrationValid())
            {
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newUser);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                    RegistrationSuccessful rs = new RegistrationSuccessful();
                    rs.labelMessage.Content = "Registration was successful!";
                    rs.Show();
                    
                    this.Close();
                }
                catch 
                {
                    NoInternetErrorMessageWindow niemw = new NoInternetErrorMessageWindow();
                    niemw.Show();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
