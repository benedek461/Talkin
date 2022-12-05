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
    /// Interaction logic for AddFriendSuccessful.xaml
    /// </summary>
    public partial class AddFriendSuccessful : Window
    {
        public AddFriendSuccessful()
        {
            InitializeComponent();
        }

        private void buttonOK_WUOR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
