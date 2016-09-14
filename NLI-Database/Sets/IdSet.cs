using System;
using System.Collections.Generic;
using Syn.Bot.Siml.Interfaces;

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
        public bool Contains(string item, string parameter = "")
        {
            return _idSet.Contains(item);
        }
        public string Name => "Emp-ID";
        public IEnumerable<string> GetValues(string parameter = "") { return _idSet; }
    }
}
