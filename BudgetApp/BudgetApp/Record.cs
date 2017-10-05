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
        private int RecordId{get; set;}
        private DateTime _Date { get; set; }
        private double _Amount { get; set; }
        // TODO: Add Document   
   
        // Accounts table
        private int AccountId { get; set; }
        private String _AccountType { get; set; }
        private double _AccounNumber { get; set; }
        private double _Balance { get; set; }

        // Category table
        private int CategoryId { get; set; }
        private String _CategoryType { get; set; }

        // Tag table
        private int TagId { get; set; }
        private String _Descrption { get; set; }

        // InterTag able
        private int InterId { get; set; }

    }
}
