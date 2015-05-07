using System.Collections.Generic;
using System.Linq;
using DemoWebApi.Entity;

namespace DemoWebApi.Models
{
    public class EmployeeQuery
    {
        /// <summary>
        /// Return All Employee
        /// </summary>
        /// <returns>Employee</returns>
        public IEnumerable<Employee> GetEmployee()
        {
            var listEmployee = new List<Employee>
            {
                new Employee {Id = 1, FirstName = "Joe", Age = 25},
                new Employee {Id = 2, FirstName = "Jack", Age = 30},
                new Employee {Id = 3, FirstName = "Allen", Age = 35}
            };

            return listEmployee.ToList();
        }
   
        /// <summary>
        /// Return Employee By Id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        public IEnumerable<Employee> GetRemoveEmployeeById(int id)
        {
            var list = GetEmployee().Select(c => c).ToList();
            list.Remove(new Employee{Id = id});

            return list;
        }

        /// <summary>
        /// Remove Employee By Id
        /// </summary>
        /// <param name="id">Employee Id</param>
        public void RemoveEmployeeById(int id)
        {
            var list = GetEmployee().Select(c => c).ToList();
            list.Remove(new Employee { Id = id });
        }
    }
}