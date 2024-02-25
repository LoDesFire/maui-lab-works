using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LabsApp.Services;

public class IntegratorProperties: INotifyPropertyChanged
{
    private double _progress;
    private string _status = "Welcome to .NET MAUI!";

    public double Progress
    {
        get => _progress;
        set => SetField(ref _progress, value);
    }
    
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}