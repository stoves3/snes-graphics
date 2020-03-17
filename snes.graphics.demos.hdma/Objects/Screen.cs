namespace snes.graphics.demos.hdma.Objects
{
    public enum ScreenId
    {
        Unknown,
        Start
    }

    public class Screen
    {
        public ScreenId ScreenId { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
