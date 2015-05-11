using System;
using System.Collections.Generic;
using Syn.Bot.Interfaces;

namespace NLI_Database.Sets
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
                _nameSet.Add(reader["name"].ToString());
            }
            reader.Close();
        }
        public bool Contains(string item)
        {
            return _nameSet.Contains(item);
        }
        public string Name { get { return "Emp-Name"; }}
        public IEnumerable<string> Values { get { return _nameSet; } }
    }
}
