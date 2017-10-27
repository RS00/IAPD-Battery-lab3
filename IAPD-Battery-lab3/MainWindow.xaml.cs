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
        private uint oldFadeTime = 0;
        public MainWindow()
        {
            oldFadeTime = PowerManagement.getVideoTimeoutDC();
            InitializeComponent();
            initText();
            createTimer();
            if (PowerType.Text == "Battery")
            {
                ComboBoxItem item = (ComboBoxItem)fadeCombo.SelectedItem;
                PowerManagement.setNewVideoTimeoutDC(Convert.ToUInt32((string)item.Content, 10));
            }
            else
            {
                fadeCombo.IsEnabled = false;
            }
            //initBrightness();
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
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

        private void fadeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PowerType.Text == "Battery")
            {
                fadeCombo.IsEnabled = true;
                ComboBoxItem item = (ComboBoxItem)fadeCombo.SelectedItem;
                PowerManagement.setNewVideoTimeoutDC(Convert.ToUInt32((string)item.Content, 10));
            }
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



        void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.StatusChange)
            {
                if (Battery.getPowerType() == "Battery")
                {
                    fadeCombo.IsEnabled = true;
                    ComboBoxItem item = (ComboBoxItem)fadeCombo.SelectedItem;
                    PowerManagement.setNewVideoTimeoutDC(Convert.ToUInt32((string)item.Content, 10));
                }
                else
                {
                    fadeCombo.IsEnabled = false;
                    PowerManagement.setNewVideoTimeoutDC(oldFadeTime);
                }
            }
        }


    }
}
