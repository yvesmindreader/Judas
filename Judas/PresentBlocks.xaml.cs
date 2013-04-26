using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;

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
        void CheckHitBlock(object sender, MouseButtonEventArgs e, bool needPopBlock = true)
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
            if (needPopBlock)
            {
                PopBlock(row, col);
            }
            else
            {
                SingleBlock item = FindBlock(row, col);
                MessageBox.Show("R" + item.Rowpos.ToString() + " C" + item.Columnpos.ToString() + " " + item.IsSelected.ToString()
                            + " Color" + ((int)(item.BlockColor)).ToString());
            }
        }
        void PopBlock(uint row, uint col)
        {
            SingleBlock block = FindBlock(row, col);
            bool isSelectionMode = block.IsSelected;

            if (!isSelectionMode)
            {
                if (openlist.Count == 0)
                {
                    openlist.Add(block);
                    AssembleSiblings(block);
                }
                else 
                {
                    foreach (var item in openlist)
                    {
                        item.IsSelected = !item.IsSelected;
                    }
                    openlist.Clear();

                    openlist.Add(block);
                    AssembleSiblings(block);

                }
            }
            if (openlist.Count > 1)
            {
                foreach (var item in openlist)
                {
                    item.IsSelected = !item.IsSelected;

                }
                Refresh();

            }
            else//selected single block
            {
                openlist.Clear();
            }

            if (isSelectionMode)
            {
                openlist.Clear();
            }

        }

        private void Refresh()
        {
            RefreshRow();
            FreshColumn();
        }

        private void RefreshRow()
        {
            foreach (var item in openlist)
            {
                if (!item.IsSelected)
                {
                    blocks.Remove(item);

                    uint removedrow = item.Rowpos;
                    uint removedcol = item.Columnpos;
                    for (uint i = removedrow + 1; i <= CommonTypes.TOTALROWS; i++)
                    {
                        if (FindBlock(i, removedcol) != null)
                        {
                            FindBlock(i, removedcol).Rowpos--;
                        }
                    }

                }
            }
        }

        private void FreshColumn(uint startcol = 1)
        {
            bool foundMissingColumn = false;
            for (uint col = startcol; col <= exstingColumns; col++)
            {
                if (FindBlock(1, col) == null)
                {
                    foundMissingColumn = true;
                    startcol = col;
                    for (uint j = col + 1; j <= exstingColumns; j++)
                    {
                        for (uint i = 1; i <= CommonTypes.TOTALROWS; i++)
                        {
                            if (FindBlock(i, j) != null)
                            {
                                FindBlock(i, j).Columnpos--;
                            }
                        }
                    }
                    if (exstingColumns > 2)
                    {
                        exstingColumns--;
                    }
                }
            }
            if (foundMissingColumn)
            {
                FreshColumn(startcol);
            }
        }
        private void AssembleSiblings(SingleBlock block)
        {
            checkedlist.Clear();
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
                        if (foundblock != null && !checkedlist.Contains(foundblock) && !openlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        foundblock = FindBlock(row, col + 1);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && !openlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        foundblock = FindBlock(row + 1, col);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && !openlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
                        {
                            openlist.Add(foundblock);
                        }
                        foundblock = FindBlock(row - 1, col);
                        if (foundblock != null && !checkedlist.Contains(foundblock) && !openlist.Contains(foundblock) && foundblock.BlockColor == checkcolor)
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

        private void Rectangle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckHitBlock(sender,e,false);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            blocks = this.DataContext as ObservableCollection<SingleBlock>;
            exstingColumns = CommonTypes.TOTALCOLUMNS;
        }
    }
}
