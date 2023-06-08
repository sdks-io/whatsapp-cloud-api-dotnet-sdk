// <copyright file="MediaController.cs" company="APIMatic">
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
    /// MediaController.
    /// </summary>
    public class MediaController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaController"/> class.
        /// </summary>
        internal MediaController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// Used to upload media. All media files sent through this endpoint are encrypted and persist for 30 days.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="messagingProduct">Required parameter: Messaging service used for the request. In this case, use whatsapp..</param>
        /// <param name="file">Required parameter: Path to the file stored in your local directory. For example: "@/local/path/file.jpg"..</param>
        /// <param name="type">Required parameter: Type of media file being uploaded. See Supported Media Types for more information.    Supported options for images are: `image/jpeg`, `image/png`    Supported options for documents are: `text/plain`, `application/pdf`, `application/vnd.ms-powerpoint`, `application/msword`, `application/vnd.ms-excel`, `application/vnd.openxmlformats-officedocument.wordprocessingml.document`, `application/vnd.openxmlformats-officedocument.presentationml.presentation`, `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`  Supported options for audio are: `audio/aac`, `audio/mp4`, `audio/mpeg`, `audio/amr`, `audio/ogg`, `audio/opus`  Supported options for video are: `video/mp4`, `video/3gp`  Supported options for stickers are: `image/webp`.</param>
        /// <returns>Returns the Models.UploadMedia response from the API call.</returns>
        public Models.UploadMedia UploadMedia(
                string phoneNumberID,
                string messagingProduct,
                string file,
                string type)
            => CoreHelper.RunTask(UploadMediaAsync(phoneNumberID, messagingProduct, file, type));

        /// <summary>
        /// Used to upload media. All media files sent through this endpoint are encrypted and persist for 30 days.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="messagingProduct">Required parameter: Messaging service used for the request. In this case, use whatsapp..</param>
        /// <param name="file">Required parameter: Path to the file stored in your local directory. For example: "@/local/path/file.jpg"..</param>
        /// <param name="type">Required parameter: Type of media file being uploaded. See Supported Media Types for more information.    Supported options for images are: `image/jpeg`, `image/png`    Supported options for documents are: `text/plain`, `application/pdf`, `application/vnd.ms-powerpoint`, `application/msword`, `application/vnd.ms-excel`, `application/vnd.openxmlformats-officedocument.wordprocessingml.document`, `application/vnd.openxmlformats-officedocument.presentationml.presentation`, `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`  Supported options for audio are: `audio/aac`, `audio/mp4`, `audio/mpeg`, `audio/amr`, `audio/ogg`, `audio/opus`  Supported options for video are: `video/mp4`, `video/3gp`  Supported options for stickers are: `image/webp`.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.UploadMedia response from the API call.</returns>
        public async Task<Models.UploadMedia> UploadMediaAsync(
                string phoneNumberID,
                string messagingProduct,
                string file,
                string type,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.UploadMedia>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/{Phone-Number-ID}/media")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Phone-Number-ID", phoneNumberID))
                      .Form(_form => _form.Setup("messaging_product", messagingProduct))
                      .Form(_form => _form.Setup("file", file))
                      .Form(_form => _form.Setup("type", type))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.UploadMedia>(_response)))
              .ExecuteAsync(cancellationToken);

        /// <summary>
        /// To retrieve your media’s URL, make a request to this endpoint. Later, you can use this URL to download the media file.
        /// </summary>
        /// <param name="mediaID">Required parameter: Media object ID from either uploading media endpoint or media message Webhooks.</param>
        /// <returns>Returns the Models.RetrieveMediaURLResponse response from the API call.</returns>
        public Models.RetrieveMediaURLResponse RetrieveMediaURL(
                string mediaID)
            => CoreHelper.RunTask(RetrieveMediaURLAsync(mediaID));

        /// <summary>
        /// To retrieve your media’s URL, make a request to this endpoint. Later, you can use this URL to download the media file.
        /// </summary>
        /// <param name="mediaID">Required parameter: Media object ID from either uploading media endpoint or media message Webhooks.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.RetrieveMediaURLResponse response from the API call.</returns>
        public async Task<Models.RetrieveMediaURLResponse> RetrieveMediaURLAsync(
                string mediaID,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.RetrieveMediaURLResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/{Media-ID}")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Media-ID", mediaID))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.RetrieveMediaURLResponse>(_response)))
              .ExecuteAsync(cancellationToken);

        /// <summary>
        /// This endpoint can be used for deleting a media object.
        /// </summary>
        /// <param name="mediaID">Required parameter: Media object ID from either uploading media endpoint or media message Webhooks.</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse DeleteMedia(
                string mediaID)
            => CoreHelper.RunTask(DeleteMediaAsync(mediaID));

        /// <summary>
        /// This endpoint can be used for deleting a media object.
        /// </summary>
        /// <param name="mediaID">Required parameter: Media object ID from either uploading media endpoint or media message Webhooks.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> DeleteMediaAsync(
                string mediaID,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.SuccessResponse>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Delete, "/{Media-ID}")
                  .WithAuth("global")
                  .Parameters(_parameters => _parameters
                      .Template(_template => _template.Setup("Media-ID", mediaID))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .Deserializer(_response => ApiHelper.JsonDeserialize<Models.SuccessResponse>(_response)))
              .ExecuteAsync(cancellationToken);
    }
}