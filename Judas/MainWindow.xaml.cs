using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
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
            ReloadBlocks();
            gameCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(gameCanvas_MouseLeftButtonDown);
        }
        void gameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckHitBlock(sender, e);
            ReloadBlocks();
        }

        void CheckHitBlock(object sender, MouseButtonEventArgs e)
        {
            //InitializeGame();
            Point mousepos = e.GetPosition(gameCanvas);
            HitTestResult result =  VisualTreeHelper.HitTest(gameCanvas, mousepos);
            int left = (int)Canvas.GetLeft((Rectangle)(result.VisualHit));
            int top = (int)Canvas.GetTop((Rectangle)(result.VisualHit));
            uint row;
            uint col;
            FindBlock(top, left,out row,out col);
            PopBlock(row, col);
//             if (result != null)
//             {
//                 MessageBox.Show(Canvas.GetLeft((Rectangle)(result.VisualHit)).ToString() + Canvas.GetTop((Rectangle)(result.VisualHit)).ToString());
//             }
        }
        void PopBlock(uint row, uint col)
        {              
            foreach (var block in newGameScene.JudasBlocks)
            {
                if (block.Rowpos == row && block.Columnpos == col)
                {
                    //for hittest
                    //block.BlockColor = CommonTypes.BlockColor.BlockColorBlack;
                    newGameScene.JudasBlocks[(int)(row * CommonTypes.TOTALCOLUMNS + col)].IsSelected = true;
                    AssembleSiblings(block);
                }
            }
        }

        private void AssembleSiblings(SingleBlock block)
        {
            
            if (block.IsSelected == true)
            {
//                 if (block.HasNeighbourLeft == 1)
//                 {
//                     AssembleSiblings(newGameScene.JudasBlocks[(int)(block.Rowpos * CommonTypes.TOTALCOLUMNS + block.Columnpos - 1)]);
//                 }
//                 else if (block.HasNeighbourTop == 1)
//                 {
//                     AssembleSiblings(newGameScene.JudasBlocks[(int)((block.Rowpos - 1) * CommonTypes.TOTALCOLUMNS + block.Columnpos)]);
//                 }
            }
        }

        void FindBlock(int top, int left, out uint row, out uint col)
        {
            row = (uint)((gameCanvas.Height - CommonTypes.BLOCKHEIGHT - (uint)top) / CommonTypes.BLOCKHEIGHT) + 1;
            col = (uint)(left / CommonTypes.BLOCKWIDTH) + 1;
        }

        public void ReloadBlocks()
        {
            if (gameCanvas.Children.Count != 0)
            {
                gameCanvas.Children.Clear();
            }
            //newGameScene.HighScore = 
            foreach (var singleBlock in newGameScene.JudasBlocks)
            {
                Rectangle star = new Rectangle();
                star.Height = CommonTypes.BLOCKNETHEIGHT;
                star.Width = CommonTypes.BLOCKNETWIDTH;
                //top = 480 - 48 - (row -1) * 48
                //row =(480 - 48 - top)/48 + 1
                Canvas.SetTop(star, gameCanvas.Height - CommonTypes.BLOCKHEIGHT - (singleBlock.Rowpos - 1) * CommonTypes.BLOCKHEIGHT);
                // left = (col - 1) * 48
                // col = left / 48 + 1;
                Canvas.SetLeft(star, (singleBlock.Columnpos - 1) * CommonTypes.BLOCKWIDTH);
                SolidColorBrush brush = new SolidColorBrush();
                
                CommonTypes.BlockColor blkcolor = (CommonTypes.BlockColor)singleBlock.BlockColor ;
                switch (blkcolor)
                {
                    case CommonTypes.BlockColor.BlockColorRed:
                        brush = Brushes.Red;
                        break;
                    case CommonTypes.BlockColor.BlockColorGreen:
                        brush = Brushes.Green;
                        break;
                    case CommonTypes.BlockColor.BlockColorBlue:
                        brush = Brushes.Blue;
                        break;
                    case CommonTypes.BlockColor.BlockColorYellow:
                        brush = Brushes.Orange;
                        break;
                    case CommonTypes.BlockColor.BlockColorPurple:
                        brush = Brushes.Purple;
                        break;
                    // for hittest
                    //case CommonTypes.BlockColor.BlockColorBlack:
                    //    brush = Brushes.Black;
                    //    break;
                    default:
                        break;
                }
                star.Fill = brush;

                gameCanvas.Children.Add(star);
              
            }
        }

        
    }
}
