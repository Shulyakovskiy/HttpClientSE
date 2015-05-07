using System.Collections.Generic;
using System.Web.Http;

namespace DemoWebApi.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        [Route("Get")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
