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
using System.Threading;

namespace Judas
{
    /// <summary>
    /// PresentBlocks.xaml 的交互逻辑
    /// </summary>
    public partial class PresentBlocks : UserControl
    {
        ObservableCollection<SingleBlock> blocks;

        List<SingleBlock> openlist = new List<SingleBlock>();
        List<SingleBlock> checkedlist = new List<SingleBlock>();
        static uint exstingColumns = CommonTypes.TOTALCOLUMNS;
        public PresentBlocks()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(PresentBlocks_Loaded);
        }

        void PresentBlocks_Loaded(object sender, RoutedEventArgs e)
        {
            blocks = this.DataContext as ObservableCollection<SingleBlock>;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          CheckHitBlock(sender, e);
        }
        void CheckHitBlock(object sender, MouseButtonEventArgs e)
        {
            Canvas gameCanvas = UIHelper.FindVisualParent<Canvas>(sender as DependencyObject);
            //InitializeGame();
            Point mousepos = e.GetPosition(gameCanvas);
            HitTestResult result = VisualTreeHelper.HitTest(gameCanvas, mousepos);            
            GeneralTransform generalTransform1 = ((UIElement)(result.VisualHit)).TransformToAncestor(gameCanvas);
            Point currentPoint = generalTransform1.Transform(new Point(0, 0));
            double left =currentPoint.X;
            double top = currentPoint.Y;
            uint row;
            uint col;
            LocateBlock(top, left, out row, out col);
            PopBlock(row, col);
        }
        void PopBlock(uint row, uint col)
        {
            SingleBlock block = FindBlock(row, col);
            bool isSelectionMode = block.IsSelected;
            //for test
            //blocks[(int)((row - 1) * 10 + col - 1)].BlockColor = CommonTypes.BlockColor.BlockColorBlack;
            if (block != null)
            {
                if (!isSelectionMode)
                {
                    openlist.Add(block);
                    AssembleSiblings(block);
                }
                if (openlist.Count > 1)
                {
                    //txtbox.Text = "Selected blocks:";
                    //PrintBlocks();

                    txtbox1.Text = "openlist\r\n";

                    foreach (var item in openlist)
                    {
                        //txtbox.Text += "\r\n" + item.Rowpos.ToString() + " " + item.Columnpos.ToString() + "\r\n" + item.BlockColor.ToString()+"\r\n";
                        if (item.IsSelected)
                        {
                            item.IsSelected = false;
                        }
                        else
                        {
                            item.IsSelected = true;
                            item.BlockColor = CommonTypes.BlockColor.BlockColorBlack;
                        }
                        txtbox1.Text += item.Rowpos.ToString() + item.Columnpos.ToString() + item.IsSelected.ToString() + ((int)(item.BlockColor)).ToString() + exstingColumns.ToString()
                        +"\r\n";

                    }
                    //txtbox.Text += "\r\ndeleted blocks:";
                    foreach (var item in openlist)
                    {
                        if (item.BlockColor == CommonTypes.BlockColor.BlockColorBlack && !item.IsSelected)
                        {
                            //PrintBlocks();
                            blocks.Remove(item);
                            //txtbox.Text += "\r\n" + item.Rowpos.ToString() + " " + item.Columnpos.ToString() + "\r\n" + item.BlockColor.ToString() + "\r\n";

                            uint removedrow = item.Rowpos;
                            uint removedcol = item.Columnpos;
                            for (uint i = removedrow + 1; i <= CommonTypes.TOTALROWS; i++)
                            {
                                if (FindBlock(i, removedcol) != null)
                                {
                                    FindBlock(i, removedcol).Rowpos--;
                                    //MessageBox.Show("Test");
                                }
                            }

                            if (removedrow == 1 && (FindBlock(item.Rowpos+1, item.Columnpos) == null))
                            {
                                //if one col is empty, shift left all right cols

                                for (uint j = removedcol + 1; j <= exstingColumns; j++)
                                {
                                    for (uint i = 1; i <= CommonTypes.TOTALROWS; i++)
                                    {
                                        if (FindBlock(i, j) != null)
                                        {
                                            FindBlock(i, j).Columnpos--;
                                        }
                                    }
                                    if (exstingColumns > 2)
                                    {
                                        exstingColumns--;
                                    }
                                }

                            }
                        }
                    }

                }
                else//selected single block
                {
                    openlist.Clear();
                    checkedlist.Clear();
                }
                           
                if (isSelectionMode)
                {
                    openlist.Clear();
                    checkedlist.Clear();   
                }            
            }
        }

        private void PrintBlocks()
        {
            txtbox.Text = "";
            int[] array = new int[CommonTypes.TOTALBLOCKS];
            foreach (var item in blocks)
            {
                array[(item.Rowpos - 1) * CommonTypes.TOTALCOLUMNS + item.Columnpos - 1] = (int)item.BlockColor;
            }
            for (int i = (int)CommonTypes.TOTALROWS - 1; i >= 0; i--)
            {
                for (int j = 0; j < CommonTypes.TOTALCOLUMNS; j++)
                {
                    txtbox.Text += array[i * CommonTypes.TOTALCOLUMNS + j].ToString();
                }
                txtbox.Text += "\r\n";
            }
            //timeDelay(5);
        }
        private void timeDelay(int iInterval)
        {
            DateTime now = DateTime.Now;
            while (now.AddMilliseconds(iInterval) > DateTime.Now)
            {
            }
            return;
        }
        private void AssembleSiblings(SingleBlock block)
        {

            CommonTypes.BlockColor checkcolor = block.BlockColor;        

            // left:    row, col - 1
            // right:   row, col + 1
            // up:      row+1, col
            //down:     row-1, col
            if (openlist.Count != 0)
            {
                for (int i = 0; i < openlist.Count; i++)
                {
                    SingleBlock item = openlist[i];
                    uint row = item.Rowpos;
                    uint col = item.Columnpos;
                    if (!checkedlist.Contains(item))
                    {
                        var foundblock = FindBlock(row, col - 1);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        foundblock = FindBlock(row, col + 1);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        foundblock = FindBlock(row + 1, col);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        foundblock = FindBlock(row - 1, col);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        checkedlist.Add(item);
                    }
                }                
            }    
        }

        void LocateBlock(double top, double left, out uint row, out uint col)
        {
            row = (uint)((CommonTypes.BLOCKHEIGHT * (CommonTypes.TOTALROWS - 1) - (uint)top) / CommonTypes.BLOCKHEIGHT) + 1;
            col = (uint)(left / CommonTypes.BLOCKWIDTH) + 1;
        }

        SingleBlock FindBlock(uint row, uint col)
        {
            foreach (var block in blocks)
            {
                if (block.Rowpos == row && block.Columnpos == col)
                {
                    return block;                  
                }
            }
            return null;
        }



    }
}
