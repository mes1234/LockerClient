using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockerClient
{
    /// <summary>
    /// Utility to encode and decode content
    /// </summary>
    public interface ICoder
    {
        /// <summary>
        /// Decode string from http response to byte[]
        /// </summary>
        /// <param name="content">http response</param>
        /// <returns>byte[]</returns>
        public byte[] Decode(string content);

        /// <summary>
        /// Encode byte[] to http accepted form
        /// </summary>
        /// <param name="content">content</param>
        /// <returns>http accepted form</returns>
        public string Encode(byte[] content);
    }

    /// <inheritdoc cref="ICoder"/>
    public class Coder : ICoder
    {
        /// <inheritdoc />
        public byte[] Decode(string content)
        {
            var padding = new char[-content.Length & 3];
            Array.Fill<char>(padding, '=');
            var paddedContent = $"{content}{new string(padding)}";

            return Convert.FromBase64String(paddedContent);
        }

        /// <inheritdoc />
        public string Encode(byte[] content)
        {
            return Convert.ToBase64String(content);
        }
    }
}
