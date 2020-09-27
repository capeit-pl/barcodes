using capeit.barcodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace test.capeit.barcodes
{
    public class Ean13Test
    {
        [Fact]
        public void it_should_draw_ean_13()
        {
            var result = "0123456789012".Ean13Encode();
            result.DrawPng(result.Count, 100, @".\it_should_draw_ean_13.png");

            Assert.True(File.Exists(@".\it_should_draw_ean_13.png"));
        }

        public void it_should_draw_ean_13_to_stream()
        {
            var result = "0123456789012".Ean13Encode();
            var stream = result.DrawPng(result.Count, 100);

            Assert.NotNull(stream);
            Assert.Equal(0, stream.Position);
            Assert.True(stream.Length > 0);
        }

        [Fact]
        public void it_should_encode_12_signs()
        {
            List<int> expected = new List<int>() { 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1 };

            var result = "590123412345".Ean13Encode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void it_should_encode_13_signs()
        {
            List<int> expected = new List<int>() { 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1 };

            var result = "9957346284897".Ean13Encode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void it_should_throw_exception()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                "".Ean13Encode();
            });

            Assert.Throws<ArgumentException>(() =>
            {
                "12345678901".Ean13Encode();
            });

            Assert.Throws<ArgumentException>(() =>
            {
                "12345678901234".Ean13Encode();
            });
        }
    }
}
