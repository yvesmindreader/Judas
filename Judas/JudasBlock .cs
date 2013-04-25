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
        bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }
        public uint Columnpos
        {
            get { return columnpos; }
            set
            {
                columnpos = value;
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
    }
}
