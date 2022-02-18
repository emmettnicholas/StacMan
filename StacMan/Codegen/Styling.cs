// <auto-generated>
//     This file was generated by a T4 template.
//     Don't change it directly as your change would get overwritten. Instead, make changes
//     to the .tt file (i.e. the T4 template) and save it to regenerate this file.
// </auto-generated>

using System;
using System.Text.Json.Serialization;

namespace StackExchange.StacMan
{
    /// <summary>
    /// StacMan Styling, corresponding to Stack Exchange API v2's styling type
    /// http://api.stackexchange.com/docs/types/styling
    /// </summary>
    public partial class Styling : StacManType
    {
        /// <summary>
        /// link_color
        /// </summary>
        [JsonPropertyName("link_color")]
        public string LinkColor { get; init; }

        /// <summary>
        /// tag_background_color
        /// </summary>
        [JsonPropertyName("tag_background_color")]
        public string TagBackgroundColor { get; init; }

        /// <summary>
        /// tag_foreground_color
        /// </summary>
        [JsonPropertyName("tag_foreground_color")]
        public string TagForegroundColor { get; init; }

    }
}
