namespace DemoWebApi.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var objAsPart = obj as Employee;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(Employee employee)
        {
            if (employee == null) return false;
            return (this.Id.Equals(employee.Id));
        }

    }

}