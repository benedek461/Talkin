using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Talkin.Assets.MVVM.View
{
    /// <summary>
    /// Interaction logic for SplashScreenRestart.xaml
    /// </summary>
    public partial class SplashScreenRestart : Window
    {
        DispatcherTimer dT = new DispatcherTimer();

        public SplashScreenRestart()
        {
            InitializeComponent();

            dT.Tick += new EventHandler(dT_Tick);
            dT.Interval = new TimeSpan(0, 0, 2);
            dT.Start();
        }

        private void dT_Tick(object sender, EventArgs e)
        {
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();

            dT.Stop();
            this.Close();
        }
    }
}
