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
using BatteryInfo;
using Microsoft.Win32;
using System.Timers;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace BatteryInfo
{

    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private uint oldFadeTime = 0;
        public MainWindow()
        {
            oldFadeTime = PowerManagement.GetVideoTimeoutDC();
            InitializeComponent();
            InitText();
            CreateTimer();
            if (PowerType.Text == "Battery")
            {
                ComboBoxItem item = (ComboBoxItem)fadeCombo.SelectedItem;
                PowerManagement.SetNewVideoTimeoutDC(Convert.ToUInt32((string)item.Content, 10));
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
            PowerManagement.SetNewVideoTimeoutDC(oldFadeTime);
        }

        private void CreateTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            InitText();
        }


        private void InitText()
        {
            PowerType.Text = Battery.GetPowerType();
            Charge.Text = Battery.GetChargeLevel();
            TimeLeft.Text = Battery.GetTime();
        }

        private void FadeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PowerType.Text == "Battery")
            {
                fadeCombo.IsEnabled = true;
                ComboBoxItem item = (ComboBoxItem)fadeCombo.SelectedItem;
                PowerManagement.SetNewVideoTimeoutDC(Convert.ToUInt32((string)item.Content, 10));
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
                if (Battery.GetPowerType() == "Battery")
                {
                    fadeCombo.IsEnabled = true;
                    ComboBoxItem item = (ComboBoxItem)fadeCombo.SelectedItem;
                    PowerManagement.SetNewVideoTimeoutDC(Convert.ToUInt32((string)item.Content, 10));
                }
                else
                {
                    fadeCombo.IsEnabled = false;
                    PowerManagement.SetNewVideoTimeoutDC(oldFadeTime);
                }
            }
        }


    }
}
