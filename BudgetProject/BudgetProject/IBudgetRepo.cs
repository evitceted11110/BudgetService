using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetProject
{
    public interface IBudgetRepo
    {
        List<Budget> getAll();
    }
}
