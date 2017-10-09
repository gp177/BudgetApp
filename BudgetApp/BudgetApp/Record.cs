using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BudgetApp
{
    class Record
    {
        // fields encapsulated

        // Records table
        public int RecordId { get; set; }
        private DateTime _Date;
        private double _Amount;

        private Image _Document;
        //new field!!!
        public string RecordType{get;set;}
        public string AccountStr { get; set;}
        public string CategoryStr { get; set;}
        public string TegDesctiption { get; set; }

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
                if ((value <= 0) || value > 2147483645)
                    throw new ArgumentOutOfRangeException("Amount must be in range");

                else
                {

                    _Amount = value;
                }
            }
        }

        // Accounts table
        public String AccountType
        {
            get { return _AccountType; }

            set
            {
                if ((value.Length <= 0) || value.Length > 60)
                {
                    throw new ArgumentOutOfRangeException("Account Type must be in between 1-60 Characters");
                }
                else
                {
                    _AccountType = value;
                }
            }
        }

        public double AccountNumber
        {
            get { return _AccountNumber; }

            set
            {
                if (value <= 0 || value >= 999999999999999999) {

                    _AccountNumber = value;
                }
               
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
                if ((value <= 0) || value > 999999999999999999)
                    throw new ArgumentOutOfRangeException("Amount must be in range");

                else
                {

                    _Balance = value;
                }
            }
        }

        public String AccountName
        {
            get { return _AccountName; }

            set
            {
                if ((value.Length <= 0) || value.Length > 60)
                {
                    throw new ArgumentOutOfRangeException("Account Name must be in between 1-60 Characters");
                }
                else
                {
                    _AccountName = value;
                }
            }
        }

       

        // Category table
        public String CategoryType
        {
            get { return _CategoryType; }

            set
            {
                if ((value.Length <= 0) || value.Length > 60)
                {
                    throw new ArgumentOutOfRangeException("Category Type must be in between 1-60 Characters");
                }
                else
                {
                    _CategoryType = value;
                }
            }
        }

        //Tag table
        public String description
        {
            get { return _Descrption; }

            set
            {
                if ((value.Length <= 0) || value.Length > 200)
                {
                    throw new ArgumentOutOfRangeException("Description must be in between 1-200 Characters");
                }
                else
                {
                    _Descrption = value;
                }
            }
        }

        public Image Document
        {
            get { return _Document; }

            set
            {
                _Document = value;
            }
        }


    }







}
