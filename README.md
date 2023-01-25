[![Build NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml)
[![Publish NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/publish-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/publish-package.yml)
![.NET](https://img.shields.io/badge/dynamic/xml?label=.NET&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FDolbyIO%2Fdolbyio-rest-apis-client-dotnet%2Fmain%2FDolbyIO.Rest%2FDolbyIO.Rest.csproj)
[![Nuget](https://img.shields.io/nuget/v/DolbyIO.Rest)](https://www.nuget.org/packages/DolbyIO.Rest)
[![License](https://img.shields.io/github/license/DolbyIO/comms-sdk-dotnet)](LICENSE)

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

JwtToken jwt;
using (DolbyIOClient client = new DolbyIOClient())
{
    jwt = await client.Authentication.GetApiAccessTokenAsync(APP_KEY, APP_SECRET);
}
```

## Communications Examples

### Get a Client Access Token

To get an access token that will be used by the client SDK for an end user to open a session against dolby.io, use the following code:

```csharp
using DolbyIO.Rest;
using DolbyIO.Rest.Models;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

JwtToken jwt;
using (DolbyIOClient client = new DolbyIOClient())
{
    jwt = await client.Communications.Authentication.GetClientAccessTokenAsync(APP_KEY, APP_SECRET);
}
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

JwtToken jwt = await client.Authentication.GetApiAccessTokenAsync(APP_KEY, APP_SECRET);

CreateConferenceOptions options = new CreateConferenceOptions
{
    Alias = "Conference Name",
    OwnerExternalId = "Fabien"
};

Conference conference = await client.Conferences.CreateAsync(jwt, options);
```

## Real-time Streaming Examples

### Get a publish token

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;

const string API_SECRET = "api_secret";

using DolbyIOClient client = new DolbyIOClient();

PublishResponse response = await client.Streaming.Director.PublishAsync(API_SECRET, "stream_name");

Console.WriteLine($"Token: {response.Data.Jwt}");
```

### Get a subscribe token

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Models;
using Newtonsoft.Json;

const string API_SECRET = "api_secret";

using DolbyIOClient client = new DolbyIOClient();

SubscribeResponse response = await client.Streaming.Director.SubscribeAsync(API_SECRET, "stream_name", "stream_account_id");

Console.WriteLine($"Token: {response.Data.Jwt}");
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
