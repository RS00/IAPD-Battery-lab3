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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using IAPD_Battery_lab3;
using Microsoft.Win32;
using System.Timers;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace IAPD_Battery_lab3
{

    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private uint fadeTime = 60;
        private uint oldFadeTime;
        public MainWindow()
        {
            InitializeComponent();
            initText();
            createTimer();
            oldFadeTime = PowerManagement.getVideoTimeoutDC();
            PowerManagement.setNewVideoTimeoutDC(fadeTime);
            /*initBrightness();
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;*/
        }

        ~MainWindow()
        {
            PowerManagement.setNewVideoTimeoutDC(oldFadeTime);
        }

        private void createTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            initText();
        }


        private void initText()
        {
            PowerType.Text = Battery.getPowerType();
            Charge.Text = Battery.getChargeLevel();
            TimeLeft.Text = Battery.getTime();
        }

        /*private static void initBrightness()
        {
            if (Battery.getPowerType() == "AC")
            {
                Monitor.SetBrightness(100);
            }
            else
            {
                Monitor.SetBrightness(20);
            }
        }*/



        /*static void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.StatusChange)
            {
                initBrightness();
            }
        }*/

        
    }
}
