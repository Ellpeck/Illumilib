using System;
using System.Threading;
using System.Threading.Tasks;
using Illumilib;

namespace Demo {
    internal static class Program {

        private static void Main(string[] args) {
            IllumilibLighting.Initialize();

            Console.WriteLine("Setting specific positions");
            IllumilibLighting.SetKeyboardLighting(0, 1, 0);
            IllumilibLighting.SetKeyboardLighting(6, 1, 1, 0, 1);
            IllumilibLighting.SetKeyboardLighting(16, 5, 1, 0, 1);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            IllumilibLighting.SetKeyboardLighting(1, 0, 0);
            IllumilibLighting.SetKeyboardLighting(8, 2, 2, 2, 0, 1, 0);
            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine("Setting all lights to blue");
            IllumilibLighting.SetAllLighting(0, 0, 1);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            IllumilibLighting.SetAllLighting(0, 0, 0);

            Console.WriteLine("Doing a fun effect");
            for (var x = 0; x < IllumilibLighting.KeyboardWidth; x++) {
                IllumilibLighting.SetKeyboardLighting(x, 0, 1, IllumilibLighting.KeyboardHeight, 0, 0, 1);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
            }
            for (var x = IllumilibLighting.KeyboardWidth - 1; x >= 0; x--) {
                IllumilibLighting.SetKeyboardLighting(x, 0, 1, IllumilibLighting.KeyboardHeight, 0, 0, 0);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
            }

            Console.WriteLine("Going through the alphabet");
            for (var i = 65; i <= 90; i++) {
                var key = (KeyboardKeys) i;
                IllumilibLighting.SetKeyboardLighting(key, 0, 1, 0);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
                IllumilibLighting.SetKeyboardLighting(key, 0, 0, 0);
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine("Pulsing");
            for (var i = 0; i < 500; i++) {
                var value = (MathF.Sin(i / 50F * MathF.PI) + 1) / 2;
                IllumilibLighting.SetAllLighting(value, 0, value);
                Thread.Sleep(10);
            }
            IllumilibLighting.SetAllLighting(0, 0, 0);

            Console.WriteLine("Setting all supported keys");
            foreach (var key in Enum.GetValues<KeyboardKeys>()) {
                IllumilibLighting.SetKeyboardLighting(key, 1, 0, 0);
                Thread.Sleep(50);
            }
            Thread.Sleep(TimeSpan.FromSeconds(15));

            Console.WriteLine("Done");
            IllumilibLighting.Dispose();
        }

    }
}