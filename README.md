[![Build NuGet Package](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml/badge.svg)](https://github.com/DolbyIO/dolbyio-rest-apis-client-dotnet/actions/workflows/build-package.yml)
![.NET](https://img.shields.io/badge/dynamic/xml?label=.NET&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FDolbyIO%2Fdolbyio-rest-apis-client-dotnet%2Fmain%2FDolbyIO.Rest%2FDolbyIO.Rest.csproj)
![Nuget](https://img.shields.io/nuget/v/DolbyIO.Rest)
![License](https://img.shields.io/github/license/DolbyIO/comms-sdk-dotnet)

# Dolby.io REST APIs Client for .NET

.NET wrapper for the dolby.io REST Communications, Streaming and Media APIs.

## Install this SDK

If you want to use NuGet, use the following command:

```bash
dotnet add package DolbyIO.Rest
```

## Authentication

To get an access token that will be used by your server to perform backend operations like creating a conference, use the following code:

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

### Authenticate

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
