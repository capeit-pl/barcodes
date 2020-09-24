using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace capeit.barcodes
{
    public static class BarcodeDrawing
    {

        public static void DrawPng(this List<int> data, int width, int height, string path)
        {
            var length = data.Count;

            if (data.Count > width) throw new ArgumentException($"Minimum image width is {data.Count} pixels.");

            var lineWidth = width / length;

            using (Image image = new Image<Rgba32>(width, height))
            {
                image.DrawBarcode(data, height, lineWidth);
                image.SaveAsPng(path);
            }
        }

        public static Stream DrawPng(this List<int> data, int width, int height)
        {
            var length = data.Count;

            if (data.Count > width) throw new ArgumentException($"Minimum image width is {data.Count} pixels.");

            MemoryStream ms = new MemoryStream();
            var lineWidth = width / length;

            using (Image image = new Image<Rgba32>(width, height))
            {
                image.DrawBarcode(data, height, lineWidth);
                image.SaveAsPng(ms);
            }

            ms.Position = 0;
            return ms;
        }

        private static void DrawBarcode(this Image image,List<int> data, int height, int lineWidth)
        {
            image.Mutate(c => c.BackgroundColor(Color.White));
            var x = 0;
            foreach (var line in data)
            {
                var r = new Rectangle(x, 0, lineWidth, height);
                image.Mutate(c => c.Fill(line == 0 ? Color.White : Color.Black, r));
                x += lineWidth;
            }
        }
    }
}
