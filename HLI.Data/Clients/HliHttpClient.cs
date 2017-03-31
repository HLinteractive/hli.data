// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.HliHttpClient.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace HLI.Data.Clients
{
    /// <summary>
    ///     A client factory for HTTP services such as WebAPI. After constructing, use <see cref="Client" /> to make API calls.
    /// </summary>
    public class HliHttpClient
    {
        #region Static Fields

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
                                                                                    {
                                                                                        NullValueHandling =
                                                                                            NullValueHandling.Ignore,
                                                                                        MissingMemberHandling =
                                                                                            MissingMemberHandling.Ignore,
                                                                                        ReferenceLoopHandling =
                                                                                            ReferenceLoopHandling.Ignore,
                                                                                        DateTimeZoneHandling =
                                                                                            DateTimeZoneHandling.Local,
                                                                                        DateFormatHandling =
                                                                                            DateFormatHandling
                                                                                            .MicrosoftDateFormat
                                                                                    };

        #endregion

        #region Fields

        /// <summary>
        ///     Default API route if any
        /// </summary>
        private readonly string defaultApiRoute;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Init client with default values
        /// </summary>
        public HliHttpClient(string apiBaseUri)
            : this(apiBaseUri, null)
        {
        }

        /// <summary>
        ///     Construct the HTTP client
        /// </summary>
        /// <param name="apiBaseUri">Base API route, usually in the form [prefix]://[domain]/api/ (if default route is used)</param>
        /// <param name="defaultApiRoute">Default API route if any</param>
        /// <param name="declareJson"><c>True</c> (default) to make sure requests ask for JSON response</param>
        /// <example>
        ///     <c>var client = new HliHttpClient("https://mysite/api/", "books");</c>
        /// </example>
        public HliHttpClient(string apiBaseUri, string defaultApiRoute, bool declareJson = true)
            : this(apiBaseUri, null, defaultApiRoute, declareJson)
        {
        }

        /// <summary>
        ///     Construct the HTTP client
        /// </summary>
        /// <param name="apiBaseUri">Base API route, usually in the form [prefix]://[domain]/api/ (if default route is used)</param>
        /// <param name="defaultApiRoute">Default API route if any</param>
        /// <param name="declareJson"><c>True</c> (default) to make sure requests ask for JSON response</param>
        /// <param name="headers">Optional <see cref="Dictionary{TKey,TValue}" /> of headers</param>
        /// <example>
        ///     <c>var client = new HliHttpClient("https://mysite/api/", "books");</c>
        /// </example>
        public HliHttpClient(string apiBaseUri, Dictionary<string, string> headers, string defaultApiRoute, bool declareJson = true)
            : this(apiBaseUri, headers, defaultApiRoute, null, declareJson)
        {
        }

        /// <summary>
        ///     Construct the HTTP client
        /// </summary>
        /// <param name="apiBaseUri">Base API route, usually in the form [prefix]://[domain]/api/ (if default route is used)</param>
        /// <param name="defaultApiRoute">Default API route if any</param>
        /// <param name="declareJson"><c>True</c> (default) to make sure requests ask for JSON response</param>
        /// <param name="messageHandler">Optional <see cref="HttpMessageHandler" /></param>
        /// <param name="headers">Optional <see cref="Dictionary{TKey,TValue}" /> of headers</param>
        /// <example>
        ///     <c>var client = new HliHttpClient("https://mysite/api/", "books");</c>
        /// </example>
        public HliHttpClient(
            string apiBaseUri,
            Dictionary<string, string> headers,
            string defaultApiRoute,
            HttpMessageHandler messageHandler,
            bool declareJson = true)
            : this(apiBaseUri, defaultApiRoute, messageHandler, new List<KeyValuePair<string, IEnumerable<string>>>(), declareJson)
        {
            if (headers == null)
            {
                return;
            }

            foreach (var header in headers)
            {
                this.Client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        /// <summary>
        ///     Construct the HTTP client
        /// </summary>
        /// <param name="apiBaseUri">Base API route, usually in the form [prefix]://[domain]/api/ (if default route is used)</param>
        /// <param name="defaultApiRoute">Default API route if any</param>
        /// <param name="declareJson"><c>True</c> (default) to make sure requests ask for JSON response</param>
        /// <param name="messageHandler">Optional <see cref="HttpMessageHandler" /></param>
        /// <param name="headers">Optional <see cref="HttpRequestHeaders" /></param>
        /// <example>
        ///     <c>var client = new HliHttpClient("https://mysite/api/", "books");</c>
        /// </example>
        public HliHttpClient(
            string apiBaseUri,
            string defaultApiRoute,
            HttpMessageHandler messageHandler,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers,
            bool declareJson = true)
        {
            this.defaultApiRoute = defaultApiRoute;
            var baseAddress = new Uri(apiBaseUri);
            this.Client = messageHandler != null
                              ? new HttpClient(messageHandler) { BaseAddress = baseAddress }
                              : new HttpClient { BaseAddress = baseAddress };

            if (declareJson)
            {
                // Make sure requests ask for JSON response
                this.Client.DefaultRequestHeaders.Clear();
                this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            if (headers == null)
            {
                return;
            }

            foreach (var header in headers)
            {
                this.AddRequestHeader(header);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Client for HTTP calls
        /// </summary>
        public HttpClient Client { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Add the specified value to <see cref="HttpRequestHeaders" /> to <see cref="Client" />
        /// </summary>
        /// <param name="header">Request header to add</param>
        public void AddRequestHeader(KeyValuePair<string, IEnumerable<string>> header)
        {
            this.Client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        /// <summary>
        ///     Delete an item
        /// </summary>
        /// <param name="apiRoute">Route to use</param>
        /// <param name="id">Entity to delete</param>
        public async Task Delete(Guid id, string apiRoute = null)
        {
            var route = apiRoute ?? this.defaultApiRoute;

            // Post to API client
            var requestUri = string.Format("{0}/{1}", route, id);

            var response = await this.Client.DeleteAsync(requestUri);

            // Validate response
            await ValidateHttpResponse(response);
        }

        /// <summary>
        ///     Read data from <see cref="Client" /> converted to type of <see cref="TProperty" />.
        /// </summary>
        /// <typeparam name="TProperty">Return type of API call</typeparam>
        /// <param name="apiRoute">Optional route. Else <see cref="defaultApiRoute" /> is used (assuming it was supplied)</param>
        /// <param name="parameter">Optional parameter. Will be appended after the final slash (/).</param>
        /// <returns>Result of API call as specified <see cref="TProperty" /> type</returns>
        public async Task<TProperty> GetDataAsTypeAsync<TProperty>(string apiRoute = null, object parameter = null)
        {
            // Form request
            var requestUri = apiRoute ?? this.defaultApiRoute;
            if (parameter != null)
            {
                requestUri += "/" + parameter;
            }

            Debug.WriteLine("SENDING HliHttpClient REQUEST: ");
            Debug.WriteLine(requestUri);

            // Read from API client
            var result = await this.Client.GetAsync(requestUri);

            await ValidateHttpResponse(result);

            // Transform to type
            var projection = await JsonDeserialize<TProperty>(result);

            // Return result assuming no exception occured
            return projection;
        }

        /// <summary>
        ///     Post data to <see cref="Client" /> converted to type of <see cref="TProperty" />.
        /// </summary>
        /// <typeparam name="TProperty">Return type of API call</typeparam>
        /// <param name="apiRoute">Optional route. Else <see cref="defaultApiRoute" /> is used (assuming it was supplied)</param>
        /// <param name="entity">Object to post</param>
        /// <returns>Result of API call as specified <see cref="TProperty" /> type</returns>
        public async Task<TProperty> PostDataAsTypeAsync<TProperty>(TProperty entity, string apiRoute = null)
        {
            // Form request
            var requestUri = apiRoute ?? this.defaultApiRoute;

            var result = await this.Client.PostAsync(requestUri, ToHttpContent(entity));

            await ValidateHttpResponse(result);

            // Transform to type
            var projection = await JsonDeserialize<TProperty>(result);

            // Return result assuming no exception occured
            return projection;
        }

        /// <summary>
        ///     Post data to <see cref="Client" />.
        /// </summary>
        /// <typeparam name="TProperty">Type of the <paramref name="entity" /></typeparam>
        /// <typeparam name="TR">Expected return type</typeparam>
        /// <param name="apiRoute">Optional route. Else <see cref="defaultApiRoute" /> is used (assuming it was supplied)</param>
        /// <param name="entity">Object to post</param>
        /// <returns>Result of API call as specified <see cref="TR" /> type</returns>
        public async Task<TR> PostDataAsTypeAsync<TProperty, TR>(TProperty entity, string apiRoute = null)
        {
            // Form request
            var requestUri = apiRoute ?? this.defaultApiRoute;

            var result = await this.Client.PostAsync(requestUri, ToHttpContent(entity));

            await ValidateHttpResponse(result);

            // Transform to type
            var projection = await JsonDeserialize<TR>(result);

            // Return result assuming no exception occured
            return projection;
        }

        /// <summary>
        ///     Put data to <see cref="Client" /> converted to type of <see cref="TProperty" />.
        /// </summary>
        /// <typeparam name="TProperty">Return type of API call</typeparam>
        /// <param name="apiRoute">Optional route. Else <see cref="defaultApiRoute" /> is used (assuming it was supplied)</param>
        /// <param name="entity">Object to put</param>
        /// <returns>Result of API call as specified <see cref="TProperty" /> type</returns>
        public async Task<TProperty> PutDataAsTypeAsync<TProperty>(TProperty entity, string apiRoute = null)
        {
            // Form request
            var requestUri = apiRoute ?? this.defaultApiRoute;

            var result = await this.Client.PutAsync(requestUri, ToHttpContent(entity));

            await ValidateHttpResponse(result);

            // Transform to type
            var projection = await JsonDeserialize<TProperty>(result);

            // Return result assuming no exception occured
            return projection;
        }

        #endregion

        #region Methods

        private static async Task<TProperty> JsonDeserialize<TProperty>(HttpResponseMessage resonse)
        {
            var stringResult = await resonse.Content.ReadAsStringAsync();
            var projection = JsonConvert.DeserializeObject<TProperty>(stringResult, JsonSerializerSettings);
            return projection;
        }

        private static HttpContent ToHttpContent(object model)
        {
            var param = JsonConvert.SerializeObject(model);
            return new StringContent(param, Encoding.UTF8, "application/json");
        }

        /// <summary>
        ///     Validates the response of a REST request
        /// </summary>
        /// <param name="response"></param>
        private static async Task ValidateHttpResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            try
            {
                // Transform to type
                var projection = await JsonDeserialize<JsonExceptionResponse>(response);

                if (projection != null)
                {
                    throw new Exception(projection.ToString());
                }
            }
            catch
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                throw new Exception(stringContent);
            }

            response.EnsureSuccessStatusCode();
        }

        #endregion
    }
}