using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Snake
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        EventPipeline pipeline;
        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(500);
            pipeline = new EventPipeline(gameWindow, gameGrid);
            timer.Elapsed += Tick;
            timer.Start();
        }
        void Tick(object s,ElapsedEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                if (!pipeline.StartPipeline())
                {
                    timer.Stop();
                    timer.Dispose();
                    Close();
                }
            });
        }
    }
}
