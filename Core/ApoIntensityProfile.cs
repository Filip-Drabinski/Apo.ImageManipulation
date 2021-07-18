using System;
using System.Collections.Generic;
using System.Drawing;

namespace Apo.Core
{
    public readonly struct ApoIntensityProfile
    {

        public ChannelArray<byte> this[int channel] => _ipcs[channel];
        public readonly Point Start;
        public readonly Point End;
        public readonly Point[] Points;
        private readonly ChannelArray<byte>[] _ipcs;

        public ApoIntensityProfile(ApoImage img, Point start, Point end)
        {
            Start = start;
            End = end;
            var width = Math.Abs(start.X - end.X);
            var height = Math.Abs(start.Y - end.Y);
            _ipcs = new ChannelArray<byte>[img.NumberOfChannels];
            if (width >= height)
            {
                Points = new Point[width];
                int cnt = 0;
                for (int x = start.X; x < end.X; x++,cnt++)
                {
                    Points[cnt] = new Point(x,start.Y+ (int)Math.Round((((double)height)/width)*cnt));
                }
            }
            else
            {
                Points = new Point[height];
                int cnt = 0;
                for (int y = start.Y; y < end.Y; y++,cnt++)
                {
                    Points[cnt] = new Point(start.X +(int)Math.Round((((double)width) / height) * cnt),y);
                }
            }
            byte[][] tmp = new byte[img.NumberOfChannels][];
            for (int ch = 0; ch < img.NumberOfChannels; ch++)
            {
                tmp[ch] = new byte[Points.Length];
            }
            for (int i = 0; i < Points.Length; i++)
            {
                for (int ch = 0; ch < img.NumberOfChannels; ch++)
                {
                    tmp[ch][i] = img.GetPixel(ch, Points[i].Y >= img.Height? img.Height-1:Points[i].Y, Points[i].X >= img.Width? img.Width-1:Points[i].X);
                }
            }
            for (int ch = 0; ch < img.NumberOfChannels; ch++)
            {
                _ipcs[ch] = new ChannelArray<byte>(tmp[ch],img.Type switch
                {
                    ImageType.Grayscale => ChannelType.Gray,
                    ImageType.Bgr when ch ==0=> ChannelType.Blue,
                    ImageType.Bgr when ch ==1=> ChannelType.Green,
                    ImageType.Bgr when ch ==2=> ChannelType.Red,
                    ImageType.Bgra when ch == 0 => ChannelType.Blue,
                    ImageType.Bgra when ch == 1 => ChannelType.Green,
                    ImageType.Bgra when ch == 2 => ChannelType.Red,
                    ImageType.Bgra when ch == 3 => ChannelType.Alpha,
                    _ => ChannelType.Unknown,
                });
            }
        }
    }
    /*
    internal class ApoIntensityProfile
    {
        public static List<ApoIntensityProfile> Create(ApoImage image, int x0, int y0, int x1, int y1)
        {
            var res = new List<ApoIntensityProfile>();
            for (var ch = 0; ch < image.NumberOfChannels; ch++) res.Add(CalculateValues(ref image, x0, y0, x1, y1, ch));
            switch (image.Type)
            {
                case ImageType.Grayscale:
                    res[0].Type = ChannelType.Gray;
                    break;
                case ImageType.Bgr:
                case ImageType.Bgra:
                {
                    res[0].Type = ChannelType.Red;
                    res[1].Type = ChannelType.Green;
                    res[2].Type = ChannelType.Blue;
                    if (image.Type == ImageType.Bgra) res[3].Type = ChannelType.Alpha;
                    break;
                }
            }

            return res;
        }

        public static ApoIntensityProfile Create(ApoImage image, int x0, int y0, int x1, int y1, int channel)
        {
            var res = CalculateValues(ref image, x0, y0, x1, y1, channel);
            res.Type = image.Type switch
            {
                ImageType.Grayscale => ChannelType.Gray,
                ImageType.Bgr when channel == 0 => ChannelType.Red,
                ImageType.Bgr when channel == 1 => ChannelType.Green,
                ImageType.Bgr when channel == 2 => ChannelType.Blue,
                ImageType.Bgra when channel == 0 => ChannelType.Red,
                ImageType.Bgra when channel == 1 => ChannelType.Green,
                ImageType.Bgra when channel == 2 => ChannelType.Blue,
                ImageType.Bgra when channel == 3 => ChannelType.Alpha,
                _ => throw new ArgumentOutOfRangeException()
            };
            return res;
        }

        private static ApoIntensityProfile CalculateValues(ref ApoImage image, int x0, int y0, int x1, int y1,
            int channel)
        {
            var ip = new ApoIntensityProfile
            {
                X0 = x0,
                Y0 = y0,
                X1 = x1,
                Y1 = y1
            };

            var data = new List<int>();
            var labels = new List<string>();
            double lenX = Math.Abs(x0 - x1);
            double lenY = Math.Abs(y0 - y1);
            var xIncr = x0 < x1 ? 1 : -1;
            var yIncr = y0 < y1 ? 1 : -1;
            if (lenX >= lenY)
            {
                var xCounter = 0;
                for (var x = x0; x != x1; x += xIncr, xCounter++)
                {
                    var y = (int) (y0 + yIncr * (lenY / lenX * xCounter));
                    data.Add(image.Pixels[channel][x, y]);
                    labels.Add($"({x},{y}): {image.Pixels[channel][x, y]}");
                }
            }
            else
            {
                var yCounter = 0;
                for (var y = y0; y != y1; y += yIncr, yCounter++)
                {
                    var x = (int) (x0 + xIncr * (lenX / lenY) * yCounter);
                    data.Add(image.Pixels[channel][x, y]);
                    labels.Add($"({x},{y}): {image.Pixels[channel][x, y]}");
                }
            }

            ip.Data = data.ToArray();
            ip.Labels = labels.ToArray();
            return ip;
        }

        public int X0 { get; private set; }
        public int Y0 { get; private set; }
        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public string[] Labels { get; private set; }
        public int[] Data { get; private set; }
        public ChannelType Type { get; private set; }

        private ApoIntensityProfile()
        {
        }
    }
*/

}