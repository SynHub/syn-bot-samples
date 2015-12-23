using System;
using System.Collections.Generic;
using Syn.Bot.Interfaces;

namespace NLI_Database.Sets
{
    public class AgeSet : ISet
    {
        private readonly HashSet<string> _ageSet;
        public AgeSet(DatabaseUtility databaseUtility)
        {
            _ageSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            databaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
            var reader = databaseUtility.Command.ExecuteReader();
            while (reader.Read())
            {
                _ageSet.Add(reader["Age"].ToString());
            }
            reader.Close();
        }
        public bool Contains(string item, string parameter = "")
        {
            return _ageSet.Contains(item);
        }
        public string Name => "Emp-Age";
        public IEnumerable<string> GetValues (string parameter = ""){  return _ageSet;  }
    }
}
