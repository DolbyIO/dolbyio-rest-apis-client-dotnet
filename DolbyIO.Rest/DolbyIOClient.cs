using System;
using System.Net.Http;
using DolbyIO.Rest.Communications;

namespace DolbyIO.Rest
{
    public sealed class DolbyIOClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        
        public Comms Communications { get; }

        public DolbyIOClient()
            : this(new HttpClient())
        {
        }

        internal DolbyIOClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            Communications = new Comms(_httpClient);
        }

        ~DolbyIOClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases the unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources.
        /// </summary>
        /// <param name="disposing">A boolean that indicates whether the method call comes from the Dispose method (true) or from a finalizer (false).</param>
        void Dispose(bool disposing)
        {
            _httpClient.Dispose();
        }
    }
}

