using snes.graphics.demos.hdma.Objects;
using System.Collections.Generic;
using Ultraviolet;
using Ultraviolet.Platform;

namespace snes.graphics.demos.hdma.Managers
{
    public class ScreenManager
    {
        public delegate void HandleScreenChanged(Size2 newSize);
        public event HandleScreenChanged ScreenChangedHandler;

        public List<Screen> Current => Screens.Peek();

        public Stack<List<Screen>> Screens { get; set; }

        public Size2 WindowSize => _window.ClientSize;
        public Vector2 ScreenSize => new Vector2(WindowSize.Width, WindowSize.Height);
                
        private IUltravioletWindow _window;

        private Size2 _previousWindowSize;

        public ScreenManager(UltravioletContext ultraviolet)
        {
            Screens = new Stack<List<Screen>>();
            SetupScreens();

            _window = ultraviolet.GetUI().GetScreens().Window;
            ultraviolet.WindowDrawn += Ultraviolet_WindowDrawn;

            _previousWindowSize = WindowSize;
        }

        private void Ultraviolet_WindowDrawn(UltravioletContext uv, UltravioletTime time, IUltravioletWindow window)
        {
            var currentWindowSize = WindowSize;
            if (_previousWindowSize != currentWindowSize)
            {
                _window.SetWindowedClientSize(currentWindowSize);
                currentWindowSize = WindowSize;
                ScreenChangedHandler(currentWindowSize);
                _previousWindowSize = currentWindowSize;
            }            
        }

        private void SetupScreens()
        {
            var screens = new List<List<Screen>>
            {
                new List<Screen>{
                new Screen
                {
                    ScreenId = ScreenId.Start,
                    Width = 1920,
                    Height = 1080
                }}
            };

            screens.Reverse();
            screens.ForEach(Screens.Push);
        }

        public void UpdateWindow(Screen screen)
        {
            Size2 size = new Size2(screen.Width, screen.Height);
            _window.SetWindowedClientSize(size);
        }
    }
}
