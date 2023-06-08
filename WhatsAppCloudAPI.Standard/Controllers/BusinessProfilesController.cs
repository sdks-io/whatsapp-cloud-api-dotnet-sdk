// <copyright file="BusinessProfilesController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace WhatsAppCloudAPI.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using APIMatic.Core;
    using APIMatic.Core.Types;
    using APIMatic.Core.Utilities;
    using APIMatic.Core.Utilities.Date.Xml;
    using Newtonsoft.Json.Converters;
    using System.Net.Http;
    using WhatsAppCloudAPI.Standard;
    using WhatsAppCloudAPI.Standard.Authentication;
    using WhatsAppCloudAPI.Standard.Http.Client;
    using WhatsAppCloudAPI.Standard.Utilities;

    /// <summary>
    /// BusinessProfilesController.
    /// </summary>
    public class BusinessProfilesController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessProfilesController"/> class.
        /// </summary>
        internal BusinessProfilesController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// Use this endpoint to retrieve your business’ profile. This business profile is visible to consumers in the chat thread next to the profile photo. The profile information will contain a business profile ID which you can use to make API calls.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="fields">Optional parameter: Here you can specify what you want to know from your business as a comma separated list of fields. Available fields include: id, about, messaging_product, address, description, vertical, email, websites, profile_picture_url.</param>
        /// <returns>Returns the Models.GetBusinessProfileIDResponse response from the API call.</returns>
        public Models.GetBusinessProfileIDResponse GetBusinessProfileID(
                string phoneNumberID,
                string fields = null)
            => CoreHelper.RunTask(GetBusinessProfileIDAsync(phoneNumberID, fields));

        /// <summary>
        /// Use this endpoint to retrieve your business’ profile. This business profile is visible to consumers in the chat thread next to the profile photo. The profile information will contain a business profile ID which you can use to make API calls.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="fields">Optional parameter: Here you can specify what you want to know from your business as a comma separated list of fields. Available fields include: id, about, messaging_product, address, description, vertical, email, websites, profile_picture_url.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.GetBusinessProfileIDResponse response from the API call.</returns>
        public async Task<Models.GetBusinessProfileIDResponse> GetBusinessProfileIDAsync(
                string phoneNumberID,
                string fields = null,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.GetBusinessProfileIDResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/{Phone-Number-ID}/whatsapp_business_profile")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Query(_query => _query.Setup("fields", fields))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.GetBusinessProfileIDResponse>(_response)))
              .ExecuteAsync(cancellationToken);

        /// <summary>
        /// Use this endpoint to update your business’ profile information such as the business description, email or address.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse UpdateBusinessProfile(
                string phoneNumberID,
                Models.UpdateBusinessProfileRequest body)
            => CoreHelper.RunTask(UpdateBusinessProfileAsync(phoneNumberID, body));

        /// <summary>
        /// Use this endpoint to update your business’ profile information such as the business description, email or address.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> UpdateBusinessProfileAsync(
                string phoneNumberID,
                Models.UpdateBusinessProfileRequest body,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/whatsapp_business_profile")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Body(_bodyParameter => _bodyParameter.Setup(body))
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Header(_header => _header.Setup("Content-Type", "application/json"))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SuccessResponse>(_response)))
              .ExecuteAsync(cancellationToken);
    }
}