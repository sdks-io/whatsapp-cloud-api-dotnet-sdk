// <copyright file="MessagesController.cs" company="APIMatic">
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
    /// MessagesController.
    /// </summary>
    public class MessagesController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        internal MessagesController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// Use this endpoint to send text messages, media, message templates to your customers.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: To send a message, you must first assemble a message object with the content you want to send..</param>
        /// <returns>Returns the Models.SendMessageResponse response from the API call.</returns>
        public Models.SendMessageResponse SendMessage(
                string phoneNumberID,
                Models.Message body)
            => CoreHelper.RunTask(SendMessageAsync(phoneNumberID, body));

        /// <summary>
        /// Use this endpoint to send text messages, media, message templates to your customers.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: To send a message, you must first assemble a message object with the content you want to send..</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SendMessageResponse response from the API call.</returns>
        public async Task<Models.SendMessageResponse> SendMessageAsync(
                string phoneNumberID,
                Models.Message body,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SendMessageResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/messages")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Body(_bodyParameter => _bodyParameter.Setup(body))
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Header(_header => _header.Setup("Content-Type", "application/json"))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SendMessageResponse>(_response)))
              .ExecuteAsync(cancellationToken);
    }
}