﻿using System;
using System.Collections.Generic;
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

        private static Dictionary<LightingType, LightingSystem> systems;
        /// <summary>
        /// A property that returns whether Illumilib is currently initialized
        /// </summary>
        public static bool Initialized => IllumilibLighting.systems != null;

        /// <summary>
        /// Initializes Illumilib, starting all of the supported lighting systems.
        /// Any lighting systems that are not supported, or for which devices are not present, will be ignored.
        /// </summary>
        /// <returns>Whether at least one lighting system was successfully initialized</returns>
        /// <exception cref="InvalidOperationException">Thrown if Illumilib has already been <see cref="Initialized"/></exception>
        public static bool Initialize() {
            if (IllumilibLighting.Initialized)
                throw new InvalidOperationException("Illumilib has already been initialized");
            IllumilibLighting.systems = new Dictionary<LightingType, LightingSystem>();
            foreach (var system in new LightingSystem[] {new LogitechLighting(), new RazerLighting(), new CorsairLighting()}) {
                if (system.Initialize())
                    IllumilibLighting.systems.Add(system.Type, system);
            }
            return IllumilibLighting.systems.Count > 0;
        }

        /// <summary>
        /// Disposes Illumilib, disposing all of the underlying lighting systems
        /// </summary>
        public static void Dispose() {
            if (!IllumilibLighting.Initialized)
                return;
            foreach (var system in IllumilibLighting.systems.Values)
                system.Dispose();
            IllumilibLighting.systems = null;
        }

        /// <summary>
        /// Returns whether the given <see cref="LightingType"/> has been initialized successfully and is enabled.
        /// </summary>
        /// <param name="type">The <see cref="LightingType"/> to query.</param>
        /// <returns>Whether the given <see cref="LightingType"/> has been initialized and is enabled.</returns>
        public static bool IsEnabled(LightingType type) {
            IllumilibLighting.EnsureInitialized();
            return IllumilibLighting.systems.ContainsKey(type);
        }

        /// <summary>
        /// Sets the lighting for all keyboards and mice to the given color
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetAllLighting(float r, float g, float b) {
            IllumilibLighting.EnsureInitialized();
            foreach (var system in IllumilibLighting.systems.Values)
                system.SetAllLighting(r, g, b);
        }

        /// <summary>
        /// Sets the lighting for all keyboards to the given color
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetKeyboardLighting(float r, float g, float b) {
            IllumilibLighting.EnsureInitialized();
            foreach (var system in IllumilibLighting.systems.Values)
                system.SetKeyboardLighting(r, g, b);
        }

        /// <summary>
        /// Sets the lighting for the given x, y position on the keyboard to the given color.
        /// The position is zero-based, with 0, 0 being the key in the top left corner of the keyboard.
        /// </summary>
        /// <param name="x">The zero-based x position of the key</param>
        /// <param name="y">The zero-based y position of the key</param>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the positions are out of range in relation to <see cref="KeyboardWidth"/> and <see cref="KeyboardHeight"/></exception>
        public static void SetKeyboardLighting(int x, int y, float r, float g, float b) {
            IllumilibLighting.EnsureInitialized();
            if (x < 0 || x >= IllumilibLighting.KeyboardWidth)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= IllumilibLighting.KeyboardHeight)
                throw new ArgumentOutOfRangeException(nameof(y));
            foreach (var system in IllumilibLighting.systems.Values)
                system.SetKeyboardLighting(x, y, r, g, b);
        }

        /// <summary>
        /// Sets the lighting in the given area on the keyboard to the given color.
        /// The position is zero-based, with 0, 0 being the key in the top left corner of the keyboard.
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
            IllumilibLighting.EnsureInitialized();
            if (x < 0 || x + width > IllumilibLighting.KeyboardWidth)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y + height > IllumilibLighting.KeyboardHeight)
                throw new ArgumentOutOfRangeException(nameof(y));
            foreach (var system in IllumilibLighting.systems.Values)
                system.SetKeyboardLighting(x, y, width, height, r, g, b);
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
            IllumilibLighting.EnsureInitialized();
            foreach (var system in IllumilibLighting.systems.Values)
                system.SetKeyboardLighting(key, r, g, b);
        }

        /// <summary>
        /// Sets the lighting for all mice to the given color
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetMouseLighting(float r, float g, float b) {
            IllumilibLighting.EnsureInitialized();
            foreach (var system in IllumilibLighting.systems.Values)
                system.SetMouseLighting(r, g, b);
        }

        private static void EnsureInitialized() {
            if (!IllumilibLighting.Initialized)
                throw new InvalidOperationException("Illumilib has not been initialized yet");
        }

    }
}