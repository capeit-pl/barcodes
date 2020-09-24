using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
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
                image.Mutate(c => c.BackgroundColor(Color.White));
                var x = 0;
                foreach (var line in data)
                {
                    var r = new Rectangle(x, 0, lineWidth, height);
                    image.Mutate(c => c.Fill(line == 0 ? Color.White : Color.Black, r));
                    x += lineWidth;
                }
                image.SaveAsPng(path);
            }
        }
    }
}
