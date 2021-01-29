using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Date
{
    public int day;
    public Month month;
    public int year;

    public static int DaysInMonth(Month month, int year)
    {
        switch (month)
        {
            case Month.January:
            case Month.March:
            case Month.May:
            case Month.July:
            case Month.August:
            case Month.October:
            case Month.December:
                return 31;
            case Month.February:
                return IsLeapYear(year) ? 29 : 28;
            case Month.April:
            case Month.June:
            case Month.September:
            case Month.November:
                return 30;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static bool IsLeapYear(int year)
    {
        if (year % 4 != 0) return false;
        if (year % 100 != 0) return true;
        return year % 400 == 0;
    }
}

public enum Month
{
    January,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December,
}
