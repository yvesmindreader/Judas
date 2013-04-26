using System.Windows;
namespace Judas
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        PlayGameScene newGameScene = new PlayGameScene();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }
       
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gameboard.DataContext = newGameScene.JudasBlocks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newGameScene = new PlayGameScene();
            gameboard.DataContext = newGameScene.JudasBlocks;
        }       
        
    }

}
