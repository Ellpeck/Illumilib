using Colore;
using Colore.Data;
using Colore.Effects.Keyboard;

namespace Illumilib.System {
    internal class RazerLighting : LightingSystem {

        public override LightingType Type => LightingType.Razer;

        private IChroma chroma;
        private CustomKeyboardEffect effect = new CustomKeyboardEffect(Color.Black);
        private bool effectOutdated;

        public override bool Initialize() {
            try {
                this.chroma = ColoreProvider.CreateNativeAsync().Result;
                return true;
            } catch {
                return false;
            }
        }

        public override void Dispose() {
            this.chroma.UninitializeAsync();
            this.effectOutdated = true;
        }

        public override void SetAllLighting(float r, float g, float b) {
            this.chroma.SetAllAsync(new Color(r, g, b));
            this.effectOutdated = true;
        }

        public override void SetKeyboardLighting(float r, float g, float b) {
            this.chroma.Keyboard?.SetAllAsync(new Color(r, g, b));
            this.effectOutdated = true;
        }

        public override void SetKeyboardLighting(int x, int y, float r, float g, float b) {
            this.chroma.Keyboard?.SetPositionAsync(y, x, new Color(r, g, b));
            this.effectOutdated = true;
        }

        public override void SetKeyboardLighting(int x, int y, int width, int height, float r, float g, float b) {
            if (this.chroma.Keyboard == null)
                return;
            if (this.effectOutdated) {
                for (var fullX = 0; fullX < KeyboardConstants.MaxColumns; fullX++) {
                    for (var fullY = 0; fullY < KeyboardConstants.MaxRows; fullY++)
                        this.effect[fullY, fullX] = this.chroma.Keyboard[fullY, fullX];
                }
                this.effectOutdated = false;
            }
            for (var xAdd = 0; xAdd < width; xAdd++) {
                for (var yAdd = 0; yAdd < height; yAdd++)
                    this.effect[y + yAdd, x + xAdd] = new Color(r, g, b);
            }
            this.chroma.Keyboard.SetCustomAsync(this.effect);
        }

        public override void SetKeyboardLighting(KeyboardKeys key, float r, float g, float b) {
            this.chroma.Keyboard?.SetKeyAsync(ConvertKey(key), new Color(r, g, b));
            this.effectOutdated = true;
        }

        public override void SetMouseLighting(float r, float g, float b) {
            this.chroma.Mouse?.SetAllAsync(new Color(r, g, b));
        }

        private static Key ConvertKey(KeyboardKeys key) {
            switch (key) {
                case KeyboardKeys.Back:
                    return Key.Backspace;
                case KeyboardKeys.Tab:
                    return Key.Tab;
                case KeyboardKeys.Enter:
                    return Key.Enter;
                case KeyboardKeys.Pause:
                    return Key.Pause;
                case KeyboardKeys.CapsLock:
                    return Key.CapsLock;
                case KeyboardKeys.Escape:
                    return Key.Escape;
                case KeyboardKeys.Space:
                    return Key.Space;
                case KeyboardKeys.PageUp:
                    return Key.PageUp;
                case KeyboardKeys.PageDown:
                    return Key.PageDown;
                case KeyboardKeys.End:
                    return Key.End;
                case KeyboardKeys.Home:
                    return Key.Home;
                case KeyboardKeys.Left:
                    return Key.Left;
                case KeyboardKeys.Up:
                    return Key.Up;
                case KeyboardKeys.Right:
                    return Key.Right;
                case KeyboardKeys.Down:
                    return Key.Down;
                case KeyboardKeys.PrintScreen:
                    return Key.PrintScreen;
                case KeyboardKeys.Insert:
                    return Key.Insert;
                case KeyboardKeys.Delete:
                    return Key.Delete;
                case KeyboardKeys.D0:
                    return Key.D0;
                case KeyboardKeys.D1:
                    return Key.D1;
                case KeyboardKeys.D2:
                    return Key.D2;
                case KeyboardKeys.D3:
                    return Key.D3;
                case KeyboardKeys.D4:
                    return Key.D4;
                case KeyboardKeys.D5:
                    return Key.D5;
                case KeyboardKeys.D6:
                    return Key.D6;
                case KeyboardKeys.D7:
                    return Key.D7;
                case KeyboardKeys.D8:
                    return Key.D8;
                case KeyboardKeys.D9:
                    return Key.D9;
                case KeyboardKeys.A:
                    return Key.A;
                case KeyboardKeys.B:
                    return Key.B;
                case KeyboardKeys.C:
                    return Key.C;
                case KeyboardKeys.D:
                    return Key.D;
                case KeyboardKeys.E:
                    return Key.E;
                case KeyboardKeys.F:
                    return Key.F;
                case KeyboardKeys.G:
                    return Key.G;
                case KeyboardKeys.H:
                    return Key.H;
                case KeyboardKeys.I:
                    return Key.I;
                case KeyboardKeys.J:
                    return Key.J;
                case KeyboardKeys.K:
                    return Key.K;
                case KeyboardKeys.L:
                    return Key.L;
                case KeyboardKeys.M:
                    return Key.M;
                case KeyboardKeys.N:
                    return Key.N;
                case KeyboardKeys.O:
                    return Key.O;
                case KeyboardKeys.P:
                    return Key.P;
                case KeyboardKeys.Q:
                    return Key.Q;
                case KeyboardKeys.R:
                    return Key.R;
                case KeyboardKeys.S:
                    return Key.S;
                case KeyboardKeys.T:
                    return Key.T;
                case KeyboardKeys.U:
                    return Key.U;
                case KeyboardKeys.V:
                    return Key.V;
                case KeyboardKeys.W:
                    return Key.W;
                case KeyboardKeys.X:
                    return Key.X;
                case KeyboardKeys.Y:
                    return Key.Y;
                case KeyboardKeys.Z:
                    return Key.Z;
                case KeyboardKeys.LWin:
                    return Key.LeftWindows;
                case KeyboardKeys.NumPad0:
                    return Key.Num0;
                case KeyboardKeys.NumPad1:
                    return Key.Num1;
                case KeyboardKeys.NumPad2:
                    return Key.Num2;
                case KeyboardKeys.NumPad3:
                    return Key.Num3;
                case KeyboardKeys.NumPad4:
                    return Key.Num4;
                case KeyboardKeys.NumPad5:
                    return Key.Num5;
                case KeyboardKeys.NumPad6:
                    return Key.Num6;
                case KeyboardKeys.NumPad7:
                    return Key.Num7;
                case KeyboardKeys.NumPad8:
                    return Key.Num8;
                case KeyboardKeys.NumPad9:
                    return Key.Num9;
                case KeyboardKeys.Multiply:
                    return Key.NumMultiply;
                case KeyboardKeys.Add:
                    return Key.NumAdd;
                case KeyboardKeys.Subtract:
                    return Key.NumSubtract;
                case KeyboardKeys.Decimal:
                    return Key.NumDecimal;
                case KeyboardKeys.Divide:
                    return Key.NumDivide;
                case KeyboardKeys.F1:
                    return Key.F1;
                case KeyboardKeys.F2:
                    return Key.F2;
                case KeyboardKeys.F3:
                    return Key.F3;
                case KeyboardKeys.F4:
                    return Key.F4;
                case KeyboardKeys.F5:
                    return Key.F5;
                case KeyboardKeys.F6:
                    return Key.F6;
                case KeyboardKeys.F7:
                    return Key.F7;
                case KeyboardKeys.F8:
                    return Key.F8;
                case KeyboardKeys.F9:
                    return Key.F9;
                case KeyboardKeys.F10:
                    return Key.F10;
                case KeyboardKeys.F11:
                    return Key.F11;
                case KeyboardKeys.F12:
                    return Key.F12;
                case KeyboardKeys.NumLock:
                    return Key.NumLock;
                case KeyboardKeys.Scroll:
                    return Key.Scroll;
                case KeyboardKeys.LShiftKey:
                    return Key.LeftShift;
                case KeyboardKeys.RShiftKey:
                    return Key.RightShift;
                case KeyboardKeys.LControlKey:
                    return Key.LeftControl;
                case KeyboardKeys.RControlKey:
                    return Key.RightControl;
                case KeyboardKeys.LMenu:
                    return Key.LeftAlt;
                case KeyboardKeys.RMenu:
                    return Key.RightAlt;
                case KeyboardKeys.OemSemicolon:
                    return Key.OemSemicolon;
                case KeyboardKeys.OemComma:
                    return Key.OemComma;
                case KeyboardKeys.OemMinus:
                    return Key.OemMinus;
                case KeyboardKeys.OemPeriod:
                    return Key.OemPeriod;
                case KeyboardKeys.OemTilde:
                    return Key.OemTilde;
                case KeyboardKeys.OemOpenBrackets:
                    return Key.OemLeftBracket;
                case KeyboardKeys.OemCloseBrackets:
                    return Key.OemRightBracket;
                case KeyboardKeys.OemBackslash:
                    return Key.OemBackslash;
                default:
                    return 0;
            }
        }

    }
}