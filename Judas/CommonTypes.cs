using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Judas
{
   static class CommonTypes
    {
       public const uint TOTALBLOCKS = 100;
       public const uint TOTALROWS = 10;
       public const uint TOTALCOLUMNS = 10;
       public const double BLOCKNETWIDTH = 47;
       public const double BLOCKNETHEIGHT = 47;
       public const double BLOCKWIDTH = 48;
       public const double BLOCKHEIGHT = 48;


       public enum BlockColor
       {
           BlockColorRed = 1,
           BlockColorGreen,
           BlockColorBlue,
           BlockColorYellow,
           BlockColorPurple,
           BlockColorBlack
       }
    }

   public static class UIHelper
   {
       /// <summary>
       /// Finds a parent of a given item on the visual tree.
       /// </summary>
       /// <typeparam name="T">The type of the queried item.</typeparam>
       /// <param name="child">A direct or indirect child of the queried item.</param>
       /// <returns>The first parent item that matches the submitted type parameter. 
       /// If not matching item can be found, a null reference is being returned.</returns>
       public static T FindVisualParent<T>(DependencyObject child)
         where T : DependencyObject
       {
           // get parent item
           DependencyObject parentObject = VisualTreeHelper.GetParent(child);

           // we’ve reached the end of the tree
           if (parentObject == null) return null;

           // check if the parent matches the type we’re looking for
           T parent = parentObject as T;
           if (parent != null)
           {
               return parent;
           }
           else
           {
               // use recursion to proceed with next level
               return FindVisualParent<T>(parentObject);
           }
       }
   }
}
