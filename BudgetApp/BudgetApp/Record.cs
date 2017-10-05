using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class Record
    {
        // fields encapsulated

       // Records table
        public int RecordId{get; set;}
        private DateTime _Date;
        private double _Amount;
        // TODO: Add Document   
   
        // Accounts table
        public int AccountId { get; set; }
        private String _AccountType;
        private double _AccountNumber;
        private double _Balance;
        private String _AccountName;

        // Category table
        public int CategoryId { get; set; }
        private String _CategoryType;

        // Tag table
        public int TagId { get; set; }
        private String _Descrption;

        // InterTag able
        public int InterId { get; set; }




        // Records table arguments
        public DateTime Date
        {
            get { return _Date; }

            set
            {
                _Date = value;
            }
        }

        public double Amount
        {
            get { return _Amount; }

            set
            {
                _Amount = value;
            }
        }

        // Accounts table
        public String AccountType
        {
            get { return _AccountType; }

            set
            {
                _AccountType = value;
            }
        }

        public double AccountNumber
        {
            get { return _AccountNumber; }

            set
            {
                _AccountNumber = value;
            }
        }

        public double Balance
        {
            get
            {
                return _Balance;
            }

            set
            {
                _Balance = value;
            }
        }

        public String AccountName
        {
            get { return _AccountName; }

            set
            {
                _AccountName = value;
            }
        }

       

        // Category table
        public String CategoryType
        {
            get { return _CategoryType; }

            set
            {
                _CategoryType = value;
            }
        }

        //Tag table
        public String description
        {
            get { return _Descrption; }

            set
            {
                _Descrption = value;
            }
        }


    }







}
