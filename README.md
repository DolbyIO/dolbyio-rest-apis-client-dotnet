[![Build NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml)
[![Publish NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/publish-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/publish-package.yml)
![.NET](https://img.shields.io/badge/dynamic/xml?label=.NET&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FDolbyIO%2Fdolbyio-rest-apis-client-dotnet%2Fmain%2FDolbyIO.Rest%2FDolbyIO.Rest.csproj)
[![Nuget](https://img.shields.io/nuget/v/DolbyIO.Rest)](https://www.nuget.org/packages/DolbyIO.Rest)
[![License](https://img.shields.io/github/license/DolbyIO/dolbyio-rest-apis-client-dotnet)](LICENSE)

# Dolby.io REST APIs Client for .NET

.NET wrapper for the dolby.io REST [Communications](https://docs.dolby.io/communications-apis/reference/authentication-api), [Streaming](https://docs.dolby.io/streaming-apis/reference) and [Media](https://docs.dolby.io/media-processing/reference/media-enhance-overview) APIs.

## Install this SDK

If you want to use NuGet, use the following command:

```bash
dotnet add package DolbyIO.Rest
```

## Authentication

To get an access token that will be used by your server to perform backend operations like creating a conference, use the following code. This is only for the Communications and Media APIs.

```csharp
using DolbyIO.Rest;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

using DolbyIOClient client = new DolbyIOClient();

JwtToken jwt = await client.Authentication.GetApiAccessTokenAsync(APP_KEY, APP_SECRET);
```

To request a particular scope for this access token:

```csharp
using DolbyIO.Rest;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

using DolbyIOClient client = new DolbyIOClient();

JwtToken jwt = await client.Authentication.GetApiAccessTokenAsync(
    APP_KEY,
    APP_SECRET,
    3600,
    new string[] { "comms:client_access_token:create" }
);
```

## Communications Examples

### Get a Client Access Token

To get an access token that will be used by the client SDK for an end user to open a session against dolby.io, use the following code:

```csharp
using DolbyIO.Rest;
using DolbyIO.Rest.Models;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

using DolbyIOClient client = new DolbyIOClient();

JwtToken apiToken = await client.Authentication.GetApiAccessTokenAsync(
    APP_KEY,
    APP_SECRET,
    3600,
    new string[] { "comms:client_access_token:create" }
);

JwtToken cat = await client.Communications.Authentication
    .GetClientAccessTokenV2Async(apiToken, new string[] {"*"});
```

### Create a conference

To create a Dolby Voice conference, you first must retrieve an API Access Token, then use the following code to create the conference.

```csharp
using DolbyIO.Rest;
using DolbyIO.Rest.Communications.Models;
using DolbyIO.Rest.Models;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

using DolbyIOClient client = new DolbyIOClient();

JwtToken jwt = await client.Authentication.GetApiAccessTokenAsync(
    APP_KEY,
    APP_SECRET,
    3600,
    new string[] { "comms:conf:create" }
);

CreateConferenceOptions options = new CreateConferenceOptions
{
    Alias = "Conference Name",
    OwnerExternalId = "Fabien"
};

Conference conference = await client.Conferences.CreateAsync(jwt, options);
```

## Real-time Streaming Examples

### Create a publish token

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

using DolbyIOClient client = new DolbyIOClient();

CreatePublishToken create = new CreatePublishToken
{
    Label = "My token",
    Streams = new List<PublishTokenStream>
    {
        new PublishTokenStream
        {
            StreamName = "feedA"
        }
    }
};
PublishToken token = await client.Streaming.PublishToken.CreateAsync("api_secret", create);
```

### Create a subscribe token

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

using DolbyIOClient client = new DolbyIOClient();

CreateSubscribeToken create = new CreateSubscribeToken
{
    Label = "My token",
    Streams = new List<SubscribeTokenStream>
    {
        new SubscribeTokenStream
        {
            StreamName = "feedA"
        }
    }
};
SubscribeToken token = await client.Streaming.SubscribeToken.CreateAsync("api_secret", create);
```

## Media Examples

### Start an enhance job

To start an enhance job, use the following code:

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

using DolbyIOClient client = new DolbyIOClient();

JwtToken jwt = await client.Authentication.GetApiAccessTokenAsync(APP_KEY, APP_SECRET);

string jobDescription = JsonConvert.SerializeObject(new {
    content = new { type = "podcast" },
    input = "dlb://in/file.mp4",
    output = "dlb://out/file.mp4"
});

string jobId = await client.Media.Enhance.StartAsync(jwt, jobDescription);
Console.WriteLine($"Job ID: {jobId}");
```
