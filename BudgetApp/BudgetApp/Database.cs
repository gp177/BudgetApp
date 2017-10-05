using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class Database
    {

        private String CONN_STRING = @"Server=tcp:redaleks.database.windows.net,1433;Initial Catalog=BudgetAppDB;Persist Security Info=False;User ID=sqladmin;Password=AG1project;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection();
            conn.ConnectionString = CONN_STRING;
            conn.Open();
        }
    }
}
