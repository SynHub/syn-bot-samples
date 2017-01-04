using System;
using System.Collections.Generic;
using Syn.Bot.Siml.Interfaces;

namespace NLI_Database_SIML.Sets
{
    public class JobSet : ISet
    {
        private readonly HashSet<string> _jobSet;
        public JobSet(DatabaseUtility databaseUtility)
        {
            _jobSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            databaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
            var reader = databaseUtility.Command.ExecuteReader();
            while (reader.Read())
            {
                _jobSet.Add(reader["Job"].ToString());
            }
            reader.Close();
        }
        public bool Contains(string item, string parameter = "")
        {
            return _jobSet.Contains(item);
        }
        public string Name => "Emp-Job";
        public IEnumerable<string> GetValues(string parameter = "") {  return _jobSet;  }
    }
}
