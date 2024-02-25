using System.Globalization;
namespace LabsApp.Pages;

public partial class Calculator : ContentPage
{
    private double _firstNumber = double.NaN;
    private double _secondNumber = double.NaN;
    private double _memory;
    private string? _operation;

    private string _display = "0";

    private string Display
    {
        get => _display;

        set
        {
            _displayNumber = Convert.ToDouble(value, CultureInfo.CurrentCulture);
            _display = value;
        }
    }
    
    private double _displayNumber;

    private double DisplayNumber
    {
        get => _displayNumber;

        set
        {
            if (double.IsNaN(value) ||
                double.IsInfinity(value))
            {
                _isError = true;
                return;
            }
            _display = double.Round(value, 15).ToString(CultureInfo.CurrentCulture);
            _displayNumber = value;
        }
    }
    
    private bool _isResultObtained;
    private bool _isError;
    private bool _isNeedRestore;
    
    
    private readonly string _numberDecimalSeparator = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
    
    
    public Calculator()
    {
        InitializeComponent();
        NumberDecimalSeparator.Text = _numberDecimalSeparator;
    }
    
    private void OnDeleteClicked(object? sender, TappedEventArgs e)
    {
        if (_isResultObtained || _isError || _isNeedRestore)
        {
            CleanDisplay();
            return;
        }

        var buffer = Display.Remove(Display.Length - 1);
        Display = buffer.Length == 0 || buffer == "-" ? "0" : buffer;
        
        UpdateLabel();
    }
    
    private void OnMemoryActionClicked(object? sender, EventArgs e)
    {
        var button = (Button?)sender;
        if (button?.Text == "MR")
        {
            DisplayNumber = _memory;
            UpdateLabel();
            return;
        }
        if (_isError) return;
        switch (button?.Text)
        {
            case "M+":
                _memory += DisplayNumber;
                break;
            case "M-":
                _memory -= DisplayNumber;
                break;
            case "MC":
                _memory = 0;
                break;
            case "MS":
                _memory = DisplayNumber;
                break;
        }
    }

    private void OnClearActionClicked(object? sender, EventArgs e)
    {
        var button = (Button?)sender;
        switch (button?.Text)
        {
            case "CE":
                if (_isResultObtained || _isError) CleanDisplay();
                Display = "0";
                break;
            
            case "C":
                CleanDisplay();
                break;
        }
        UpdateLabel();
    }

    private void OnSingleActionClicked(object? sender, EventArgs e)
    {
        if (_isError) return;
        
        var button = (Button?)sender;
        DisplayNumber = button?.Text switch
        {
            "1/x" => 1 / DisplayNumber,
            "x²" => DisplayNumber * DisplayNumber,
            "√x" => double.Sqrt(DisplayNumber),
            "+/-" => DisplayNumber = Display == "0" ? DisplayNumber : DisplayNumber * -1,
            "%" => DisplayNumber / 100.0d,
            "2^x" => Math.Pow(2, DisplayNumber),
            _ => DisplayNumber
        };
        
        if (_isResultObtained && !_isError) CleanDisplay(Display);
        if (_isError)
        {
            _firstNumber = double.NaN;
            _secondNumber = double.NaN;
            _operation = null;
        }
        if (button?.Text != "+/-") _isNeedRestore = true;
        UpdateLabel();
    }

    private void OnDoubleActionClicked(object? sender, EventArgs e)
    {
        if (_isError) return;
        
        _isNeedRestore = true;
        _isResultObtained = false;
        _firstNumber = DisplayNumber;
        
        var operation = ((Button?)sender)?.Text;
        if (operation != null) _operation = operation;
        if (_isResultObtained) UpdateLabel();
    }

    private void OnNumberClicked(object? sender, EventArgs e)
    {
        var numberString = ((Button?)sender)?.Text;
        if (numberString == null || _isError) return;
        
        if (_isResultObtained) CleanDisplay();
        if (_isNeedRestore)
        {
            Display = "0";
            _isNeedRestore = false;
            _secondNumber = double.NaN;
        }
        Display = Display == "0" ? numberString : Display + numberString;

        UpdateLabel();
    }
    
    private void OnCommaClicked(object? sender, EventArgs e)
    {
        if (_isError || _isResultObtained || _isNeedRestore || Display.Contains(_numberDecimalSeparator)) return;
        Display += _numberDecimalSeparator;
        UpdateLabel();
    }

    private void OnEqualClicked(object? sender, EventArgs e)
    {
        if (_operation == null || _isError) return;
        if (!_isResultObtained) _secondNumber = DisplayNumber;
        else _firstNumber = DisplayNumber;
        
        DisplayNumber = _operation switch
        {
            "+" => _firstNumber + _secondNumber,
            "-" => _firstNumber - _secondNumber,
            "×" => _firstNumber * _secondNumber,
            "÷" => _firstNumber / _secondNumber,
            _ => double.NaN
        };

        _isResultObtained = true;
        UpdateLabel();
    }
    
    private void CleanDisplay(string display="0")
    {
        _firstNumber = double.NaN;
        _secondNumber = double.NaN;
        _operation = null;
        Display = display;
        _isResultObtained = false;
        _isError = false;
        UpdateLabel();
    }
    
    private void UpdateLabel()
    {
        if (!double.IsNaN(_firstNumber))
        {
            CurrentCalculation.Text = 
                !double.IsNaN(_secondNumber) ? 
                    $"{_firstNumber} {_operation} {_secondNumber} =" : 
                    $"{_firstNumber} {_operation ?? ""}";
        }
        else
        {
            CurrentCalculation.Text = "";
        }
        
        if (_isError)
        {
            ResultText.Text = "Error";
            return;
        }
        
        ResultText.Text = _display;
    }
    
}