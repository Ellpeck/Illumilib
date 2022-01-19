using Illumilib.System;

namespace Illumilib {
    /// <summary>
    /// An enumeration of possible lighting engines that Illumilib currently supports.
    /// To query whether a lighting type is available, see <see cref="IllumilibLighting.IsEnabled"/>.
    /// </summary>
    public enum LightingType {

        /// <summary>
        /// The logitech lighting type, controlled by <see cref="LogitechLighting"/>.
        /// </summary>
        Logitech,
        /// <summary>
        /// The corsair lighting type, controlled by <see cref="CorsairLighting"/>.
        /// </summary>
        Corsair,
        /// <summary>
        /// The razer lighting type, controlled by <see cref="RazerLighting"/>.
        /// </summary>
        Razer

    }
}