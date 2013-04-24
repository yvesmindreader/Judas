using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.ComponentModel;

namespace Judas
{
    class SingleBlock : INotifyPropertyChanged
    {
        /// <summary>
        /// Fired whenever a property changes.  Required for
        /// INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        uint columnpos;
        uint rowpos;
        CommonTypes.BlockColor blockColor;
        int hasNeighbourTop = 1;
        int hasNeighbourBottom = 1;
        int hasNeighbourLeft = 1;
        int hasNeighbourRight = 1;
        bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        public uint Columnpos
        {
            get { return columnpos; }
            set { columnpos = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Columnpos"));
            }
        }
        public uint Rowpos
        {
            get { return rowpos; }
            set { rowpos = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Rowpos"));
            }
        }
        public CommonTypes.BlockColor BlockColor
        {
            get { return blockColor; }
            set {
                blockColor = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BlockColor"));
            }
        }
        public int HasNeighbourTop
        {
            get { return hasNeighbourTop; }
            set { hasNeighbourTop = value; }
        }
        public int HasNeighbourBottom
        {
            get { return hasNeighbourBottom; }
            set { hasNeighbourBottom = value; }
        }
        public int HasNeighbourLeft
        {
            get { return hasNeighbourLeft; }
            set { hasNeighbourLeft = value; }
        }
        public int HasNeighbourRight
        {
            get { return hasNeighbourRight; }
            set { hasNeighbourRight = value; }
        }     
    }
}
