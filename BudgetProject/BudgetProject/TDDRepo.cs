using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetProject
{
    public class TDDRepo : IBudgetRepo
    {
        public List<Budget> getAll()
        {
            return new List<Budget>()
            {
                new Budget("202201",31),
                new Budget("202202",280),
                new Budget("202203",62),
                new Budget("202204",3000),
                new Budget("202205",62000)
            };
        }
    }
}
