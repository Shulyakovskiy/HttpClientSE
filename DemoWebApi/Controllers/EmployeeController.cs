using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoWebApi.Entity;
using DemoWebApi.Models;
using JetBrains.Annotations;

namespace DemoWebApi.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        readonly EmployeeQuery _query = new EmployeeQuery();

        [Route("GetEmployeById/{id}")]
        [HttpGet]
        [UsedImplicitly]
        public IEnumerable<Employee> GetEmployeById(int id)
        {
            return _query.GetEmployee().Where(c => c.Id == id).ToList();
        }

        [Route("GetEmploye/{employeeId}/{name}")]
        [HttpGet]
        [UsedImplicitly]
        public IEnumerable<Employee> GetEmploye(int employeeId, string name)
        {
            return _query.GetEmployee().Where(c => c.Id == employeeId && c.FirstName == name).ToList();
        }

        [Route("GetRemoveEmployeeById/{id}")]
        [HttpGet]
        [UsedImplicitly]
        public IEnumerable<Employee> GetRemoveEmployeeById(int id)
        {
            return _query.GetRemoveEmployeeById(id);
        }

        [Route("RemoveEmployeeById/{id}")]
        [HttpPost]
        [UsedImplicitly]
        public void RemoveEmployeeById(int id)
        {
            _query.RemoveEmployeeById(id);
        }
    }
}
