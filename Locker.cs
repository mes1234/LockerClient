// Copyright (c) github.com/mes1234, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestClient;

namespace LockerClient
{
    /// <summary>
    /// <item><inheritdoc/></item>
    /// </summary>
    /// <inheritdoc cref="ILocker" />
    /// <remarks>This is test implementation</remarks>
    public class Locker : ILocker
    {
        private readonly HttpClient httpClient;
        private readonly LockerHttp lockerHttp;
        private HashSet<Guid> registeredLockers = new();
        private bool authorized;



        /// <summary>
        /// Initializes a new instance of the <see cref="Locker"/> class.
        /// </summary>
        public Locker()
        {
            httpClient = new HttpClient();
            lockerHttp = new LockerHttp(httpClient);
        }

        /// <inheritdoc  />
        public bool Authorized => authorized;

        /// <inheritdoc  />
        public byte[] this[string secretName] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        /// <inheritdoc  />
        public async Task<Guid> AddLockerAsync()
        {
            var response = await lockerHttp.AddlockerAsync();
            if (Guid.TryParse(response.Lockerid, out var lockerId))
            {
                registeredLockers.Add(lockerId);
                return lockerId;
            }
            else
            {
                throw new NullReferenceException("Cannot add new locker");
            }
        }

        /// <inheritdoc  />
        public async Task AuthorizeAsync(string username, string password)
        {
            var payload = Newtonsoft.Json.JsonConvert.SerializeObject(new Token
            {
                Username = username,
                Password = password,
            });

            var bytes = Encoding.UTF8.GetBytes(payload);
            using var stream = new MemoryStream(bytes);
            var response = await lockerHttp.TokenAsync(stream);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

            authorized = true;
        }

        /// <inheritdoc  />
        public void Connect(Guid lockerId)
        {
            throw new NotImplementedException();
        }
    }
}
