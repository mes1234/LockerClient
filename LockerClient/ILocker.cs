// Copyright (c) github.com/mes1234, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LockerClient
{
    /// <summary>
    /// Client allowing to access locker service using .NET C# code
    /// </summary>
    public interface ILocker
    {
        /// <summary>
        /// Gets a value indicating whether locker client is authorized
        /// </summary>
        public bool Authorized { get; }

        /// <summary>
        /// Access items in locker
        /// </summary>
        /// <param name="secretName">name of secret</param>
        /// <returns>byte array</returns>
        byte[] this[string secretName] { get; set; }

        /// <summary>
        /// Authorize connection with Locker Service.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Task</returns>
        public Task AuthorizeAsync(string username, string password);

        /// <summary>
        /// Add new  locker to Locker Service
        /// </summary>
        /// <returns>Id of new locker</returns>
        Task<Guid> AddLockerAsync();

        /// <summary>
        /// Define which locker to use
        /// </summary>
        /// <param name="lockerId">Locker to work with</param>
        public void UseLocker(Guid lockerId);
    }
}
