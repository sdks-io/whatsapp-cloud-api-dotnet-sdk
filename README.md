
# Getting Started with WhatsApp Cloud API

## Introduction

Welcome to the WhatsApp API from Meta.

Individual developers and existing Business Service Providers (BSPs) can now send and receive messages via the WhatsApp API using a cloud-hosted version of the WhatsApp Business API. Compared to the previous solutions, the cloud-based WhatsApp API is simpler to use and is a more cost-effective way for businesses to use WhatsApp. Please keep in mind the following configurations:

| Name | Description |
| --- | --- |
| Version | Latest [Graph API version](https://developers.facebook.com/docs/graph-api/). For example: v13.0 |
| User-Access-Token | Your user access token after signing up at [developers.facebook.com](https://developers.facebook.com). |
| WABA-ID | Your WhatsApp Business Account (WABA) ID. |
| Phone-Number-ID | ID for the phone number connected to the WhatsApp Business API. You can get this with a [Get Phone Number ID request](3184f675-d289-46f1-88e5-e2b11549c418). |
| Business-ID | Your Business' ID. Once you have your Phone-Number-ID, make a [Get Business Profile request](#99fd3743-46cf-46c4-95b5-431c6a4eb0b0) to get your Business' ID. |
| Recipient-Phone-Number | Phone number that you want to send a WhatsApp message to. |
| Media-ID | ID for the media to [send a media message](#0a632754-3788-43bf-b785-ac6a73423d5a) or [media template message](#439c926a-8a6c-4972-ab2c-d99297716da9) to your customers. |
| Media-URL | URL for the media to [download media content](#cbe5ece3-246c-48f3-b338-074187dfef66). |

## Install the Package

If you are building with .NET CLI tools then you can also use the following command:

```bash
dotnet add package sdksio.WhatsappCloudApiSDK --version 1.0.0
```

You can also view the package at:
https://www.nuget.org/packages/sdksio.WhatsappCloudApiSDK/1.0.0

## Initialize the API Client

**_Note:_** Documentation for the client can be found [here.](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/client.md)

The following parameters are configurable for the API Client:

| Parameter | Type | Description |
|  --- | --- | --- |
| `Version` | `string` | *Default*: `"v13.0"` |
| `Environment` | Environment | The API environment. <br> **Default: `Environment.Production`** |
| `Timeout` | `TimeSpan` | Http client timeout.<br>*Default*: `TimeSpan.FromSeconds(100)` |
| `AccessToken` | `string` | The OAuth 2.0 Access Token to use for API requests. |

The API client can be initialized as follows:

```csharp
WhatsAppCloudAPI.Standard.WhatsAppCloudAPIClient client = new WhatsAppCloudAPI.Standard.WhatsAppCloudAPIClient.Builder()
    .AccessToken("AccessToken")
    .Environment(WhatsAppCloudAPI.Standard.Environment.Production)
    .Version("v13.0")
    .Build();
```

## Authorization

This API uses `OAuth 2 Bearer token`.

## List of APIs

* [Business Profiles](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/controllers/business-profiles.md)
* [Phone Numbers](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/controllers/phone-numbers.md)
* [Two-Step Verification](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/controllers/two-step-verification.md)
* [Messages](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/controllers/messages.md)
* [Registration](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/controllers/registration.md)
* [Media](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/controllers/media.md)

## Classes Documentation

* [Utility Classes](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/utility-classes.md)
* [HttpRequest](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/http-request.md)
* [HttpResponse](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/http-response.md)
* [HttpStringResponse](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/http-string-response.md)
* [HttpContext](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/http-context.md)
* [HttpClientConfiguration](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/http-client-configuration.md)
* [HttpClientConfiguration Builder](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/http-client-configuration-builder.md)
* [IAuthManager](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/i-auth-manager.md)
* [ApiException](https://www.github.com/sdks-io/whatsapp-cloud-api-dotnet-sdk/tree/1.0.0/doc/api-exception.md)

