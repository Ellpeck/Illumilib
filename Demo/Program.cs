using System;
using System.Threading;
using Illumilib;

namespace Demo {
    internal static class Program {

        private static void Main(string[] args) {
            IllumilibLighting.Initialize();

            Console.WriteLine("Setting all lights to blue");
            IllumilibLighting.SetAllLighting(r: 0, g: 0, b: 1);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            IllumilibLighting.SetAllLighting(r: 0, g: 0, b: 0);

            Console.WriteLine("Setting specific positions");
            IllumilibLighting.SetKeyboardLighting(x: 6, y: 1, r: 1, g: 0, b: 1);
            IllumilibLighting.SetKeyboardLighting(x: 16, y: 5, r: 1, g: 0, b: 1);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            IllumilibLighting.SetKeyboardLighting(r: 0, g: 0, b: 0);
            IllumilibLighting.SetKeyboardLighting(x: 8, y: 2, width: 2, height: 2, r: 0, g: 1, b: 0);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            IllumilibLighting.SetKeyboardLighting(r: 0, g: 0, b: 0);

            Console.WriteLine("Doing a fun effect");
            for (var x = 0; x < IllumilibLighting.KeyboardWidth; x++) {
                IllumilibLighting.SetKeyboardLighting(x: x, y: 0, width: 1, height: IllumilibLighting.KeyboardHeight, r: 0, g: 0, b: 1);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
            }
            for (var x = IllumilibLighting.KeyboardWidth - 1; x >= 0; x--) {
                IllumilibLighting.SetKeyboardLighting(x: x, y: 0, width: 1, height: IllumilibLighting.KeyboardHeight, r: 0, g: 0, b: 0);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
            }

            Console.WriteLine("Going through the alphabet");
            for (var i = 65; i <= 90; i++) {
                var key = (KeyboardKeys) i;
                IllumilibLighting.SetKeyboardLighting(key: key, r: 0, g: 1, b: 0);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
                IllumilibLighting.SetKeyboardLighting(key: key, r: 0, g: 0, b: 0);
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine("Pulsing");
            for (var i = 0; i < 500; i++) {
                var value = (MathF.Sin(i / 50F * MathF.PI) + 1) / 2;
                IllumilibLighting.SetAllLighting(r: value, g: 0, b: value);
                Thread.Sleep(10);
            }
            IllumilibLighting.SetAllLighting(r: 0, g: 0, b: 0);

            Console.WriteLine("Setting all supported keys");
            foreach (var key in Enum.GetValues<KeyboardKeys>()) {
                IllumilibLighting.SetKeyboardLighting(key: key, r: 1, g: 0, b: 0);
                Thread.Sleep(50);
            }
            Thread.Sleep(TimeSpan.FromSeconds(15));

            Console.WriteLine("Done");
            IllumilibLighting.Dispose();
        }

    }
}