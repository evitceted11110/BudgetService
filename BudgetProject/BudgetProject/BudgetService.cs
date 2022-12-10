using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetProject
{
    public class BudgetService
    {
        private IBudgetRepo repo;

        private Dictionary<string, int> yearMonthBudget;
        BudgetService(IBudgetRepo r)
        {
            repo = r;

            yearMonthBudget = new Dictionary<string, int>();
            var repoDatas = repo.getAll();


            for (int i = 0; i < repoDatas.Count; i++)
            {
                yearMonthBudget.Add(repoDatas[i].YearMonth, repoDatas[i].Amount);
            }

        }

        decimal Query(DateTime starTime, DateTime endDateTime)
        {
            if (dateTimeInverseInput(starTime, endDateTime))
            {
                return 0;
            }



        }

        //起訖錯誤
        bool dateTimeInverseInput(DateTime startTime, DateTime endDateTime)
        {
            return startTime > endDateTime;
        }

        int GetSingleDayInMonth(int year, int month)
        {
            var monnthBudget = GetBudgetByYearMonth(new DateTime(year, month, 0));

            return monnthBudget / GetTotalDayInMonth(year, month);
        }

        int GetTotalDayInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        int GetBudgetByYearMonth(DateTime time)
        {
            string yyyymm = time.Year.ToString() + time.Month.ToString("0:00");

            if (!yearMonthBudget.ContainsKey(yyyymm))
            {
                return 0;
            }

            return yearMonthBudget[yyyymm];
        }

    }
}
