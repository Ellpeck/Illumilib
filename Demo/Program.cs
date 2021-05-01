using System;
using System.Threading;
using System.Threading.Tasks;
using Illumilib;

namespace Demo {
    internal static class Program {

        private static async Task Main(string[] args) {
            await IllumilibLighting.Initialize();

            Console.WriteLine("Setting all lights to blue");
            IllumilibLighting.SetAllLighting(0, 0, 1);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            IllumilibLighting.SetAllLighting(0, 0, 0);

            Console.WriteLine("Going through the alphabet");
            for (var i = 65; i <= 90; i++) {
                var key = (KeyboardKeys) i;
                IllumilibLighting.SetKeyLighting(key, 0, 1, 0);
                Thread.Sleep(TimeSpan.FromSeconds(0.25F));
                IllumilibLighting.SetKeyLighting(key, 0, 0, 0);
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
                IllumilibLighting.SetKeyLighting(key, 1, 0, 0);
                Thread.Sleep(50);
            }
            Thread.Sleep(TimeSpan.FromSeconds(15));

            Console.WriteLine("Done");
            IllumilibLighting.Dispose();
        }

    }
}