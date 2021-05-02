using System.Collections.Generic;
using System.Linq;
using Corsair.CUE.SDK;

namespace Illumilib.System {
    internal class CorsairLighting : LightingSystem {

        private DeviceInfo[] devices;

        public override bool Initialize() {
            try {
                CUESDK.CorsairPerformProtocolHandshake();
                if (CUESDK.CorsairGetLastError() != CorsairError.CE_Success)
                    return false;

                // initialize device information
                this.devices = new DeviceInfo[CUESDK.CorsairGetDeviceCount()];
                for (var d = 0; d < this.devices.Length; d++)
                    this.devices[d] = new DeviceInfo(d);

                return true;
            } catch {
                return false;
            }
        }

        public override void Dispose() {
            // no op
        }

        public override void SetAllLighting(float r, float g, float b) {
            foreach (var device in this.devices) {
                device.SetAllColors(r, g, b);
                device.UpdateColors();
            }
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        public override void SetKeyboardLighting(float r, float g, float b) {
            foreach (var device in this.devices) {
                if (!device.IsKeyboard())
                    continue;
                device.SetAllColors(r, g, b);
                device.UpdateColors();
            }
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        public override void SetKeyboardLighting(int x, int y, float r, float g, float b) {
            foreach (var device in this.devices) {
                if (!device.IsKeyboard())
                    continue;
                if (device.Positions.TryGetValue((x, y), out var leds)) {
                    foreach (var led in leds)
                        device.SetColorForId(led, r, g, b);
                    device.UpdateColors();
                }
            }
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        public override void SetKeyboardLighting(int x, int y, int width, int height, float r, float g, float b) {
            foreach (var device in this.devices) {
                if (!device.IsKeyboard())
                    continue;
                for (var xAdd = 0; xAdd < width; xAdd++) {
                    for (var yAdd = 0; yAdd < height; yAdd++) {
                        if (device.Positions.TryGetValue((x + xAdd, y + yAdd), out var leds)) {
                            foreach (var led in leds)
                                device.SetColorForId(led, r, g, b);
                        }
                    }
                }
                device.UpdateColors();
            }
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        public override void SetKeyboardLighting(KeyboardKeys key, float r, float g, float b) {
            var id = ConvertKey(key);
            foreach (var device in this.devices) {
                if (!device.IsKeyboard())
                    continue;
                if (device.SetColorForId(id, r, g, b)) {
                    device.UpdateColors();
                    break;
                }
            }
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        public override void SetMouseLighting(float r, float g, float b) {
            foreach (var device in this.devices) {
                if (!device.IsMouse())
                    continue;
                device.SetAllColors(r, g, b);
                device.UpdateColors();
            }
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        private static CorsairLedId ConvertKey(KeyboardKeys key) {
            switch (key) {
                case KeyboardKeys.Back:
                    return CorsairLedId.CLK_Backspace;
                case KeyboardKeys.Tab:
                    return CorsairLedId.CLK_Tab;
                case KeyboardKeys.Enter:
                    return CorsairLedId.CLK_Enter;
                case KeyboardKeys.Pause:
                    return CorsairLedId.CLK_PauseBreak;
                case KeyboardKeys.CapsLock:
                    return CorsairLedId.CLK_CapsLock;
                case KeyboardKeys.Escape:
                    return CorsairLedId.CLK_Escape;
                case KeyboardKeys.Space:
                    return CorsairLedId.CLK_Space;
                case KeyboardKeys.PageUp:
                    return CorsairLedId.CLK_PageUp;
                case KeyboardKeys.PageDown:
                    return CorsairLedId.CLK_PageDown;
                case KeyboardKeys.End:
                    return CorsairLedId.CLK_End;
                case KeyboardKeys.Home:
                    return CorsairLedId.CLK_Home;
                case KeyboardKeys.Left:
                    return CorsairLedId.CLK_LeftArrow;
                case KeyboardKeys.Up:
                    return CorsairLedId.CLK_UpArrow;
                case KeyboardKeys.Right:
                    return CorsairLedId.CLK_RightArrow;
                case KeyboardKeys.Down:
                    return CorsairLedId.CLK_DownArrow;
                case KeyboardKeys.PrintScreen:
                    return CorsairLedId.CLK_PrintScreen;
                case KeyboardKeys.Insert:
                    return CorsairLedId.CLK_Insert;
                case KeyboardKeys.Delete:
                    return CorsairLedId.CLK_Delete;
                case KeyboardKeys.D0:
                    return CorsairLedId.CLK_0;
                case KeyboardKeys.D1:
                    return CorsairLedId.CLK_1;
                case KeyboardKeys.D2:
                    return CorsairLedId.CLK_2;
                case KeyboardKeys.D3:
                    return CorsairLedId.CLK_3;
                case KeyboardKeys.D4:
                    return CorsairLedId.CLK_4;
                case KeyboardKeys.D5:
                    return CorsairLedId.CLK_5;
                case KeyboardKeys.D6:
                    return CorsairLedId.CLK_6;
                case KeyboardKeys.D7:
                    return CorsairLedId.CLK_7;
                case KeyboardKeys.D8:
                    return CorsairLedId.CLK_8;
                case KeyboardKeys.D9:
                    return CorsairLedId.CLK_9;
                case KeyboardKeys.A:
                    return CorsairLedId.CLK_A;
                case KeyboardKeys.B:
                    return CorsairLedId.CLK_B;
                case KeyboardKeys.C:
                    return CorsairLedId.CLK_C;
                case KeyboardKeys.D:
                    return CorsairLedId.CLK_D;
                case KeyboardKeys.E:
                    return CorsairLedId.CLK_E;
                case KeyboardKeys.F:
                    return CorsairLedId.CLK_F;
                case KeyboardKeys.G:
                    return CorsairLedId.CLK_G;
                case KeyboardKeys.H:
                    return CorsairLedId.CLK_H;
                case KeyboardKeys.I:
                    return CorsairLedId.CLK_I;
                case KeyboardKeys.J:
                    return CorsairLedId.CLK_J;
                case KeyboardKeys.K:
                    return CorsairLedId.CLK_K;
                case KeyboardKeys.L:
                    return CorsairLedId.CLK_L;
                case KeyboardKeys.M:
                    return CorsairLedId.CLK_M;
                case KeyboardKeys.N:
                    return CorsairLedId.CLK_N;
                case KeyboardKeys.O:
                    return CorsairLedId.CLK_O;
                case KeyboardKeys.P:
                    return CorsairLedId.CLK_P;
                case KeyboardKeys.Q:
                    return CorsairLedId.CLK_Q;
                case KeyboardKeys.R:
                    return CorsairLedId.CLK_R;
                case KeyboardKeys.S:
                    return CorsairLedId.CLK_S;
                case KeyboardKeys.T:
                    return CorsairLedId.CLK_T;
                case KeyboardKeys.U:
                    return CorsairLedId.CLK_U;
                case KeyboardKeys.V:
                    return CorsairLedId.CLK_V;
                case KeyboardKeys.W:
                    return CorsairLedId.CLK_W;
                case KeyboardKeys.X:
                    return CorsairLedId.CLK_X;
                case KeyboardKeys.Y:
                    return CorsairLedId.CLK_Y;
                case KeyboardKeys.Z:
                    return CorsairLedId.CLK_Z;
                case KeyboardKeys.LWin:
                    return CorsairLedId.CLH_LeftLogo;
                case KeyboardKeys.RWin:
                    return CorsairLedId.CLH_RightLogo;
                case KeyboardKeys.NumPad0:
                    return CorsairLedId.CLK_Keypad0;
                case KeyboardKeys.NumPad1:
                    return CorsairLedId.CLK_Keypad1;
                case KeyboardKeys.NumPad2:
                    return CorsairLedId.CLK_Keypad2;
                case KeyboardKeys.NumPad3:
                    return CorsairLedId.CLK_Keypad3;
                case KeyboardKeys.NumPad4:
                    return CorsairLedId.CLK_Keypad4;
                case KeyboardKeys.NumPad5:
                    return CorsairLedId.CLK_Keypad5;
                case KeyboardKeys.NumPad6:
                    return CorsairLedId.CLK_Keypad6;
                case KeyboardKeys.NumPad7:
                    return CorsairLedId.CLK_Keypad7;
                case KeyboardKeys.NumPad8:
                    return CorsairLedId.CLK_Keypad8;
                case KeyboardKeys.NumPad9:
                    return CorsairLedId.CLK_Keypad9;
                case KeyboardKeys.Multiply:
                    return CorsairLedId.CLK_KeypadAsterisk;
                case KeyboardKeys.Add:
                    return CorsairLedId.CLK_KeypadPlus;
                case KeyboardKeys.Subtract:
                    return CorsairLedId.CLK_KeypadMinus;
                case KeyboardKeys.Decimal:
                    return CorsairLedId.CLK_KeypadPeriodAndDelete;
                case KeyboardKeys.Divide:
                    return CorsairLedId.CLK_KeypadSlash;
                case KeyboardKeys.F1:
                    return CorsairLedId.CLK_F1;
                case KeyboardKeys.F2:
                    return CorsairLedId.CLK_F2;
                case KeyboardKeys.F3:
                    return CorsairLedId.CLK_F3;
                case KeyboardKeys.F4:
                    return CorsairLedId.CLK_F4;
                case KeyboardKeys.F5:
                    return CorsairLedId.CLK_F5;
                case KeyboardKeys.F6:
                    return CorsairLedId.CLK_F6;
                case KeyboardKeys.F7:
                    return CorsairLedId.CLK_F7;
                case KeyboardKeys.F8:
                    return CorsairLedId.CLK_F8;
                case KeyboardKeys.F9:
                    return CorsairLedId.CLK_F9;
                case KeyboardKeys.F10:
                    return CorsairLedId.CLK_F10;
                case KeyboardKeys.F11:
                    return CorsairLedId.CLK_F11;
                case KeyboardKeys.F12:
                    return CorsairLedId.CLK_F12;
                case KeyboardKeys.NumLock:
                    return CorsairLedId.CLK_NumLock;
                case KeyboardKeys.Scroll:
                    return CorsairLedId.CLK_ScrollLock;
                case KeyboardKeys.LShiftKey:
                    return CorsairLedId.CLK_LeftShift;
                case KeyboardKeys.RShiftKey:
                    return CorsairLedId.CLK_RightShift;
                case KeyboardKeys.LControlKey:
                    return CorsairLedId.CLK_LeftCtrl;
                case KeyboardKeys.RControlKey:
                    return CorsairLedId.CLK_RightCtrl;
                case KeyboardKeys.LMenu:
                    return CorsairLedId.CLK_LeftAlt;
                case KeyboardKeys.RMenu:
                    return CorsairLedId.CLK_RightAlt;
                case KeyboardKeys.OemSemicolon:
                    return CorsairLedId.CLK_SemicolonAndColon;
                case KeyboardKeys.OemComma:
                    return CorsairLedId.CLK_CommaAndLessThan;
                case KeyboardKeys.OemMinus:
                    return CorsairLedId.CLK_MinusAndUnderscore;
                case KeyboardKeys.OemPeriod:
                    return CorsairLedId.CLK_PeriodAndBiggerThan;
                case KeyboardKeys.OemTilde:
                    return CorsairLedId.CLK_GraveAccentAndTilde;
                case KeyboardKeys.OemOpenBrackets:
                    return CorsairLedId.CLK_BracketLeft;
                case KeyboardKeys.OemCloseBrackets:
                    return CorsairLedId.CLK_BracketRight;
                case KeyboardKeys.OemBackslash:
                    return CorsairLedId.CLK_Backslash;
                default:
                    return 0;
            }
        }

        private class DeviceInfo {

            public readonly Dictionary<(int, int), List<CorsairLedId>> Positions;

            private readonly int id;
            private readonly CorsairDeviceInfo info;
            private readonly CorsairLedColor[] colors;
            private readonly Dictionary<CorsairLedId, int> ledIdToColorIndex;

            public DeviceInfo(int id) {
                this.id = id;
                this.info = CUESDK.CorsairGetDeviceInfo(id);

                var positions = CUESDK.CorsairGetLedPositionsByDeviceIndex(id);
                this.ledIdToColorIndex = new Dictionary<CorsairLedId, int>();
                this.colors = new CorsairLedColor[positions.numberOfLeds];
                for (var c = 0; c < this.colors.Length; c++) {
                    var led = positions.pLedPosition[c].ledId;
                    this.colors[c] = new CorsairLedColor {ledId = led};
                    this.ledIdToColorIndex.Add(led, c);
                }

                // corsair doesn't supply index-based key positions so we're approximating it using millimeter width and height
                this.Positions = new Dictionary<(int, int), List<CorsairLedId>>();
                var minX = positions.pLedPosition.Min(p => p.left);
                var minY = positions.pLedPosition.Min(p => p.top);
                var width = positions.pLedPosition.Max(p => p.left + p.width) - minX;
                var height = positions.pLedPosition.Max(p => p.top + p.height) - minY;
                foreach (var led in positions.pLedPosition) {
                    var pos = (
                        (int) ((led.left + led.width / 2 - minX) / width * IllumilibLighting.KeyboardWidth),
                        (int) ((led.top + led.height / 2 - minY) / height * IllumilibLighting.KeyboardHeight));
                    if (!this.Positions.TryGetValue(pos, out var leds)) {
                        leds = new List<CorsairLedId>();
                        this.Positions.Add(pos, leds);
                    }
                    leds.Add(led.ledId);
                }
            }

            public void SetAllColors(float r, float g, float b) {
                foreach (var color in this.colors)
                    SetColor(color, r, g, b);
            }

            public bool SetColorForId(CorsairLedId id, float r, float g, float b) {
                if (this.ledIdToColorIndex.TryGetValue(id, out var index)) {
                    SetColor(this.colors[index], r, g, b);
                    return true;
                }
                return false;
            }

            public void UpdateColors() {
                CUESDK.CorsairSetLedsColorsBufferByDeviceIndex(this.id, this.colors.Length, this.colors);
            }

            public bool IsKeyboard() {
                return this.info.logicalLayout != CorsairLogicalLayout.CLL_Invalid;
            }

            public bool IsMouse() {
                return this.info.physicalLayout != CorsairPhysicalLayout.CPL_Invalid && !this.IsKeyboard();
            }

            private static void SetColor(CorsairLedColor color, float r, float g, float b) {
                color.r = (int) (r * 255);
                color.g = (int) (g * 255);
                color.b = (int) (b * 255);
            }

        }

    }
}