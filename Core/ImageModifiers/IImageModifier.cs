namespace Apo.Core.ImageModifiers
{
    public interface IImageModifier
    {
        public void Work(int channel, int y, int x,ref byte value);
    }
}
