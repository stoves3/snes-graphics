using Ultraviolet;
using Ultraviolet.Input;

namespace snes.graphics.demos.hdma.Managers
{
    public enum InputResult
    {
        Unknown,
        SwapViews,
        SpeedUp,
        SlowDown,
        Exit
    }

    internal class InputManager
    {
        private readonly UltravioletContext _uv;

        public InputManager(UltravioletContext ultravioletContext)
        {
            _uv = ultravioletContext;
        }

        public InputResult GetInput()
        {
            if (_uv.GetInput().GetActions().SwapViews.IsPressed()) return InputResult.SwapViews;

            if (_uv.GetInput().GetActions().SpeedUp.IsPressed()) return InputResult.SpeedUp;

            if (_uv.GetInput().GetActions().SlowDown.IsPressed()) return InputResult.SlowDown;

            if (_uv.GetInput().GetActions().ExitApplication.IsPressed()) return InputResult.Exit;

            return InputResult.Unknown;
        }
    }

    internal static class InputExtensions
    {
        public static Actions GetActions(this IUltravioletInput input) => Actions.Instance;

        public class Actions : InputActionCollection
        {
            public Actions(UltravioletContext uv) : base(uv) { }

            public static Actions Instance { get; } = CreateSingleton<Actions>();

            public InputAction SwapViews { get; private set; }
            public InputAction SpeedUp { get; private set; }
            public InputAction SlowDown { get; private set; }
            public InputAction ExitApplication { get; private set; }

            protected override void OnCreatingActions()
            {
                SwapViews = CreateAction("SWAP_VIEWS");
                SpeedUp = CreateAction("SPEED_UP");
                SlowDown = CreateAction("SLOW_DOWN");
                ExitApplication = CreateAction("EXIT_APPLICATION");

                base.OnCreatingActions();
            }

            protected override void OnResetting()
            {
                Reset_Desktop();
                base.OnResetting();
            }

            private void Reset_Desktop()
            {
                SwapViews.Primary = CreateKeyboardBinding(Key.Space);
                SpeedUp.Primary = CreateKeyboardBinding(Key.Plus);
                SlowDown.Primary = CreateKeyboardBinding(Key.Minus);
                ExitApplication.Primary = CreateKeyboardBinding(Key.Escape);
            }
        }
    }
}
