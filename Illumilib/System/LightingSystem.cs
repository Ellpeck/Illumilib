using System;
using System.Threading.Tasks;

namespace Illumilib.System {
    internal abstract class LightingSystem : IDisposable {

        public abstract bool Initialize();

        public abstract void Dispose();

        public abstract void SetAllLighting(float r, float g, float b);

        public abstract void SetKeyboardLighting(float r, float g, float b);

        public abstract void SetKeyboardLighting(int x, int y, float r, float g, float b);

        public abstract void SetKeyboardLighting(int x, int y, int width, int height, float r, float g, float b);

        public abstract void SetKeyboardLighting(KeyboardKeys key, float r, float g, float b);

        public abstract void SetMouseLighting(float r, float g, float b);

    }
}