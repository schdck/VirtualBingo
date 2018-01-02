using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VirtualBingo.Helpers
{
    public static class RendererHelper
    {
        public static RenderTargetBitmap Render(Control obj, Size size)
        {
            RenderTargetBitmap render = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            Canvas c = new Canvas();

            c.Children.Add(obj);

            obj.Width = size.Width;
            obj.Height = size.Height;

            c.Measure(size);
            c.Arrange(new Rect(size));

            obj.UpdateLayout();

            render.Render(c);

            return render;
        }

        public static void SaveImageOfRender(RenderTargetBitmap render, string pathToSave)
        {
            PngBitmapEncoder enc = new PngBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(render));

            using (var s = File.OpenWrite(pathToSave))
            {
                enc.Save(s);
            }
        }
    }
}
