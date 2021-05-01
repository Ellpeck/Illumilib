﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Illumilib.System;

namespace Illumilib {
    /// <summary>
    /// The class that houses all Illumilib methods.
    /// This class does not need to be instantiated.
    /// </summary>
    public static class IllumilibLighting {

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
        /// Sets the lighting for all keyboards to the given color.
        /// Note that, if Logitech is used, some keyboards do not support this method.
        /// </summary>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetKeyboardLighting(float r, float g, float b) {
            ForEach(s => s.SetKeyboardLighting(r, g, b));
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

        /// <summary>
        /// Sets the lighting for the specified <see cref="KeyboardKeys"/> to the given color.
        /// Only a single key can be specified at a time.
        /// </summary>
        /// <param name="key">The key value to set the lighting for</param>
        /// <param name="r">The color's red value, between 0 and 1</param>
        /// <param name="g">The color's green value, between 0 and 1</param>
        /// <param name="b">The color's blue value, between 0 and 1</param>
        public static void SetKeyLighting(KeyboardKeys key, float r, float g, float b) {
            ForEach(s => s.SetKeyLighting(key, r, g, b));
        }

        private static void ForEach(Action<LightingSystem> action) {
            if (!Initialized)
                throw new InvalidOperationException("Illumilib has not been initialized yet");
            foreach (var system in systems)
                action(system);
        }

    }
}