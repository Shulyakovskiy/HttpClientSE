using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleHttpClientSE.Entity;
using ConsoleHttpClientSE.Repository;
using HttpClientSE;
using JetBrains.Annotations;

namespace ConsoleHttpClientSE
{
    [UsedImplicitly]
    class Program
    {
        static void Main()
        {
            RunAsync().Wait();

            Console.ReadLine();
        }

        /// <summary>
        /// Init Method
        /// </summary>
        /// <returns>Task</returns>
        static async Task RunAsync()
        {
            var restRepository = new RestRepository();
            var restMemoryRepository = new RestMemoryRepository();
            Console.Title = "RestClient";

            #region <<< HTTP POST Get Token >>>
            var token =
                await
                    restRepository.GetRestConnection(
                            (c, x) => new HttpClient()
                            .QueryPostGetToken("sysadmin@mail.ru", "Pa$$w0rd654"));
            Console.WriteLine(token);

            #endregion
            Console.WriteLine(new string('#', 30));

            #region <<< HTTP GET Security >>>
            var responce = await restRepository
                        .GetRestConnection((c, x) =>new HttpClient()
                        .QueryGet<string>(string.Format("api/values"), "sysadmin@mail.ru", "Pa$$w0rd654"));

            Console.WriteLine(responce.Select(c => c).FirstOrDefault());
            #endregion

            #region <<< HTTP GET >>>
            const int parId = 1;
            var response = await restMemoryRepository
                    .GetRestConnection((c, x) => new HttpClient()
                    .QueryGet<Employee>(string.Format("api/employee/getemployebyid/{0}", @parId)));
            Console.WriteLine(response.Select(c => c.FirstName).FirstOrDefault());

            #endregion
            Console.WriteLine(new string('#', 30));

            #region <<< HTTP POST/GET >>>
            var removeEmployeeAndGet = new Employee { Id = 1 };
            response = await restMemoryRepository
                    .GetRestConnection((c, x) => new HttpClient()
                    .QueryGet<Employee>(string.Format("api/employee/GetRemoveEmployeeById/{0}", removeEmployeeAndGet.Id)));

            foreach (var emp in response)
            {
                Console.WriteLine("{0}:{1}:{2}", emp.Id, emp.FirstName, emp.Age);
            }
            #endregion
            Console.WriteLine(new string('#', 30));

            #region <<< HTTP POST >>>
            var removeEmployee = new Employee { Id = 1 };
            var responsePost = await restMemoryRepository
                    .GetRestConnection((c, x) => new HttpClient()
                    .QueryPost(string.Format("api/employee/RemoveEmployeeById/{0}", removeEmployee.Id), removeEmployee));

            if (responsePost.IsSuccessStatusCode)
            {
                Console.WriteLine((int)responsePost.EnsureSuccessStatusCode().StatusCode);
            }
            #endregion
            Console.ReadLine();
        }
    }
}
