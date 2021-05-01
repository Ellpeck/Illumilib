using System;
using System.Threading.Tasks;

namespace Illumilib.System {
    internal abstract class LightingSystem : IDisposable {

        public abstract Task<bool> Initialize();

        public abstract void Dispose();

        public abstract void SetAllLighting(float r, float g, float b);

        public abstract void SetKeyboardLighting(float r, float g, float b);

        public abstract void SetMouseLighting(float r, float g, float b);

        public abstract void SetKeyLighting(KeyboardKeys key, float r, float g, float b);

    }
}