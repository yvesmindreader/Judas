using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Judas
{
   static class CommonTypes
    {
       public static uint TOTALBLOCKS = 100;
       public static uint TOTALROWS = 10;
       public static uint TOTALCOLUMNS = 10;
       public static uint BLOCKNETWIDTH = 47;
       public static uint BLOCKNETHEIGHT = 47;
       public static uint BLOCKWIDTH = 48;
       public static uint BLOCKHEIGHT = 48;


       public enum BlockColor
       {
           BlockColorRed,
           BlockColorGreen,
           BlockColorBlue,
           BlockColorYellow,
           BlockColorPurple,
           BlockColorBlack
       }
    }
}
