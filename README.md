[![Build NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml)
[![Publish NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/publish-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/publish-package.yml)
![.NET](https://img.shields.io/badge/dynamic/xml?label=.NET&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FDolbyIO%2Fdolbyio-rest-apis-client-dotnet%2Fmain%2FDolbyIO.Rest%2FDolbyIO.Rest.csproj)
[![Nuget](https://img.shields.io/nuget/v/DolbyIO.Rest)](https://www.nuget.org/packages/DolbyIO.Rest)
[![License](https://img.shields.io/github/license/DolbyIO/dolbyio-rest-apis-client-dotnet)](LICENSE)

# Dolby.io REST APIs Client for .NET

.NET wrapper for the [Dolby Millicast](https://docs.dolby.io/streaming-apis/reference) and [Media](https://docs.dolby.io/media-processing/reference/media-enhance-overview) APIs.

## Install this SDK

If you want to use NuGet, use the following command:

```bash
dotnet add package DolbyIO.Rest
```

## Real-time Streaming Examples

### Create a publish token

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

const string API_SECRET = "api_secret";

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
PublishToken token = await client.Streaming.PublishToken.CreateAsync(API_SECRET, create);
```

### Create a subscribe token

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Streaming.Models;
using Newtonsoft.Json;

const string API_SECRET = "api_secret";

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
SubscribeToken token = await client.Streaming.SubscribeToken.CreateAsync(API_SECRET, create);
```

## Media Examples

### Start an enhance job

To start an enhance job, use the following code:

```csharp
using System;
using DolbyIO.Rest;
using DolbyIO.Rest.Media.Models;
using Newtonsoft.Json;

const string APP_KEY = "app_key";
const string APP_SECRET = "app_secret";

using DolbyIOClient client = new DolbyIOClient();

JwtToken jwt = await client.Media.Authentication.GetApiAccessTokenAsync(APP_KEY, APP_SECRET);

string jobDescription = JsonConvert.SerializeObject(new {
    content = new { type = "podcast" },
    input = "dlb://in/file.mp4",
    output = "dlb://out/file.mp4"
});

string jobId = await client.Media.Enhance.StartAsync(jwt, jobDescription);
Console.WriteLine($"Job ID: {jobId}");
```
