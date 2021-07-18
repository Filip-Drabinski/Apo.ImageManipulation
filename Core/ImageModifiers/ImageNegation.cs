using Apo.Core.ImageModifiersCv;

namespace Apo.Core.ImageModifiers
{
    class ImageNegation:IImageModifier
    {
        public void Work(int channel, int y, int x, ref byte value)
        {
            value = (byte) (byte.MaxValue - value);
        }
    }
}