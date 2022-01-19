using Illumilib.Lib;

namespace Illumilib.System {
    internal class LogitechLighting : LightingSystem {

        public override LightingType Type => LightingType.Logitech;

        private readonly byte[] bitmap = new byte[LogitechGsdk.LogiLedBitmapSize];
        private bool bitmapDirty;

        public override bool Initialize() {
            try {
                LogitechGsdk.LogiLedInit();
                return true;
            } catch {
                return false;
            }
        }

        public override void Dispose() {
            this.ClearBitmap();
            LogitechGsdk.LogiLedShutdown();
        }

        public override void SetAllLighting(float r, float g, float b) {
            this.ClearBitmap();
            LogitechGsdk.LogiLedSetLighting((int) (r * 100), (int) (g * 100), (int) (b * 100));
        }

        public override void SetKeyboardLighting(float r, float g, float b) {
            // setting keyboard zone lighting doesn't work for some keyboards, so we fill the bitmap instead
            for (var x = 0; x < LogitechGsdk.LogiLedBitmapWidth; x++) {
                for (var y = 0; y < LogitechGsdk.LogiLedBitmapHeight; y++)
                    this.SetBitmapColor(x, y, r, g, b, 1);
            }
            LogitechGsdk.LogiLedSetLightingFromBitmap(this.bitmap);
        }

        public override void SetKeyboardLighting(int x, int y, float r, float g, float b) {
            this.SetBitmapColor(x, y, r, g, b, 1);
            LogitechGsdk.LogiLedSetLightingFromBitmap(this.bitmap);
        }

        public override void SetKeyboardLighting(int x, int y, int width, int height, float r, float g, float b) {
            for (var xAdd = 0; xAdd < width; xAdd++) {
                for (var yAdd = 0; yAdd < height; yAdd++)
                    this.SetBitmapColor(x + xAdd, y + yAdd, r, g, b, 1);
            }
            LogitechGsdk.LogiLedSetLightingFromBitmap(this.bitmap);
        }

        public override void SetKeyboardLighting(KeyboardKeys key, float r, float g, float b) {
            this.ClearBitmap();
            LogitechGsdk.LogiLedSetLightingForKeyWithKeyName(ConvertKey(key), (int) (r * 100), (int) (g * 100), (int) (b * 100));
        }

        public override void SetMouseLighting(float r, float g, float b) {
            for (var i = 0; i <= 2; i++)
                LogitechGsdk.LogiLedSetLightingForTargetZone(DeviceType.Mouse, i, (int) (r * 100), (int) (g * 100), (int) (b * 100));
        }

        private void SetBitmapColor(int x, int y, float r, float g, float b, float a) {
            // since illumilib constants are a bit bigger than logi's constants, we need to make sure here
            if (x >= LogitechGsdk.LogiLedBitmapWidth || y >= LogitechGsdk.LogiLedBitmapHeight)
                return;
            var i = LogitechGsdk.LogiLedBitmapBytesPerKey * (y * LogitechGsdk.LogiLedBitmapWidth + x);
            this.bitmap[i + 0] = (byte) (b * 255);
            this.bitmap[i + 1] = (byte) (g * 255);
            this.bitmap[i + 2] = (byte) (r * 255);
            this.bitmap[i + 3] = (byte) (a * 255);
            this.bitmapDirty = true;
        }

        private void ClearBitmap() {
            if (this.bitmapDirty) {
                for (var i = 0; i < this.bitmap.Length; i++)
                    this.bitmap[i] = 0;
                this.bitmapDirty = false;
            }
        }

        private static KeyboardNames ConvertKey(KeyboardKeys key) {
            switch (key) {
                case KeyboardKeys.Back:
                    return KeyboardNames.Backspace;
                case KeyboardKeys.Tab:
                    return KeyboardNames.Tab;
                case KeyboardKeys.Enter:
                    return KeyboardNames.Enter;
                case KeyboardKeys.Pause:
                    return KeyboardNames.PauseBreak;
                case KeyboardKeys.CapsLock:
                    return KeyboardNames.CapsLock;
                case KeyboardKeys.Escape:
                    return KeyboardNames.Esc;
                case KeyboardKeys.Space:
                    return KeyboardNames.Space;
                case KeyboardKeys.PageUp:
                    return KeyboardNames.PageUp;
                case KeyboardKeys.PageDown:
                    return KeyboardNames.PageDown;
                case KeyboardKeys.End:
                    return KeyboardNames.End;
                case KeyboardKeys.Home:
                    return KeyboardNames.Home;
                case KeyboardKeys.Left:
                    return KeyboardNames.ArrowLeft;
                case KeyboardKeys.Up:
                    return KeyboardNames.ArrowUp;
                case KeyboardKeys.Right:
                    return KeyboardNames.ArrowRight;
                case KeyboardKeys.Down:
                    return KeyboardNames.ArrowDown;
                case KeyboardKeys.Select:
                    return KeyboardNames.ApplicationSelect;
                case KeyboardKeys.PrintScreen:
                    return KeyboardNames.PrintScreen;
                case KeyboardKeys.Insert:
                    return KeyboardNames.Insert;
                case KeyboardKeys.Delete:
                    return KeyboardNames.KeyboardDelete;
                case KeyboardKeys.D0:
                    return KeyboardNames.Zero;
                case KeyboardKeys.D1:
                    return KeyboardNames.One;
                case KeyboardKeys.D2:
                    return KeyboardNames.Two;
                case KeyboardKeys.D3:
                    return KeyboardNames.Three;
                case KeyboardKeys.D4:
                    return KeyboardNames.Four;
                case KeyboardKeys.D5:
                    return KeyboardNames.Five;
                case KeyboardKeys.D6:
                    return KeyboardNames.Six;
                case KeyboardKeys.D7:
                    return KeyboardNames.Seven;
                case KeyboardKeys.D8:
                    return KeyboardNames.Eight;
                case KeyboardKeys.D9:
                    return KeyboardNames.Nine;
                case KeyboardKeys.A:
                    return KeyboardNames.A;
                case KeyboardKeys.B:
                    return KeyboardNames.B;
                case KeyboardKeys.C:
                    return KeyboardNames.C;
                case KeyboardKeys.D:
                    return KeyboardNames.D;
                case KeyboardKeys.E:
                    return KeyboardNames.E;
                case KeyboardKeys.F:
                    return KeyboardNames.F;
                case KeyboardKeys.G:
                    return KeyboardNames.G;
                case KeyboardKeys.H:
                    return KeyboardNames.H;
                case KeyboardKeys.I:
                    return KeyboardNames.I;
                case KeyboardKeys.J:
                    return KeyboardNames.J;
                case KeyboardKeys.K:
                    return KeyboardNames.K;
                case KeyboardKeys.L:
                    return KeyboardNames.L;
                case KeyboardKeys.M:
                    return KeyboardNames.M;
                case KeyboardKeys.N:
                    return KeyboardNames.N;
                case KeyboardKeys.O:
                    return KeyboardNames.O;
                case KeyboardKeys.P:
                    return KeyboardNames.P;
                case KeyboardKeys.Q:
                    return KeyboardNames.Q;
                case KeyboardKeys.R:
                    return KeyboardNames.R;
                case KeyboardKeys.S:
                    return KeyboardNames.S;
                case KeyboardKeys.T:
                    return KeyboardNames.T;
                case KeyboardKeys.U:
                    return KeyboardNames.U;
                case KeyboardKeys.V:
                    return KeyboardNames.V;
                case KeyboardKeys.W:
                    return KeyboardNames.W;
                case KeyboardKeys.X:
                    return KeyboardNames.X;
                case KeyboardKeys.Y:
                    return KeyboardNames.Y;
                case KeyboardKeys.Z:
                    return KeyboardNames.Z;
                case KeyboardKeys.LWin:
                    return KeyboardNames.LeftWindows;
                case KeyboardKeys.RWin:
                    return KeyboardNames.RightWindows;
                case KeyboardKeys.Apps:
                    return KeyboardNames.ApplicationSelect;
                case KeyboardKeys.NumPad0:
                    return KeyboardNames.NumZero;
                case KeyboardKeys.NumPad1:
                    return KeyboardNames.NumOne;
                case KeyboardKeys.NumPad2:
                    return KeyboardNames.NumTwo;
                case KeyboardKeys.NumPad3:
                    return KeyboardNames.NumThree;
                case KeyboardKeys.NumPad4:
                    return KeyboardNames.NumFour;
                case KeyboardKeys.NumPad5:
                    return KeyboardNames.NumFive;
                case KeyboardKeys.NumPad6:
                    return KeyboardNames.NumSix;
                case KeyboardKeys.NumPad7:
                    return KeyboardNames.NumSeven;
                case KeyboardKeys.NumPad8:
                    return KeyboardNames.NumEight;
                case KeyboardKeys.NumPad9:
                    return KeyboardNames.NumNine;
                case KeyboardKeys.Multiply:
                    return KeyboardNames.NumAsterisk;
                case KeyboardKeys.Add:
                    return KeyboardNames.NumPlus;
                case KeyboardKeys.Subtract:
                    return KeyboardNames.NumMinus;
                case KeyboardKeys.Decimal:
                    return KeyboardNames.NumPeriod;
                case KeyboardKeys.Divide:
                    return KeyboardNames.NumSlash;
                case KeyboardKeys.F1:
                    return KeyboardNames.F1;
                case KeyboardKeys.F2:
                    return KeyboardNames.F2;
                case KeyboardKeys.F3:
                    return KeyboardNames.F3;
                case KeyboardKeys.F4:
                    return KeyboardNames.F4;
                case KeyboardKeys.F5:
                    return KeyboardNames.F5;
                case KeyboardKeys.F6:
                    return KeyboardNames.F6;
                case KeyboardKeys.F7:
                    return KeyboardNames.F7;
                case KeyboardKeys.F8:
                    return KeyboardNames.F8;
                case KeyboardKeys.F9:
                    return KeyboardNames.F9;
                case KeyboardKeys.F10:
                    return KeyboardNames.F10;
                case KeyboardKeys.F11:
                    return KeyboardNames.F11;
                case KeyboardKeys.F12:
                    return KeyboardNames.F12;
                case KeyboardKeys.NumLock:
                    return KeyboardNames.NumLock;
                case KeyboardKeys.Scroll:
                    return KeyboardNames.ScrollLock;
                case KeyboardKeys.LShiftKey:
                    return KeyboardNames.LeftShift;
                case KeyboardKeys.RShiftKey:
                    return KeyboardNames.RightShift;
                case KeyboardKeys.LControlKey:
                    return KeyboardNames.LeftControl;
                case KeyboardKeys.RControlKey:
                    return KeyboardNames.RightControl;
                case KeyboardKeys.LMenu:
                    return KeyboardNames.LeftAlt;
                case KeyboardKeys.RMenu:
                    return KeyboardNames.RightAlt;
                case KeyboardKeys.OemSemicolon:
                    return KeyboardNames.Semicolon;
                case KeyboardKeys.OemComma:
                    return KeyboardNames.Comma;
                case KeyboardKeys.OemMinus:
                    return KeyboardNames.Minus;
                case KeyboardKeys.OemPeriod:
                    return KeyboardNames.Period;
                case KeyboardKeys.OemTilde:
                    return KeyboardNames.Tilde;
                case KeyboardKeys.OemOpenBrackets:
                    return KeyboardNames.OpenBracket;
                case KeyboardKeys.OemCloseBrackets:
                    return KeyboardNames.CloseBracket;
                case KeyboardKeys.OemBackslash:
                    return KeyboardNames.Backslash;
                default:
                    return 0;
            }
        }

    }
}