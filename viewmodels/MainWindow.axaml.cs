using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace DateApp;

public partial class MainWindow : Window
{
    private Date _date = new Date(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);

    public MainWindow()
    {
        InitializeComponent();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        DayTextBox.Text = _date.Day.ToString();
        MonthTextBox.Text = _date.Month.ToString();
        YearTextBox.Text = _date.Year.ToString();
        WeekDayTextBlock.Text = $"День недели: {_date.WeekDay}";
    }

    private void UpdateDate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int day = ParseInt(DayTextBox.Text, "День");
            int month = ParseInt(MonthTextBox.Text, "Месяц");
            int year = ParseInt(YearTextBox.Text, "Год");

            _date = new Date(day, month, year);
            UpdateDisplay();
            ResultTextBlock.Text = "Дата успешно обновлена";
        }
        catch (Exception ex)
        {
            ResultTextBlock.Text = $"Ошибка: {ex.Message}";
        }
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int days = ParseInt(AddDaysTextBox.Text, "Дней");
            int months = ParseInt(AddMonthsTextBox.Text, "Месяцев");
            int years = ParseInt(AddYearsTextBox.Text, "Лет");

            if (days < 0 || months < 0 || years < 0)
                throw new ArgumentException("Значения не могут быть отрицательными");

            _date.Add(days, months, years);
            UpdateDisplay();
            ResultTextBlock.Text = "Дата успешно изменена";
        }
        catch (Exception ex)
        {
            ResultTextBlock.Text = $"Ошибка: {ex.Message}";
        }
    }

    private void Validate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int day = ParseInt(DayTextBox.Text, "День");
            int month = ParseInt(MonthTextBox.Text, "Месяц");
            int year = ParseInt(YearTextBox.Text, "Год");

            new Date(day, month, year);
            ResultTextBlock.Text = "Дата корректна";
        }
        catch (Exception ex)
        {
            ResultTextBlock.Text = $"Ошибка: {ex.Message}";
        }
    }

    private void CalculateDays_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int month = ParseInt(MonthForDaysTextBox.Text, "Месяц");
            int year = ParseInt(YearForDaysTextBox.Text, "Год");

            int days = _date.DaysInMonth(month, year);
            DaysResultTextBlock.Text = $"Дней в месяце: {days}";
        }
        catch (Exception ex)
        {
            DaysResultTextBlock.Text = $"Ошибка: {ex.Message}";
        }
    }

    private int ParseInt(string? input, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        string trimmed = input.TrimStart('0');
        if (trimmed == "")
            trimmed = "0";

        if (!int.TryParse(trimmed, out int value))
            throw new ArgumentException($"Некорректное значение для {fieldName}");

        return value;
    }
}
