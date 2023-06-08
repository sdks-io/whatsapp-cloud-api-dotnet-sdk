// <copyright file="RegistrationController.cs" company="APIMatic">
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
    /// RegistrationController.
    /// </summary>
    public class RegistrationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        internal RegistrationController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// Used to register a phone number or to migrate WhatsApp Business Accounts from a current On-Premises deployment to the new Cloud-Based API. Business Solution Providers (BSPs) must authenticate themselves with an access token with the whatsapp_business_management permission.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse RegisterPhone(
                string phoneNumberID,
                Models.RegisterPhoneRequest body)
            => CoreHelper.RunTask(RegisterPhoneAsync(phoneNumberID, body));

        /// <summary>
        /// Used to register a phone number or to migrate WhatsApp Business Accounts from a current On-Premises deployment to the new Cloud-Based API. Business Solution Providers (BSPs) must authenticate themselves with an access token with the whatsapp_business_management permission.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> RegisterPhoneAsync(
                string phoneNumberID,
                Models.RegisterPhoneRequest body,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/register")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Body(_bodyParameter => _bodyParameter.Setup(body))
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Header(_header => _header.Setup("Content-Type", "application/json"))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SuccessResponse>(_response)))
              .ExecuteAsync(cancellationToken);

        /// <summary>
        /// Used to deregister a phone number. Deregister phone removes a previously registered phone. You can always re-register your phone using by repeating the registration process. Business Solution Providers (BSPs) must authenticate themselves with an access token with the whatsapp_business_management permission.
        /// </summary>
        /// <param name="contentType">Required parameter: Example: .</param>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse DeregisterPhone(
                Models.ContentTypeEnum contentType,
                string phoneNumberID)
            => CoreHelper.RunTask(DeregisterPhoneAsync(contentType, phoneNumberID));

        /// <summary>
        /// Used to deregister a phone number. Deregister phone removes a previously registered phone. You can always re-register your phone using by repeating the registration process. Business Solution Providers (BSPs) must authenticate themselves with an access token with the whatsapp_business_management permission.
        /// </summary>
        /// <param name="contentType">Required parameter: Example: .</param>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> DeregisterPhoneAsync(
                Models.ContentTypeEnum contentType,
                string phoneNumberID,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/deregister")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Header(_header => _header.Setup("Content-Type", ApiHelper.JsonSerialize(contentType).Trim('\"')))
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SuccessResponse>(_response)))
              .ExecuteAsync(cancellationToken);
    }
}