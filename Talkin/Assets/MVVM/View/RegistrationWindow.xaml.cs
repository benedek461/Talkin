using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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

        public static void FillDateAttributes(RegistrationWindow rw)
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

        public static void SetDefaultDateAttributes(RegistrationWindow rw)
        {
            rw.comboBoxMonth.SelectedItem = rw.comboBoxMonth.Items.GetItemAt(0);
            rw.comboBoxMonth.SelectedItem = rw.comboBoxMonth.Items.GetItemAt(0);
            rw.comboBoxDay.SelectedItem = rw.comboBoxDay.Items.GetItemAt(0);
        }

        public void CalculateDays(string selectedMonth)
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

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
