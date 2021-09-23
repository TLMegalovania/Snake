using System.Timers;
using System.Windows;

namespace Snake
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Timer timer;
        readonly EventPipeline pipeline;
        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(500);
            pipeline = new EventPipeline(gameWindow, gameGrid);
            timer.Elapsed += Tick;
            timer.Start();
        }
        void Tick(object s, ElapsedEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                if (!pipeline.StartPipeline(out int score))
                {
                    timer.Stop();
                    timer.Dispose();
                    _ = MessageBox.Show(this, $"得分：{score}", "游戏结束");
                    Close();
                }
            });
        }
    }
}
