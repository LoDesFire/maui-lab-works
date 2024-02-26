using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabsApp.Entities;
using LabsApp.Services;

namespace LabsApp.Pages;

public partial class CurrencyConvertorViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Rate> Rates { get; }

    public ObservableCollection<string> Currencies { get; }

    private decimal _bynValue;
    private decimal _currencyValue;
    private string _chosenCurrency;
    private string? _lastChangedEntry;

    public decimal BynValue
    {
        get => _bynValue;
    
        set
        {
            try
            {
                value = Convert.ToDecimal(value);
            }
            catch (FormatException)
            {
                value = 0m;
            }
            
            SetField(ref _bynValue, decimal.Round(value, 4));
        }
    }

    public string ChosenCurrency
    {
        get => _chosenCurrency;

        set => SetField(ref _chosenCurrency, value);
    }

    public decimal CurrencyValue
    {
        get => _currencyValue;

        set
        {
            try
            {
                value = Convert.ToDecimal(value);
            }
            catch (FormatException)
            {
                value = 0m;
            }

            SetField(ref _currencyValue, decimal.Round(value, 4));
        }
    }

    private readonly IRateService _rateService;

    public CurrencyConvertorViewModel(IRateService rateService)
    {
        _rateService = rateService;
        _chosenCurrency = "RUB";
        Currencies = new ObservableCollection<string> { "RUB", "EUR", "USD", "CHF", "CNY", "GBP" };
        Rates = new ObservableCollection<Rate>();
    }

    [RelayCommand]
    private async Task LoadCurrencies(DateTime dateTime)
    {
        var ratesList = await _rateService.GetRates(dateTime.Date, Currencies);
        Rates.Clear();
        foreach (var rate in ratesList)
        {
            Rates.Add(rate);
        }

        await CalculateCurrencies();
    }

    [RelayCommand]
    private async Task BynConvert()
    {
        _lastChangedEntry = "BYN";
        await CalculateCurrencies();
    }

    [RelayCommand]
    private async Task CurrencyConvert()
    {
        _lastChangedEntry = "CUR";
        await CalculateCurrencies();
    }

    [RelayCommand]
    private async Task CalculateCurrencies()
    {
        switch(_lastChangedEntry)
        {
            case "CUR": 
                await Converter(rate => { BynValue  = CurrencyValue * rate?.Cur_OfficialRate / rate?.Cur_Scale ?? 0m; });
                break;
            case "BYN":
                await Converter(rate => { CurrencyValue = BynValue / rate?.Cur_OfficialRate * rate?.Cur_Scale ?? 0m; });
                break;
        }
    }
    
    private async Task Converter(Action<Rate?> converter)
    {
        var ratesList = await _rateService.GetRates(null, Currencies);
        var rate = ratesList.FirstOrDefault(rate => rate.Cur_Abbreviation == ChosenCurrency);
        converter.Invoke(rate);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}