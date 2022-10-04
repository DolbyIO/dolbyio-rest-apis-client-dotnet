using System;
using System.Net.Http;

namespace DolbyIO.Rest.Communications.Monitor
{
    public sealed class Monitoring
    {
        public Conferences Conferences { get; }

        public Recordings Recordings { get; }

        public Webhooks Webhooks { get; }

        internal Monitoring(HttpClient httpClient)
        {
            Conferences = new Conferences(httpClient);
            Recordings = new Recordings(httpClient);
            Webhooks = new Webhooks(httpClient);
        }
    }
}

