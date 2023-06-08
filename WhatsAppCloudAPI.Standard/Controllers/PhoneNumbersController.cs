// <copyright file="PhoneNumbersController.cs" company="APIMatic">
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
    /// PhoneNumbersController.
    /// </summary>
    public class PhoneNumbersController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbersController"/> class.
        /// </summary>
        internal PhoneNumbersController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// When you query all the phone numbers for a WhatsApp Business Account, each phone number has an id. You can directly query for a phone number using this id.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <returns>Returns the Models.GetPhoneNumberByIDResponse response from the API call.</returns>
        public Models.GetPhoneNumberByIDResponse GetPhoneNumberByID(
                string phoneNumberID)
            => CoreHelper.RunTask(GetPhoneNumberByIDAsync(phoneNumberID));

        /// <summary>
        /// When you query all the phone numbers for a WhatsApp Business Account, each phone number has an id. You can directly query for a phone number using this id.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.GetPhoneNumberByIDResponse response from the API call.</returns>
        public async Task<Models.GetPhoneNumberByIDResponse> GetPhoneNumberByIDAsync(
                string phoneNumberID,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.GetPhoneNumberByIDResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/{Phone-Number-ID}")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.GetPhoneNumberByIDResponse>(_response)))
              .ExecuteAsync(cancellationToken);

        /// <summary>
        /// Used to request a code to verify a phone number's ownership. You need to verify the phone number you want to use to send messages to your customers. Phone numbers must be verified through SMS/voice call. The verification process can be done through the Graph API calls specified below.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="codeMethod">Required parameter: Chosen method for verification..</param>
        /// <param name="locale">Required parameter: Your locale. For example: "en_US"..</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse RequestVerificationCode(
                string phoneNumberID,
                Models.RequestVerificationCodeMethodEnum codeMethod,
                string locale)
            => CoreHelper.RunTask(RequestVerificationCodeAsync(phoneNumberID, codeMethod, locale));

        /// <summary>
        /// Used to request a code to verify a phone number's ownership. You need to verify the phone number you want to use to send messages to your customers. Phone numbers must be verified through SMS/voice call. The verification process can be done through the Graph API calls specified below.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="codeMethod">Required parameter: Chosen method for verification..</param>
        /// <param name="locale">Required parameter: Your locale. For example: "en_US"..</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> RequestVerificationCodeAsync(
                string phoneNumberID,
                Models.RequestVerificationCodeMethodEnum codeMethod,
                string locale,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/request_code")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Form(_form => _form.Setup("code_method", ApiHelper.JsonSerialize(codeMethod).Trim('\"')))
                      .Form(_form => _form.Setup("locale", locale))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SuccessResponse>(_response)))
              .ExecuteAsync(cancellationToken);

        /// <summary>
        /// Used to verify a phone number's ownership. After you have received a SMS or Voice request code for verification, you need to verify the code that was sent to you.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="code">Required parameter: The code you received after calling FROM_PHONE_NUMBER_ID/request_code..</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse VerifyCode(
                string phoneNumberID,
                string code)
            => CoreHelper.RunTask(VerifyCodeAsync(phoneNumberID, code));

        /// <summary>
        /// Used to verify a phone number's ownership. After you have received a SMS or Voice request code for verification, you need to verify the code that was sent to you.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="code">Required parameter: The code you received after calling FROM_PHONE_NUMBER_ID/request_code..</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> VerifyCodeAsync(
                string phoneNumberID,
                string code,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/verify_code")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Form(_form => _form.Setup("code", code))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SuccessResponse>(_response)))
              .ExecuteAsync(cancellationToken);
    }
}