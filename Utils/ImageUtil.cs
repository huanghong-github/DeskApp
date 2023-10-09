using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DeskApp.Utils
{
    internal static class ImageUtil
    {
        public static Bitmap Resize(this Bitmap source, int new_width, int new_height)
        {

            Bitmap new_bitmap = new Bitmap(new_width, new_height);

            using (Graphics g = Graphics.FromImage(new_bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.CompositingMode = CompositingMode.SourceCopy;
                g.InterpolationMode = InterpolationMode.Low;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.Clear(Color.White);
                g.DrawImage(image: source,
                            destRect: new Rectangle(0, 0, new_width, new_height),
                            srcRect: new Rectangle(0, 0, source.Width, source.Height),
                            srcUnit: GraphicsUnit.Pixel);
            }

            return new_bitmap;

        }
        public static Bitmap Resize(this Bitmap source)
        {
            return Resize(source, 50, 50);
        }

        /// <summary>
        /// ImgToBase64String
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string ToBase64(this Bitmap bitmap)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                _ = ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                string strbaser64 = Convert.ToBase64String(arr);
                return strbaser64;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("ImgToBase64String 转换失败 Exception:" + ex.Message);
                return string.Empty;
            }
        }
        /// <summary>
        /// Base64StringToImage
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Bitmap ToImage(this string input)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(input);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Base64StringToImage 转换失败 /nException：" + ex.Message);
                return Properties.Resources.error_icon.ToBitmap();
            }
        }
    }
}
