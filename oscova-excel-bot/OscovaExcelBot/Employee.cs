namespace OscovaExcelBot
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public string GetValue(string propertyName)
        {
            switch (propertyName)
            {
                case "id":
                    return Id.ToString();
                case "age":
                    return Age.ToString();
                case "salary":
                    return Salary.ToString();
                case "name":
                    return Name;
                case "role":
                    return Role;
                default:
                    return string.Empty;
            }
        }
    }
}