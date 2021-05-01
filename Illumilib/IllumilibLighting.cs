using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Illumilib.System;

namespace Illumilib {
    /// <summary>
    /// The class that houses all Illumilib methods.
    /// This class does not need to be instantiated.
    /// </summary>
    public static class IllumilibLighting {

        /// <summary>
        /// The maximum width that a keyboard can have, in amount of keys
        /// </summary>
        public const int KeyboardWidth = 22;
        /// <summary>
        /// The maximum height that a keyboard can have, in amount of keys
        /// </summary>
        public const int KeyboardHeight = 6;

        private static List<LightingSystem> systems;
        /// <summary>
        /// A property that returns whether Illumilib is currently initialized
        /// </summary>
        public static bool Initialized => systems != null;

        /// <summary>
        /// Initializes Illumilib, starting all of the supported lighting systems.
        /// Any lighting systems that are not supported, or for which devices are not present, will be ignored.
        /// This function runs asynchronously. 
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if Illumilib has already been <see cref="Initialized"/></exception>
        public static async Task Initialize() {
            if (Initialized)
                throw new InvalidOperationException("Illumilib has already been initialized");
            var ret = new List<LightingSystem>();
            foreach (var system in new LightingSystem[] {new LogitechLighting(), new RazerLighting()}) {
                if (await system.Initialize())
                    ret.Add(system);
            }
            systems = ret;
        }

        /// <summary>
        /// Disposes Illumilib, disposing all of the underlying lighting systems
        /// </summary>
        public static void Dispose() {
            if (!Initialized)
                return;
            ForEach(s => s.Dispose());
            systems = null;
        }

        /// <summary>
        /// Sets the lighting for all keyboards and mice to the given color
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetAllLighting(float r, float g, float b) {
            ForEach(s => s.SetAllLighting(r, g, b));
        }

        /// <summary>
        /// Sets the lighting for all keyboards to the given color
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetKeyboardLighting(float r, float g, float b) {
            ForEach(s => s.SetKeyboardLighting(r, g, b));
        }

        /// <summary>
        /// Sets the lighting for the given x, y position on the keyboard to the given color.
        /// The position is zero-based, with 0, 0 being in the top left corner of the keyboard.
        /// </summary>
        /// <param name="x">The zero-based x position of the key</param>
        /// <param name="y">The zero-based y position of the key</param>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the positions are out of range in relation to <see cref="KeyboardWidth"/> and <see cref="KeyboardHeight"/></exception>
        public static void SetKeyboardLighting(int x, int y, float r, float g, float b) {
            if (x < 0 || x >= KeyboardWidth)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= KeyboardHeight)
                throw new ArgumentOutOfRangeException(nameof(y));
            ForEach(s => s.SetKeyboardLighting(x, y, r, g, b));
        }

        /// <summary>
        /// Sets the lighting in the given area on the keyboard to the given color.
        /// The position is zero-based, with 0, 0 being in the top left corner of the keyboard.
        /// The position is the top left corner of the rectangle that represents the area to set colors in.
        /// </summary>
        /// <param name="x">The zero-based x position of the key</param>
        /// <param name="y">The zero-based y position of the key</param>
        /// <param name="width">The width of the area to set the color in</param>
        /// <param name="height">The height of the area to set the color in</param>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the positions are out of range in relation to <see cref="KeyboardWidth"/> and <see cref="KeyboardHeight"/></exception>
        public static void SetKeyboardLighting(int x, int y, int width, int height, float r, float g, float b) {
            if (x < 0 || x + width > KeyboardWidth)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y + height > KeyboardHeight)
                throw new ArgumentOutOfRangeException(nameof(y));
            ForEach(s => s.SetKeyboardLighting(x, y, width, height, r, g, b));
        }

        /// <summary>
        /// Sets the lighting for the specified <see cref="KeyboardKeys"/> to the given color.
        /// Only a single key can be specified at a time.
        /// </summary>
        /// <param name="key">The key value to set the lighting for</param>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetKeyboardLighting(KeyboardKeys key, float r, float g, float b) {
            ForEach(s => s.SetKeyboardLighting(key, r, g, b));
        }

        /// <summary>
        /// Sets the lighting for all mice to the given color
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetMouseLighting(float r, float g, float b) {
            ForEach(s => s.SetMouseLighting(r, g, b));
        }

        private static void ForEach(Action<LightingSystem> action) {
            if (!Initialized)
                throw new InvalidOperationException("Illumilib has not been initialized yet");
            foreach (var system in systems)
                action(system);
        }

    }
}