using System;
using System.Collections.Generic;
using Syn.Bot.Interfaces;

namespace NLI_Database.Sets
{
    public class SalarySet : ISet
    {
        private readonly HashSet<string> _salarySet;
        public SalarySet(DatabaseUtility databaseUtility)
        {
            _salarySet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            databaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
            var reader = databaseUtility.Command.ExecuteReader();
            while (reader.Read())
            {
                _salarySet.Add(reader["Salary"].ToString());
            }
            reader.Close();
        }
        public bool Contains(string item)
        {
            return _salarySet.Contains(item);
        }
        public string Name { get { return "Emp-Salary"; }}
        public IEnumerable<string> Values { get { return _salarySet; } }
    }
}
