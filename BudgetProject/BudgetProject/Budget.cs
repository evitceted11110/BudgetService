using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BudgetProject
{
    public class Budget
    {
        public Budget(string yyyymm, int a)
        {
            YearMonth = yyyymm;
            Amount = a;
        }
        public string YearMonth;
        public int Amount;
    }
}
