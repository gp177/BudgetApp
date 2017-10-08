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

      

        //public void AddRecord(Record r)
        //{
        //    SqlCommand insertCommand = new SqlCommand("INSERT INTO Record (Name,Age,Height) VALUES (@Amount,@Date)", conn);
        //    insertCommand.Parameters.Add(new SqlParameter("Amount", r._Amount));
        //    insertCommand.Parameters.Add(new SqlParameter("Date", r._Date));
        //    insertCommand.Parameters.Add(new SqlParameter("Height", height));
        //    insertCommand.ExecuteNonQuery();
        //}


        public void AddCategory(String CategoryType)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Category (CategoryType) VALUES (@CategoryType)", conn);
            insertCommand.Parameters.Add(new SqlParameter("CategoryType", CategoryType));
            insertCommand.ExecuteNonQuery();
        }

        public List<Record> GetCategories()
        {
            List<Record> CatList = new List<Record>();
            SqlCommand selectCommand = new SqlCommand("SELECT CategoryType FROM Category", conn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    String xCategoryType = (String)reader["CategoryType"];
                    Record rec = new Record { CategoryType = xCategoryType };

                    CatList.Add(rec);

                }
            }
            return CatList;
        }

      public void AddTags(String Description)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Tag (Description) VALUES (@Description)", conn);
            insertCommand.Parameters.Add(new SqlParameter("Description", Description));
            insertCommand.ExecuteNonQuery();
        }



        public void AddAmount(int Amount)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Records (Amount) VALUES (@Amount)", conn);
            insertCommand.Parameters.Add(new SqlParameter("Amount", Amount));
            insertCommand.ExecuteNonQuery();
        }

        public void AddDate(DateTime Date)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Records (Date) VALUES (@Date)", conn);
             insertCommand.Parameters.Add(new SqlParameter("Date", Date));
        }

        public void AddAccount(String AccountName, String AccountType, int AccountNumber, double Balance)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Accounts (AccountName,AccountType,Balance,AccountNumber) VALUES (@Name,@Type,@Balance,@Number)", conn);
            insertCommand.Parameters.Add(new SqlParameter("Number", AccountNumber));
            insertCommand.Parameters.Add(new SqlParameter("Balance", Balance));
            insertCommand.Parameters.Add(new SqlParameter("Type", AccountType));
            insertCommand.Parameters.Add(new SqlParameter("Name", AccountName));
            insertCommand.ExecuteNonQuery();
        }

        public List<Record> GetAccounts()
        {
            List<Record> AccList = new List<Record>();
            SqlCommand selectCommand = new SqlCommand("SELECT AccountName FROM Accounts", conn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                  
                    String xAccountName = (String)reader["AccountName"];
                    Record rec = new Record { AccountName = xAccountName };

                    AccList.Add(rec);

                }
            }
            return AccList;
        }

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

