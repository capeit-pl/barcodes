using capeit.barcodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace test.capeit.barcodes
{
    public class Itf14Test
    {
        [Fact]
        public void it_should_draw_itf_14()
        {
            var result = "9876543210921".Itf14Encode();
            result.DrawPng(result.Count, 100, @".\it_should_draw_itf_14.png");

            Assert.True(File.Exists(@".\it_should_draw_itf_14.png"));
        }

        [Fact]
        public void it_should_draw_itf_14_to_stream()
        {
            var result = "9876543210921".Itf14Encode();
            var stream = result.DrawPng(result.Count, 100);

            Assert.NotNull(stream);
            Assert.Equal(0, stream.Position);
            Assert.True(stream.Length > 0);
        }

        [Fact]
        public void Ecode()
        {
            List<int> expectedEncoded = new List<int>() {
                1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1 };

            var result = "2591648510131".Itf14Encode();
            Assert.Equal(expectedEncoded, result);

            result = "25916485101318".Itf14Encode(false);
            Assert.Equal(expectedEncoded, result);
        }
    }
}
