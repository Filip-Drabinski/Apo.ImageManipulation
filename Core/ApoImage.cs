using System;
using System.IO;
using System.Windows.Media;
using Apo.Core.ImageModifiers;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Apo.Core
{
    public abstract class ApoImage
    {
        public abstract byte this[int y, int x, int channel] { get; protected set; }
        public abstract byte[,,] Pixels { get; }
        public abstract void SetPixel(int channel, int y, int x, byte val);
        public abstract byte GetPixel(int channel, int y, int x);
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int NumberOfChannels { get; }
        public string Path { get; protected set; }
        public bool IsSaved { get; protected set; }
        public abstract ImageType Type { get; }

        public static ApoImage Create(int height, int width, ImageType type)
        {
            return type switch
            {
                ImageType.Grayscale => new ApoImageGray(new Image<Gray, byte>(width, height)),
                ImageType.Bgr => new ApoImageBgr(new Image<Bgr, byte>(width, height)),
                ImageType.Bgra => new ApoImageBgra(new Image<Bgra, byte>(width, height)),
                ImageType.Unknown => throw new NotSupportedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static ApoImage FromFile(string path)
        {
            var mat = new Mat(path);
            return mat.NumberOfChannels switch
            {
                1 => new ApoImageGray(mat.ToImage<Gray, byte>(), path, true),
                3 => new ApoImageBgr(mat.ToImage<Bgr, byte>(), path, true),
                _ => new ApoImageBgra(mat.ToImage<Bgra, byte>(), path, true)
            };
        }

        public abstract void Save();
        public abstract bool SaveAs(string path);
        public abstract ApoImage Duplicate();
        public abstract ApoImage Clone();
        public abstract ApoImage Duplicate(int counter);
        public abstract ApoImage ToGrayscale();
        public abstract ApoImage ToBgr();
        public abstract ApoImage ToBgra();
        public abstract ImageSource ToImageSource();
        public abstract int[][] GenerateLuts();
        protected abstract int[] GenerateLut(int channel);
        public abstract ApoHistogram GenerateHistogram();
        public abstract void Modify(IImageModifier modifier);
        public abstract Image<Gray, byte> ToOpenCv();

        public abstract void Modify(IImageModifierCv modifier);
    }

    internal abstract class ApoImage<TColor> : ApoImage where TColor : struct, IColor
    {
        public override byte this[int y, int x, int channel]
        {
            get => Image.Data[y, x, channel];
            protected set => Image.Data[y, x, channel] = value;
        }

        public override byte[,,] Pixels => Image.Data;

        public override void SetPixel(int channel, int y, int x, byte val)
        {
            Image.Data[y, x, channel] = val;
        }

        public override byte GetPixel(int channel, int y, int x)
        {
            return Image.Data[y, x, channel];
        }

        public override int Width => Image.Width;
        public override int Height => Image.Height;
        public override int NumberOfChannels => Image.NumberOfChannels;
        protected Image<TColor, byte> Image;

        public ApoImage(Image<TColor, byte> image)
        {
            Image = image;
            Path = null;
            IsSaved = false;
        }

        public ApoImage(Image<TColor, byte> image, string path, bool isSaved)
        {
            Image = image;
            Path = path;
            IsSaved = isSaved;
        }

        public override void Save()
        {
            Image.Save(Path);
            IsSaved = true;
        }

        public override bool SaveAs(string path)
        {
            Path = path;
            Save();
            IsSaved = true;
            return true;
        }

        public override void Modify(IImageModifier modifier)
        {
            IsSaved = false;
            for (var ch = 0; ch < NumberOfChannels; ch++)
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                modifier.Work(ch, y, x, ref Image.Data[y, x, ch]);
        }

        protected override int[] GenerateLut(int channel)
        {
            if (channel >= NumberOfChannels || channel < 0)
                throw new ArgumentOutOfRangeException(nameof(channel), channel,
                    $"{nameof(channel)} should be number in range [0,NumberOfChannels - 1({NumberOfChannels - 1})]");
            var res = new int[256];
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                res[this[y, x, channel]]++;
            return res;
        }

        public override int[][] GenerateLuts()
        {
            var res = new int[NumberOfChannels][];
            for (var ch = 0; ch < NumberOfChannels; ch++) res[ch] = GenerateLut(ch);
            return res;
        }

        public override ImageSource ToImageSource()
        {
            return Image.ToBitmap().ToImageSource();
        }

        public override Image<Gray, byte> ToOpenCv()
        {
            return Image.Convert<Gray, byte>();
        }

        public override ApoImage ToGrayscale()
        {
            return new ApoImageGray(Image.Convert<Gray, byte>(), Path, false);
        }

        public override ApoImage ToBgr()
        {
            return new ApoImageBgr(Image.Convert<Bgr, byte>(), Path, false);
        }

        public override ApoImage ToBgra()
        {
            return new ApoImageBgra(Image.Convert<Bgra, byte>(), Path, false);
        }

        public override ApoHistogram GenerateHistogram()
        {
            return new ApoHistogram(this);
        }
    }

    internal class ApoImageBgra : ApoImage<Bgra>
    {
        public ApoImageBgra(Image<Bgra, byte> image) : base(image)
        {
        }

        public ApoImageBgra(Image<Bgra, byte> image, string path, bool isSaved) : base(image, path, isSaved)
        {
        }

        public override ImageType Type => ImageType.Bgra;

        public override ApoImage Duplicate()
        {
            return new ApoImageBgra(Image.Clone(), Path, IsSaved);
        }

        public override ApoImage Clone()
        {
            return Duplicate();
        }

        public override ApoImage Duplicate(int counter)
        {
            var extIndex = Path.LastIndexOf('.');
            return new ApoImageBgra(Image.Clone(),
                $"{Path.Substring(0, extIndex - 1)}({counter}){Path.Substring(extIndex)}", false);
        }

        public override void Modify(IImageModifierCv modifier)
        {
            IsSaved = false;
            modifier.Work(ref Image);
        }

        public override int NumberOfChannels => 4;
    }

    internal class ApoImageGray : ApoImage<Gray>
    {
        public ApoImageGray(Image<Gray, byte> image) : base(image)
        {
        }

        public ApoImageGray(Image<Gray, byte> image, string path, bool isSaved) : base(image, path, isSaved)
        {
        }

        public override void Modify(IImageModifierCv modifier)
        {
            IsSaved = false;
            modifier.Work(ref Image);
        }

        public override ImageType Type => ImageType.Grayscale;

        public override ApoImage Duplicate()
        {
            return new ApoImageGray(Image.Clone(), Path, IsSaved);
        }

        public override ApoImage Clone()
        {
            return Duplicate();
        }

        public override ApoImage Duplicate(int counter)
        {
            var extIndex = Path.LastIndexOf('.');
            return new ApoImageGray(Image.Clone(),
                $"{Path.Substring(0, extIndex - 1)}({counter}){Path.Substring(extIndex)}", false);
        }

        public override int NumberOfChannels => 1;
    }

    internal class ApoImageBgr : ApoImage<Bgr>
    {
        public ApoImageBgr(Image<Bgr, byte> image) : base(image)
        {
        }

        public ApoImageBgr(Image<Bgr, byte> image, string path, bool isSaved) : base(image, path, isSaved)
        {
        }

        public override void Modify(IImageModifierCv modifier)
        {
            IsSaved = false;
            modifier.Work(ref Image);
        }

        public override ImageType Type => ImageType.Bgr;

        public override ApoImage Duplicate()
        {
            return new ApoImageBgr(Image.Clone(), Path, IsSaved);
        }

        public override ApoImage Clone()
        {
            return Duplicate();
        }

        public override ApoImage Duplicate(int counter)
        {
            var extIndex = Path.LastIndexOf('.');
            return new ApoImageBgr(Image.Clone(),
                $"{Path.Substring(0, extIndex - 1)}({counter}){Path.Substring(extIndex)}", false);
        }

        public override int NumberOfChannels => 3;
    }

    public enum ImageType
    {
        Grayscale,
        Bgr,
        Bgra,
        Unknown
    }

    public enum ChannelType
    {
        Gray,
        Blue,
        Green,
        Red,
        Alpha,
        Unknown
    }
}


/*using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Apo.Core
{
    public class ApoImage
    {
        public byte[][,] Pixels { get; set; }
        public string Path { get; private set; }
        public bool IsSaved { get; private set; }
        public ImageType Type { get; private set; }
        public int Width => Pixels[0].GetLength(0);
        public int Height => Pixels[0].GetLength(1);
        public int ChannelCount => Pixels.Length;
        private PixelFormat _pixelFormat;


        public static ApoImage FromFile(string path)
        {
            using var bmp = new ImageSource(path);
            var width = bmp.Width;
            var height = bmp.Height;
            var isAlpha = bmp.PixelFormat.HasFlag(PixelFormat.Alpha);
            var isGreyscale = true;
            var red = new byte[width, height];
            var green = new byte[width, height];
            var blue = new byte[width, height];
            var alpha = isAlpha ? new byte[width, height] : null;
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    var color = bmp.GetPixel(x, y);
                    red[x, y] = color.R;
                    green[x, y] = color.G;
                    blue[x, y] = color.B;
                    if (isAlpha) alpha[x, y] = color.A;
                    if (!(color.R == color.G && color.R == color.B && color.G == color.B)) isGreyscale = false;
                }

            ApoImage res;
            if (isGreyscale) res = new ApoImage { Path = path,_pixelFormat = bmp.PixelFormat, Type = ImageType.Greyscale, Pixels = new[] { red } };
            else if (isAlpha)
                res = new ApoImage { Path = path, _pixelFormat = bmp.PixelFormat, Type = ImageType.Rgba, Pixels = new[] { red, green, blue, alpha } };
            else res = new ApoImage { Path = path, _pixelFormat = bmp.PixelFormat, Type = ImageType.Rgb, Pixels = new[] { red, green, blue } };
            res.IsSaved = true;
            return res;
        }


        public void Save(string path)
        {
            using var bmp = new ImageSource(Width, Height, _pixelFormat);
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    //
                    if (Pixels.Length == 1)
                        bmp.SetPixel(x, y, Color.FromArgb(255, Pixels[0][x, y], Pixels[0][x, y], Pixels[0][x, y]));
                    else if (Pixels.Length == 3)
                        bmp.SetPixel(x, y, Color.FromArgb(255, Pixels[0][x, y], Pixels[1][x, y], Pixels[2][x, y]));
                    else
                        bmp.SetPixel(x, y,
                            Color.FromArgb(Pixels[3][x, y], Pixels[0][x, y], Pixels[1][x, y], Pixels[2][x, y]));
            bmp.Save(path);
            IsSaved = true;
            Path = path;
        }

        public WriteableBitmap ToWriteableBitmap()
        {
            var wb = new WriteableBitmap(Width, Height, 96, 96, PixelFormats.Bgra32, null);
            var buffer = new byte[Width * Height * 4];
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    if (Pixels.Length == 1)
                    {
                        buffer[(y * Width + x) * 4 + 0] = Pixels[0][x, y]; //blue
                        buffer[(y * Width + x) * 4 + 1] = Pixels[0][x, y]; //green
                        buffer[(y * Width + x) * 4 + 2] = Pixels[0][x, y]; //red
                        buffer[(y * Width + x) * 4 + 3] = 255; //alpha
                    }
                    else if (Pixels.Length == 3)
                    {
                        buffer[(y * Width + x) * 4 + 0] = Pixels[2][x, y]; //blue
                        buffer[(y * Width + x) * 4 + 1] = Pixels[1][x, y]; //green
                        buffer[(y * Width + x) * 4 + 2] = Pixels[0][x, y]; //red
                        buffer[(y * Width + x) * 4 + 3] = 255; //alpha
                    }
                    else
                    {
                        buffer[(y * Width + x) * 4 + 0] = Pixels[2][x, y]; //blue
                        buffer[(y * Width + x) * 4 + 1] = Pixels[1][x, y]; //green
                        buffer[(y * Width + x) * 4 + 2] = Pixels[0][x, y]; //red
                        buffer[(y * Width + x) * 4 + 3] = Pixels[3][x, y]; //alpha
                    }

            wb.WritePixels(new Int32Rect(0, 0, Width, Height), buffer, wb.BackBufferStride, 0);
            return wb;
        }

        public ApoImage Duplicate(int duplicateCounter)
        {
            string dir = System.IO.Path.GetDirectoryName(Path) ?? string.Empty;
            var name = System.IO.Path.GetFileNameWithoutExtension(Path);
            var ext = System.IO.Path.GetExtension(Path);
            var res = new ApoImage
            {
                Path = System.IO.Path.Combine(dir , $"{name}_copy({duplicateCounter}){ext}"),
                _pixelFormat = _pixelFormat,
                Pixels = new byte[Pixels.Length][,]
            };
            for (var chan = 0; chan < Pixels.Length; chan++)
            {
                res.Pixels[chan] = new byte[Width, Height];
                for (var x = 0; x < Width; x++)
                    for (var y = 0; y < Height; y++)
                        res.Pixels[chan][x, y] = Pixels[chan][x, y];
            }

            return res;
        }
        public Image<Gray,byte> ToOpenCv()
        {
            var res = new Image<Gray, byte>(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    res.Data[y,x,0] = Pixels[0][x,y];
                }
            }

            return res;
        }
        public ApoImage Clone( )
        {
            var res = new ApoImage
            {
                Path = this.Path,
                _pixelFormat = this._pixelFormat,
                Pixels = new byte[Pixels.Length][,]
            };
            for (var chan = 0; chan < Pixels.Length; chan++)
            {
                res.Pixels[chan] = new byte[Width, Height];
                for (var x = 0; x < Width; x++)
                    for (var y = 0; y < Height; y++)
                        res.Pixels[chan][x, y] = Pixels[chan][x, y];
            }

            return res;
        }
    }

    public enum ImageType
    {
        Greyscale,
        Rgb,
        Rgba
    }

    public enum ChannelType
    {
        Grey,
        Red,
        Green,
        Blue,
        Alpha,
        Unknown
    }
}*/