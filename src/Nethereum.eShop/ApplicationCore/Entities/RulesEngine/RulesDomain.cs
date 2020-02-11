using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{
    public class RulesDomain: BaseEntity
    {
        public RulesDomain() 
        {
            Connection  = null;
            TargetTable = null;
            Columns     = null;
        }

        public RulesDomain(SqlConnection conn, string table, List<string> cols) 
        {
            Connection  = conn;
            TargetTable = table;
            Columns     = cols;
        }

        public SqlConnection Connection { get; set; }

        public string TargetTable { get; set; }

        public List<string> Columns { get; set; }
    }
}
