using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Apo.Core
{
    internal static class Extensions
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);
        public static ImageSource ToImageSource(this Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
    /*
    public static class Extensions
    {
        /// <summary>
        ///     Delete a GDI object
        /// </summary>
        /// <param name="o">The poniter to the GDI object to be deleted</param>
        /// <returns></returns>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        ///     Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        public static BitmapSource ToBitmapSource(this Image<Gray, byte> image)
        {
            using var source = image.ToBitmap();
            var ptr = source.GetHbitmap(); //obtain the Hbitmap

            var bs = Imaging.CreateBitmapSourceFromHBitmap(
                ptr,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(ptr); //release the HBitmap
            return bs;
        }

        /// <summary>
        ///     Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        public static WriteableBitmap ToWriteableBitmap(this Image<Gray, byte> image)
        {
            var wb = new WriteableBitmap(image.Width, image.Height, 96, 96, PixelFormats.Bgra32, null);
            var buffer = new byte[image.Width * image.Height * 4];
            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
            {
                buffer[(y * image.Width + x) * 4 + 0] = image.Data[y, x, 0]; //blue
                buffer[(y * image.Width + x) * 4 + 1] = image.Data[y, x, 0]; //green
                buffer[(y * image.Width + x) * 4 + 2] = image.Data[y, x, 0]; //red
                buffer[(y * image.Width + x) * 4 + 3] = 255; //alpha
            }

            wb.WritePixels(new Int32Rect(0, 0, image.Width, image.Height), buffer, wb.BackBufferStride, 0);
            return wb;
        }
    }*/
}