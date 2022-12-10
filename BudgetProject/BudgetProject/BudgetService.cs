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
        public BudgetService(IBudgetRepo r)
        {
            repo = r;

            yearMonthBudget = new Dictionary<string, int>();
            var repoDatas = repo.getAll();


            for (int i = 0; i < repoDatas.Count; i++)
            {
                yearMonthBudget.Add(repoDatas[i].YearMonth, repoDatas[i].Amount);
            }

        }

        public decimal Query(DateTime startTime, DateTime endDateTime)
        {
            if (dateTimeInverseInput(startTime, endDateTime))
            {
                return 0;
            }

            //跨月
            if (startTime.Month != endDateTime.Month)
            {
                int total = 0;
                for (int i = startTime.Month; i <= endDateTime.Month; i++)
                {

                    if (i == startTime.Month)
                    {
                        int remainDay = DateTime.DaysInMonth(startTime.Year, startTime.Month);
                        total += GetSingleDayBudgetInMonth(startTime.Year, i) * GetSameMonthDays(startTime, new DateTime(startTime.Year, startTime.Month, remainDay));
                        Console.WriteLine(GetSingleDayBudgetInMonth(startTime.Year, i) * GetSameMonthDays(startTime, new DateTime(startTime.Year, startTime.Month, remainDay)));

                    }
                    else if (i == endDateTime.Month)
                    {
                        Console.WriteLine(GetSingleDayBudgetInMonth(startTime.Year, i) * GetSameMonthDays(new DateTime(endDateTime.Year, endDateTime.Month, 1), endDateTime));

                        total += GetSingleDayBudgetInMonth(startTime.Year, i) * GetSameMonthDays(new DateTime(endDateTime.Year, endDateTime.Month, 1), endDateTime);

                    }
                    else
                    {
                        Console.WriteLine(GetBudgetByYearMonth(new DateTime(startTime.Year, i, 1)));
                       total += GetBudgetByYearMonth(new DateTime(startTime.Year, i, 1));

                    }

                  
                }

                return total;
            }


            //單日
            return GetSingleDayBudgetInMonth(startTime.Year, startTime.Month) * GetSameMonthDays(startTime, endDateTime);
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

        //int GetDayRange(DateTime startTime, DateTime endDateTime, DateTime searchMonthDateTime)
        //{
        //    int year = searchMonthDateTime.Year;
        //    int month = searchMonthDateTime.Month;


        //}

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
