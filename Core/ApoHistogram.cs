using System;

namespace Apo.Core
{

    public readonly struct ApoHistogram
    {
        public ChannelArray<int> this[int channel] => _hcs[channel];
        public readonly int NumberOfChannels;
        private readonly ChannelArray<int>[] _hcs;
        public ApoHistogram(ApoImage img)
        {
            var luts = img.GenerateLuts();
            _hcs = new ChannelArray<int>[luts.Length];
            NumberOfChannels = img.NumberOfChannels;
            for (int i = 0; i < luts.Length; i++)
            {
                _hcs[i] = new ChannelArray<int>(luts[i], img.Type switch
                {
                    ImageType.Grayscale => ChannelType.Gray,
                    ImageType.Bgr when i == 0 => ChannelType.Blue,
                    ImageType.Bgr when i == 1 => ChannelType.Green,
                    ImageType.Bgr when i == 2 => ChannelType.Red,
                    ImageType.Bgra when i == 0 => ChannelType.Blue,
                    ImageType.Bgra when i == 1 => ChannelType.Green,
                    ImageType.Bgra when i == 2 => ChannelType.Red,
                    ImageType.Bgra when i == 3 => ChannelType.Alpha,
                    _ => ChannelType.Unknown
                });
            }
        }
    }
    public readonly struct ChannelArray<TType> where TType : IComparable
    {
        public TType this[int val] => _data[val];
        public readonly ChannelType Type;
        public readonly int Length;
        public readonly TType Min;
        public readonly TType Max;

        private readonly TType[] _data;
        public ChannelArray(TType[] data, ChannelType type)
        {
            _data = data;
            Min = data[0];
            Max = data[0];
            for (var i = 1; i < data.Length; i++)
            {
                if (data[i].CompareTo(Min) < 0) Min = data[i];
                if (data[i].CompareTo(Max) > 0) Max = data[i];
            }
            Length = data.Length;
            Type = type;
        }

        public TType[] Data => _data;
    }
    /*
        internal class ApoHistogram
        {
            public static ApoHistogram[] Create(ApoImage image)
            {
                var res = new ApoHistogram[image.ChannelCount];
                for (var ch = 0; ch < image.ChannelCount; ch++)
                {
                    var hist = new ApoHistogram
                    {
                        Data = Create(image.Pixels[ch])
                    };
                    res[ch] = hist;
                }

                switch (image.Type)
                {
                    case ImageType.Greyscale:
                        res[0].Type = ChannelType.Grey;
                        break;
                    case ImageType.Rgb:
                    case ImageType.Rgba:
                    {
                        res[0].Type = ChannelType.Red;
                        res[1].Type = ChannelType.Green;
                        res[2].Type = ChannelType.Blue;
                        if (image.Type == ImageType.Rgba) res[3].Type = ChannelType.Alpha;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return res;
            }

            public static ApoHistogram Create(ApoImage image, int channel)
            {
                return new ApoHistogram
                {
                    Data = Create(image.Pixels[channel]),
                    Type = image.Type switch
                    {
                        ImageType.Greyscale => ChannelType.Grey,
                        ImageType.Rgb when channel == 0 => ChannelType.Red,
                        ImageType.Rgb when channel == 1 => ChannelType.Green,
                        ImageType.Rgb when channel == 2 => ChannelType.Blue,
                        ImageType.Rgba when channel == 0 => ChannelType.Red,
                        ImageType.Rgba when channel == 1 => ChannelType.Green,
                        ImageType.Rgba when channel == 2 => ChannelType.Blue,
                        ImageType.Rgba when channel == 3 => ChannelType.Blue,
                        _ => throw new ArgumentOutOfRangeException()
                    }
                };
            }

            public static int[] Create(byte[,] imgPixels)
            {
                var data = new int[256];
                for (var x = 0; x < imgPixels.GetLength(0); x++)
                for (var y = 0; y < imgPixels.GetLength(1); y++)
                    data[imgPixels[x, y]]++;
                return data;
            }

            public int[] Data { get; private set; }
            public ChannelType Type { get; private set; }

            private ApoHistogram()
            {
            }
        }*/
}