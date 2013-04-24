using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace Judas
{
    [ValueConversion(typeof(CommonTypes.BlockColor), typeof(Brush))]
    public class colorconverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
      object parameter, CultureInfo culture)
        {
            CommonTypes.BlockColor color = (CommonTypes.BlockColor)value;
            var brush = new SolidColorBrush();
            switch (color)
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
               //  for hittest
                case CommonTypes.BlockColor.BlockColorBlack:
                    brush = Brushes.Black;
                    break;
                default:
                    break;
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(uint), typeof(double))]
    public class rowposconverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
      object parameter, CultureInfo culture)
        {
            uint row = (uint)value;
            return CommonTypes.BLOCKHEIGHT * (CommonTypes.TOTALROWS - 1) - (row - 1) * CommonTypes.BLOCKHEIGHT;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(uint), typeof(double))]
    public class colposconverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
      object parameter, CultureInfo culture)
        {
            uint col = (uint)value;
            return (col - 1) * CommonTypes.BLOCKWIDTH;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
