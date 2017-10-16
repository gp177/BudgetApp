using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BudgetApp
{
    class Database
    {

        private String CONN_STRING = @"Server=tcp:redaleks.database.windows.net,1433;Initial Catalog=BudgetAppDB;Persist Security Info=False;User ID=sqladmin;Password=AG1project;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection();
            conn.ConnectionString = CONN_STRING;
            conn.Open();
        }
        //Work with Records Table

        public int AddRecord(Record r)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Records (Date,Amount,AccountId,CategoryId,RecordType) OUTPUT INSERTED.RecordId VALUES (@Date,@Amount,@AccountId,@CategoryId,@RecordType)", conn);
            insertCommand.Parameters.Add(new SqlParameter("Date", r.Date));
            insertCommand.Parameters.Add(new SqlParameter("Amount", r.Amount));
            insertCommand.Parameters.Add(new SqlParameter("AccountId", r.AccountId));
            insertCommand.Parameters.Add(new SqlParameter("CategoryId", r.CategoryId));
            insertCommand.Parameters.Add(new SqlParameter("RecordType", r.RecordType));
            int addedId = (int)insertCommand.ExecuteScalar();
            return addedId;
        }
        public void UpdateRecord(Record r)
        {
            SqlCommand updateCommand = new SqlCommand("Update Records SET Date=@Date,Amount=@Amount,AccountId=@AccountId,CategoryId=@CategoryId,RecordType=@RecordType WHERE RecordId=@RecordId ", conn);
            updateCommand.Parameters.Add(new SqlParameter("RecordId", r.RecordId));
            updateCommand.Parameters.Add(new SqlParameter("Date", r.Date));
            updateCommand.Parameters.Add(new SqlParameter("Amount", r.Amount));
            updateCommand.Parameters.Add(new SqlParameter("AccountId", r.AccountId));
            updateCommand.Parameters.Add(new SqlParameter("CategoryId", r.CategoryId));
            updateCommand.Parameters.Add(new SqlParameter("RecordType", r.RecordType));
            updateCommand.ExecuteNonQuery();
        }


        public List<Record> GetRecord()
        {
            List<Record> AccList = new List<Record>();
            SqlCommand selectCommand = new SqlCommand(@"Select Records.RecordId,Accounts.AccountName,Category.CategoryType, Records.Date,Records.Amount,Records.RecordType 
                                                        from Records
                                                        Inner Join Accounts
                                                        On Records.AccountId = Accounts.AccountId
                                                        Inner Join Category
                                                        On Records.CategoryId = Category.CategoryId ", conn);

            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Record rec = new Record();
                    rec.RecordId = (int)reader["RecordId"];
                    rec.AccountStr = (string)reader["AccountName"];
                    rec.CategoryStr = (string)reader["CategoryType"];
                    rec.Date = (DateTime)reader["Date"];
                    rec.Amount = Convert.ToDouble(reader["Amount"]);
                    rec.TagDesctiption = String.Join(",", GetTagsbyId(rec.RecordId));
                    rec.RecordType = (string)reader["RecordType"];
                    AccList.Add(rec);

                }
            }
            return AccList;
        }
        public int GetRecordID(String AccName)
        {
            int id = 0;
            SqlCommand selectCommand = new SqlCommand("SELECT AccountID FROM Accounts Where AccountName=@AccountName", conn);
            selectCommand.Parameters.Add(new SqlParameter("AccountName", AccName));
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {

                while (reader.Read())
                {
                    id = (int)reader[0];
                }
            }
            return id;
        }
        public void DeleteRecord(int id)
        {
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM  Records where RecordId=@id", conn);
            deleteCommand.Parameters.Add(new SqlParameter("id", id));
            deleteCommand.ExecuteNonQuery();
        }
        //end
        //Category table
        public void AddCategory(String CategoryType)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Category (CategoryType) VALUES (@CategoryType)", conn);
            insertCommand.Parameters.Add(new SqlParameter("CategoryType", CategoryType));
            insertCommand.ExecuteNonQuery();
        }

        public List<String> GetCategories()
        {
            List<String> CatList = new List<String>();
            SqlCommand selectCommand = new SqlCommand("SELECT CategoryType FROM Category", conn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    String xCategoryType = (String)reader["CategoryType"];

                    CatList.Add(xCategoryType);

                }
            }
            return CatList;
        }
        //Tag Table
        public void AddTags(String Description)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Tag (Description) OUTPUT INSERTED.TagId VALUES (@Description)", conn);
            insertCommand.Parameters.Add(new SqlParameter("Description", Description));
            insertCommand.ExecuteNonQuery();


        }

        public void AddPictures(Byte[] p, int Id)
        {
            // SqlCommand insertCOmmand = new SqlCommand("INSERT INTO Records (Document) VALUES (@Document) Where RecordId=@RecordId", conn);
            SqlCommand insertCOmmand = new SqlCommand("UPDATE Records SET Document = @Document WHERE RecordId = @RecordId", conn);
//            insertCOmmand.Parameters.Add(new SqlParameter("Document", p));
            SqlParameter binParam = new SqlParameter("Document", SqlDbType.VarBinary);
            binParam.Value = p;
            insertCOmmand.Parameters.Add(binParam);
            insertCOmmand.Parameters.Add(new SqlParameter("RecordId", Id));
            insertCOmmand.ExecuteNonQuery();

        }

        public byte[] GetPicture(int id)

        {
            var cmd = new SqlCommand("SELECT Document FROM Records WHERE RecordId = @ID", conn);
            
                cmd.Parameters.AddWithValue("@ID", id);
                return cmd.ExecuteScalar() as byte[];
            
            //Byte[] ggg=null;
            //SqlDataAdapter dataAdapter = new SqlDataAdapter (new SqlCommand("SELECT Document FROM Records WHERE RecordId = 77", conn));



                //DataSet dataSet = new DataSet();
                //dataAdapter.Fill(dataSet);


                //    Byte[] data = new Byte[0];
                //    data = (Byte[])(dataSet.Tables[0].Rows[0]["Document"]);
                //    MemoryStream mem = new MemoryStream(data);
                //    //yourPictureBox.Image = Image.FromStream(mem);

                ////selectCommand.Parameters.Add(new SqlParameter("RecordId", id));
                ////using (SqlDataReader reader = selectCommand.ExecuteReader())
                ////{
                ////    while (reader.Read())
                ////    {
                ////        ggg = ((Byte[])reader[0]);

                ////    }
                ////}
                //return mem;
        }


        public int GetTagIdbyDescription(String description)
        {
            int tagId = 0;
            SqlCommand selectCommand = new SqlCommand("SELECT TagId FROM Tag WHERE Description=@Description", conn);
            selectCommand.Parameters.Add(new SqlParameter("Description", description));
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    tagId = (int)reader[0];

                }
            }
            return tagId;
        }
        public List<String> GetTags()
        {
            List<String> TagList = new List<String>();
            SqlCommand selectCommand = new SqlCommand("SELECT Description FROM Tag", conn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    String tag = (String)reader["Description"];

                    TagList.Add(tag);

                }
            }
            return TagList;
        }

        public List<String> GetTagsbyId(int id)
        {
            List<String> list = new List<String>();
            SqlCommand selectCommand = new SqlCommand(@"Select Tag.Description
                                                        from tag
                                                        inner join InterTag
                                                        on InterTag.TagId = Tag.TagId
                                                        where InterTag.RecordId = @RecordId", conn);
            selectCommand.Parameters.Add(new SqlParameter("RecordId", id));
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    String tag = (String)reader["Description"];

                    list.Add(tag);

                }
            }
            return list;
        }


        //end
        //InterTag table
        public void AddInterTeg(int TagId, int RecordId)
        {
            SqlCommand insertCommand = new SqlCommand("INSERT INTO InterTag (TagId,RecordId) VALUES (@TagId,@RecordId)", conn);
            insertCommand.Parameters.Add(new SqlParameter("TagId", TagId));
            insertCommand.Parameters.Add(new SqlParameter("RecordId", RecordId));
            insertCommand.ExecuteNonQuery();


        }
        public void deleteInterTag(int id)
        {
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM InterTag where RecordId=@id", conn);
            deleteCommand.Parameters.Add(new SqlParameter("id", id));
            deleteCommand.ExecuteNonQuery();
        }
        //end

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
        //Account Table
        public int GetAccountID(String AccName)
        {
            int id = 0;
            SqlCommand selectCommand = new SqlCommand("SELECT AccountID FROM Accounts Where AccountName=@AccountName", conn);
            selectCommand.Parameters.Add(new SqlParameter("AccountName", AccName));
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {

                while (reader.Read())
                {
                    id = (int)reader[0];
                }
            }
            return id;
        }
        public double GetBalanceById(int id)
        {
            double balance=0;
            SqlCommand selectCommande = new SqlCommand("SELECT  Balance FROM Accounts WHERE AccountId=@AccountId ", conn);
            selectCommande.Parameters.Add(new SqlParameter("AccountId", id));
            using (SqlDataReader reader = selectCommande.ExecuteReader())
            {

                while (reader.Read())
                {
                 balance = Convert.ToDouble(reader[0]);
                }
            }
            return balance;
        }
        public void UpdateBalance(int id, double amount)
        {
           SqlCommand updateCommande = new SqlCommand("UPDATE Accounts Set Balance=@Balance where AccountId = @id", conn);
            updateCommande.Parameters.Add(new SqlParameter("Balance", amount));
            updateCommande.Parameters.Add(new SqlParameter("Id", id));
           
            updateCommande.ExecuteNonQuery();

        }
        //for Chatr
        public Dictionary<String, double> getBalance()
        {
            Dictionary<String, double> dictionary = new Dictionary<String, double>();
            SqlCommand selectCommand = new SqlCommand(@"select accountName,balance 
                                                        from Accounts
                                                        where AccountType = 'debit'
                                                        or
                                                        AccountType = 'Savings'", conn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            while (reader.Read())
            {
                dictionary.Add(Convert.ToString(reader[0]), Convert.ToDouble(reader[1]));
            }
            return dictionary;
        }


        //end for Chart

        //end account

        //Category
        //get Category ID for inserting in Record
        public int GetCategoryID(String CatType)
        {
            int id = 0;
            SqlCommand selectCommand = new SqlCommand("SELECT CategoryId FROM Category Where CategoryType=@CatType", conn);
            selectCommand.Parameters.Add(new SqlParameter("CatType", CatType));
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {

                while (reader.Read())
                {
                    id = (int)reader[0];

                }
            }
            return id;
        }
        //end 
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

    }
}

