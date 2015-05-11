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
        public bool Contains(string item)
        {
            return _ageSet.Contains(item);
        }
        public string Name { get { return "Emp-Age"; }}
        public IEnumerable<string> Values { get { return _ageSet; } }
    }
}
