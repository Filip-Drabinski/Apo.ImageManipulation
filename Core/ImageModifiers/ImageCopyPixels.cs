using Apo.Core.ImageModifiersCv;

namespace Apo.Core.ImageModifiers
{
    class ImageCopyPixels:IImageModifier
    {
        private ApoImage _img;

        public ImageCopyPixels(ApoImage image)
        {
            _img = image;
        }
        public void Work(int channel, int y, int x, ref byte value)
        {
            value = _img.GetPixel(channel, y, x);
        }
    }
}