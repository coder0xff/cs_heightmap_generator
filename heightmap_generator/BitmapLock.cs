using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace heightmap_generator
{
    class BitmapLock : IDisposable
    {
        public readonly IntPtr Scan0;
        public readonly int Stride;

        private readonly Bitmap _bmp;
        private readonly BitmapData _data;

        public BitmapLock(Bitmap bmp, BitmapData data)
        {
            _bmp = bmp;
            _data = data;
            _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height), ImageLockMode.ReadWrite, _data.PixelFormat, _data);
            Scan0 = _data.Scan0;
            Stride = _data.Stride;
        }

        public void Dispose()
        {
            _bmp.UnlockBits(_data);
        }
    }

    static class BitmapExtensions
    {
        public static BitmapLock Lock(this Bitmap bmp, BitmapData data)
        {
            return new BitmapLock(bmp, data);
        }

    }
}
