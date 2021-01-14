using NetpbmViewer.Readers;
using SkiaSharp;
using System;
using System.IO;

namespace NetpbmViewer
{
    public static class NetpbmFile
    {
        public static SKBitmap Process(string fileName)
        {
            using var stream = new FileStream(fileName, FileMode.Open);

            var reader = RetrievePixelReader(stream);
            var width = stream.ReadNextInt();
            var height = stream.ReadNextInt();
            reader.SetColorMax(stream);

            var bmp = new SKBitmap(width + 1, height + 1);

            for (var row = 0; row < height; row++){
                for (var col = 0; col < width; col++)
                {
                    bmp.SetPixel(col, row, reader.ReadPixel(stream));
                }
            }

            return bmp;
        }

        private static INetpbmPixelReader RetrievePixelReader(Stream stream)
        {
            var c1 = (char)stream.ReadByte();
            var c2 = (char)stream.ReadByte();

            if (c1 == 'P')
            {
                switch (c2)
                {
                    case '1':
                        return new PbmAsciiPixelReader();
                    case '2':
                        return new PgmAsciiPixelReader();
                    case '3':
                        return new PpmAsciiPixelReader();
                    case '4':
                        // PBM-binary, not implemented
                        break;
                    case '5':
                        // PGM-binary, not implemented
                        break;
                    case '6':
                        // PPM-binary, not implemented
                        break;
                }
            }

            throw new Exception($"Unsupported format: {c1}{c2}");
        }
    }
}
