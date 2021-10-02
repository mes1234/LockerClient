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
        private readonly HttpClient _httpClient;
        private readonly LockerHttp _lockerHttp;
        private readonly ICoder _coder;
        private HashSet<Guid> _registeredLockers = new();
        private bool _authorized;


        /// <summary>
        /// Initializes a new instance of the <see cref="Locker"/> class.
        /// </summary>
        /// <param name="coder">coder to use</param>
        public Locker(ICoder coder)
        {
            _httpClient = new HttpClient();
            _lockerHttp = new LockerHttp(_httpClient);
            _coder = coder ?? throw new ArgumentNullException(nameof(coder));
        }

        /// <inheritdoc  />
        public bool Authorized => _authorized;

        /// <inheritdoc  />
        public byte[] this[string secretName]
        {
            get => GetData(secretName).Result;
            set => SaveData(secretName, value).Wait();
        }

        /// <inheritdoc  />
        public async Task<Guid> AddLockerAsync()
        {
            var response = await _lockerHttp.AddlockerAsync();
            if (Guid.TryParse(response.Lockerid, out var lockerId))
            {
                _registeredLockers.Add(lockerId);
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
            using var payload = Streamify(new Token
            {
                Username = username,
                Password = password,
            });

            var response = await _lockerHttp.TokenAsync(payload);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

            _authorized = true;
        }

        /// <inheritdoc  />
        public void Connect(Guid lockerId)
        {
            throw new NotImplementedException();
        }

        private async Task<byte[]> GetData(string secretName)
        {
            if (!_authorized) throw new AuthorizationException();

            using var payload = Streamify(new GetItem
            {
                Lockerid = Guid.NewGuid().ToString(),
                Secretid = secretName,
            });

            var content = await _lockerHttp.GetAsync(payload);

            return _coder.Decode(content.Content);
        }

        private async Task SaveData(string secretName, byte[] content)
        {
            if (!_authorized) throw new AuthorizationException();

            using var payload = Streamify(new AddItem
            {
                Lockerid = Guid.NewGuid().ToString(),
                Secretid = secretName,
                Content = _coder.Encode(content),
            });

            var response = await _lockerHttp.AddAsync(payload);

        }



        private MemoryStream Streamify(object obj)
        {
            var payload = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            var bytes = Encoding.UTF8.GetBytes(payload);
            var stream = new MemoryStream(bytes);

            return stream;
        }
    }
}
