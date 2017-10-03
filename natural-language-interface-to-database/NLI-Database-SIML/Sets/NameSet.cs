using System;
using System.Collections.Generic;
using Syn.Bot.Siml.Interfaces;

namespace NLI_Database_SIML.Sets
{
    public class NameSet : ISet
    {
        private readonly HashSet<string> _nameSet; 
        public NameSet(DatabaseUtility databaseUtility)
        {
            _nameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            databaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
            var reader = databaseUtility.Command.ExecuteReader();
            while (reader.Read())
            {
                _nameSet.Add(reader["Name"].ToString());
            }
            reader.Close();
        }
        public bool Contains(string item, string parameter="")
        {
            return _nameSet.Contains(item);
        }
        public string Name => "Emp-Name";
        public IEnumerable<string> GetValues(string parameter = "") { return _nameSet;  }
    }
}
