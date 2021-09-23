using System.Windows;

namespace Snake
{
    public partial class MainMenu : Window
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
