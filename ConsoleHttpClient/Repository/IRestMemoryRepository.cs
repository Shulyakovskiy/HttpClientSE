using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace ConsoleHttpClientSE.Repository
{
    public interface IRestMemoryRepository
    {
        /// <summary>
        /// Return RestConnection Connection and include param Query
        /// </summary>
        /// <typeparam name="T">T Entity</typeparam>
        /// <param name="getData">HttpClient Base Address Connection</param>
        /// <returns>T</returns>
        [UsedImplicitly]
        T GetRestConnection<T>(Func<HttpClient, string, T> getData);
    }
}