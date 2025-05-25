using System;


public enum DayOfWeek
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public class Date
{
    private int _day;
    private int _month;
    private int _year;

    public int Day
    {
        get => _day;
        set
        {
            if (value < 1 || value > DaysInMonth())
                throw new ArgumentException("Некорректный день");
            _day = value;
            UpdateWeekDay();
        }
    }

    public int Month
    {
        get => _month;
        set
        {
            if (value < 1 || value > 12)
                throw new ArgumentException("Некорректный месяц");
            _month = value;
            UpdateWeekDay();
        }
    }

    public int Year
    {
        get => _year;
        set
        {
            _year = value;
            UpdateWeekDay();
        }
    }

    public DayOfWeek WeekDay { get; private set; }

    public Date(int day, int month, int year)
    {
        _day = day;
        _month = month;
        _year = year;
        if (!IsValidDate())
            throw new ArgumentException("Некорректная дата");
        UpdateWeekDay();
    }

    private void UpdateWeekDay()
    {
        var date = new DateTime(Year, Month, Day);
        WeekDay = (DayOfWeek)((int)date.DayOfWeek == 0 ? 6 : (int)date.DayOfWeek - 1);
    }

    public int DaysInMonth() => DateTime.DaysInMonth(Year, Month);

    public int DaysInMonth(int month, int year)
    {
        if (month < 1 || month > 12)
            throw new ArgumentException("Некорректный месяц");
        return DateTime.DaysInMonth(year, month);
    }

    public bool IsValidDate() => DateTime.TryParse($"{Year}-{Month}-{Day}", out _);

    public void Add(int days, int months, int years)
    {
        var newDate = new DateTime(Year, Month, Day)
            .AddDays(days)
            .AddMonths(months)
            .AddYears(years);
            
        if (newDate < DateTime.MinValue || newDate > DateTime.MaxValue)
            throw new ArgumentException("Результат выходит за допустимый диапазон");
            
        _day = newDate.Day;
        _month = newDate.Month;
        _year = newDate.Year;
        UpdateWeekDay();
    }
}