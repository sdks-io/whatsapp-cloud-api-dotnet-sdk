// <copyright file="WhatsAppCloudAPIClient.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace WhatsAppCloudAPI.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APIMatic.Core;
    using APIMatic.Core.Authentication;
    using APIMatic.Core.Types;
    using WhatsAppCloudAPI.Standard.Authentication;
    using WhatsAppCloudAPI.Standard.Controllers;
    using WhatsAppCloudAPI.Standard.Http.Client;
    using WhatsAppCloudAPI.Standard.Utilities;

    /// <summary>
    /// The gateway for the SDK. This class acts as a factory for Controller and
    /// holds the configuration of the SDK.
    /// </summary>
    public sealed class WhatsAppCloudAPIClient : IConfiguration
    {
        // A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environment, Dictionary<Enum, string>> EnvironmentsMap =
            new Dictionary<Environment, Dictionary<Enum, string>>
        {
            {
                Environment.Production, new Dictionary<Enum, string>
                {
                    { Server.Default, "https://graph.facebook.com/{Version}" },
                }
            },
        };

        private readonly GlobalConfiguration globalConfiguration;
        private const string userAgent = "APIMATIC 3.0";
        private readonly BearerAuthManager bearerAuthManager;
        private readonly Lazy<MessagesController> messages;
        private readonly Lazy<RegistrationController> registration;
        private readonly Lazy<BusinessProfilesController> businessProfiles;
        private readonly Lazy<MediaController> media;
        private readonly Lazy<PhoneNumbersController> phoneNumbers;
        private readonly Lazy<TwoStepVerificationController> twoStepVerification;

        private WhatsAppCloudAPIClient(
            Environment environment,
            string version,
            string accessToken,
            IHttpClientConfiguration httpClientConfiguration)
        {
            this.Environment = environment;
            this.Version = version;
            this.HttpClientConfiguration = httpClientConfiguration;
            bearerAuthManager = new BearerAuthManager(accessToken);
            globalConfiguration = new GlobalConfiguration.Builder()
                .AuthManagers(new Dictionary<string, AuthManager> {
                        {"global", bearerAuthManager}
                })
                .HttpConfiguration(httpClientConfiguration)
                .ServerUrls(EnvironmentsMap[environment], Server.Default)
                .Parameters(globalParameter => globalParameter
                    .Template(templateParameter => templateParameter.Setup("Version", this.Version)))
                .UserAgent(userAgent)
                .Build();


            this.messages = new Lazy<MessagesController>(
                () => new MessagesController(globalConfiguration));
            this.registration = new Lazy<RegistrationController>(
                () => new RegistrationController(globalConfiguration));
            this.businessProfiles = new Lazy<BusinessProfilesController>(
                () => new BusinessProfilesController(globalConfiguration));
            this.media = new Lazy<MediaController>(
                () => new MediaController(globalConfiguration));
            this.phoneNumbers = new Lazy<PhoneNumbersController>(
                () => new PhoneNumbersController(globalConfiguration));
            this.twoStepVerification = new Lazy<TwoStepVerificationController>(
                () => new TwoStepVerificationController(globalConfiguration));
        }

        /// <summary>
        /// Gets MessagesController controller.
        /// </summary>
        public MessagesController MessagesController => this.messages.Value;

        /// <summary>
        /// Gets RegistrationController controller.
        /// </summary>
        public RegistrationController RegistrationController => this.registration.Value;

        /// <summary>
        /// Gets BusinessProfilesController controller.
        /// </summary>
        public BusinessProfilesController BusinessProfilesController => this.businessProfiles.Value;

        /// <summary>
        /// Gets MediaController controller.
        /// </summary>
        public MediaController MediaController => this.media.Value;

        /// <summary>
        /// Gets PhoneNumbersController controller.
        /// </summary>
        public PhoneNumbersController PhoneNumbersController => this.phoneNumbers.Value;

        /// <summary>
        /// Gets TwoStepVerificationController controller.
        /// </summary>
        public TwoStepVerificationController TwoStepVerificationController => this.twoStepVerification.Value;

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
        /// Gets Environment.
        /// Current API environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets Version.
        /// Version value.
        /// </summary>
        public string Version { get; }


        /// <summary>
        /// Gets the credentials to use with BearerAuth.
        /// </summary>
        private IBearerAuthCredentials BearerAuthCredentials => this.bearerAuthManager;

        /// <summary>
        /// Gets the access token to use with OAuth 2 authentication.
        /// </summary>
        public string AccessToken => this.BearerAuthCredentials.AccessToken;

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends
        /// it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:DEFAULT.</param>
        /// <returns>Returns the baseurl.</returns>
        public string GetBaseUri(Server alias = Server.Default)
        {
            return globalConfiguration.ServerUrl(alias);
        }

        /// <summary>
        /// Creates an object of the WhatsAppCloudAPIClient using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            Builder builder = new Builder()
                .Environment(this.Environment)
                .Version(this.Version)
                .AccessToken(BearerAuthCredentials.AccessToken)
                .HttpClientConfig(config => config.Build());

            return builder;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return
                $"Environment = {this.Environment}, " +
                $"Version = {this.Version}, " +
                $"HttpClientConfiguration = {this.HttpClientConfiguration}, ";
        }

        /// <summary>
        /// Creates the client using builder.
        /// </summary>
        /// <returns> WhatsAppCloudAPIClient.</returns>
        internal static WhatsAppCloudAPIClient CreateFromEnvironment()
        {
            var builder = new Builder();

            string environment = System.Environment.GetEnvironmentVariable("WHATS_APP_CLOUD_API_STANDARD_ENVIRONMENT");
            string version = System.Environment.GetEnvironmentVariable("WHATS_APP_CLOUD_API_STANDARD_VERSION");
            string accessToken = System.Environment.GetEnvironmentVariable("WHATS_APP_CLOUD_API_STANDARD_ACCESS_TOKEN");

            if (environment != null)
            {
                builder.Environment(ApiHelper.JsonDeserialize<Environment>($"\"{environment}\""));
            }

            if (version != null)
            {
                builder.Version(version);
            }

            if (accessToken != null)
            {
                builder.AccessToken(accessToken);
            }

            return builder.Build();
        }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private Environment environment = WhatsAppCloudAPI.Standard.Environment.Production;
            private string version = "v13.0";
            private string accessToken = "";
            private HttpClientConfiguration.Builder httpClientConfig = new HttpClientConfiguration.Builder();

            /// <summary>
            /// Sets credentials for BearerAuth.
            /// </summary>
            /// <param name="accessToken">AccessToken.</param>
            /// <returns>Builder.</returns>
            public Builder AccessToken(string accessToken)
            {
                this.accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
                return this;
            }

            /// <summary>
            /// Sets Environment.
            /// </summary>
            /// <param name="environment"> Environment. </param>
            /// <returns> Builder. </returns>
            public Builder Environment(Environment environment)
            {
                this.environment = environment;
                return this;
            }

            /// <summary>
            /// Sets Version.
            /// </summary>
            /// <param name="version"> Version. </param>
            /// <returns> Builder. </returns>
            public Builder Version(string version)
            {
                this.version = version ?? throw new ArgumentNullException(nameof(version));
                return this;
            }

            /// <summary>
            /// Sets HttpClientConfig.
            /// </summary>
            /// <param name="action"> Action. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientConfig(Action<HttpClientConfiguration.Builder> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                action(this.httpClientConfig);
                return this;
            }

           

            /// <summary>
            /// Creates an object of the WhatsAppCloudAPIClient using the values provided for the builder.
            /// </summary>
            /// <returns>WhatsAppCloudAPIClient.</returns>
            public WhatsAppCloudAPIClient Build()
            {

                return new WhatsAppCloudAPIClient(
                    environment,
                    version,
                    accessToken,
                    httpClientConfig.Build());
            }
        }
    }
}
