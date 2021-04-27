using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BakuBus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Key { get; set; } = "d4EDilYSWp1wt8hiSYA5~IOGy6hBBw2prepa5AUA-nQ~Ajn5kkQeHiwtalBf9qTZP_3FqMCq_jC25IdbJFgMMOd3007UawO2HKDkXyeIYLl0";
        public ApplicationIdCredentialsProvider Provider { get; set; }
        public MainWindow()
        {
            this.DataContext = this;
            Provider = new ApplicationIdCredentialsProvider(Key);
            InitializeComponent();
            GetBusses();
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                lbl.Content = DateTime.Now.ToLongTimeString();

            });
        }

        public Timer Timer { get; set; }
        public void GetBusses()
        {
            var client = new HttpClient();
            var link = "https://www.bakubus.az/az/ajax/apiNew1";
            dynamic busses = JsonConvert.DeserializeObject(client.GetAsync(link)
                .Result.Content.ReadAsStringAsync().Result);
            foreach (var item in busses.BUS)
            {
                dynamic bus = item["@attributes"];
                string name = bus["DRIVER_NAME"];
            }
        }
    }
}
