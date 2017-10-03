using System.Data.SQLite;
using System.IO;

namespace NLI_Database_Oscova
{
    public class DatabaseUtility
    {
        private const string DataSource = "EmployeesTable.db";
        public SQLiteCommand Command { get; set; }
        public SQLiteConnection Connection { get; set; }

        public DatabaseUtility(MainWindow window)
        {
            Window = window;
        }

        public void Initialize()
        {       
            if (File.Exists(DataSource)) File.Delete(DataSource);
            Connection = new SQLiteConnection { ConnectionString = "Data Source=" + DataSource };
            Connection.Open();
            ExecuteCommand("CREATE TABLE IF NOT EXISTS EMPLOYEES (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name VARCHAR(100) NOT NULL, Job VARCHAR(10), Age INTEGER NOT NULL, Salary INTEGER NOT NULL);");

            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(1, 'Lincoln', 'Manager', 43, 54000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(2, 'George', 'CEO', 46, 75000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(3, 'Rick', 'Admin', 32, 18000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(4, 'Jorge', 'Engineer', 28, 35000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(5, 'Ivan', 'Tech', 23, 34000)");

            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(6, 'Mark', 'Tech', 25, 34000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(7, 'Vincent', 'Support', 21, 20000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(8, 'Carl', 'Support', 20, 20000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(9, 'Marco', 'Tech', 24, 34000)");
            ExecuteCommand("INSERT INTO EMPLOYEES VALUES(10, 'Craig', 'Admin', 25, 18000)");
        }

        public MainWindow Window { get; }

        private void ExecuteCommand(string commandText)
        {
            Command = new SQLiteCommand(Connection) { CommandText = commandText };
            Command.ExecuteNonQuery(); 
        }

        public void Evaluate(string commandText)
        {
            Window.UpdateDataGrid(commandText);
        }

        public void UpdatePropertyByName(string employeeName, string propertyName, string propertyValue)
        {
            var updateString = $"UPDATE EMPLOYEES SET {propertyName}={propertyValue} WHERE UPPER(Name) LIKE UPPER('%{employeeName}%');";
            var selectString = $"SELECT * FROM EMPLOYEES WHERE UPPER(Name) LIKE UPPER('{employeeName}%');";

            Evaluate(updateString);
            Evaluate(selectString);
        }

        public void UpdatePropertyById(string employeeId, string propertyName, string propertyValue)
        {
            var updateString = $"UPDATE EMPLOYEES SET {propertyName}={propertyValue} WHERE ID={employeeId};";
            var selectString = $"SELECT * FROM EMPLOYEES WHERE ID={employeeId};";

            Evaluate(updateString);
            Evaluate(selectString);
        }

        public void PropertyByName(string employeeName, string propertyName)
        {
            var selectString = $"SELECT {propertyName} FROM Employees WHERE UPPER(Name) LIKE UPPER('%{employeeName}%');";
            Evaluate(selectString);
        }

        public void PropertyByJob(string employeeJob, string propertyName)
        {
            var selectString = $"SELECT DISTINCT {propertyName} FROM Employees WHERE UPPER(Job) LIKE UPPER('%{employeeJob}%')";
            Evaluate(selectString);
        }

        public void EmployeeByName(string employeeName)
        {
            var selectString = $"SELECT * FROM Employees WHERE UPPER(Name) LIKE UPPER('%{employeeName}%');";
            Evaluate(selectString);
        }

        public void EmployeeByJob(string employeeJob)
        {
            var selectString = $"SELECT * FROM Employees WHERE UPPER(Job) LIKE UPPER('%{employeeJob}%');";
            Evaluate(selectString);
        }

        public void Close()
        {
            Command.Dispose();
            Connection.Dispose();
        }
    }
}