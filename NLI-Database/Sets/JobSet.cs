using System;
using System.Collections.Generic;
using Syn.Bot.Interfaces;

namespace NLI_Database.Sets
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
        public bool Contains(string item)
        {
            return _jobSet.Contains(item);
        }
        public string Name { get { return "Emp-Job"; }}
        public IEnumerable<string> Values { get { return _jobSet; } }
    }
}
