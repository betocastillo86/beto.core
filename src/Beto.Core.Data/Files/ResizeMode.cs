//-----------------------------------------------------------------------
// <copyright file="ResizeMode.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Files
{
    /// <summary>
    /// Resize Mode
    /// </summary>
    public enum ResizeMode
    {
        /// <summary>
        /// Crops the image
        /// </summary>
        Crop,

        /// <summary>
        /// Only resizes
        /// </summary>
        Pad,

        /// <summary>
        /// Resizes with border
        /// </summary>
        BoxPad,

        /// <summary>
        /// The maximum value resize
        /// </summary>
        Max,

        /// <summary>
        /// The minimum value resize
        /// </summary>
        Min
    }
}