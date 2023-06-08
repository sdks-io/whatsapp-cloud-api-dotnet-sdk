// <copyright file="TwoStepVerificationController.cs" company="APIMatic">
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
    /// TwoStepVerificationController.
    /// </summary>
    public class TwoStepVerificationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoStepVerificationController"/> class.
        /// </summary>
        internal TwoStepVerificationController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// You are required to set up two-step verification for your phone number, as this provides an extra layer of security to the business accounts. You can use this endpoint to change two-step verification code associated with your account. .
        /// After you change the verification code, future requests like changing the name, must use the new code. .
        /// You set up two-factor verification and register a phone number in the same API call.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse SetTwoStepVerificationCode(
                string phoneNumberID,
                Models.SetTwoStepVerificationCodeRequest body)
            => CoreHelper.RunTask(SetTwoStepVerificationCodeAsync(phoneNumberID, body));

        /// <summary>
        /// You are required to set up two-step verification for your phone number, as this provides an extra layer of security to the business accounts. You can use this endpoint to change two-step verification code associated with your account. .
        /// After you change the verification code, future requests like changing the name, must use the new code. .
        /// You set up two-factor verification and register a phone number in the same API call.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> SetTwoStepVerificationCodeAsync(
                string phoneNumberID,
                Models.SetTwoStepVerificationCodeRequest body,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}")
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