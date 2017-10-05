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

        //public List<Records>GetAllPeople()
        //{
        //    List<Records> list = new List<People>();
        //    SqlCommand selectCommand = new SqlCommand("SELECT * FROM People ORDER BY id", conn);
        //    using (SqlDataReader reader = selectCommand.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            Record r = new Record);
        //            r.Id = (int)reader[0];
        //            p.Name = (String)reader[1];
        //            p.Age = (int)reader[2];
        //            p.Height = (double)reader[3];
        //            list.Add(p);

        //        }
        //    }
        //    return list;
        //}
        //public void AddRecord(Record r)
        //{
        //    SqlCommand insertCommand = new SqlCommand("INSERT INTO Record (Name,Age,Height) VALUES (@Amount,@Date)", conn);
        //    insertCommand.Parameters.Add(new SqlParameter("Amount", r._Amount));
        //    insertCommand.Parameters.Add(new SqlParameter("Date", r._Date));
        //    insertCommand.Parameters.Add(new SqlParameter("Height", height));
        //    insertCommand.ExecuteNonQuery();
        //}
        //public void AddCategory(Record r)
        //{
        //    SqlCommand insertCommand = new SqlCommand("INSERT INTO Record (Name,Age,Height) VALUES (@Name,@Age,@Height)", conn);
        //    insertCommand.Parameters.Add(new SqlParameter("Name", name));
        //    insertCommand.Parameters.Add(new SqlParameter("Age", age));
        //    insertCommand.Parameters.Add(new SqlParameter("Height", height));
        //    insertCommand.ExecuteNonQuery();
        //}
        //public void AddTag(Record r)
        //{
        //    SqlCommand insertCommand = new SqlCommand("INSERT INTO Record (Name,Age,Height) VALUES (@Name,@Age,@Height)", conn);
        //    insertCommand.Parameters.Add(new SqlParameter("Name", name));
        //    insertCommand.Parameters.Add(new SqlParameter("Age", age));
        //    insertCommand.Parameters.Add(new SqlParameter("Height", height));
        //    insertCommand.ExecuteNonQuery();
        //}
    }
}

