using System;
using System.Net.Http;
using HttpClientSE;
using JetBrains.Annotations;

namespace ConsoleHttpClientSE.Repository
{
    [UsedImplicitly]
    public sealed class RestMemoryRepository : IRestMemoryRepository
    {
        /// <summary>
        ///Base URL Web API
        /// </summary>
        [CanBeNull]
        private static readonly string ApiBaseAddress = AppConfigSetting.ReadSetting("BaseURI");

        /// <summary>
        /// Return RestConnection Connection and include param Query
        /// </summary>
        /// <typeparam name="T">T Entity</typeparam>
        /// <param name="getData">HttpClient Base Address Connection</param>
        /// <returns>T</returns>
        public T GetRestConnection<T>(Func<HttpClient, string, T> getData)
        {
            using (var httpClient = new HttpClient())
            {
                RestClient.BaseAddress = ApiBaseAddress;

                return getData(httpClient, ApiBaseAddress);
            }
        }
    }
}