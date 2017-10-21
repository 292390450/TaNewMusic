using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TaNewHelper
{
    /// <summary>
    /// 异步或者多线程使用此方法，耗时。
    /// </summary>
    public class ImageHelper
    {
        public static Bitmap GetBitmap(byte[] buff)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(buff, 0, buff.Length);
                    Bitmap bit = new Bitmap(Bitmap.FromStream(ms));
                    bit = ChangeImageStyle(bit);
                    return bit;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 主线程上调用此方法给图像赋值，不解.
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(Bitmap bit)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bit.Save(ms, ImageFormat.Bmp);
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;

                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 重绘图片，更改格式为位图
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap ChangeImageStyle(Bitmap bitmap)
        {
            Bitmap newbitmap = new Bitmap(bitmap.Width, bitmap.Height);
            System.Drawing.Color pixel;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int r, g, b;
                    pixel = bitmap.GetPixel(x, y);
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    newbitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(r, g, b));
                }
            }
            return newbitmap;
        }

        /// <summary>
        /// bitmaImage转为字节数组
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitMapImageToByteArray(BitmapImage bmp)
        {
            byte[] bytearray = null;
            try
            {
                Stream smarket = bmp.StreamSource; ;
                if (smarket != null && smarket.Length > 0)
                {
                    //设置当前位置
                    smarket.Position = 0;
                    using (BinaryReader br = new BinaryReader(smarket))
                    {
                        bytearray = br.ReadBytes((int)smarket.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return bytearray;
        }


        /// <summary>
        /// 字节转成图片
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static Bitmap ConvertByteToImg(byte[] bytes)
        {
            Bitmap img = null;
            try
            {
                if (bytes != null && bytes.Length != 0)
                {
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        img = new Bitmap(ms);
                    }
                    return img;
                }
            }
            catch (Exception ex)
            {
                
            }
            return img;
        }
        /// <summary>
        /// 异步下载图像
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<Bitmap> DownAsync(string uri)
        {
           
            try
            {
                byte[] buff;
                using (WebClient client = new WebClient())
                {
                    buff = await client.DownloadDataTaskAsync(uri);
                }
                return await Task.Run((() =>
                {
                    return GetBitmap(buff);
                }));
            }
            catch (Exception e)
            {
                return null;
            }
         
        }
    }
}
