using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VirtualBingo.Helpers
{
    public class BitmapImageHelper
    {
        /* From: https://social.msdn.microsoft.com/Forums/vstudio/en-US/5b2270cb-f182-4f5f-a6c6-c78dfe4e3230/how-to-dispose-a-systemwindowsmediaimagesource?forum=wpf
         * By: Alex Skalozub
        */
        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
    }
}
