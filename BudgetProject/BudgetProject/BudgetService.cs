using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetProject
{
    public class BudgetService
    {
        private IBudgetRepo repo;

        private Dictionary<string, int> yearMonthBudget=new Dictionary<string, int>();
        public BudgetService(IBudgetRepo r)
        {
            repo = r;
        }

        public decimal Query(DateTime startTime, DateTime endDateTime)
        {
            if (dateTimeInverseInput(startTime, endDateTime))
            {
                return 0;
            }

            UpdateRepoData();
            if ((startTime.Year == endDateTime.Year) && startTime.Month == endDateTime.Month)
            {
                //同年同月
                return GetSingleDayBudgetInMonth(startTime.Year, startTime.Month) *
                       GetSameMonthDays(startTime, endDateTime);
            }

            DateTime valueDateTime = new DateTime(startTime.Year, startTime.Month,1);
            //DateTime valueDateTime = startTime;
            int total = 0;
            while (valueDateTime <= endDateTime)
            {
                int defineYear = valueDateTime.Year;
                int defineMonth = valueDateTime.Month;

                if (valueDateTime.ToString("yyyyMM") == startTime.ToString("yyyyMM"))
                {
                    int remainDay = DateTime.DaysInMonth(defineYear, defineMonth);
                    Console.WriteLine(remainDay);
                    total += GetSingleDayBudgetInMonth(defineYear, defineMonth) * GetSameMonthDays(startTime, new DateTime(startTime.Year, startTime.Month, remainDay));

                }
                else if (valueDateTime.ToString("yyyyMM") == endDateTime.ToString("yyyyMM"))
                {

                    total += GetSingleDayBudgetInMonth(defineYear, defineMonth) * GetSameMonthDays(new DateTime(endDateTime.Year, endDateTime.Month, 1), endDateTime);

                }
                else
                {
                    total += GetBudgetByYearMonth(new DateTime(defineYear, defineMonth, 1));

                }

                valueDateTime = valueDateTime.AddMonths(1);
                
            }

            return total;
            
        }

        private void UpdateRepoData()
        {
            yearMonthBudget.Clear();
            var repoDatas = repo.getAll();

            foreach (var data in repoDatas)
            {
                yearMonthBudget.Add(data.YearMonth, data.Amount);
            }
        }
        //起訖錯誤
        bool dateTimeInverseInput(DateTime startTime, DateTime endDateTime)
        {
            return startTime > endDateTime;
        }

        int GetSameMonthDays(DateTime startTime, DateTime endDateTime)
        {
            return endDateTime.Day - startTime.Day + 1;
        }

        int GetSingleDayBudgetInMonth(int year, int month)
        {
            var monnthBudget = GetBudgetByYearMonth(new DateTime(year, month, 1));

            return monnthBudget / GetTotalDayInMonth(year, month);
        }

        int GetTotalDayInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        int GetBudgetByYearMonth(DateTime time)
        {
            string yyyymm = time.Year.ToString() + time.Month.ToString("00");
            if (!yearMonthBudget.ContainsKey(yyyymm))
            {
                return 0;
            }

            return yearMonthBudget[yyyymm];
        }

    }
}
