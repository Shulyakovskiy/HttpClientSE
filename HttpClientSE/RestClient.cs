using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SEA.HttpClientSE
{
    [UsedImplicitly]
    public static class RestClient
    {
        /// <summary>
        ///Base URL Web API
        /// </summary>
        [CanBeNull]
        public static string BaseAddress { get; set; }

        /// <summary>
        /// Extension GetQuery for HttpClient
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="httpClient">Instance of HttpClient</param>
        /// <param name="actionUrl">Action URL call method</param>
        /// <param name="userName">Login</param>
        /// <param name="password">Password</param>
        /// <param name="credential">Impersonate Windows Credential </param>
        /// <returns>T</returns>
        [UsedImplicitly]
        public static async Task<IList<T>> QueryGet<T>(this HttpClient httpClient, string actionUrl, string userName = null, string password = null, bool credential = false)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");
            List<T> entityModel;
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password));
            const int bitArrayDelemiter = 58;

            try
            {
                using (httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = credential }))
                {
                    httpClient.BaseAddress = new Uri(BaseAddress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (byteArray[0] != bitArrayDelemiter)
                    {
                        var token = await new HttpClient().QueryPostGetToken(userName, password);
                        httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);
                    }

                    //HTTP GET
                    var response = await httpClient.GetAsync(actionUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        entityModel = JsonConvert.DeserializeObject<List<T>>(jsonString);
                    }
                    else
                        throw new HttpRequestException(((int)response.EnsureSuccessStatusCode().StatusCode).ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            return entityModel.ToList();
        }

        /// <summary>
        /// Extension PostQuery for HttpClient
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="httpClient">Instance for HttpClient</param>
        /// <param name="actionUrl">Action URL call method</param>
        /// <param name="entity">Entity</param>
        /// <param name="userName">Login</param>
        /// <param name="password">Password</param>
        /// <param name="credential">Impersonate Windows Credential </param>
        /// <returns>HttpResponseMessage</returns>
        [UsedImplicitly]
        public static async Task<HttpResponseMessage> QueryPost<T>(this HttpClient httpClient, string actionUrl, T entity, string userName = null, string password = null, bool credential = false) where T : class ,new()
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");
            if (entity == null)
                throw new ArgumentNullException("entity");
            entity = new T();
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password));
            HttpResponseMessage response;
            const int bitArrayDelemiter = 58;

            try
            {
                using (httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = credential }))
                {
                    httpClient.BaseAddress = new Uri(BaseAddress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (byteArray[0] != bitArrayDelemiter)
                    {
                        var token = await new HttpClient().QueryPostGetToken(userName, password);
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }


                    //HTTP POST
                    response = await httpClient.PostAsJsonAsync(actionUrl, entity);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(((int)response.EnsureSuccessStatusCode().StatusCode).ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Return token application
        /// </summary>
        /// <param name="httpClient">httpclient instance</param>
        /// <param name="userName">Username</param>
        /// <param name="password">PaSSWord</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<string> QueryPostGetToken(this HttpClient httpClient, string userName, string password)
        {
            HttpResponseMessage response;
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("invalid_grant", "The user name or password is incorrect.");

            try
            {
                using (httpClient = new HttpClient())
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", userName),
                        new KeyValuePair<string, string>("Password", password)
                    };
                    var content = new FormUrlEncodedContent(pairs);

                    //Attempt to get a token from the token endpoint of the Web Api host:
                    response = httpClient.PostAsync(BaseAddress + "Token", content).Result;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }

            var result = response.Content.ReadAsStringAsync().Result;
            // De-Serialize into a dictionary and return:
            var tokenDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            return tokenDictionary["access_token"];
        }
    }
}
