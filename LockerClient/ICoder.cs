using System;
using System.Collections.Generic;
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
            return Convert.FromBase64String(content);
        }

        /// <inheritdoc />
        public string Encode(byte[] content)
        {
            return Convert.ToBase64String(content);
        }
    }
}
