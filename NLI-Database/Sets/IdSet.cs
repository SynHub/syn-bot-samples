using System;
using System.Collections.Generic;
using Syn.Bot.Interfaces;

namespace NLI_Database.Sets
{
    public class IdSet : ISet
    {
        private readonly HashSet<string> _idSet;
        public IdSet(DatabaseUtility databaseUtility)
        {
            _idSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            databaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
            var reader = databaseUtility.Command.ExecuteReader();
            while (reader.Read())
            {
                _idSet.Add(reader["ID"].ToString());
            }
            reader.Close();
        }
        public bool Contains(string item)
        {
            return _idSet.Contains(item);
        }
        public string Name { get { return "Emp-ID"; }}
        public IEnumerable<string> Values { get { return _idSet; } }
    }
}
