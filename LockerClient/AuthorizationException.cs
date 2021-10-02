using System;
using System.Collections.Generic;
using System.Text;

namespace LockerClient
{
    /// <summary>
    /// Not autorized to connect with Locker exception
    /// </summary>
    public class AuthorizationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        public AuthorizationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        public AuthorizationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="inner">inner exception</param>
        public AuthorizationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
